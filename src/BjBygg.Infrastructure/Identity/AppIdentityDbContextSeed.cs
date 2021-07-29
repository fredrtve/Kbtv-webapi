using BjBygg.Application.Common;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BjBygg.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAppIdentityDbContext context,
            IIdGenerator idGenerator)
        {
            //if (!roleManager.RoleExistsAsync(Roles.Employee).Result)
            //    await roleManager.CreateAsync(new IdentityRole { Name = Roles.Employee });

            //if (!roleManager.RoleExistsAsync(Roles.Management).Result)
            //    await roleManager.CreateAsync(new IdentityRole { Name = Roles.Management });

            //if (!roleManager.RoleExistsAsync(Roles.Leader).Result)
            //    await roleManager.CreateAsync(new IdentityRole { Name = Roles.Leader });

            //if (!roleManager.RoleExistsAsync(Roles.Employer).Result)
            //    await roleManager.CreateAsync(new IdentityRole { Name = Roles.Employer });

            if (!roleManager.RoleExistsAsync(Roles.Admin).Result)
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin });

            //if (userManager.FindByNameAsync(Roles.Management).Result == null)
            //{
            //    var user = new ApplicationUser { UserName = Roles.Management, SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Karin", LastName = "Kro", PhoneNumber = "94446434" };
            //    var result = await userManager.CreateAsync(user, "passord1");

            //    if (result.Succeeded)
            //        userManager.AddToRoleAsync(user, Roles.Management).Wait();
            //}

            //if (userManager.FindByNameAsync(Roles.Leader).Result == null)
            //{
            //    var user = new ApplicationUser { UserName = Roles.Leader, SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Peder", LastName = "Lan", PhoneNumber = "94446434" };
            //    var result = await userManager.CreateAsync(user, "passord1");

            //    if (result.Succeeded)
            //        userManager.AddToRoleAsync(user, Roles.Leader).Wait();
            //}

            //if (userManager.FindByNameAsync(Roles.Employee).Result == null)
            //{
            //    var user = new ApplicationUser { UserName = Roles.Employee, SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Kasper", LastName = "Kro", PhoneNumber = "94446434" };
            //    var result = await userManager.CreateAsync(user, "passord1");

            //    if (result.Succeeded)
            //        userManager.AddToRoleAsync(user, Roles.Employee).Wait();
            //}

            //if (userManager.FindByNameAsync(Roles.Employer).Result == null)
            //{
            //    var user = new ApplicationUser { UserName = Roles.Employer, SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Markus", LastName = "Krok", PhoneNumber = "94446434" };
            //    var result = await userManager.CreateAsync(user, "passord1");

            //    if (result.Succeeded)
            //        userManager.AddToRoleAsync(user, Roles.Employer).Wait();
            //}

            context.SaveChanges();
        }
    }
}

