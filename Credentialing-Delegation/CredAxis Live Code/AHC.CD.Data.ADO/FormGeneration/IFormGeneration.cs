
using AHC.CD.Data.ADO.DTO.FormDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.FormGeneration
{
    public interface IFormGeneration
    {
        
        Task<List<FormData>> GetData(int profileID, string query);
       
    }
}
