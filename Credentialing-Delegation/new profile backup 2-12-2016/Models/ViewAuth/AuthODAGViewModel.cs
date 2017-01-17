using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.ViewAuth
{
    public class AuthODAGViewModel
    {
        public int ODAGID { get; set; }

        // foregn key and object of question --------
        public int QuestionID { get; set; }

        public string OptionAnswer { get; set; }

        //[RegularExpression(RegularExpression.DATE_FORMAT_MM_DD_YYYY, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? OptionDate { get; set; }

        public string TempForDataInitialise { get; set; }
    }
}