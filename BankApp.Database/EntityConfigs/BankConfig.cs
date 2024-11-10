using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankApp.Database.EntityConfigs
{
    public class BankConfig : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).UseIdentityColumn(1,1);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(20).HasColumnType("NVARCHAR");
            builder.Property(b => b.CreatedDate).IsRequired().HasColumnType("DATETIME");
            builder.Property(b => b.UpdatedDate).IsRequired(false).HasColumnType("DATETIME");


        }
    }
}
