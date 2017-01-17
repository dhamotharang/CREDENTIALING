using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.DocumentRepo
{
    public class FolderViewModel
    {
        public string FolderName { get; set; }
        public List<DocumentViewModel> Documents { get; set; }
    }
}