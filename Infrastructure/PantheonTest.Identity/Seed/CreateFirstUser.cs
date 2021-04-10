using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PantheonTest.Identity.Models;

namespace PantheonTest.Identity.Seed
{
    public static class UserCreator
    {
        public static async Task SeedAsync(UserManager<BankUser> userManager, BankUser firstUser)
        {
            var user = await userManager.FindByEmailAsync(firstUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(firstUser, "Plural&01?");
            }
        }
    }
}