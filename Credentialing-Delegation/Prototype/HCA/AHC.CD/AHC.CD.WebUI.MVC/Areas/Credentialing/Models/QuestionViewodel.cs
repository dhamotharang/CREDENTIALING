using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models
{
    public class QuestionViewodel
    {
        public int QuestionID { get; set; }

        public string QuestionText { get; set; }

        public QuestionType Type { get; set; }  

        public string Answer { get; set; }
    }
}
