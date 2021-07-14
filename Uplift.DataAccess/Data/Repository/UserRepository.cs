using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void UnLockUser(string UserId)
        {
            var userFromDb = _applicationDbContext.Users.FirstOrDefault(u => u.Id == UserId);
            userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            _applicationDbContext.SaveChanges();
        }

        public void LockUser(string UserId)
        {
            var userFromDb = _applicationDbContext.Users.FirstOrDefault(u => u.Id == UserId);
            userFromDb.LockoutEnd = DateTime.Now;
            _applicationDbContext.SaveChanges();
        }
    }
}
