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

        public IEnumerable<T> ExecuteQuery<T>(DynamicParameters DP, string Query)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    var result = connection.Query<T>(Query,DP);
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

        public IEnumerable<T> ExecuteQueryWithoutParams<T>(string Query)
        {
            using (IDbConnection connection = OpenConnection())
            {
                try
                {
                    var result = connection.QueryMultiple(Query, commandType: CommandType.StoredProcedure);
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

    }
}
