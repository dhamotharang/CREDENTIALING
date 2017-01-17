using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.PDFGenerator
{
    public class PlanFormGenerationBusinessModel
    {
        public int ProfileID { get; set; }

        public List<string> GeneratedFilePaths { get; set; }
    }
}
