using Dapper;
using Microsoft.Extensions.Configuration;
using ORM.Define;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ORM.DataFactory
{
    public class DataFactory : IDataFactory
    {
        private readonly IConfiguration _config;
        private readonly string ConnectionString = "BaseRepoConnection";


        public async Task<int> Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (DbConnection db = new SqlConnection(ConnectionString))
            {
                int result = 0;
                try
                {
                    if (db.State == ConnectionState.Closed)
                        await db.OpenAsync();

                    using var tran = await db.BeginTransactionAsync();
                    try
                    {
                        result = await db.ExecuteAsync(sp, parms, commandType: commandType, transaction: tran);
                        await tran.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await tran.RollbackAsync();
                        throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        await db.CloseAsync();
                }

                return result;
            }
        }

        public async Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var db = new SqlConnection(_config.GetConnectionString(ConnectionString)))
            {
                try
                {
                    await db.OpenAsync();
                    return await db.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<T>> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (var db = new SqlConnection(_config.GetConnectionString(ConnectionString)))
                {
                    await db.OpenAsync();
                    return await db.QueryAsync<T>(sp, parms, commandType: commandType);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_config.GetConnectionString(ConnectionString));
        }

        public async Task<T> Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var db = new SqlConnection(_config.GetConnectionString(ConnectionString)))
            {
                T result;

                try
                {
                    if (db.State == ConnectionState.Closed)
                        await db.OpenAsync();

                    using var tran = await db.BeginTransactionAsync();
                    try
                    {
                        result = await db.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType, transaction: tran);
                        await tran.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        await db.CloseAsync();
                }

                return result;
            }
        }

        public async Task<T> Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var db = new SqlConnection(_config.GetConnectionString(ConnectionString)))
            {
                T result;
                try
                {
                    if (db.State == ConnectionState.Closed)
                        await db.OpenAsync();

                    using var tran = await db.BeginTransactionAsync();
                    try
                    {
                        result = await db.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType, transaction: tran);
                        await tran.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        await db.CloseAsync();
                }

                return result;
            }
        }
    }
}
