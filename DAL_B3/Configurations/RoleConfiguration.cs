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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(c => c.RoleId);
            builder.Property(e => e.RoleId).HasDefaultValueSql("(newid())");
            builder.Property(e => e.RoleCode).HasMaxLength(10).IsRequired(true).IsUnicode(false);
            builder.HasIndex(e => e.RoleCode).IsUnique(true);// không cho trùng


        }
    }
}
