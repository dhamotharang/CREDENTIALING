using PortalTemplate.Areas.CredAxis.Models.DocumentRepoViewModel;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.CredAxis.Services
{
    public class DocumentRepoService : IDocumentService
    {
        private DoumentRepoMainViewModel _DocRepo;

        public DocumentRepoService()
        {
            _DocRepo = new DoumentRepoMainViewModel();
        }

        public DoumentRepoMainViewModel GetDocRepoData()
        {

            try
            {
                string pathprofile, pathpsv, pathgenforms, pathforms;
                //path = Path.Combine(HttpRuntime.AppDomainAppPath, "~/Areas/CredAxis/Resources/CredAxisJson/PersonalDetails.json");
     

                pathprofile = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/DocumentRepo/ProfileDocs.json");

                pathpsv = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/DocumentRepo/PSV.json");

                pathgenforms = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/DocumentRepo/GeneratedForms.json");

                pathforms = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/DocumentRepo/Forms.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathprofile))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    _DocRepo.ProfileDoc = serial.Deserialize<List<ProfileDocViewModel>>(text);
                }
                using (System.IO.TextReader reader = System.IO.File.OpenText(pathpsv))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    _DocRepo.PSV = serial.Deserialize<List<PSVViewModel>>(text);
                }
                using (System.IO.TextReader reader = System.IO.File.OpenText(pathgenforms))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    _DocRepo.GeneratedForms = serial.Deserialize<List<GeneratedFormsViewModel>>(text);
                }
                using (System.IO.TextReader reader = System.IO.File.OpenText(pathforms))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    _DocRepo.Forms = serial.Deserialize<List<FormsViewModel>>(text);
                }
                return _DocRepo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}