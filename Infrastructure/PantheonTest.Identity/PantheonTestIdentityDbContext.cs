using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PantheonTest.Identity.Models;

namespace PantheonTest.Identity
{
    public class PantheonTestIdentityDbContext : IdentityDbContext<BankUser>
    {
        public PantheonTestIdentityDbContext(DbContextOptions<PantheonTestIdentityDbContext> options) : base(options)
        {
            
        }
    }
}
