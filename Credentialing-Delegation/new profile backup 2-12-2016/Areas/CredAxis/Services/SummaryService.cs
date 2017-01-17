using PortalTemplate.Areas.CredAxis.Models.SummaryViewModel;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.CredAxis.Services
{
    internal class SummaryService : ISummaryService
    {
        private SummaryMainViewModel _SummaryCode;

        public SummaryService()
        {
            _SummaryCode = new SummaryMainViewModel();
        }
        SummaryMainViewModel ISummaryService.GetSummary()
        {


            SummaryMainViewModel SummaryDeatils = new SummaryMainViewModel();

            try
            {
                string pathProfileStatus, pathDataDoc,pathRecentTask, pathCredDecred,pathHospitals,pathAssociatedPlans;
                //path = Path.Combine(HttpRuntime.AppDomainAppPath, "~/Areas/CredAxis/Resources/CredAxisJson/PersonalDetails.json");
                pathProfileStatus = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Summary/ProfileStatus.json");
      
                pathDataDoc = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Summary/DataDocumentStatus.json");

                pathRecentTask = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Summary/RecentTasksPerformed.json");

                pathCredDecred = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Summary/CredentialingDetails.json");

                pathHospitals = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Summary/Hospitals.json");

                pathAssociatedPlans = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Summary/GroupIPA.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathProfileStatus))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    SummaryDeatils.ProfileStatus = serial.Deserialize<ProfileStatusViewModel>(text);
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathDataDoc))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<DataDocumentViewModel> datadocu = new List<DataDocumentViewModel>();
                    datadocu = serial.Deserialize<List<DataDocumentViewModel>>(text);
                    foreach (var data in datadocu)
                    {
                        SummaryDeatils.DataDocumentStatus.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathRecentTask))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<RecentTasksViewModel> RecentTasks = new List<RecentTasksViewModel>();
                    RecentTasks = serial.Deserialize<List<RecentTasksViewModel>>(text);
                    foreach (var data in RecentTasks)
                    {
                        SummaryDeatils.RecentActivities.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathCredDecred))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<CredentialingDetailsViewModel> CredentialingDetails = new List<CredentialingDetailsViewModel>();
                    var Res = serial.Deserialize<List<CredentialingDetailsViewModel>>(text);
                    //CredentialingDetails = serial.Deserialize<List<CredentialingDetailsViewModel>>(text).ToList();
                    foreach (var data in Res)
                    {

                        SummaryDeatils.CredentailingDetails.Add(data);
                    }
                }
                using (System.IO.TextReader reader = System.IO.File.OpenText(pathHospitals))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<HospitalViewModel> CredentialingDetails = new List<HospitalViewModel>();
                    var Hospitals = serial.Deserialize<List<HospitalViewModel>>(text);
                    //string json = text;
                    //var Hospitals = new JavaScriptSerializer().Deserialize<dynamic>(json);
                    foreach (var item in Hospitals)
                    {
                        SummaryDeatils.Hospitals.Add(item);
                    }
                }
                using (System.IO.TextReader reader = System.IO.File.OpenText(pathAssociatedPlans))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<GroupIpaViewModel> AssociatedPlans = new List<GroupIpaViewModel>();
                    AssociatedPlans = serial.Deserialize<List<GroupIpaViewModel>>(text);
                    foreach (var data in AssociatedPlans)
                    {
                        SummaryDeatils.AssoicateGroupPlans.Add(data);
                    }
                }
                return SummaryDeatils;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
    }
}