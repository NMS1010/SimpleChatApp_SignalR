using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Social_Backend.Core.Common;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces.User;
using Social_Backend.Infrastructure.Configurations;
using Social_Backend.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Data
{
    public class SocialDBContext : IdentityDbContext<AppUser>
    {
        private ICurrentUserService _currentUserService;

        public ICurrentUserService CurrentUserService
        {
            set
            {
                this._currentUserService = value;
            }
        }

        public SocialDBContext()
        { }

        public SocialDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new ChatConfigurations());
            modelBuilder.ApplyConfiguration(new ChatRoleConfigurations());
            modelBuilder.ApplyConfiguration(new UserChatConfigurations());
            modelBuilder.ApplyConfiguration(new MessageConfigurations());
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                string tableName = type.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    type.SetTableName(tableName.Substring(6));
                }
            }
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Creator = _currentUserService.UserId ?? "System";
                        entry.Entity.CreateDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.Modifier = _currentUserService.UserId ?? "System";
                        entry.Entity.ModifyDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<ChatRole> ChatRoles { get; set; }
    }
}