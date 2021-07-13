using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ServiceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<SelectListItem> GetServiceListFromDropDown()
        {
            return _applicationDbContext.categories.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }); ;
        }

        public void Update(Service service)
        {
            var objFromDb = _applicationDbContext.services.FirstOrDefault(s => s.Id == service.Id);
            objFromDb.Name = service.Name;
            objFromDb.CategoryId = service.CategoryId;
            objFromDb.FrequencyId = service.FrequencyId;
            objFromDb.ImageUrl = service.ImageUrl;
            objFromDb.LongDesc = service.LongDesc;
            objFromDb.Price = service.Price;

            _applicationDbContext.SaveChanges();
        }
    }
}
