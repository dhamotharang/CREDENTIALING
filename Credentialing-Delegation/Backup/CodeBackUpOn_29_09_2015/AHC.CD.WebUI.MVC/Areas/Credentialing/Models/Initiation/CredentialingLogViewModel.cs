using AHC.CD.Entities.Credentialing.AppointmentInformation;
using AHC.CD.Entities.Credentialing.PSVInformation;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Initiation
{
    public class CredentialingLogViewModel
    {
        public int CredentialingLogID { get; set; }

        public CredentialingType? CredentialingType { get; set; }
       
    }
}