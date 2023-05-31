using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Data
{
    public class SocialDBContextFactory : IDesignTimeDbContextFactory<SocialDBContext>
    {
        public SocialDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("SocialDbContext");
            var optionsBuilder = new DbContextOptionsBuilder<SocialDBContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new SocialDBContext(optionsBuilder.Options);
        }
    }
}