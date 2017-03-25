using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface IProfileDataDuplicateManager
    {
        bool IsDLNumberDoesNotExists(string licenseNumber, int profileId = 0);
        bool IsSSNumberDoesNotExists(string ssNumber, int profileId = 0);
        bool IsVisaNumberDoesNotExists(string number, int profileId = 0);
        bool IsGreenCardNumberDoesNotExists(string number, int profileId = 0);
        bool IsNationalIDNumberDoesNotExists(string number, int profileId = 0);
        bool IsStateLicenseNumberDoesNotExists(string number, int stateLicenseInformationId = 0);
        bool IsFederalDEANumberDoesNotExists(string number, int federalDEAInformationId = 0);
        bool IsCDSCLicenseNumberDoesNotExists(string number, int cdscInformationId = 0);
        bool IsMedicareLicenseNumberDoesNotExists(string number, int medicareInformationId = 0);
        bool IsMedicaidLicenseNumberDoesNotExists(string number, int medicaidInformationId = 0);
        bool IsUPINNumberDoesNotExists(string number, int profileId = 0);
        bool IsUSMLENumberDoesNotExists(string number, int profileId = 0);        

        bool IsEmailAddressDoesNotExists(string emailAddress, int emailDetailID = 0);
        bool IsContactNumberDoesNotExists(string phoneNumber, int phoneDetailID = 0);
        bool IsNPINumberDoesNotExists(string number, int profileId = 0);
        bool IsNPIUsernameDoesNotExists(string number, int profileId = 0);
        bool IsCAQHNumberDoesNotExists(string number, int profileId = 0);
        bool IsCAQHUsernameDoesNotExists(string number, int profileId = 0);
        bool IsIndividualTaxIDDoesNotExists(string IndividualTaxId, int contractInfoID = 0);
    }
}
