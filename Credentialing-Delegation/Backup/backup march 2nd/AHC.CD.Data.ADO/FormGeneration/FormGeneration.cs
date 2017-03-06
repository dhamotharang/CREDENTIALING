using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Data.ADO.DTO.FormDTO;

using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.FormGeneration
{
    public class FormGeneration : IFormGeneration
    {
        DAPPERRepository dp = null;
        public FormGeneration()
        {
            this.dp = new DAPPERRepository();
        }

        
        public async Task<List<FormData>> GetData(int profileID, string query)
        {
            List<FormData> Data = new List<FormData>();
            try
            {
                DynamicParameters values = new DynamicParameters();
                values.Add("@profileID", profileID);
                Data = await dp.ExecuteAsyncQuery<FormData>(values, query);

            }
            catch (Exception e)
            {
                throw e;
            }
            return Data;

        }
    }
}
