using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Configurations
{
    public class UserChatConfigurations : IEntityTypeConfiguration<UserChat>
    {
        public void Configure(EntityTypeBuilder<UserChat> builder)
        {
            builder.ToTable("UserChats");

            builder.HasKey(x => new { x.UserId, x.ChatId });

            builder.HasOne(x => x.Chat).WithMany(x => x.UserChats).HasForeignKey(x => x.ChatId);
            builder.HasOne(x => x.User).WithMany(x => x.UserChats).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.ChatRole).WithMany(x => x.UserChats).HasForeignKey(x => x.ChatRoleId);
        }
    }
}