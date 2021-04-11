using Microsoft.AspNetCore.Identity;

namespace PantheonTest.Identity.Models
{
    public class BankUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
