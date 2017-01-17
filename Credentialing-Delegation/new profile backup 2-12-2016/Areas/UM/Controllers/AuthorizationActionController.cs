using Newtonsoft.Json;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.IServices.Authorization;
using PortalTemplate.Areas.UM.Models.Enums;
using PortalTemplate.Areas.UM.Models.PowerDriveService;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using PortalTemplate.Areas.UM.Models.ViewModels.QueueBucket;
using PortalTemplate.Areas.UM.Services;
using PortalTemplate.Areas.UM.Services.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class AuthorizationActionController : Controller
    {
        IAuthorizationService _authorizationService;
        IPowerDriveService _powerDriveService;

        public AuthorizationActionController()
        {
            _authorizationService = new AuthorizationService();
            _powerDriveService = new PowerDriveService();
        }


        public ActionResult GetUserRoles(QueueBucketViewModel queuBucket)
        {
            try
            {
                TempData["actionPerformed"] = queuBucket.ActionPerformed;
                QueueBucketViewModel queue = new QueueBucketViewModel();
                queue.MemberName = queuBucket.MemberName;
                queue.MemberId = queuBucket.MemberId;
                queue.AuthType = queuBucket.AuthType;
                bool isAdmissionPos = IsAdmissionAuthorization(queuBucket.POS, queuBucket.OutPatientType);
                queue.Roles = Bucket(queuBucket.CurrenUserRole, queuBucket.ActionPerformed, isAdmissionPos);
                return PartialView("~/Areas/UM/Views/QueueBuckets/_QueueBucket.cshtml", queue);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<string> Bucket(string role, string action, bool isAdmission)
        {
            switch (role.ToLower())
            {
                case "intake":
                    if (ConfigurationManager.AppSettings["Flow"] == "IPA")
                    {
                        return GetBucketData(ConfigurationManager.AppSettings["IntakeIPA"], action);
                    }
                    else
                    {
                        if (isAdmission)
                        {
                            return GetBucketData(ConfigurationManager.AppSettings["IntakeAdmHP"], action);
                        }
                        else
                        {
                            return GetBucketData(ConfigurationManager.AppSettings["IntakeVisitHP"], action);
                        }
                    }
                case "nurse":
                    if (ConfigurationManager.AppSettings["Flow"] == "IPA")
                    {
                        return GetBucketData(ConfigurationManager.AppSettings["NurseIPA"], action);
                    }
                    else
                    {
                        return GetBucketData(ConfigurationManager.AppSettings["NurseVisitHP"], action);
                    }
                case "fnurse": return GetBucketData(ConfigurationManager.AppSettings["FNurseAdmHP"], action);

                case "md":
                    if (ConfigurationManager.AppSettings["Flow"] == "IPA")
                    {
                        return GetBucketData(ConfigurationManager.AppSettings["MDIPA"], action);
                    }
                    else
                    {
                        if (isAdmission)
                        {
                            return GetBucketData(ConfigurationManager.AppSettings["MDAdmHP"], action);
                        }
                        else
                        {
                            return GetBucketData(ConfigurationManager.AppSettings["MDVisitHP"], action);
                        }
                    }
                case "pac":
                    if (ConfigurationManager.AppSettings["Flow"] == "IPA")
                    {
                        return GetBucketData(ConfigurationManager.AppSettings["PACIPA"], action);
                    }
                    else
                    {
                        return GetBucketData(ConfigurationManager.AppSettings["PACVisitHP"], action);
                    }
                case "Facility": return GetBucketData(ConfigurationManager.AppSettings["FacilityAdmHP"], action);

                case "hpsubmitted": return GetBucketData(ConfigurationManager.AppSettings["PACHPSubmitted"], action);

                default: return null;
            }
        }

        private List<string> GetBucketData(string p, string action)
        {
            string[] options = new string[] { "?" };
            string[] option2 = new string[] { "'" };
            List<string> bucketOnAction = new List<string>();
            bucketOnAction = Getwords(p, options);
            switch (action.ToLower())
            {
                case "refer": var referRoles = Getwords(bucketOnAction[0], option2);
                    referRoles.RemoveAt(0);
                    return referRoles;
                case "approve": var approveRole = Getwords(bucketOnAction[1], option2);
                    approveRole.RemoveAt(0);
                    return approveRole;
                default: return null;
            }
        }

        private List<string> Getwords(string sentence, string[] options)
        {
            try
            {
                List<string> words = new List<string>();
                if (sentence != null)
                {
                    foreach (string word in sentence.Split(options, StringSplitOptions.RemoveEmptyEntries))
                    {
                        words.Add(word);
                    }
                }
                return words;
            }
            catch
            {
                throw;
            }
        }

        private bool IsAdmissionAuthorization(string pos, string outpatientType)
        {
            bool isAdmission = false;
            string admissionPOS = ConfigurationManager.AppSettings["AdmissionPOS"];
            string[] option = new string[] { "," };
            List<string> admissionPOSList = new List<string>();
            admissionPOSList = Getwords(admissionPOS, option);
            if (admissionPOSList.Contains(pos))
            {
                if (pos == "22-OutPatient Hospital" && (outpatientType == "OP OBSERVATION" || outpatientType == "OP IN A BED"))
                {
                    isAdmission = true;
                }
                else
                {
                    isAdmission = true;
                }
            }
            return isAdmission;
        }

        public ActionResult GetUsers(string userQueue)
        {
            List<UserViewModel> users = new List<UserViewModel>();
            try
            {
                if (userQueue.Equals("Intake"))
                {
                    using (StreamReader sr = new StreamReader(Server.MapPath("~/Areas/UM/Resources/JSONData/QueueBuckets/IntakeUserList.txt")))
                    {
                        users = JsonConvert.DeserializeObject<List<UserViewModel>>(sr.ReadToEnd());
                    }
                }
                else if (userQueue.Equals("Nurse"))
                {
                    using (StreamReader sr = new StreamReader(Server.MapPath("~/Areas/UM/Resources/JSONData/QueueBuckets/NurseUserList.txt")))
                    {
                        users = JsonConvert.DeserializeObject<List<UserViewModel>>(sr.ReadToEnd());
                    }
                }
                else if (userQueue.Equals("PAC"))
                {
                    using (StreamReader sr = new StreamReader(Server.MapPath("~/Areas/UM/Resources/JSONData/QueueBuckets/NurseUserList.txt")))
                    {
                        users = JsonConvert.DeserializeObject<List<UserViewModel>>(sr.ReadToEnd());
                    }
                }

                //ViewBag.Users = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(users));
                //ViewBag.Users = new JavaScriptSerializer().Serialize(users);
                return PartialView("~/Areas/UM/Views/QueueBuckets/_AuthorizationUserList.cshtml", users);
            }
            catch (Exception)
            {
                return null;
            }


        }

        public ActionResult GetUsersGraph(string userQueue)
        {
            List<UserViewModel> users = new List<UserViewModel>();
            try
            {

                using (StreamReader sr = new StreamReader(Server.MapPath("~/Areas/UM/Resources/JSONData/QueueBuckets/IntakeUserList.txt")))
                {
                    users = JsonConvert.DeserializeObject<List<UserViewModel>>(sr.ReadToEnd());
                }



                var Users = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(users));

                return Json(users, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }


        }


        public ActionResult GetSelectedUsersGraph(AuthorizationViewModel authorization)
        {
            
            try
            {
                var FromUser=new UserViewModel();
                
                FromUser.StandardCount=authorization.AuthorizationStatus.ReferFromUserStandardCount;
                FromUser.ExpeditedCount=authorization.AuthorizationStatus.ReferFromUserExpeditedCount;

                var ToUser=new UserViewModel();

                ToUser.StandardCount=authorization.AuthorizationStatus.ReferToUserStandardCount;
                ToUser.ExpeditedCount=authorization.AuthorizationStatus.ReferToUserExpeditedCount;

                return Json(new { FromUser = FromUser, ToUser = ToUser }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }


        }

        public ActionResult SaveAuthorization(AuthorizationViewModel authorization)
        {
            try
            {
                if (TempData["actionPerformed"] != null)
                {
                    if (TempData["actionPerformed"] != "Pend")
                    {
                     authorization.AuthorizationStatus.ActionPerformed = TempData["actionPerformed"].ToString();
                }
                    else
                    {
                        if ((TempData["actionPerformed"] == "Pend"))
                        {
                            authorization.AuthorizationStatus.ActionPerformed = TempData["actionPerformed"].ToString();
                        }
                    }
                }

                 //------- This is for Power Drive - Should be moved later while referring an auth-----
                FileService fileService = new FileService();
                fileService.UserInfo = new UserInfo { UserName = "testprev@gmail.com", Path = "", ApplicaionOrGroupName = "TestPrev", Token = "" };
                fileService.DocumentAndStreams = new List<DocumentAndStream>();
                for (int i = 0; i < authorization.Attachments.Count; i++)
                {
                    if (authorization.Attachments[i].DocumentFile != null)
                    {
                        DocumentAndStream DocumentAndStream = new DocumentAndStream { Document = new Document { FileName = authorization.Attachments[i].DocumentFile.FileName, TransferType = TransferType.Stream, Extension = authorization.Attachments[i].Name }, InputStream = authorization.Attachments[i].DocumentFile.InputStream };
                        fileService.DocumentAndStreams.Add(DocumentAndStream);
                    }
                
                }
                FileUploadResponse response = _powerDriveService.UploadFileService(fileService);
                // ---- Place the response key in attachment view model ------
                if (response.FileInfomations.Count() > 0)
                {
                    for (int i = 0; i < response.FileInfomations.Count(); i++)
                    {
                        authorization.Attachments[i].DocKey = response.FileInfomations[i].FileKey;
                        authorization.Attachments[i].FileName = authorization.Attachments[i].DocumentFile.FileName;
                        authorization.Attachments[i].DocumentFile = null;
                
                    }
                }
                 _authorizationService.SaveAuthorization(authorization);

                return PartialView("~/Areas/UM/Views/QueueBuckets/_SelectedUserPartial.cshtml", authorization.AuthorizationStatus);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}