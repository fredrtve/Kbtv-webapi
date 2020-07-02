using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            AppIdentityDbContext context)
        {           
            if (!roleManager.RoleExistsAsync("Ansatt").Result)
                await roleManager.CreateAsync(new IdentityRole { Name = "Ansatt" });

            if (!roleManager.RoleExistsAsync("Mellomleder").Result)
                await roleManager.CreateAsync(new IdentityRole { Name = "Mellomleder" });

            if (!roleManager.RoleExistsAsync("Leder").Result)
                await roleManager.CreateAsync(new IdentityRole { Name = "Leder" });

            if (!roleManager.RoleExistsAsync("Oppdragsgiver").Result)
                await roleManager.CreateAsync(new IdentityRole { Name = "Oppdragsgiver" });

            if (userManager.FindByNameAsync("leder").Result == null)
            {
                var user = new ApplicationUser { UserName = "leder", SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Bjørn August", LastName = "Olafsson", PhoneNumber = "95546434" };
                var result = await userManager.CreateAsync(user,"passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Leder").Wait();
            }

            if (userManager.FindByNameAsync("mellomleder").Result == null)
            {
                var user = new ApplicationUser { UserName = "mellomleder", SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Karin", LastName = "Kro", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Mellomleder").Wait();
            }

            if (userManager.FindByNameAsync("oppdragsgiver").Result == null)
            {
                var user = new ApplicationUser { UserName = "oppdragsgiver", SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Oppdrags", LastName = "Giver", PhoneNumber = "94446434", EmployerId = 1 };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Oppdragsgiver").Wait();
            }

            if (userManager.FindByNameAsync("ansatt").Result == null)
            {
                var user = new ApplicationUser { UserName = "ansatt", SecurityStamp = Guid.NewGuid().ToString(), Email = "test@test.com", FirstName = "Jens", LastName = "Nordmann", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Ansatt").Wait();
            }

            if (userManager.FindByNameAsync("ansatt1").Result == null)
            {
                var user = new ApplicationUser { UserName = "ansatt1", SecurityStamp = Guid.NewGuid().ToString(), Email = "test2@test.com", FirstName = "Jens2", LastName = "Nordmann2", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Ansatt").Wait();
            }

            if (userManager.FindByNameAsync("ansatt2").Result == null)
            {
                var user = new ApplicationUser { UserName = "ansatt2", SecurityStamp = Guid.NewGuid().ToString(), Email = "test3@test.com", FirstName = "Jens3", LastName = "Nordmann3", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Ansatt").Wait();
            }

            if (userManager.FindByNameAsync("ansatt3").Result == null)
            {
                var user = new ApplicationUser { UserName = "ansatt3", SecurityStamp = Guid.NewGuid().ToString(), Email = "test4@test.com", FirstName = "Jens4", LastName = "Nordmann4", PhoneNumber = "94446434" };
                var result = await userManager.CreateAsync(user, "passord1");

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Ansatt").Wait();
            }

            context.Database.OpenConnection();
            context.InboundEmailPasswords.Add(new InboundEmailPassword() { Id = 1, Password = "passord1" });
            context.SaveChanges();
            context.Database.CloseConnection();

        }
    }
}

