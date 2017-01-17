using PortalTemplate.Areas.CredAxis.Models.LicensesViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
   public  interface ILicensesService
    {
       //LicenseMainViewModel GetAllLicensesCode(int ProfileId);
       ////LicenseMainViewModel AddEditLicensesCode(LicenseMainViewModel licenseMainViewModel);   
       //LicenseMainViewModel AddEditStateLicense(StateLicense stateLicense, int ProfileId,int StateLicenseId);
       //FederalDea AddEditFederalDEA(FederalDea federalDEA, int ProfileId, int FederalDEAId);
       //MedicareInfo AddEditMedicareInfo(MedicareInfo medicareInfo, int ProfileId, int MedicareId);
       //MedicaidInfo AddEditMedicaidInfo(MedicaidInfo medicaidInfo, int ProfileId, int MedicaidId);
       //CdsInfo AddEditCDSInfo(CdsInfo cdsInfo, int ProfileId, int CDSId);
       //OtherIdentification AddEditOtherIdentification(OtherIdentification otherIdentification, int ProfileId, int OtherIdentificationId);
        LicenseMainViewModel GetAllLicensesCode();
        LicenseMainViewModel AddEditLicensesCode(LicenseMainViewModel licenseMainViewModel);

       
    }
}
