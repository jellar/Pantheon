using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PantheonTest.Domain.Entities;

namespace PantheonTest.Persistence
{
    public class PantheonDbContext : DbContext
    {
        public PantheonDbContext(DbContextOptions<PantheonDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PantheonDbContext).Assembly);
        }
    }
}
