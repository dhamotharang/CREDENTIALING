using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
   public class ODAGViewModel
    {
        public int ODAGID { get; set; }
        public int QuestionID { get; set; }
        public string Description { get; set; }
        public string QuestionType { get; set; }
        public string OptionAnswer { get; set; }
        public DateTime? OptionDate { get; set; }
        public List<Option> Options { get; set; }
        public string TempForDataInitialise { get; set; }
        public ODAGViewModel()
        {
            Options = new List<Option>();
        }
    }
   public class Option
   {
       public int OptionID { get; set; }
       public string Value { get; set; }
   }
}
