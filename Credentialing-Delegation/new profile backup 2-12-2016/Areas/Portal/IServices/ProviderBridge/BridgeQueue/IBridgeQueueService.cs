using PortalTemplate.Areas.Portal.Models.ProviderBridge.BridgeQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Portal.IServices.ProviderBridge.BridgeQueue
{
    public interface IBridgeQueueService
    {
        List<BridgeQueueViewModel> GetQueueData();
        List<BridgeQueueViewModel> GetApprovedQueueData();
        List<BridgeQueueViewModel> GetRejectedQueueData();
        //List<BridgeQueueViewModel> GetFilteredList(string filterType);

        // Get the List of Bridge Request of a Particular Type e.g Open, Assigned, Approved and Pending Request
        List<BridgeQueueViewModel> GetQueueDataByRequestType();

        //Get a Request from Bridge Request Provider Profile
        BridgeQueueViewModel GetRequestProfile(int  ProviderID);
        
        //Assign Request to Other Authorized Person for Approval of a Request
        //BridgeQueueViewModel AssignBridgeRequest(BridgeQueueViewModel Request);

        //Approve a BridgeRequest
        //BridgeQueueViewModel ApproveBridgeRequest(BridgeQueueViewModel Request);

        //Move a Approved Request into CredAxis Profile
        //BridgeQueueViewModel MoveApprovedRequestToCredAxisProfile(BridgeQueueViewModel Request);
    }
}
