using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_B3.DomainClass;

namespace DAL_B3.Configurations
{
    public class SMSsConfiguration : IEntityTypeConfiguration<SMS>
    {
        public void Configure(EntityTypeBuilder<SMS> builder)
        {
            builder.ToTable("SMS");
            builder.HasKey(c => c.SmsId);
            builder.Property(e => e.SmsId).HasDefaultValueSql("(newid())");
            builder.Property(e => e.CreatedPerson).HasMaxLength(10).IsRequired(true);
            builder.Property(e => e.Phone).HasMaxLength(10).IsRequired(true);

        }
    }
}
