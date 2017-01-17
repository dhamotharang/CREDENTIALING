using PortalTemplate.Areas.Portal.IServices.Queues;
using PortalTemplate.Areas.Portal.Models.PriorAuth.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.Portal.Services.PriorAuth.Queues
{
    public class QueueServices : IQueueServices
    {
        public PortalQueueViewModel GetAuthList(string QueueType, string QueueSubTab, string UserID = null, string RequestType = "", int? StartIndex = null, int? DataLength = null, List<string> filterData = null, string from = null, string to = null)
        {
            PortalQueueViewModel QueueModule = new PortalQueueViewModel();
            QueueModule.AuthorizationsList = GetQueueData(QueueType, QueueSubTab);
            QueueModule.AuthorizationsCount = GetCount(QueueModule.AuthorizationsList);
            QueueModule.QueueSubTab = GetSubTabView(QueueType, "");
            if (RequestType != "")
            {
                QueueModule.AuthorizationsList = GetFilterDataOnRequest(QueueModule.AuthorizationsList, RequestType);
            }
            return QueueModule;
        }

        private List<QueueViewModel> GetQueueData(string QueueType, string QueueTab)
        {
            string file = HostingEnvironment.MapPath(GetResourceLink(QueueType, QueueTab));
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<QueueViewModel> QueueModal = new List<QueueViewModel>();
            QueueModal = serial.Deserialize<List<QueueViewModel>>(json);
            return QueueModal;
        }
        private QueueCountViewModel GetCount(List<QueueViewModel> QueueModel)
        {
            QueueCountViewModel CountObject = new QueueCountViewModel(42);
            CountObject.TotalCount = QueueModel.Count();
            CountObject.StandardCount = QueueModel.Count(e => e.RequestType.ToLower() == "standard");
            CountObject.ExpeditedCount = QueueModel.Count(e => e.RequestType.ToLower() == "expedited");
            return CountObject;
        }

        private QueueSubTabViewModel GetSubTabView(string QueueType, string Role, params string[] args)
        {
            QueueSubTabViewModel CountObject = new QueueSubTabViewModel(false);
            if (QueueType != null)
            {
                int yo = QueueType.CompareTo("OWN");
                if (QueueType.ToUpperInvariant().Equals("OWN") || QueueType.ToUpperInvariant().Equals("GLOBAL"))
                {
                    CountObject = new QueueSubTabViewModel(true);
                }
                if (QueueType.ToUpperInvariant().Equals("PAC") || QueueType.ToUpperInvariant().Equals("CD"))
                {
                    CountObject = new QueueSubTabViewModel(true);
                    CountObject.IsCompletedTab = false;
                    CountObject.IsCTFUTab = false;
                }
                if (QueueType.ToUpperInvariant().Equals("PCP"))
                {

                    CountObject = new QueueSubTabViewModel(true);
                    CountObject.IsSubmittedTab = false;
                    CountObject.IsCTFUTab = false;

                }

            }
            return CountObject;
        }

        private List<QueueViewModel> GetFilterDataOnRequest(List<QueueViewModel> QueueModel, string RequestType)
        {
            return QueueModel.FindAll(e => e.RequestType.ToLower() == RequestType.ToLowerInvariant());
        }

        private string GetResourceLink(string QueueType, string QueueTab)
        {
            return "~/Areas/UM/Resources/JSONData/Queue/jsonForIntakeQueue.txt";
        }
    }
}