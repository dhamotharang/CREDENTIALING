using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing
{
    public class Plan
    {
        public int PlanID
        {
            get;
            set;
        }

        [Required]
        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        [Required]
        [ForeignKey("InsuranceCompany")]
        public int InsuranceCompanyID { get; set; }

        public virtual InsuranceCompany InsuranceCompany { get; set; }
    }
}
