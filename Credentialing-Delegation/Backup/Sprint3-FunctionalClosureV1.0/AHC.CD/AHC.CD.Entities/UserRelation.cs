using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities
{
    public class UserRelation
    {
        public UserRelation()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int UserRelationID { get; set; }

        public virtual ICollection<UserRoleRelation> UserRoleRelations { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
