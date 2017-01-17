using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PartnersAssociatesCoveringColleagues
    {
        public int PartnersAssociatesCoveringColleaguesID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string SpecialtyCode { get; set; }
        
        public bool CoveringColleague  { get; set; }
        public bool AssociatesPartners { get; set; }
        //public AssociateType AssociateType { get; set; }

        [Required]
        public virtual string Title { get; private set; }

        //[NotMapped]
        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Select a Title!!")]
        //[Display(Name = "Title *")]
        //public virtual TitleType TitleType
        //{
        //    get
        //    {
        //        return (TitleType)Enum.Parse(typeof(TitleType), this.Title);
        //    }
        //    set
        //    {
        //        this.Title = value.ToString();
        //    }
        //}


    }

    //public enum AssociateType
    //{
    //    Partner = 1,
    //    Associate,
    //    CoveringColleage
    //}
}
