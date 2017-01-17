using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.DisclosureQuestions
{
    public class ProfileDisclosure
    {
        public ProfileDisclosure()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProfileDisclosureID { get; set; }

        public ICollection<ProfileDisclosureQuestionAnswer> ProfileDisclosureQuestionAnswers { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
