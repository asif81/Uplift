using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.Models;
using Uplift.Utility;

namespace Uplift.DataAccess.Data.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_applicationDbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _applicationDbContext.Database.Migrate();
                }
            }
            catch(Exception ex)
            {

            }

            if (_applicationDbContext.Roles.Any(r => r.Name == SD.Admin))
                return;

            _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult(); // makes sure that it waits for it complete
            _roleManager.CreateAsync(new IdentityRole(SD.Manager)).GetAwaiter().GetResult();// makes sure that it waits for it complete

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                Name = "Admin"
            },"Admin123*" ).GetAwaiter().GetResult();

            ApplicationUser user = _applicationDbContext.applicationUsers.Where(u=> u.Email == "admin@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, SD.Admin).GetAwaiter().GetResult();
        }
    }
}
