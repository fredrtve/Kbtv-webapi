using BjBygg.Application.Common;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAppIdentityDbContext context)
        {
            if (!roleManager.RoleExistsAsync(Roles.Employee).Result)
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Employee });

            if (!roleManager.RoleExistsAsync(Roles.Management).Result)
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Management });

            if (!roleManager.RoleExistsAsync(Roles.Leader).Result)
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Leader });

            if (!roleManager.RoleExistsAsync(Roles.Employer).Result)
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Employer });

            if (userManager.FindByNameAsync("leder").Result == null)
            {
                var user = new ApplicationUser { UserName = "leder", SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Bjørn August", LastName = "Olafsson", PhoneNumber = "95546434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Leader).Wait();
            }

            if (userManager.FindByNameAsync("mellomleder").Result == null)
            {
                var user = new ApplicationUser { UserName = "mellomleder", SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Karin", LastName = "Kro", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Management).Wait();
            }

            if (userManager.FindByNameAsync("oppdragsgiver").Result == null)
            {
                var user = new ApplicationUser { UserName = "oppdragsgiver", SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Oppdrags", LastName = "Giver", PhoneNumber = "94446434", EmployerId = 1 };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Employer).Wait();
            }

            if (userManager.FindByNameAsync("ansatt").Result == null)
            {
                var user = new ApplicationUser { UserName = "ansatt", SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Jens", LastName = "Nordmann", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Employee).Wait();
            }

            if (userManager.FindByNameAsync("ansatt1").Result == null)
            {
                var user = new ApplicationUser { UserName = "ansatt1", SecurityStamp = Guid.NewGuid().ToString(), Email = "test2@test.com", FirstName = "Jens2", LastName = "Nordmann2", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Employee).Wait();
            }

            if (userManager.FindByNameAsync("ansatt2").Result == null)
            {
                var user = new ApplicationUser { UserName = "ansatt2", SecurityStamp = Guid.NewGuid().ToString(), Email = "test3@test.com", FirstName = "Jens3", LastName = "Nordmann3", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Employee).Wait();
            }

            if (userManager.FindByNameAsync("ansatt3").Result == null)
            {
                var user = new ApplicationUser { UserName = "ansatt3", SecurityStamp = Guid.NewGuid().ToString(), Email = "test4@test.com", FirstName = "Jens4", LastName = "Nordmann4", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Employee).Wait();
            }

            context.Database.OpenConnection();
            context.InboundEmailPasswords.Add(new InboundEmailPassword() { Id = 1, Password = "passord1" });
            context.SaveChanges();
            context.Database.CloseConnection();

        }
    }
}

