using Dapper;
using System.Data;
using System.Data.Common;

namespace ORM.Define
{
    public interface IDataFactory
    {
        DbConnection GetDbconnection();

        Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<IEnumerable<T>> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<int> Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<T> Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<T> Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
