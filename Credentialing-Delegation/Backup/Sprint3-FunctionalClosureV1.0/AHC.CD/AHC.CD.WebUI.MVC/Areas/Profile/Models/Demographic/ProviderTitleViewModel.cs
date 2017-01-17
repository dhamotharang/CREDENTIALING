using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class ProviderTitleViewModel
    {

        public ProviderTitleViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProviderTitleID { get; set; }

        [Required]
        public int ProviderTypeId { get; set; }

        public ProviderType ProviderType { get; set; }

        #region Status

        public string Status
        {
            get
            {
                return this.StatusType.ToString();
            }
            private set
            {
                this.StatusType = (StatusType)Enum.Parse(typeof(StatusType), value);
            }
        }

        [Required]
        public StatusType StatusType { get; set; }

        #endregion 

        public DateTime LastModifiedDate { get; set; }

        
        public ICollection<Specialty> Specialities { get; set; }
        public ICollection<SpecialtyBoard> SpecialtyBoards { get; set; }
    }
}