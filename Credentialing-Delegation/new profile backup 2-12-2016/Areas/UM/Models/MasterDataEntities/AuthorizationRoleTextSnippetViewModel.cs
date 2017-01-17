using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class AuthorizationRoleTextSnippetViewModel
    {
        public int AuthorizationRoleTextSnippetID { get; set; }

        public int? TextSnippetID { get; set; }
        
        public TextSnippetViewModel TextSnippet { get; set; }

        public int? AuthorizationRoleID { get; set; }

        public AuthorizationRoleViewModel AuthorizationRole { get; set; }
    }
}
