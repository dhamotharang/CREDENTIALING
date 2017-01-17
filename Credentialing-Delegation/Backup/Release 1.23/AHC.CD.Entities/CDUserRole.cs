using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities
{
    public class CDUserRole
    {
        public CDUserRole()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CDUserRoleID { get; set; }

        public int CDUserId { get; set; }
        [ForeignKey("CDUserId")]
        public CDUser CDUser { get; set; }

        public int CDRoleId { get; set; }
        [ForeignKey("CDRoleId")]
        public CDRole CDRole { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
