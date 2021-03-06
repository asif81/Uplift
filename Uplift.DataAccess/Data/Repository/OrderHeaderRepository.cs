using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeder>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public OrderHeaderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
              
        public void ChangeOrderHeader(int Id, string Status)
        {
            var OrderFromDb = _applicationDbContext.orderHeders.FirstOrDefault(o=>o.Id == Id);
            OrderFromDb.Status = Status;
            _applicationDbContext.SaveChanges();
        }
    }
}
