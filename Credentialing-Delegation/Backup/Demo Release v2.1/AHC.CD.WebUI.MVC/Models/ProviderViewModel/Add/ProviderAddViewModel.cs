using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Add
{
    public class ProviderAddViewModel
    {

        public ProviderAddressViewModel Address { get; set; }

        public ProviderPersonalInfoViewModel Personal { get; set; }

        //public ProviderContractInfoViewModel ContractInfo { get; set; }

        public bool IsPartOfGroup
        {
            get;
            set;
        }

        public int ProviderTypeId
        {
            get;
            set;
        }



        public ProviderRelation ProviderRelation
        {
            get;
            set;
        }

        public int GroupID
        {
            get;
            set;
        }

        public string Remarks
        {
            get;
            set;
        }

        public HttpPostedFileBase ProviderDoc { get; set; } 
    }
}