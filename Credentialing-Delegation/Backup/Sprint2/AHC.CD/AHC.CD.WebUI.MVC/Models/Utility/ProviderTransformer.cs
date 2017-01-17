using AHC.CD.Entities.ProfileDemographicInfo;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.Utility
{


    public class ProviderTransformer
    {

        public static Individual TransformToIndividual(ProviderAddViewModel providerViewModel)
        {

            Individual individual = new Individual();

            PersonalInfo personalInfo = new PersonalInfo();

            AddressInfo addressInfo = new AddressInfo();

            ContactInfo profileContact = new ContactInfo();

            personalInfo.Title = providerViewModel.Personal.Title;
            personalInfo.FirstName = providerViewModel.Personal.FirstName;
            personalInfo.MiddleName = providerViewModel.Personal.MiddleName;
            personalInfo.LastName = providerViewModel.Personal.LastName;
            personalInfo.Email = providerViewModel.Personal.Email;
            personalInfo.Sex = providerViewModel.Personal.Gender;

            personalInfo.LastUpdatedDateTime = DateTime.Now;

            addressInfo.City = providerViewModel.Address.City;
            addressInfo.County = providerViewModel.Address.County;
            addressInfo.Address = providerViewModel.Address.Street;
            addressInfo.State = providerViewModel.Address.State;
            addressInfo.ZipCode = providerViewModel.Address.ZipCode;
            addressInfo.Country = providerViewModel.Address.Country;

            addressInfo.LastUpdatedDateTime = DateTime.Now;

            profileContact.CountryCode = providerViewModel.Personal.Contact.CountryCode;
            profileContact.PhoneNo = providerViewModel.Personal.Contact.PhoneNo;

            if (providerViewModel.GroupID != 0)
                individual.GroupID = providerViewModel.GroupID;


            individual.LastUpdatedDateTime = DateTime.Now;
            individual.ProviderStatus = ProviderStatus.Active;
            individual.ProviderTypeID = providerViewModel.ProviderTypeId;
            individual.IsPartOfGroup = providerViewModel.IsPartOfGroup;
            individual.Relation = providerViewModel.ProviderRelation;

            individual.FullName = providerViewModel.Personal.FirstName + " " + providerViewModel.Personal.MiddleName + " " + providerViewModel.Personal.LastName;

            //individual.Relation = (ProviderRelation)int.Parse(providerViewModel.ProviderRelation);

            individual.AddressInfos = new List<AddressInfo>();

            individual.AddressInfos.Add(addressInfo);

            individual.ContactInfos = new List<ContactInfo>();

            individual.ContactInfos.Add(profileContact);

            individual.PersonalInfo = personalInfo;

            return individual;

        }

        public static List<ProviderCategoryViewModel> TransformProviderCategory(IEnumerable<ProviderCategory> categories)
        {
            List<ProviderCategoryViewModel> providercategories = new List<ProviderCategoryViewModel>();

            List<ProviderTypeViewModel> PTypes = null;

            foreach (ProviderCategory p in categories)
            {
                PTypes = new List<ProviderTypeViewModel>();

                foreach (ProviderType pType in p.ProviderTypes)
                {

                    PTypes.Add(new ProviderTypeViewModel { typeId = pType.ProviderTypeID, typeName = pType.Title });

                }

                providercategories.Add(new ProviderCategoryViewModel
                {
                    categoryId = p.ProviderCategoryID,
                    categoryName = p.Title,
                    providerTypes = PTypes

                });

            }

            return providercategories;
        }

        public static List<GroupViewModel> TransformGroup(IEnumerable<Group> groups)
        {
            List<GroupViewModel> viewGroups = new List<GroupViewModel>();

            foreach (Group group in groups)
            {

                viewGroups.Add(new GroupViewModel
                {
                    GroupID = group.GroupID,
                    GroupName = group.GroupName
                });

            }

            return viewGroups;
        }

    }
}