using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.CoreRepository
{
    internal class ADORepository
    {
        public static DataTable GetData(SqlCommand cmd)
        {
            try
            {
                DataTable resultSet;
                using (SqlDataAdapter dataAdapterObject = new SqlDataAdapter(cmd))
                {
                    resultSet = new DataTable();
                    dataAdapterObject.Fill(resultSet);
                }
                return resultSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetConnectionString(DataBaseSchemaEnum type)
        {
            try
            {
                string connectionString = null;

                switch (type)
                {
                    case DataBaseSchemaEnum.CredentialingConnectionString:
                        connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EFEntityContext"].ToString();
                        break;
                }

                return connectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

