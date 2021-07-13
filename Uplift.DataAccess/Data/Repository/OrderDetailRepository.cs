using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class OrderDetailRepository : Repository<OrderDetails>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public OrderDetailRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<SelectListItem> GetCategoryListFromDropDown()
        {
            return _applicationDbContext.categories.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }); ;
        }

        public void Update(Category category)
        {
          var objFromDb = _applicationDbContext.categories.FirstOrDefault(s=> s.Id == category.Id);
          objFromDb.Name = category.Name;
          objFromDb.DisplayOrder = category.DisplayOrder;

          _applicationDbContext.SaveChanges();
        }
    }
}
