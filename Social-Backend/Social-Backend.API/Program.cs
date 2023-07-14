using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Social_Backend.API.Filters;
using Social_Backend.API.Hubs;
using Social_Backend.Application.Common.Constants;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Helpers;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.Auth;
using Social_Backend.Core.Interfaces.Chat;
using Social_Backend.Core.Interfaces.ChatRole;
using Social_Backend.Core.Interfaces.Message;
using Social_Backend.Core.Interfaces.User;
using Social_Backend.Core.Interfaces.UserChat;
using Social_Backend.Infrastructure;
using Social_Backend.Infrastructure.Data;
using Social_Backend.Infrastructure.Repositories;
using Social_Backend.Infrastructure.Services;
using Social_Backend.Infrastructure.Services.Upload;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddDbContext<SocialDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SocialDbContext")));
services
    .AddIdentity<AppUser, IdentityRole>(opts =>
    {
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequiredLength = 5;
        opts.Password.RequireDigit = false;
        opts.Password.RequireLowercase = false;
        opts.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<SocialDBContext>();

services.AddTransient<IUnitOfWork, UnitOfWork>();

services.AddSingleton<ICurrentUserService, CurrentUserService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<ITokenService, TokenService>();
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IUploadService, LocalUploadService>();
services.AddScoped<IChatService, ChatService>();
services.AddScoped<IMessageService, MessageService>();
services.AddScoped<IUserChatService, UserChatService>();

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IChatRepository, ChatRepository>();
services.AddScoped<IMessageRepository, MessageRepository>();
services.AddScoped<IUserChatRepository, UserChatRepository>();
services.AddScoped<IChatRoleRepository, ChatRoleRepository>();

services.AddSignalR();
services.AddDistributedMemoryCache();
services.AddHttpContextAccessor();
services.AddControllers(opt => opt.Filters.Add<APIExceptionFilter>())
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
services.AddEndpointsApiExplorer();
services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile(provider.GetService<IHttpContextAccessor>(), provider.GetService<ICurrentUserService>()));
}).CreateMapper());
//services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SocialWebApp API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        }
    );
});
string issuer = builder.Configuration.GetValue<string>("Tokens:Issuer");
string signingKey = builder.Configuration.GetValue<string>("Tokens:Key");
byte[] signingKeyBytes = Encoding.UTF8.GetBytes(signingKey);
services
    .AddAuthentication(opts =>
    {
        opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opts =>
    {
        opts.RequireHttpsMetadata = false;
        opts.SaveToken = true; // Save token in HttpContext object
        opts.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = issuer,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
        };
        opts.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    path.StartsWithSegments("/hub"))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });
services.AddAuthorization();
services.AddSession();
services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
    foreach (var roleName in USER_ROLE.Roles)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
    await unitOfWork.CreateTransaction();
    foreach (var roleName in CHAT_ROLE.ChatRoles)
    {
        var roleExist = await unitOfWork.ChatRoleRepository.GetByName(roleName);
        if (roleExist == null)
        {
            await unitOfWork.ChatRoleRepository.Insert(new ChatRole() { ChatRoleName = roleName });
        }
    }
    await unitOfWork.Save();
    await unitOfWork.Commit();
}
await CreateRoles(services.BuildServiceProvider());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatHub>("/hub/chat");
app.MapControllers();

app.Run();