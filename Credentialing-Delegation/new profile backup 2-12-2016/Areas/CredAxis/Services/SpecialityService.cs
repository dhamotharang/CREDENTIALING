using PortalTemplate.Areas.CredAxis.Models.SpecialtyViewModel;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.CredAxis.Services
{
    public class SpecialityService : ISpecialityService
    {
        private SpecialtyDetailViewModel _SpecialityDetail;

        public SpecialityService()
        {
            _SpecialityDetail = new SpecialtyDetailViewModel();
        }

        public List<SpecialtyDetailViewModel> GetAllSpeciality()
        {
            List<SpecialtyDetailViewModel> SpecialityDetails = new List<SpecialtyDetailViewModel>();

            try
            {
                string pathSpeciality, pathBoard;
                //path = Path.Combine(HttpRuntime.AppDomainAppPath, "~/Areas/CredAxis/Resources/CredAxisJson/PersonalDetails.json");
                pathSpeciality = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Speciality/SpecialityDetails.json");
                pathBoard = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Speciality/BoardDetails.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathSpeciality))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    SpecialityDetails = serial.Deserialize<List<SpecialtyDetailViewModel>>(text);
                }

                //using (System.IO.TextReader reader = System.IO.File.OpenText(pathBoard))
                //{
                //    string text = reader.ReadToEnd();
                //    JavaScriptSerializer serial = new JavaScriptSerializer();
                //    List<BoardDetailViewModel> CitizenData = new List<BoardDetailViewModel>();
                //    CitizenData = serial.Deserialize<List<BoardDetailViewModel>>(text);
                //    foreach (var data in CitizenData)
                //    {
                //        SpecialityDetails.boardDetail.Add(data);
                //    }
                //}


                return SpecialityDetails;
                               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SpecialityMainViewModel AddEditSpeciality(SpecialityMainViewModel specialityMainViewModel)
        {
            return null;
        }

        public SpecialityMainViewModel GetAllSpeciality(int ProfileId, int SpecialtyId)
        {
            throw new NotImplementedException();
        }

        public SpecialtyDetailViewModel AddEditSpeciality(SpecialityMainViewModel specialtyMainViewModel, int ProfileId, int SpecialtyId)
        {
            throw new NotImplementedException();
        }
    }
}