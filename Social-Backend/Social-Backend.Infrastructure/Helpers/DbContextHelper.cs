using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Helpers
{
    public class DbContextHelper
    {
        public static DbContextOptions GetDBContextOptions()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("SocialDbContext");
            var optionsBuilder = new DbContextOptionsBuilder<SocialDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return optionsBuilder.Options;
        }
    }
}