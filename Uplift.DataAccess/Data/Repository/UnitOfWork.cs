using System;
using System.Collections.Generic;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;

namespace Uplift.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            category = new CategoryRepository(applicationDbContext);
            frequency = new FrequencyRepository(applicationDbContext);
            service = new ServiceRepository(applicationDbContext);
            orderDetail = new OrderDetailRepository(applicationDbContext);
            orderHeader = new OrderHeaderRepository(applicationDbContext);
            user = new UserRepository(applicationDbContext);
            webImage = new WebImageRepository(applicationDbContext);
            sp_Call = new SP_Call(applicationDbContext);
        }

        public ICategoryRepository category { get; private set; }
        public IFrequencyRepository frequency { get; private set; }
        public IServiceRepository service { get; private set; }
        public IOrderDetailRepository orderDetail { get; private set; }
        public IOrderHeaderRepository orderHeader { get; private set; }
        public IUserRepository user { get; private set; }
        public IWebImageRepository webImage { get; private set; }
        public ISP_Call sp_Call { get; private set; }
        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
              
        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}
