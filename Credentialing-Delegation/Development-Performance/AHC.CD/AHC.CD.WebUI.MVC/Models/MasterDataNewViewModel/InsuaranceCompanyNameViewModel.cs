using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class InsuaranceCompanyNameViewModel
    {
        public int InsuaranceCompanyNameID { get; set; }


        public string CompanyName { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        //[Column(TypeName = "datetime2")]
        //public DateTime LastModifiedDate { get; set; }
    }
}