using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities
{
    public class DocumentCheckListPDFViewModel
    {
        public int ProfileID { get; set; }
        public string PdfText { get; set; }
        public string PageSize { get; set; }
        public string PageOrientation { get; set; }
        public int PageHeight { get; set; }
        public int PageWidth { get; set; }

    }
}
