using System;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CMS.Models
{
    public class CommonPropViewModel
    {
        public CommonPropViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        [Display(Name = "Code")]
        public string Code { get; set; }

        #region status

        [Display(Name = "Status")]
        public bool Status { get; set; }
        
        //public StatusType StatusType
        //{
        //    get
        //    {
        //        return Status ? StatusType.ACTIVE : StatusType.INACTIVE;
        //    }
        //    set
        //    {
        //        this.Status = value.Equals(StatusType.ACTIVE) ? true : false;

        //    }
        //}

        #endregion
            
        public string Source { get; set; }
        
        public string CreatedBy { get; set; }
        
        //public DateTime CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime LastModifiedDate { get; set; }        
    }
}