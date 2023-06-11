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
    public class ChatRoleConfigurations : IEntityTypeConfiguration<ChatRole>
    {
        public void Configure(EntityTypeBuilder<ChatRole> builder)
        {
            builder.ToTable("ChatRole");
            builder.HasKey(x => x.ChatRoleId);
            builder.Property(x => x.ChatRoleName).HasMaxLength(256).IsRequired();

            builder
                .HasMany(x => x.UserChats)
                .WithOne(x => x.ChatRole)
                .HasForeignKey(x => x.ChatRoleId);
        }
    }
}