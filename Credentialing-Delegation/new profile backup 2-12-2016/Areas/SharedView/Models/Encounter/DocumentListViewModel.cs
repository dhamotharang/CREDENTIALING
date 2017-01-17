using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.SharedView.Models.Encounter
{
    public class DocumentListViewModel
    {
        public DocumentListViewModel()
        {
            UploadedDocuments = new List<DocumentViewModel>();
            DocumentHistory = new List<DocumentViewModel>();
        }
        public List<DocumentViewModel> UploadedDocuments { get; set; }
        public List<DocumentViewModel> DocumentHistory { get; set; }
    }
}