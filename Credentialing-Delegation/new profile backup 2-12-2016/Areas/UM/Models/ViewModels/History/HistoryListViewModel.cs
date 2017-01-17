using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.History
{
    public class HistoryListViewModel
    {
        public string MemberID { get; set; }
        public List<MemberHistoriesViewModel> MemberHistoriesList { get; set; }
    }
}