using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface ISP_Call : IDisposable
    {
        IEnumerable<T> ReturnList<T>(string procedureName, Dapper.DynamicParameters parameters = null);
        void Execute(string procedureName, Dapper.DynamicParameters parameters = null);
        T ExecuteScalar<T>(string procedureName, Dapper.DynamicParameters parameters = null);
    }
}
