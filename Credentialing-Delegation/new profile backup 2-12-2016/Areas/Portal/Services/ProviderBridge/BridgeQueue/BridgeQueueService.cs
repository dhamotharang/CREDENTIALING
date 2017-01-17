using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.Portal.IServices.ProviderBridge.BridgeQueue;
using PortalTemplate.Areas.Portal.Models.ProviderBridge.BridgeQueue;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PortalTemplate.Areas.Portal.Services.ProviderBridge.BridgeQueue
{
    public class BridgeQueueService : IBridgeQueueService
    {
        private BridgeQueueViewModel _BridgeQueueData;

        public List<BridgeQueueViewModel> BridgeQueueData = new List<BridgeQueueViewModel>();
        public List<BridgeQueueViewModel> ApprovedBridgeQueueData = new List<BridgeQueueViewModel>();
        public List<BridgeQueueViewModel> RejectedBridgeQueueData = new List<BridgeQueueViewModel>();

        public BridgeQueueService()
        {
            _BridgeQueueData = new BridgeQueueViewModel();
            BridgeQueueData.Add(new BridgeQueueViewModel { 
                NPINumber= "9984870574",
                LastName= "Smith",
                FirstName= "John",
                ReqBy= "B JOY",
                Gender= "male",
                PhoneNumber= "(835) 511-2275",
                Module= "UM-Create New Auth",
                ReqDate= "06-10-2015",
                Request= "Standard",
                Address= "629 Lewis Place",
                City= "Tampa",
                State= "FL",
                Status= "Rejected",
	            TimeLeft="5",
	            Timer="65:33:14",
	            FilterType="Pending",
	            AssignedTo="Catherine",
	            NetworkParticipation="OON",
	            QueueStatus="Open"});
            BridgeQueueData.Add(new BridgeQueueViewModel {
                NPINumber= "9679222494",
                LastName= "Jones",
                FirstName= "Sarah",
                ReqBy= "Sheila",
                Gender= "male",
                PhoneNumber= "(905) 474-2812",
                Module= "Billing",
                ReqDate= "02-12-2014",
                Request= "Standard",
                Address= "772 Mayfair Drive",
                City= "Tampa",
                State= "FL",
                Status= "Rejected",
	            TimeLeft="8",
	            Timer="68:25:12",
	            FilterType="Pending",
	            AssignedTo="Catherine",
	            NetworkParticipation="OON",
	            QueueStatus="Open"
            });

            ApprovedBridgeQueueData.Add(new BridgeQueueViewModel { 
                NPINumber= "1116191884",
                LastName= "BARTLINSKI",
                FirstName= "PAUL",
                ReqBy= "Sheila",
                Gender= "male",
                PhoneNumber= "(931) 502-3084",
                Module= "Billing",
                ReqDate= "01-20-2015",
	            AppDate="01-23-2015",
	            AppBy="CATHERINE",
                Address= "541 Pleasant Place",
                City= "Tampa",
                State= "FL",
                Status= "Approved",
	            Request= "Standard",
	            NetworkParticipation="OON"
            });
            ApprovedBridgeQueueData.Add(new BridgeQueueViewModel
            {
                NPINumber= "5252890974",
                LastName= "JOSE",
                FirstName= "VILLAPLANA",
                ReqBy= "B JOY",
                Gender= "male",
                PhoneNumber= "(948) 498-3216",
                Module= "UM-CreateNewAuth",
                ReqDate= "02-03-2015",
	            AppDate="02-06-2015",
	            AppBy="JOCYLENE",
                Address= "335 Grace Court",
                City= "Tampa",
                State= "FL",
                Status= "Approved",
	            Request= "Standard",
	            NetworkParticipation="OON"
            });

            RejectedBridgeQueueData.Add(new BridgeQueueViewModel
            {
                NPINumber= "1356369227",
                LastName= "CAMP",
                FirstName= "JULIE",
                ReqBy= "B JOY",
                Gender= "female",
                PhoneNumber= "(869) 453-2098",
                Module= "UM-CreateNewAuth",
                ReqDate= "06-23-2014",
                RejDate= "06-25-2014",
                Address= "155 Crescent Street",
                City= "Tampa",
                State= "Florida",
	            Request= "Expedite",
                Status= "Rejected",
	            AssignedBy="Sherry",
	            TimeLeft="12"
            });
            RejectedBridgeQueueData.Add(new BridgeQueueViewModel
            {
                NPINumber= "1257336534",
                LastName= "GUNTER",
                FirstName= "TARA",
                ReqBy= "B JOY",
                Gender= "female",
                PhoneNumber= "(907) 586-3730",
                Module= "UM-CreateNewAuth",
                ReqDate= "09-25-2016",
                RejDate= "10-05-2016",
                Address= "537 Rochester Avenue",
                City= "Tampa",
                State= "Florida",
	            Request= "Standard",
                Status= "Rejected",
	            AssignedBy="Sherry",
	            TimeLeft="14"
            });
        }
        public List<BridgeQueueViewModel> GetQueueData()
        {
            //List<BridgeQueueViewModel> BridgeQueueData = new List<BridgeQueueViewModel>();
            //string queueData;
            try
            {
                //queueData = HostingEnvironment.MapPath("~/Areas/Portal/Resources/BridgeQueue/BridgeQueue.json");

                //using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                //{
                //    string text = reader.ReadToEnd();
                //    JavaScriptSerializer serial = new JavaScriptSerializer();
                //    BridgeQueueData = serial.Deserialize<List<BridgeQueueViewModel>>(text);
                //}
                return BridgeQueueData;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public List<BridgeQueueViewModel> GetApprovedQueueData()
        {
            //List<BridgeQueueViewModel> BridgeQueueData = new List<BridgeQueueViewModel>();
            //string queueData;
            try
            {
                //queueData = HostingEnvironment.MapPath("~/Areas/Portal/Resources/BridgeQueue/ApproveBridgeQueue.json");

                //using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                //{
                //    string text = reader.ReadToEnd();
                //    JavaScriptSerializer serial = new JavaScriptSerializer();
                //    ApprovedBridgeQueueData = serial.Deserialize<List<BridgeQueueViewModel>>(text);
                //}
                return ApprovedBridgeQueueData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<BridgeQueueViewModel> GetRejectedQueueData()
        {
            //List<BridgeQueueViewModel> BridgeQueueData = new List<BridgeQueueViewModel>();
            //string queueData;
            try
            {
                //queueData = HostingEnvironment.MapPath("~/Areas/Portal/Resources/BridgeQueue/RejectedBridgeQueue.json");

                //using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                //{
                //    string text = reader.ReadToEnd();
                //    JavaScriptSerializer serial = new JavaScriptSerializer();
                //    RejectedBridgeQueueData = serial.Deserialize<List<BridgeQueueViewModel>>(text);
                //}
                return RejectedBridgeQueueData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #region Bridge Queue Abstractions
        /// <summary>
        /// Get the List of Bridge Request of a Particular Type e.g Open, Assigned, Approved and Pending Request
        /// </summary>
        /// <param name="RequestType"></param>
        /// <returns></returns>
        public List<BridgeQueueViewModel> GetQueueDataByRequestType()
        {
            try
            {
                var result= Task.Run(() => ServiceRepository.GetDataFromService<List<BridgeQueueViewModel>>("api/BridgeQueue/GetQueueByStatus", "PVProviderServiceWebAPIURL")).Result;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get a Request from Bridge Request Provider Profile
        /// </summary>
        /// <param name="ProviderID"></param>
        /// <returns></returns>
        public BridgeQueueViewModel GetRequestProfile(int ProviderID)
        {
            if (ProviderID == 0)
            {
                return null;
            }
            else
            {
                var result = Task.Run(() => ServiceRepository.GetDataFromService<BridgeQueueViewModel>("api/ProviderService/GetRequestProfile?ProviderID=" + ProviderID + "", "PVProviderServiceWebAPIURL")).Result;
                return result;
            }
        }

        #endregion

    }
}