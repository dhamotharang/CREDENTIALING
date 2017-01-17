using PortalTemplate.Areas.CredAxis.Models.ProfessionalLiabilityViewModel;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.CredAxis.Services
{
    public class ProfessionalLiabilityService : IProfessionalLiabilityService
    {
        private ProfessionalLiabilityViewModel _ProfessionalLiability;

        public ProfessionalLiabilityService()
        {
            _ProfessionalLiability = new ProfessionalLiabilityViewModel();
        }

        public List<ProfessionalLiabilityViewModel> GetAllProfessionalLiability()
        {
            List<ProfessionalLiabilityViewModel> ProfessionalLiabilityDetails = new List<ProfessionalLiabilityViewModel>();

            try
            {
                string pathProfLiability;
                //path = Path.Combine(HttpRuntime.AppDomainAppPath, "~/Areas/CredAxis/Resources/CredAxisJson/PersonalDetails.json");
                pathProfLiability = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/ProfessionalLiability/ProfessionalLiability.json");
                
                using (System.IO.TextReader reader = System.IO.File.OpenText(pathProfLiability))
                    {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    ProfessionalLiabilityDetails = serial.Deserialize<List<ProfessionalLiabilityViewModel>>(text);
                    return ProfessionalLiabilityDetails;
                }
                               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProfessionalLiabilityViewModel AddEditProfessionalLiability(ProfessionalLiabilityViewModel professionalLiabilityViewModel)
        {
            return null;
        }

        public ProfessionalLiablityMainModel GetAllProfessionalLiability(int ProfileId, int LiabilityId)
        {
            throw new NotImplementedException();
        }

        public ProfessionalLiabilityViewModel AddEditProfessionalLiability(ProfessionalLiablityMainModel professionalLiabilityViewModel, int ProfileId, int LiabilityId)
        {
            throw new NotImplementedException();
        }
    }
}