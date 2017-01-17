
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.CM.Models.Queue;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.CM.Services
{

    // -----Method to get the data and push it to QueueView Model-------
    public class CMQueueServices
    {
       List<QueueViewModel> QueueModel = new List<QueueViewModel>();
       public List<QueueViewModel> GetQueueData(string QueueType, string QueueTab)
        {
            string file = "";
            if (QueueTab == "UserQueueTabs")
            {
                 file = HostingEnvironment.MapPath(GetResourceLink(QueueType, QueueTab));
               
            }
            else
            {
                 file = HostingEnvironment.MapPath(GetResourceLink(QueueType, QueueTab));
               
            }
           // -----Read The Data From the Text File------
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
           // ---Data In Json Format----
            QueueModel = serial.Deserialize<List<QueueViewModel>>(json);
            return QueueModel; 
        }

        //  ----Method to get the text file for time being-----------
       private string GetResourceLink(string QueueType, string QueueTab)
       {
           if (QueueTab == "UserQueueTabs")
               return "~/Areas/CM/Resources/JSONData/queueData.txt";
           else
               return "~/Areas/CM/Resources/JSONData/SystemIdentifiedQueueData.txt";
       }
     
    }
}