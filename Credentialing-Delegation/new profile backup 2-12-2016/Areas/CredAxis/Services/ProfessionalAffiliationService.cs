using PortalTemplate.Areas.CredAxis.Models.ProfessionalAffiliationViewModel;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.CredAxis.Services
{
    public class ProfessionalAffiliationService : IProfessionalAffiliationService
    {
        private ProfessionalAffiliationViewModel _ProfessionalAffiliation;

        public ProfessionalAffiliationService()
        {
            _ProfessionalAffiliation = new ProfessionalAffiliationViewModel();
        }

        public List<ProfessionalAffiliationViewModel> GetAllProfessionalAffiliationCode()
        {
            List<ProfessionalAffiliationViewModel> ProfessionalAffiliationDetails = new List<ProfessionalAffiliationViewModel>();

            try
            {
                string pathProfAff;
                //path = Path.Combine(HttpRuntime.AppDomainAppPath, "~/Areas/CredAxis/Resources/CredAxisJson/PersonalDetails.json");
                pathProfAff = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/ProfessionalAffiliation/ProfessionalAffiliation.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathProfAff))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    ProfessionalAffiliationDetails = serial.Deserialize<List<ProfessionalAffiliationViewModel>>(text);
                }
                return ProfessionalAffiliationDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProfessionalAffiliationViewModel AddEditProfessionalAffiliationCode(PortalTemplate.Areas.CredAxis.Models.ProfessionalAffiliationViewModel.ProfessionalAffiliationMainModel professionalAffiliationViewModel)
        {
            return null;
        }


        public ProfessionalAffiliationMainModel GetAllProfessionalAffiliationCode(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public ProfessionalAffiliationViewModel AddEditProfessionalAffiliationCode(ProfessionalAffiliationMainModel professionalAffiliationViewModel, int ProfileId, int ProfAffiliationId)
        {
            throw new NotImplementedException();
        }
    }
}