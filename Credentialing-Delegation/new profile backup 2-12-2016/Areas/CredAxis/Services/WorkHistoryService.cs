using PortalTemplate.Areas.CredAxis.Models.WorkHistoryViewModel;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using PortalTemplate.Areas.CredAxisProduct.Models.ProviderProfileViewModel.WorkHistoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.CredAxis.Services
{
    public class WorkHistoryService : IWorkHistoryService
    {
        private WorkHistoryMainViewModel _WorkHistory;

        public WorkHistoryService()
        {
            _WorkHistory = new WorkHistoryMainViewModel();
        }

        public WorkHistoryMainViewModel GetAllWorkHistory()
        {
            WorkHistoryMainViewModel WorkHistoryDetails = new WorkHistoryMainViewModel();

            try
            {
                string pathMilServ, pathWorkGap, pathWorkExp, pathPubHealth;
                //path = Path.Combine(HttpRuntime.AppDomainAppPath, "~/Areas/CredAxis/Resources/CredAxisJson/PersonalDetails.json");
                pathMilServ = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/WorkHistory/MilitaryService.json");

                pathWorkGap = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/WorkHistory/WorkGap.json");

                pathWorkExp = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/WorkHistory/ProfessionalWorkExperience.json");

                pathPubHealth = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/WorkHistory/PublicHealthService.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathMilServ))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<MilitaryServiceViewModel> BirthData = new List<MilitaryServiceViewModel>();
                    BirthData = serial.Deserialize<List<MilitaryServiceViewModel>>(text);
                    foreach (var data in BirthData)
                    {
                        WorkHistoryDetails.militaryService.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathWorkGap))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<WorkGapViewModel> CitizenData = new List<WorkGapViewModel>();
                    CitizenData = serial.Deserialize<List<WorkGapViewModel>>(text);
                    foreach (var data in CitizenData)
                    {
                        WorkHistoryDetails.workGap.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathWorkExp))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<ProfessionalWorkExperienceViewModel> CitizenData = new List<ProfessionalWorkExperienceViewModel>();
                    CitizenData = serial.Deserialize<List<ProfessionalWorkExperienceViewModel>>(text);
                    foreach (var data in CitizenData)
                    {
                        WorkHistoryDetails.professionalWorkExperience.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathPubHealth))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<PublicHealthServicesViewModel> CitizenData = new List<PublicHealthServicesViewModel>();
                    CitizenData = serial.Deserialize<List<PublicHealthServicesViewModel>>(text);
                    foreach (var data in CitizenData)
                    {
                        WorkHistoryDetails.publicHealthService.Add(data);
                    }
                }

                return WorkHistoryDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WorkHistoryMainViewModel AddEditWorkHistory(WorkHistoryMainViewModel workHistoryMainViewModel)
        {
            return null;
        }

        public WorkHistoryMainViewModel GetAllWorkHistory(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public ProfessionalWorkExperienceViewModel AddEditWorkHistory(ProfessionalWorkExperienceViewModel workHistoryMainViewModel, int ProfileId, int workHistoryId)
        {
            throw new NotImplementedException();
        }

        public MilitaryServiceViewModel AddEditMilitaryService(MilitaryServiceViewModel militaryServiceViewModel, int ProfileId, int MilitaryServiceId)
        {
            throw new NotImplementedException();
        }

        public PublicHealthServicesViewModel AddEditPublicHealthService(PublicHealthServicesViewModel publicHealthServiceViewModel, int ProfileId, int PublicHealthServiceId)
        {
            throw new NotImplementedException();
        }

        public WorkGapViewModel AddEditWorkGap(WorkGapViewModel workGapViewModel, int ProfileId, int WorkGapId)
        {
            throw new NotImplementedException();
        }
    }
}