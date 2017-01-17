using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class TextSnippetViewModel
    {
        public TextSnippetViewModel()
        {
            IsActive = true;
            Roles = new List<AuthorizationRoleTextSnippetViewModel>();
        }
        public int TextSnippetId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatorRole { get; set; }
        public string SnippetType { get; set; }
        public List<AuthorizationRoleTextSnippetViewModel> Roles { get; set; }
        public string Note { get; set; }
        public string Plan { get; set; }
        public string Svs_Subject_to_Notice { get; set; }
        public string Specific_Item_Requested { get; set; }
        public string MedicalNecessaries { get; set; }
        public string Rationale { get; set; }
        public string Criteria { get; set; }
        public string Alt_Plan_of_Care { get; set; }
        public string NCQA_Statement_ALL { get; set; }
        public string Module { get; set; }
        public string ReviewType { get; set; }

    }
}