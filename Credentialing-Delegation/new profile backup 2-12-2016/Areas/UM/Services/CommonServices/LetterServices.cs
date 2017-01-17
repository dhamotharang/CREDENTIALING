using Newtonsoft.Json;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.ViewModels.Letter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.Services.CommonServices
{
    public class LetterServices : ILetterServices
    {
        LetterViewModel LetterModel = new LetterViewModel();
        List<LetterViewModel> LetterModels = new List<LetterViewModel>();
        public List<LetterViewModel> GetAllLetters(int AuthorizationID)
        {
            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/JSONData/Authorization/Letter.txt");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            LetterModels = serial.Deserialize<List<LetterViewModel>>(json);
            return LetterModels;
        }

        public List<LetterViewModel> GetAllLettersByBatchNo(string BatchNumber)
        {
            throw new NotImplementedException();
        }

        public string EditServiceRequested(int LetterID)
        {
            throw new NotImplementedException();
        }

        public void DeleteLetter(LetterViewModel model)
        {
            throw new NotImplementedException();
        }

        public PortalTemplate.Areas.UM.Models.ViewModels.Letter.ApprovalLetterViewModel PreviewLetter(int LetterID,string MemberID,string LetterTemplateName)
        {
            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/JSONData/LetterBatching/ApprovalLetter.txt");
            var json = System.IO.File.ReadAllText(file);
            PortalTemplate.Areas.UM.Models.ViewModels.Letter.ApprovalLetterViewModel ApprovalLetterModal = new PortalTemplate.Areas.UM.Models.ViewModels.Letter.ApprovalLetterViewModel();
            ApprovalLetterModal = JsonConvert.DeserializeObject<PortalTemplate.Areas.UM.Models.ViewModels.Letter.ApprovalLetterViewModel>(json);
            return ApprovalLetterModal;
        }

        public LetterViewModel SaveLetter(LetterViewModel model)
        {
            throw new NotImplementedException();
        }



        IServices.ApprovalLetterViewModel ILetterServices.PreviewLetter(int LetterID, string MemberID, string LetterTemplateName)
        {
            throw new NotImplementedException();
        }
    }
}