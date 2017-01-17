using PortalTemplate.Areas.CredAxis.Models.EducationViewModel;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.CredAxis.Services
{
      public class EducationServices : IEducationServices
    {
        private EducationMainViewModel _EducationsCode;

        public EducationServices()
        {
            _EducationsCode = new EducationMainViewModel();
        }

        public EducationMainViewModel GetAllEducations()
        {
            EducationMainViewModel EducationDetails = new EducationMainViewModel();

            try
            {
                string pathECFMG, pathGrad, pathPG, pathResidency, pathUG;
                //path = Path.Combine(HttpRuntime.AppDomainAppPath, "~/Areas/CredAxis/Resources/CredAxisJson/PersonalDetails.json");
                pathECFMG = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/EducationData/ECFMGData.json");

                pathGrad = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/EducationData/GraduateSchool.json");

                pathPG = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/EducationData/PostGraduationData.json");

                pathResidency = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/EducationData/ResidencyData.json");

                pathUG = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/EducationData/UnderGraduationData.json");


                using (System.IO.TextReader reader = System.IO.File.OpenText(pathECFMG))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    EducationDetails.ECFMGs = serial.Deserialize<ECFMGViewModel>(text);
                    //foreach (var data in BirthData)
                    //{
                    //    EducationDetails.ECFMGs.Add(data);
                    //}
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathGrad))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<GraduateSchoolViewModel> CitizenData = new List<GraduateSchoolViewModel>();
                    CitizenData = serial.Deserialize<List<GraduateSchoolViewModel>>(text);
                    foreach (var data in CitizenData)
                    {
                        EducationDetails.GradSchools.Add(data);
                    }
                   
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathPG))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<PostGraduationViewModel> ContactData = new List<PostGraduationViewModel>();
                    ContactData = serial.Deserialize<List<PostGraduationViewModel>>(text);
                    foreach (var data in ContactData)
                    {
                        EducationDetails.PostGradSchools.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathResidency))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<ResidencyViewModel> HomeData = new List<ResidencyViewModel>();
                    HomeData = serial.Deserialize<List<ResidencyViewModel>>(text);
                    foreach (var data in HomeData)
                    {
                        EducationDetails.Residency.Add(data);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathUG))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<UnderGraduationViewModel> LangData = new List<UnderGraduationViewModel>();
                    LangData = serial.Deserialize<List<UnderGraduationViewModel>>(text);
                    foreach (var data in LangData)
                    {
                        EducationDetails.UnderGradSchools.Add(data);
                    }
                }

                return EducationDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EducationMainViewModel> AddEditEducations(EducationMainViewModel educationsCode)
        {
            return null;
        }

        public EducationMainViewModel GetAllEducations(int ProfileId, int EducationId)
        {
            throw new NotImplementedException();
        }

        public UnderGraduationViewModel AddEditUnderGraduate(UnderGraduationViewModel underGraduate, int ProfileId, int EducationId)
        {
            throw new NotImplementedException();
        }

        public GraduateSchoolViewModel AddEditGraduate(GraduateSchoolViewModel graduate, int ProfileId, int EducationId)
        {
            throw new NotImplementedException();
        }

        public ECFMGViewModel AddEditECFMGDetail(ECFMGViewModel ecfmgDetail, int ProfileId)
        {
            throw new NotImplementedException();
        }

        public ResidencyViewModel AddEditResidencyDetails(ResidencyViewModel residencyDetails, int ProfileId, int EducationId)
        {
            throw new NotImplementedException();
        }

        public PostGraduationViewModel AddEditPostGraduation(PostGraduationViewModel postGraduation, int ProfileId, int EducationId)
        {
            throw new NotImplementedException();
        }
    }
}