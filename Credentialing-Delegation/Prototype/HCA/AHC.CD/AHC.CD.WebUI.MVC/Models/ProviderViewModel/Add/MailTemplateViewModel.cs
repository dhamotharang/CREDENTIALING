using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Add
{
    public class MailTemplateViewModel
    {
        public DateTime MailDate { get; set; }
        public string Title { get; set; }
        public MailCategoryViewModel MailCategory { get; set; }
    }
}
