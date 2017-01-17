using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Coding.Models.PowerDriveService
{
    public class Result
    {
        public Result()
        {
            FileInfomations = new List<FileInformation>();
        }
        public List<FileInformation> FileInfomations { get; set; }
       
 
    }

}
