using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class WebImageRepository : Repository<WebImages>, IWebImageRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public WebImageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Update(WebImages webImage)
        {
            var objFromDb = _applicationDbContext.WebImages.FirstOrDefault(s => s.Id == webImage.Id);
            objFromDb.Name = webImage.Name;
            objFromDb.Picture = webImage.Picture;

            _applicationDbContext.SaveChanges();
        }
    }
}
