using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankApp.Database.EntityConfigs
{
    public class ExchnageRateConfig : IEntityTypeConfiguration<ExchangeRate>
    {
        public void Configure(EntityTypeBuilder<ExchangeRate> builder)
        {
            builder.HasKey(er=>er.Id);
            builder.Property(er => er.Id).UseIdentityColumn(1, 1);
            builder.Property(er => er.CurrentDate).IsRequired().HasColumnType("date");
            builder.Property(er => er.Rate).IsRequired().HasColumnType("DECIMAL(18,4)");
            builder.Property(b => b.CreatedDate).IsRequired().HasColumnType("DATETIME");
            builder.Property(b => b.UpdatedDate).IsRequired(false).HasColumnType("DATETIME");
        }
    }
}
