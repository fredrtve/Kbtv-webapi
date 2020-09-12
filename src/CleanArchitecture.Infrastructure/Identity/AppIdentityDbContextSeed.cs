using BjBygg.Application.Common;
using BjBygg.Application.Common.Interfaces;
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
            IAppIdentityDbContext context,
            IIdGenerator idGenerator)
        {
            if (!roleManager.RoleExistsAsync(Roles.Employee).Result)
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Employee });

            if (!roleManager.RoleExistsAsync(Roles.Management).Result)
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Management });

            if (!roleManager.RoleExistsAsync(Roles.Leader).Result)
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Leader });

            if (!roleManager.RoleExistsAsync(Roles.Employer).Result)
                await roleManager.CreateAsync(new IdentityRole { Name = Roles.Employer });

            if (userManager.FindByNameAsync(Roles.Leader).Result == null)
            {
                var user = new ApplicationUser { UserName = Roles.Leader, SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Bjørn August", LastName = "Olafsson", PhoneNumber = "95546434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Leader).Wait();
            }

            if (userManager.FindByNameAsync(Roles.Management).Result == null)
            {
                var user = new ApplicationUser { UserName = Roles.Management, SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Karin", LastName = "Kro", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Management).Wait();
            }

            if (userManager.FindByNameAsync(Roles.Employer).Result == null)
            {
                var user = new ApplicationUser { UserName = Roles.Employer, SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Oppdrags", LastName = "Giver", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Employer).Wait();
            }

            if (userManager.FindByNameAsync(Roles.Employee).Result == null)
            {
                var user = new ApplicationUser { UserName = Roles.Employee, SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Jens", LastName = "Nordmann", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Employee).Wait();
            }

            if (userManager.FindByNameAsync("Ansatt1").Result == null)
            {
                var user = new ApplicationUser { UserName = "Ansatt1", SecurityStamp = Guid.NewGuid().ToString(), Email = "test2@test.com", FirstName = "Jens2", LastName = "Nordmann2", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Employee).Wait();
            }

            if (userManager.FindByNameAsync("Ansatt2").Result == null)
            {
                var user = new ApplicationUser { UserName = "Ansatt2", SecurityStamp = Guid.NewGuid().ToString(), Email = "test3@test.com", FirstName = "Jens3", LastName = "Nordmann3", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Employee).Wait();
            }

            if (userManager.FindByNameAsync("Ansatt3").Result == null)
            {
                var user = new ApplicationUser { UserName = "Ansatt3", SecurityStamp = Guid.NewGuid().ToString(), Email = "test4@test.com", FirstName = "Jens4", LastName = "Nordmann4", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, Roles.Employee).Wait();
            }

            context.Database.OpenConnection();
            context.InboundEmailPasswords.Add(new InboundEmailPassword() { Id = idGenerator.Generate(), Password = "passord1" });
            context.SaveChanges();
            context.Database.CloseConnection();

        }
    }
}

