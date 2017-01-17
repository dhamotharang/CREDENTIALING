using PortalTemplate.Areas.CredAxis.Models.PofessionalReferenceViewModel;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using PortalTemplate.Areas.CredAxis.Models.ProviderProfileViewModel.PofessionalReferenceViewModel;

namespace PortalTemplate.Areas.CredAxis.Services
{
    public class ProfessionalReferenceService : IProfessionalReferencesService
    {
        private ProfessionalReferenceViewModel _ProfessionalLiability;

        public ProfessionalReferenceService()
        {
            _ProfessionalLiability = new ProfessionalReferenceViewModel();
        }

        public List<ProfessionalReferenceViewModel> GetAllProfessionalRef()
        {
            List<ProfessionalReferenceViewModel> ProfessionalRefDetails = new List<ProfessionalReferenceViewModel>();

            try
            {
                string pathProfRef;
                //path = Path.Combine(HttpRuntime.AppDomainAppPath, "~/Areas/CredAxis/Resources/CredAxisJson/PersonalDetails.json");
                pathProfRef = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/ProfessionalReference/ProfessionalReference.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathProfRef))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    ProfessionalRefDetails = serial.Deserialize<List<ProfessionalReferenceViewModel>>(text);
                    return ProfessionalRefDetails;
                }
                               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProfessionalReferenceMainModel AddEditProfessionalRef(ProfessionalReferenceViewModel professionalReferenceViewModel)
        {
            return null;
        }

        public ProfessionalReferenceMainModel GetAllProfessionalRef(int ProfileId, int ProfessionalReferenceId)
        {
            throw new NotImplementedException();
        }

        public ProfessionalReferenceViewModel AddEditProfReference(ProfessionalReferenceMainModel professionalReferenceViewModel, int ProfileId, int ProfessionalReferenceId)
        {
            throw new NotImplementedException();
        }
    }
}