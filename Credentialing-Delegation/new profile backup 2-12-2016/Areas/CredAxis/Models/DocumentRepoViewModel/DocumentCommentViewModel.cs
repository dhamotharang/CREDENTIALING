using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.CredAxis.Models.DocumentRepoViewModel
{
    public class DocumentCommentViewModel
    {
        public string UserName { get; set; }
        public string CommentBody { get; set; }
        public DateTime CommentDateTime { get; set; }
    }
}
