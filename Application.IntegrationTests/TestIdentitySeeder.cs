using BjBygg.Application.Common;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    public class TestIdentitySeeder
    {

        public static async Task SeedAllAsync(RoleManager<IdentityRole> roleManager)
        {
            await SeedRolesAsync(roleManager);
        }

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.Roles.Count() != 0) return;
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Employee });
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Management });
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Leader });
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Employer });
        }
    }
}
