using PortalTemplate.Areas.CredAxis.Models.LicensesViewModel;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.CredAxis.Services
{
    public class LicenseService : ILicensesService
    {
        private LicenseMainViewModel _LicenseCode;

        public LicenseService()
        {
            _LicenseCode = new LicenseMainViewModel();
        }

        public LicenseMainViewModel GetAllLicensesCode()
        {
            LicenseMainViewModel LicenseCodeDetails = new LicenseMainViewModel();

            try
            {
                string pathCDN, pathFederal, pathMedicaid, pathMedicare, pathOtherIden, pathState;
                //path = Path.Combine(HttpRuntime.AppDomainAppPath, "~/Areas/CredAxis/Resources/CredAxisJson/PersonalDetails.json");
                pathCDN = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Licences/CdnInfo.json");

                pathFederal = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Licences/FedaralDea.json");

                pathMedicaid = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Licences/Medicaidinfo.json");

                pathMedicare = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Licences/MedicareInfo.json");

                pathOtherIden = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Licences/OtherIdentification.json");

                pathState = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Licences/StateLicense.json");


                using (System.IO.TextReader reader = System.IO.File.OpenText(pathState))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<StateLicense> BirthData = new List<StateLicense>();
                    BirthData = serial.Deserialize<List<StateLicense>>(text);
                    foreach (var data in BirthData)
                    {
                        LicenseCodeDetails.stateLicense.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathFederal))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<FederalDea> BirthData = new List<FederalDea>();
                    BirthData = serial.Deserialize<List<FederalDea>>(text);
                    foreach (var data in BirthData)
                    {
                        LicenseCodeDetails.federalDEA.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathMedicaid))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<MedicaidInfo> CitizenData = new List<MedicaidInfo>();
                    CitizenData = serial.Deserialize<List<MedicaidInfo>>(text);
                    foreach (var data in CitizenData)
                    {
                        LicenseCodeDetails.mediciad.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathMedicare))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<MedicareInfo> ContactData = new List<MedicareInfo>();
                    ContactData = serial.Deserialize<List<MedicareInfo>>(text);
                    foreach (var data in ContactData)
                    {
                        LicenseCodeDetails.medicare.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathCDN))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<CdsInfo> HomeData = new List<CdsInfo>();
                    HomeData = serial.Deserialize<List<CdsInfo>>(text);
                    foreach (var data in HomeData)
                    {
                        LicenseCodeDetails.CDS.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathOtherIden))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<OtherIdentification> LangData = new List<OtherIdentification>();
                    LangData = serial.Deserialize<List<OtherIdentification>>(text);
                    foreach (var data in LangData)
                    {
                        LicenseCodeDetails.otherIdentification.Add(data);
                    }
                }

                return LicenseCodeDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LicenseMainViewModel AddEditLicensesCode(LicenseMainViewModel licenseMainViewModel)
        {
            return null;
        }

        public LicenseMainViewModel GetAllLicensesCode(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public LicenseMainViewModel AddEditStateLicense(StateLicense stateLicense, int ProfileId, int StateLicenseId)
        {
            throw new NotImplementedException();
        }

        public FederalDea AddEditFederalDEA(FederalDea federalDEA, int ProfileId, int FederalDEAId)
        {
            throw new NotImplementedException();
        }

        public MedicareInfo AddEditMedicareInfo(MedicareInfo medicareInfo, int ProfileId, int MedicareId)
        {
            throw new NotImplementedException();
        }

        public MedicaidInfo AddEditMedicaidInfo(MedicaidInfo medicaidInfo, int ProfileId, int MedicaidId)
        {
            throw new NotImplementedException();
        }

        public CdsInfo AddEditCDSInfo(CdsInfo cdsInfo, int ProfileId, int CDSId)
        {
            throw new NotImplementedException();
        }

        public OtherIdentification AddEditOtherIdentification(OtherIdentification otherIdentification, int ProfileId, int OtherIdentificationId)
        {
            throw new NotImplementedException();
        }
    }
}