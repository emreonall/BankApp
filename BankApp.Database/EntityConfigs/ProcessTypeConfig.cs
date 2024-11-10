using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankApp.Database.EntityConfigs
{
    public class ProcessTypeConfig : IEntityTypeConfiguration<ProcessType>
    {
        public void Configure(EntityTypeBuilder<ProcessType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p=> p.Id).UseIdentityColumn(1,1);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(10).HasColumnType("NVARCHAR");
            builder.Property(p => p.Symbol).IsRequired().HasMaxLength(1).HasColumnType("NVARCHAR");
            builder.Property(p => p.Multiplier).IsRequired().HasColumnType("INT");
            builder.Property(b => b.CreatedDate).IsRequired().HasColumnType("DATETIME");
            builder.Property(b => b.UpdatedDate).IsRequired(false).HasColumnType("DATETIME");
        }
    }
}
