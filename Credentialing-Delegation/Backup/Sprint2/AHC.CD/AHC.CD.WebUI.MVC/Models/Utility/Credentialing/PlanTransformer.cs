using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Credentialing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.Utility.Credentialing
{
    public class PlanTransformer
    {

        public static List<PlanViewModel> TransformPlan(IEnumerable<Plan> plans)
        {
            List<PlanViewModel> planDtos = new List<PlanViewModel>();

            foreach (Plan plan in plans)
            {


                planDtos.Add(new PlanViewModel { PlanID = plan.PlanID, Title = plan.Title, Logo ="/Content/Images/Plans/"+plan.InsuranceCompany.Title+".jpg" ,});

            }

            return planDtos;
        }

        public static CredentialingDetailsDTO TransformToCredentialDetailsDTO(InitiateCreadentialingVM initiateCreadentialingVM)
        {
            CredentialingDetailsDTO credentialingDetailsDTO = new CredentialingDetailsDTO();

            credentialingDetailsDTO.ProviderID = initiateCreadentialingVM.ProviderID;

            credentialingDetailsDTO.Remarks = initiateCreadentialingVM.Remarks;

            credentialingDetailsDTO.CredentialedBy = initiateCreadentialingVM.CredentialedBy;


            var plans = new List<Plan>();

            foreach (int planId in initiateCreadentialingVM.SelectedPlans)
            {
                plans.Add(new Plan { PlanID=planId});

            }

            credentialingDetailsDTO.credentialingPlans = plans;

            return credentialingDetailsDTO;
        }

    }
}