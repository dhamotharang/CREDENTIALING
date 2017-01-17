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

        public List<T> ExecuteQuery<T>(string Query)
        {
            using (IDbConnection connection = OpenConnection())
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

        public List<T> ExecuteQuery<T>(Dapper.DynamicParameters DP, string Query)
        {
            using (IDbConnection connection = OpenConnection())
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
    }
}
