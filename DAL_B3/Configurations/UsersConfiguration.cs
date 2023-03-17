using DAL_B3.DomainClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(c => c.UserId);
            builder.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            builder.Property(e => e.UserName).IsUnicode(false).HasMaxLength(10).IsRequired(true);// unicode là không cho nhập kí tự đặc biệt
            builder.HasIndex(c => c.UserName).IsUnique(true);// không cho trùng

            builder.HasOne(x => x.RoleId_Navigation).WithMany(c => c.Users).HasForeignKey(c => c.RoleId);
            builder.Property(e => e.Password).HasMaxLength(20).IsRequired(true);

        }
    }
}
