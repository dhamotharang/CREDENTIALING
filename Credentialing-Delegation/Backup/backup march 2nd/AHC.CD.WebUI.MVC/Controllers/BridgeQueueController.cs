using AHC.CD.WebUI.MVC.Models.BridgeQueue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class BridgeQueueController : Controller
    {
        //
        // GET: /BridgeQueue/
        public ActionResult Index()
        {
            return View();
        }
        public string GetData()
        {
            string pathECFMG, text;
            BridgeQueueModel TheModel = new BridgeQueueModel();
            pathECFMG = HostingEnvironment.MapPath("~/Scripts/Custom/BridgeQueue/BridgeQueueJSON.json");
            using (System.IO.TextReader reader = System.IO.File.OpenText(pathECFMG))
            {
                text = reader.ReadToEnd();
                //JavaScriptSerializer serial = new JavaScriptSerializer();
                //TheModel = serial.Deserialize<BridgeQueueModel>(text);
            }
            return text;
        }

        public string GetTempProfile()
        {
            string path, text;
            TempProfileViewModel TheModel = new TempProfileViewModel();
            path = HostingEnvironment.MapPath("~/Scripts/Custom/BridgeQueue/TempProfile.json");
            using (System.IO.TextReader reader = System.IO.File.OpenText(path))
            {
                text = reader.ReadToEnd();
              
            }
            return text;

        }

        //public TempProfileViewModel GetTempProfile()
        //{
        //    string path, text;
        //    TempProfileViewModel obj = Activator.CreateInstance<TempProfileViewModel>();
        //    path = HostingEnvironment.MapPath("~/Scripts/Custom/BridgeQueue/TempProfile.json");
        //    using (System.IO.TextReader reader = System.IO.File.OpenText(path))
        //    {
        //        text = reader.ReadToEnd();
               
        //    }
        //    MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(text));
        //    DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
        //    obj = (TempProfileViewModel)serializer.ReadObject(ms);
        //    ms.Close();
        //    return obj;
        //}
    }
}