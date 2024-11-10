using BankApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankApp.Database.EntityConfigs
{
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
           // builder.ToTable( nameof(Company),"dbo");
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Id).UseIdentityColumn(1, 1);
            builder.Property(c => c.CompanyCode).HasMaxLength(5).IsRequired().HasColumnType("NVARCHAR");
            builder.Property(c => c.CompanyName).HasMaxLength(50).IsRequired().HasColumnType("NVARCHAR");
            builder.Property(b => b.CreatedDate).IsRequired().HasColumnType("DATETIME");
            builder.Property(b => b.UpdatedDate).IsRequired(false).HasColumnType("DATETIME");
        }
    }
}
