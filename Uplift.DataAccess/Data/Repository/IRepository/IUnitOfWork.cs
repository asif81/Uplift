using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository category { get; }
        IFrequencyRepository frequency { get; }
        IServiceRepository service { get; }
        IOrderDetailRepository orderDetail { get; }
        IOrderHeaderRepository orderHeader { get; }
        IUserRepository user { get; }
        ISP_Call sp_Call { get; }
        void Save();
    }
}
