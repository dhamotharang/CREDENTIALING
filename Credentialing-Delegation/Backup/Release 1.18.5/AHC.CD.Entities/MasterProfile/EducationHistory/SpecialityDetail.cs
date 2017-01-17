using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.EducationHistory
{
    public class SpecialityDetail
    {
        public int SpecialityDetailID { get; set; }
        [Required]
        public SpecialityType SpecialityType { get; set; }
        [Required]
        public string SpecialityCode { get; set; }
        public BoardDetail BoardDetail { get; set; }

        public bool ListedInHMO { get; set; }
        public bool ListedInPPO { get; set; }
        public bool ListedInPOS { get; set; }
    }

    public enum SpecialityType
    {
        Primary=1,
        Secondary
    }
}
