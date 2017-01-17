using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing
{
    public class InsuranceCompany
    {
        public int InsuranceCompanyID
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

        public byte[] Logo
        {
            get;
            set;
        }

        public virtual ICollection<Plan> Plans
        {
            get;
            set;
        }
    }
}
