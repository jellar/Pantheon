using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.AccountId).IsRequired();
            builder.Property(t => t.Reference).IsRequired().HasMaxLength(125);
            builder.Property(t => t.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(t => t.Balance).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}
