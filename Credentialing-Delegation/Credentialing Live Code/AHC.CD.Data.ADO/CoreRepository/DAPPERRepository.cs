using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AHC.CD.Data.ADO.CoreRepository
{
    internal class DAPPERRepository
    {
        protected static IDbConnection OpenConnection()
        {
            try
            {
                IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString);
                connection.Open();
                return connection;
            }
            catch (ConfigurationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected static IDbConnection OpenConnectionForASPNETUsers()
        {
            try
            {
                IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                connection.Open();
                return connection;
            }
            catch (ConfigurationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected static IDbConnection OpenConnectionForAuditLogger()
        {
            try
            {
                IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString);
                connection.Open();
                return connection;
            }
            catch (ConfigurationErrorsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string Query)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    var result = await connection.QueryAsync<T>(Query);
                    return result;
                }

                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(DynamicParameters DP, string Query)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    var result = await connection.QueryAsync<T>(Query,DP);
                    return result;
                }

                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public async Task<IEnumerable<T>> ExecuteQueryWithoutParamsAsync<T>(string Query)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    var result = await connection.QueryMultipleAsync(Query, commandType: CommandType.StoredProcedure);
                    return result.Read<T>();
                }

                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public IEnumerable<T> ExecuteQuery<T>(string Query)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    var result = connection.Query<T>(Query);
                    return result;
                }

                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        public async Task<List<T>> ExecuteAsyncQuery<T>(string Query)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    var result = await connection.QueryAsync<T>(Query);
                    return result.ToList();
                }
                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<T>> ExecuteAsyncQuery<T>(Dapper.DynamicParameters DP, string Query)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    var result = await connection.QueryAsync<T>(Query,DP);
                    return result.ToList();
                }
                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<T> ExecuteAsyncQueryFirstOrDefault<T>(Dapper.DynamicParameters DP, string Query)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    var result = await connection.QueryAsync<T>(Query, DP);
                    return result.FirstOrDefault();
                }
                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<T> ExecuteQuery<T>(Dapper.DynamicParameters DP, string Query)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    var result = connection.Query<T>(Query,DP);
                    return result.ToList();
                }

                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public List<T> ExecuteQueryWithoutParams<T>(string Query)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    var result = connection.QueryMultiple(Query, commandType: CommandType.StoredProcedure);
                    return result.Read<T>().ToList();
                }

                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        public List<T> ExecuteQueryForASPNETUsers<T>(Dapper.DynamicParameters DP, string Query)
        {
            using (IDbConnection connection = OpenConnectionForASPNETUsers())
            {
                try
                {
                    var result = connection.Query<T>(Query,DP);
                    return result.ToList();
                }

                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }


        
        public List<T> ExecuteQueryForASPNETUsers<T>(string Query)
        {
            using (IDbConnection connection = OpenConnectionForASPNETUsers())
            {
                try
                {
                    var result = connection.Query<T>(Query);
                    return result.ToList();
                }

                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        public List<T> ExecuteQueryForAuditLogger<T>(string Query)
        {
            using (IDbConnection connection = OpenConnectionForAuditLogger())
            {
                try
                {
                    var result = connection.Query<T>(Query);
                    return result.ToList();
                }

                catch (ArgumentNullException ex)
                {
                    throw ex;
                }


                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }


        public T QueryMultiple<T>(string Query, DynamicParameters dp, Func<SqlMapper.GridReader, T> ObjectMapping)
        {
            using (IDbConnection connection = OpenConnection())
            {
                SqlMapper.GridReader result = connection.QueryMultiple(Query, param: dp, commandType: CommandType.StoredProcedure);
                return ObjectMapping(result);
            }
        }

        public SqlMapper.GridReader QueryMultiple(string Query, DynamicParameters dp)
        {
            using (IDbConnection connection = OpenConnection())
            {
                SqlMapper.GridReader result = connection.QueryMultiple(Query, param: dp, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        
    }
}
