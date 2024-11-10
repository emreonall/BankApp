using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Database.EntityConfigs
{
    public class CurrencyConfig : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1,1);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50).HasColumnType("NVARCHAR");
            builder.Property(x => x.ShortName).IsRequired().HasMaxLength(3).HasColumnType("NVARCHAR");
            builder.Property(b => b.CreatedDate).IsRequired().HasColumnType("DATETIME");
            builder.Property(b => b.UpdatedDate).IsRequired(false).HasColumnType("DATETIME");
        }
    }
}
