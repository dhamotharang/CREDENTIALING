using AHC.CD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
   public interface IProfileReportManager
    {
         ICollection<ProfileReport> GetProfileReport();
         string SaveProfileReportPDFFile(byte[] pdfbytes);
    }
}
