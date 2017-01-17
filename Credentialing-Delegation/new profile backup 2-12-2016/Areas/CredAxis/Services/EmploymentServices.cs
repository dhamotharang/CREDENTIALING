using PortalTemplate.Areas.CredAxis.Models.EmploymentInformationVieModel;
using PortalTemplate.Areas.CredAxis.Models.ProviderProfileViewModel.EmploymentInformationVieModel;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.CredAxis.Services
{
    public class EmploymentServices : IEmploymentServices
    {
        private EmploymentInformationMainViewModel _EmploymentsCode;

        public EmploymentServices()
        {
            _EmploymentsCode = new EmploymentInformationMainViewModel();
        }

        public EmploymentInformationMainViewModel GetAllEmploymentInformations()
        {
            EmploymentInformationMainViewModel EmploymentDetails = new EmploymentInformationMainViewModel();

            try
            {
                string pathEmpInfo, pathGrpInfo;
                //path = Path.Combine(HttpRuntime.AppDomainAppPath, "~/Areas/CredAxis/Resources/CredAxisJson/PersonalDetails.json");
                pathEmpInfo = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/EmploymentInfo/EmploymentInfo.json");

                pathGrpInfo = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/EmploymentInfo/GroupInfo.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathEmpInfo))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    EmploymentInformationViewModel BirthData = new EmploymentInformationViewModel();
                    BirthData = serial.Deserialize<EmploymentInformationViewModel>(text);
                    EmploymentDetails.employmentInformation = BirthData;
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathGrpInfo))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<GroupInformationViewModel> CitizenData = new List<GroupInformationViewModel>();
                    CitizenData = serial.Deserialize<List<GroupInformationViewModel>>(text);
                    foreach (var data in CitizenData)
                    {
                        EmploymentDetails.groupInformation.Add(data);
                    }
                }


                return EmploymentDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmploymentInformationMainViewModel AddEditEmploymentInformations(EmploymentInformationMainViewModel employmentInformations)
        {
            return null;
        }
    }
}