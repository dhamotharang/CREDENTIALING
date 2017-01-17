using AHC.CD.Entities.ProviderInfo;
using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.Utility
{
    public class SearchIndividualTransformer
    {

        public static List<SearchProviderViewModel> TransformToProviderSearchVM(IEnumerable<Individual> individuals)
        {
            List<SearchProviderViewModel> SearchProviderView = new List<SearchProviderViewModel>();

            foreach (Individual individual in individuals)
            {
                SearchProviderView.Add(new SearchProviderViewModel
                {
                    ProviderID = individual.ProviderID,
                    Title = individual.PersonalInfo.Title!=null?individual.PersonalInfo.Title:"NA",
                    FirstName = individual.PersonalInfo.FirstName!=null?individual.PersonalInfo.FirstName:"NA",
                    LastName = individual.PersonalInfo.LastName!=null?individual.PersonalInfo.LastName:"NA",
                    City = individual.AddressInfos.First().City!=null?individual.AddressInfos.First().City:"NA",
                    County = individual.AddressInfos.First().County!=null?individual.AddressInfos.First().County:"NA",
                    State = individual.AddressInfos.First().State!=null?individual.AddressInfos.First().State:"NA",
                    ProviderType = individual.ProviderType!=null?individual.ProviderType.Title:"NA",
                    ProviderRelation = individual.Relation.ToString(),
                    Group = individual.Group!=null?individual.Group.GroupName:"NA"
                });
            }

            return SearchProviderView;
        }
    }
}