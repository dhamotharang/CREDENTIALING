using Newtonsoft.Json;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.ViewModels.ViewAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.Services
{
    public class ViewAuthService : IViewAuthServices
    {
        public ViewAuthorizationViewModel GetAuthByID(int ID)
        {
            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/JSONData/Authorization/ViewAuth.txt");
            var json = System.IO.File.ReadAllText(file);
            ViewAuthorizationViewModel ViewAuthModal = new ViewAuthorizationViewModel();
            ViewAuthModal = JsonConvert.DeserializeObject<ViewAuthorizationViewModel>(json);
            return ViewAuthModal;
        }

       public ViewAuthorizationViewModel ConvertAuth(ViewAuthorizationViewModel auth)
       {
           throw new NotImplementedException();
       }

       public DateTime UpdateDischargeDate(int AuthID, DateTime DischargeDate)
       {
           throw new NotImplementedException();
       }



       public ViewAuthorizationViewModel GetTabData(string TabId, int AuthID)
       {
           throw new NotImplementedException();
       }

       internal ViewAuthorizationViewModel GetAllContactsServices(int AuthID)
       {
           throw new NotImplementedException();
       }
    }
}