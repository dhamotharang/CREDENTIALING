using AHC.CD.Business;
using AHC.CD.Business.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using AHC.UtilityService;

using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Hosting;
using AHC.CD.WebApiForMobileApp.Models;
using AHC.CD.Business.MasterData;
using System.Configuration;

namespace AHC.CD.WebApiForMobileApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
  [Authorize]
    public class MobileAppServiceController : ApiController
    {
        private readonly IProfileServiceManager profiles = null;

        private readonly IProfileManager profilemanager = null;

        private readonly IMasterDataManager MasterDataManager = null;

        public MobileAppServiceController(IProfileServiceManager profiles, IProfileManager profile,IMasterDataManager master)
        {
            this.profiles = profiles;
            this.profilemanager = profile;
            this.MasterDataManager = master;
        }

        [HttpOptions]
        [HttpGet]
        public object GetAllExpiriesForAProvider(int ProfileID)
        {
            var expiries = profiles.GetAllExpiriesForAProvider(ProfileID);
            return Json<object>(expiries);
            //return Json(new { expiries = expiries }, JsonRequestBehavior.AllowGet);
        }

        [HttpOptions]
        [HttpGet]
        public object GetAccountInfo(int ProfileID)
        {
            return Json<object>(profiles.GetAccountInfo(ProfileID));
        }
        [HttpOptions]
        [HttpGet]
        public object GetContractsForAprovider(int profileID)
        {
            return Json<object>(profiles.GetContractsForAprovider(profileID));
        }

        [HttpOptions]
        [HttpGet]
        public object GetProfileIDByEmail(string EmailID)
        {
            return Json<object>(profiles.GetProfileIDByEmail(EmailID));
        }
        [HttpOptions]
        [HttpGet]
        public object GetAllExpiryDatesForAProvider(int ProfileID)
        {
            return Json<object>(profiles.GetAllExpiryDates(ProfileID));
        }

        [HttpOptions]
        [HttpGet]
        public object GetCountOfPlansForAProvider(int ProfileID)
        {
            return Json<int>(profiles.GetCountOfPlansForAProvider(ProfileID));
        }
        [HttpOptions]
        [HttpGet]
        public object GetCountOfActiveContractsForaProvider(int ProfileID)
        {
            return Json<int>(profiles.GetCountOfActiveContractsForaProvider(ProfileID));
        }
        [HttpOptions]
        [HttpGet]
        public async Task<object> GetNotificationsForAProvider(int ProfileID)
        {
            int CDUserID = profiles.GetCDUserIDByProfileID(ProfileID);
            var result = await profilemanager.GetMyNotification(CDUserID);
            return Json<object>(result);


        }
        [HttpOptions]
        [HttpGet]
        public async Task<object> GetAllSpecialities()
        {
            var Specialities = await MasterDataManager.GetAllSpecialtyAsync();
            var SpecialityNames=Specialities.Select(x=>x.Name).ToList();
            return Json<List<string>>(SpecialityNames);
        }
        [HttpOptions]
        [HttpGet]
        public async Task<object> GetAllPlans()
        {
            var Plans = await MasterDataManager.GetAllPlanAsync();
            var PlanNames = Plans.Select(x => x.PlanName).ToList();
            return Json<List<string>>(PlanNames);
        }
        [HttpOptions]
        [HttpPost]        
        public HttpResponseMessage SaveAttestation()
        {
            //try
            //{
            //    //string SignatureBitCode1 = t.SignatureBitCode;
            //    //int ProfileID = t.ProfileID;
            //    HttpResponseMessage result = null;

            //    var httpRequest = HttpContext.Current.Request;
            //    string SignatureBitCode1 = httpRequest.QueryString["signature"];
            //     

            //    //Bitmap bmpReturn = null;
            //    //// string filename = UniqueKeyGenerator.GetUniqueKey() + "_CCMSignature.png";
            //    //string path = HostingEnvironment.MapPath("~/ApplicationDocuments/ProviderSignatureDocuments/" + ProfileID);
            //    //var base64Data = Regex.Match(SignatureBitCode1, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            //    //byte[] byteBuffer = Convert.FromBase64String(base64Data);
            //    //MemoryStream memoryStream = new MemoryStream(byteBuffer);

            //    //using (memoryStream)
            //    //{
            //    //    memoryStream.Position = 0;
            //    //    bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            //    //    bmpReturn.Save(path, ImageFormat.Png);
            //    //    memoryStream.Close();
            //    //}
            //   return Request.CreateResponse(HttpStatusCode.OK);
            //}
        
            //catch (Exception)
            //{

            //  return Request.CreateResponse(HttpStatusCode.NotAcceptable,);
            //}

            //catch (Exception)
            //{

            //    return false;
            //}
            //try
            //{
            //    HttpResponseMessage result = null;


            try
            {
                HttpResponseMessage result = null;

                var httpRequest = HttpContext.Current.Request;
                int ProfileID = int.Parse(httpRequest["ProfileID"].ToString());
                if (httpRequest.Files.Count > 0)
                {

                    var docfiles = new List<string>();

                    //int ProfileID = int.Parse(httpRequest.QueryString["ProfileID"]);
                    foreach (string file in httpRequest.Files)
                    {

                        var postedFile = httpRequest.Files[file];


                        var filePath = HttpContext.Current.Server.MapPath("~/ApplicationDocuments/ProviderSignatureDocuments/" + ProfileID + ".png");

                        postedFile.SaveAs(filePath);




                        docfiles.Add(filePath);

                    }

                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);

                }

                else
                {

                    result = Request.CreateResponse(HttpStatusCode.BadRequest);

                }

                return result;

            }
            catch (Exception)
            {
                
              return  Request.CreateResponse(HttpStatusCode.InternalServerError);
            }



            //    if (httpRequest.Files.Count > 0)
            //    {

            //        var docfiles = new List<string>();

            //        foreach (string file in httpRequest.Files)
            //        {

            //            var postedFile = httpRequest.Files[file];

            //            var filePath = HttpContext.Current.Server.MapPath("~/" + id);

            //            postedFile.SaveAs(filePath);



            //            docfiles.Add(filePath);

            //        }

            //        result = Request.CreateResponse(HttpStatusCode.Created, docfiles);

            //    }

            //    else
            //    {

            //        result = Request.CreateResponse(HttpStatusCode.BadRequest);

            //    }

            //    return result;
            //}
            
                                                     







        }
        [HttpOptions]
        [HttpPost]
            public HttpResponseMessage UploadDocuments()
        {
            try
            {
                HttpResponseMessage result = null;

                var httpRequest = HttpContext.Current.Request;
                int ProfileID = int.Parse(httpRequest["ProfileID"].ToString());
                string DocumentType = httpRequest["DocumentType"].ToString();
                //int ProfileID = 104;
                //string DocumentType = "Suhas1";
                if (httpRequest.Files.Count > 0)
                {

                    var docfiles = new List<string>();

                   
                    foreach (string file in httpRequest.Files)
                    {
                       
                        var postedFile = httpRequest.Files[file];


                     var filePath = HttpContext.Current.Server.MapPath("~/ApplicationDocuments/ProviderDocuments/" + DocumentType+"/"+ProfileID+"/");
                      //  var filePath = HttpContext.Current.Server.MapPath("~/AppFolder/" +  ProfileID + "/" + postedFile.FileName);
                        DirectoryInfo d = new DirectoryInfo(filePath);
                        if (!d.Exists)
                        {
                            d.Create();
                        }
                        string FileName = UniqueKeyGenerator.GetUniqueKey() + ".png";
                        string Path = System.IO.Path.Combine(filePath, FileName);
                        postedFile.SaveAs(Path);
                        docfiles.Add(filePath);
                    }

                    result = Request.CreateResponse(HttpStatusCode.Created, "File Saved Succesfully");
                  
                }

                else
                {

                    result = Request.CreateResponse(HttpStatusCode.BadRequest);

                }

                return result;

            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        [HttpOptions]
        public object GetDocumentTypes()
        {
            List<string> DocumentTypes = ConfigurationManager.AppSettings["Documents"].Split(',').OrderBy(x => x).ToList();
            return Json<List<string>>(DocumentTypes);
        }
    }
}
