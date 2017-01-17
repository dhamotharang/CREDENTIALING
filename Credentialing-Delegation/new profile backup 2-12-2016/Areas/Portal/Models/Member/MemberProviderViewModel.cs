using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberProviderViewModel
    {
        [Display(Name = "PhysicianNameMBRProvider", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhysicianName { get; set; }

        [Display(Name = "PhysicianFirstNameMBRProvider", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhysicianFirstName { get; set; }

        [Display(Name = "PhysicianMiddleNameMBRProvider", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhysicianMiddleName { get; set; }

        [Display(Name = "PhysicianLastNameMBRProvider", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhysicianLastName { get; set; }

        [Display(Name = "NPIMBRProvider", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string NPI { get; set; }

        [Display(Name = "AddressMBRProvider", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Address { get; set; }


        [Display(Name = "CenterNoMBRProvider", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string CenterNO { get; set; }

        [Display(Name = "IsPcpMBRProvider", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ISPCP { get; set; }

        [Display(Name = "EffectiveFromMBRProvider", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EffectiveFrom { get; set; }

        [Display(Name = "EffectiveToMBRProvider", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EffectiveTo { get; set; }

        [Display(Name = "PhoneMBRProvider", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Phone { get; set; }

    

        
    }
}

