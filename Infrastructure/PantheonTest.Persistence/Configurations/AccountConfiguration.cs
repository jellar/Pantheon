using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(a => a.Id).IsRequired();
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.Name).IsRequired().HasMaxLength(65);
            builder.Property(a => a.Number).IsRequired().HasMaxLength(8);
            builder.Property(a => a.Balance).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(a => a.Currency).IsRequired().HasMaxLength(3);
        }
    }
}
