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
    public class GroupsConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");
            builder.HasKey(c => c.GroupId);
            builder.Property(e => e.GroupId).HasDefaultValueSql("(newid())");
            builder.Property(e => e.GroupCode).HasMaxLength(10).IsRequired(true).IsUnicode(false);
            builder.HasIndex(e => e.GroupCode).IsUnique(true);// không cho trùng

        }
    }
}
