using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.PDFGenerator
{
    public class PDFSupplementData_DisclosureQnsBusinessModel
    {
        public int? QuestionCode1 { get; set; }
        public string QuestionExplanation1 { get; set; }

        public int? QuestionCode2 { get; set; }
        public string QuestionExplanation2 { get; set; }

        public int? QuestionCode3 { get; set; }
        public string QuestionExplanation3 { get; set; }
    }
}
