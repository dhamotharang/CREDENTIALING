using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web.Script.Serialization;
using PortalTemplate.Areas.UM.Models.ViewModels.Queue;
using PortalTemplate.Areas.UM.Models.MasterDataEntities;


namespace PortalTemplate.Areas.UM.Services
{
    public class UMServices
    {
        public List<AuthorizationTypeViewModel> GetAuthorizationTypes()
        {
            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/MasterData/MasterAuthorizationTypes.js");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<AuthorizationTypeViewModel> AuthorizationTypes = new List<AuthorizationTypeViewModel>();
            AuthorizationTypes = serial.Deserialize<List<AuthorizationTypeViewModel>>(json);
            return AuthorizationTypes;
        }

        public List<RequestTypeViewModel> GetRequestTypes()
        {
            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/MasterData/MasterRequestTypes.js");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<RequestTypeViewModel> RequestTypes = new List<RequestTypeViewModel>();
            RequestTypes = serial.Deserialize<List<RequestTypeViewModel>>(json);
            return RequestTypes;
        }
    }
}