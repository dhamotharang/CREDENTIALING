using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.SummaryViewModel
{
    public class SummaryMainViewModel
    {
        public SummaryMainViewModel()
        {
            ProfileStatus = new ProfileStatusViewModel();
            DataDocumentStatus = new List<DataDocumentViewModel>();
            RecentActivities = new List<RecentTasksViewModel>();
            CredentailingDetails = new List<CredentialingDetailsViewModel>();
            Hospitals = new List<HospitalViewModel>();
            AssoicateGroupPlans = new List<GroupIpaViewModel>();
        }

        public ProfileStatusViewModel ProfileStatus { get; set; }
        public List<DataDocumentViewModel> DataDocumentStatus { get; set; }
        public List<RecentTasksViewModel> RecentActivities { get; set; }
        public List<CredentialingDetailsViewModel> CredentailingDetails { get; set; }
        public List<HospitalViewModel> Hospitals { get; set; }
        public List<GroupIpaViewModel> AssoicateGroupPlans { get; set; }

    }
}