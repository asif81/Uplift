using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;

namespace Uplift.DataAccess.Data.Repository
{
    public class SP_Call : ISP_Call
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private static string ConnectionString = "";

        public SP_Call(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            ConnectionString = _applicationDbContext.Database.GetDbConnection().ConnectionString;
        }
            
        void ISP_Call.Execute(string procedureName, DynamicParameters parameters)
        {
            using (SqlConnection sql = new SqlConnection(ConnectionString))
            {
                sql.Open();
                sql.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        T ISP_Call.ExecuteScalar<T>(string procedureName, DynamicParameters parameters)
        {
            using (SqlConnection sql = new SqlConnection(ConnectionString))
            {
                sql.Open();
                return (T)Convert.ChangeType(sql.ExecuteScalar<T>(procedureName, parameters, commandType: CommandType.StoredProcedure),typeof(T));
            }
        }

        IEnumerable<T> ISP_Call.ReturnList<T>(string procedureName, DynamicParameters parameters)
        {
            using (SqlConnection sql = new SqlConnection(ConnectionString))
            {
                sql.Open();
                return sql.Query<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        void IDisposable.Dispose()
        {
            _applicationDbContext.Dispose();
        }
    }
}
