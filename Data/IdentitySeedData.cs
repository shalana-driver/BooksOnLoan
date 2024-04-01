using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksOnLoan.Models;
using Microsoft.AspNetCore.Identity;

namespace BooksOnLoan.Data
{
    public class IdentitySeedData
    {
        public static async Task Initialize(ApplicationDbContext context,
        UserManager<ExtendedUser> userManager,
        RoleManager<IdentityRole> roleManager) {
        context.Database.EnsureCreated();

        string adminRole = "Admin";
        string memberRole = "Member";
        string password4all = "P@$$w0rd";

        if (await roleManager.FindByNameAsync(adminRole) == null) {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        if (await roleManager.FindByNameAsync(memberRole) == null) {
            await roleManager.CreateAsync(new IdentityRole(memberRole));
        }

        if (await userManager.FindByNameAsync("aa@aa.aa") == null){
            var user = new ExtendedUser {
                UserName = "aa@aa.aa",
                Email = "aa@aa.aa",
                PhoneNumber = "6902341234",
                FirstName = "Alice",
                LastName = "Adams",
                Street = "123 Main St",
                City = "Toronto",
                Province = "ON",
                PostalCode = "M1M 1M1"
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded) {
                await userManager.AddPasswordAsync(user, password4all);
                await userManager.AddToRoleAsync(user, adminRole);
            }
        }

        if (await userManager.FindByNameAsync("mm@mm.mm") == null) {
            var user = new ExtendedUser {
                UserName = "mm@mm.mm",
                Email = "mm@mm.mm",
                PhoneNumber = "6572136821",
                FirstName = "Mary",
                LastName = "Morgan",
                Street = "456 Elm St",
                City = "Vancouver",
                Province = "BC",
                PostalCode = "M2M 2M2"
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded) {
                await userManager.AddPasswordAsync(user, password4all);
                await userManager.AddToRoleAsync(user, memberRole);
            }
        }
    }
    }
}