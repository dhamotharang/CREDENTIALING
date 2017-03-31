using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities
{
    public class CDUserRelation
    {
        public CDUserRelation()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CDUserRelationID { get; set; }

        public virtual ICollection<CDUserRoleRelation> UserRoleRelations { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
