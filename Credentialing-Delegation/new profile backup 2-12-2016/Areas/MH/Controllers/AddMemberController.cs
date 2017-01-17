using Newtonsoft.Json;
using PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile;
using PortalTemplate.Areas.MH.ServiceFacade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.MH.Controllers
{
    public class AddMemberController : Controller
    {
        //
        // GET: /MH/AddMember/
        public ActionResult Index()
        {
            return View();
        }

        ServiceLocator serviceLocator = new ServiceLocator();

        public JsonResult CopyMemberDetails(MemberProfileViewModel member)
        {
            member.MemberMemberships[0].MembershipInformation.Subscriber.PersonalInformation.FirstName = member.PersonalInformation.FirstName;
            member.MemberMemberships[0].MembershipInformation.Subscriber.PersonalInformation.MiddleInitial = member.PersonalInformation.MiddleInitial;
            member.MemberMemberships[0].MembershipInformation.Subscriber.PersonalInformation.LastName = member.PersonalInformation.LastName;
            member.MemberMemberships[0].MembershipInformation.Subscriber.PersonalInformation.Gender = member.PersonalInformation.Gender;
            member.MemberMemberships[0].MembershipInformation.Subscriber.PersonalInformation.DOB = member.PersonalInformation.DOB;
            member.MemberMemberships[0].MembershipInformation.Subscriber.AddressInformation[0].HomeAddress = member.AddressInformation[0].HomeAddress;
            member.MemberMemberships[0].MembershipInformation.Subscriber.AddressInformation[0].ApartmentOrSuiteNumber = member.AddressInformation[0].ApartmentOrSuiteNumber;
            member.MemberMemberships[0].MembershipInformation.Subscriber.AddressInformation[0].City = member.AddressInformation[0].City;
            member.MemberMemberships[0].MembershipInformation.Subscriber.AddressInformation[0].State = member.AddressInformation[0].State;
            member.MemberMemberships[0].MembershipInformation.Subscriber.AddressInformation[0].County = member.AddressInformation[0].County;
            member.MemberMemberships[0].MembershipInformation.Subscriber.AddressInformation[0].Country = member.AddressInformation[0].Country;
            member.MemberMemberships[0].MembershipInformation.Subscriber.AddressInformation[0].ZipCode = member.AddressInformation[0].ZipCode;
            member.MemberMemberships[0].MembershipInformation.Subscriber.ContactInformation[0].Number = member.ContactInformation[0].Number;
            return Json(new { Insurance = member.MemberMemberships[0] }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAddforAddingMember()
        {
            return PartialView("~/Areas/MH/Views/NewMember/Add/_AddNewMember.cshtml");
        }

        [HttpPost]
        public ActionResult AddNewMember(MemberProfileViewModel member)
        {
            if(!member.MemberMemberships[0].HasOtherInsurance){
                member.MemberMemberships = member.MemberMemberships.Take(1).ToList();
            }

            string serviceName = serviceLocator.Locate("Member");
            Task<long> MemberId = Task.Run(async () =>
            {
                long result = await MemberServiceRepository.AddMemberData(serviceName, "api/FacilityService/GetAllFacilities?source=1", member);
                return result;
            });
            
            return null;
        }
	}
}