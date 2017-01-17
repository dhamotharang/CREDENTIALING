using PortalTemplate.Areas.UM.Models.DTO;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IMiscServices
    {
        List<UserDTO> GetBucketData(BucketDTO bucket);
        AuthorizationViewModel Refer(ReferDTO dto);
        AuthorizationViewModel SubmitAuth(ReferDTO dto);
        AuthorizationViewModel GetPreviewModal(AuthorizationViewModel auth, string action = "");
        void ConfirmationFromPlan(ReferDTO dto);
        void MoveWork(ReferDTO dto);
        void CTFUComplete(ReferDTO dto);
    }
}