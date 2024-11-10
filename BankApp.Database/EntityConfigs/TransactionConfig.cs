using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankApp.Database.EntityConfigs
{
    public class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn(1, 1);
            builder.Property(t => t.CurrentDate).IsRequired().HasColumnType("DATE");
            builder.Property(t => t.Amount).IsRequired().HasColumnType("DECIMAL(18, 2)");
            builder.Property(t => t.AmountInPublicCurrency).IsRequired().HasColumnType("DECIMAL(18,2)");
            builder.Property(t => t.ExchRate).IsRequired().HasColumnType("DECIMAL(18,4)");
            builder.Property(t => t.Description).IsRequired().HasColumnType("NVARCHAR").HasMaxLength(100);
            builder.Property(b => b.CreatedDate).IsRequired().HasColumnType("DATETIME");
            builder.Property(b => b.UpdatedDate).IsRequired(false).HasColumnType("DATETIME");
        }
    }
}
