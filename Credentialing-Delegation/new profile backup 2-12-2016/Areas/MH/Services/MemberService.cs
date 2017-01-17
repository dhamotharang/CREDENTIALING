using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortalTemplate.Areas.MH.Common;
using PortalTemplate.Areas.MH.IServices;
using PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile;
using PortalTemplate.Areas.MH.Models.ViewModels.ProfileManagement;
using PortalTemplate.Areas.MH.Models.ViewModels.SearchMember;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.MH.Services
{
    public class MemberService : IMemberService
    {
        CommonMethods commonMethods = new CommonMethods();


        /// <summary>This Method is Used to Get All the Members.</summary>
        /// <returns>List of Members.</returns>
        public List<SearchMemberResultViewModel> GetAllMembers()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<SearchMemberResultViewModel> Member = serial.Deserialize<List<SearchMemberResultViewModel>>(commonMethods.GetJSONData("MemberServiceData.JSON"));
            return Member;
        }

        /// <summary>This Method is Used to Get All the Members.</summary>
        /// <returns>List of Members Based on Search.</returns>
        public List<SearchMemberResultViewModel> SearchMember(string param, IEnumerable<SearchFactorViewModel> searchdata)
        {
            SearchMemberViewModel m = new SearchMemberViewModel();
            JavaScriptSerializer serial = new JavaScriptSerializer();
            if(param == "Enrolled"){
                List<SearchMemberResultViewModel> mResult = serial.Deserialize<List<SearchMemberResultViewModel>>(commonMethods.GetJSONData("MemberServiceData.JSON"));
                m.Members = mResult;
            }else{
                List<SearchMemberResultViewModel> mResult = serial.Deserialize<List<SearchMemberResultViewModel>>(commonMethods.GetJSONData("MemberServiceData.JSON"));
                m.Members = mResult.ToList();
            }
            return m.Members;
        }

        public MemberProfileHeaderViewModel GetMemberHeader(string subscriberId, string type, string status){
            MemberProfileHeaderViewModel headerData = new MemberProfileHeaderViewModel();
            if (type == "View" && status == "Enrolled")
            {
                JavaScriptSerializer serial = new JavaScriptSerializer();
                List<SearchMemberResultViewModel> members = serial.Deserialize<List<SearchMemberResultViewModel>>(commonMethods.GetJSONData("MemberServiceData.JSON"));
                foreach (var member in members)
                {
                    if (member.SubscriberID == subscriberId)
                    {
                        headerData = MapMemberData(member);
                        break;
                    }
                }
            }
            return headerData;
        }

        private MemberProfileHeaderViewModel MapMemberData(SearchMemberResultViewModel member)
        {
            MemberProfileHeaderViewModel h = new MemberProfileHeaderViewModel();
            var today = DateTime.Today;
            int year = Array.ConvertAll(member.DOB.Split('/'), int.Parse)[2];
            var age = today.Year - year;
            h.SubscriberID = member.SubscriberID;
            h.FirstName = member.FirstName;
            h.LastName = member.LastName;
            h.Gender = member.Gender;
            h.DOB = member.DOB;
            h.PCP = member.PCP;
            h.Age = age;
            return h;
        }

        public GeneralInformationViewModel GetDemographicsDetails(string umId)
        {
            GeneralInformationViewModel generalInfo = new GeneralInformationViewModel();
            generalInfo.Prefix = "Mr.";
            generalInfo.LastName = "John Smith";
            generalInfo.FirstName = "William";
            generalInfo.Gender = "Male";
            return generalInfo;
        }

        public List<MembershipListViewModel> GetAllMemberships()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<MembershipListViewModel> Memberships = serial.Deserialize<List<MembershipListViewModel>>(commonMethods.GetJSONData("MembershipsList.JSON"));
            return Memberships;
        }

    }
}