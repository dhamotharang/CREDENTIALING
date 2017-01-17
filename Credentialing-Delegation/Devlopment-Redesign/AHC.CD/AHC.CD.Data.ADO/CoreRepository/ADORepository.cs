using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.CoreRepository
{
    class ADORepository
    {
        public DataTable GetData(SqlCommand cmd)
        {
            try
            {
                DataTable result;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    result = new DataTable();
                    da.Fill(result);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetConnectionString(DataBaseSchemaEnum type)
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}

