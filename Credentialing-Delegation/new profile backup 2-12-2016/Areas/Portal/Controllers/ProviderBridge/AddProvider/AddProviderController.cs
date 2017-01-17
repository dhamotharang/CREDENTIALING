using PortalTemplate.Areas.Portal.Models.ProviderBridge.AddProvider;
using PortalTemplate.Areas.Portal.Models.ProviderBridge.BridgeQueue;
using PortalTemplate.Areas.Portal.Services.ProviderBridge.AddProvider;
using PortalTemplate.Areas.Portal.Services.ProviderBridge.BridgeQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.ProviderBridge
{
    public class AddProviderController : Controller
    {
        ProviderService providerService;
        BridgeQueueService bridgeQueueService;
        public AddProviderController()
        {
            providerService = new ProviderService();
            bridgeQueueService = new BridgeQueueService();
        }
        //
        // GET: /Portal/AddProvider/
        public ActionResult Index()
        {

            return PartialView("~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/_ProviderBody.cshtml");
        }
        public ActionResult AddNewProvider(AddProviderViewModel provider)
        {
            try
            {
                List<BridgeQueueViewModel> BridgeQueueData = new List<BridgeQueueViewModel>();

                var p = providerService.AddProvider(provider);
                BridgeQueueData = bridgeQueueService.GetQueueDataByRequestType();
                return PartialView("~/Areas/Portal/Views/ProviderBridge/BridgeQueue/_BridgeQueue.cshtml", BridgeQueueData);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

	}
}