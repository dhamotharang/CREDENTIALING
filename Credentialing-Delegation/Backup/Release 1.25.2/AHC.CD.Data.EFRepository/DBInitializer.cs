using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository
{
    public class DBInitializer// :  DbMigrationsConfiguration<EFEntityContext> //DropCreateDatabaseIfModelChanges<EFEntityContext>
    {
        
        
        public static void Seed()
        {
            //EFEntityContext context = new EFEntityContext();
          

            //var docCat1 = new DocumentCategory { Title = "Personal" };
            //var docCat2 = new DocumentCategory { Title = "License" };
            //var docCat3 = new DocumentCategory { Title = "Miscellaneous" };

            //var docType1 = new DocumentType { Title = "DL", DocumentCategory = docCat1 };
            //var docType2 = new DocumentType { Title = "Passport", DocumentCategory = docCat1 };
            //var docType3 = new DocumentType { Title = "Resume", DocumentCategory = docCat1 };
            //var docType4 = new DocumentType { Title = "Medical", DocumentCategory = docCat2 };
            //var docType5 = new DocumentType { Title = "Medicare", DocumentCategory = docCat2 };
            //var docType6 = new DocumentType { Title = "Medicade", DocumentCategory = docCat2 };
            //var docType7 = new DocumentType { Title = "DEA", DocumentCategory = docCat2 };

            //context.DocumentCategories.Add(docCat1);
            //context.DocumentCategories.Add(docCat2);
            //context.DocumentCategories.Add(docCat3);

            //context.DocumentTypes.Add(docType1);
            //context.DocumentTypes.Add(docType2);
            //context.DocumentTypes.Add(docType3);
            //context.DocumentTypes.Add(docType4);
            //context.DocumentTypes.Add(docType5);
            //context.DocumentTypes.Add(docType6);
            //context.DocumentTypes.Add(docType7);

            //var providerTypes = new List<ProviderType>
            //{
            //    new ProviderType{ProviderCategory =pc1,Title = "Physicians (MD, DO, DMD)" , Code="MDDODMD"},
            //    new ProviderType{ProviderCategory =pc1,Title = "Podiatrists (DPM)" , Code="DPM"},
            //    new ProviderType{ProviderCategory =pc1,Title = "Dentists (DDS)", Code ="DDS" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Advanced Registered Nurse Practitioners (ARNP)", Code ="ARNP" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Physician Assistants (PA)", Code="PA" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Certified Nurse Anesthetists (CNA)", Code="CNA" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Certified Registered Nurse Anesthetists (CRNA)", Code="CRNA" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Advanced Registered Nurse Practitioners (ARNP)", Code="ARNP" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Physical Therapist", Code="PT" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Occupational Therapist", Code="OT" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Speech Therapist", Code="ST" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Respiratory Therapist", Code="RT" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Optometrist (OD)", Code="OD" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Psychologist (PhD)", Code="PhD" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Licensed Clinical Social Worker (LCSW)", Code="LCSW" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Masters in Social Work (MSW)", Code="MSW" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Licensed Mental Health Counselor (LMHC)", Code="LMCH" },
            //    new ProviderType{ProviderCategory =pc1,Title = "Diabetes Self-Management Training Providers", Code="DSMTP" },
            //    new ProviderType{ProviderCategory =pc1,Title = "ESRD Providers", Code="ESRD" },
               
            //};
            //providerTypes.ForEach(pt => context.ProviderTypes.Add(pt));

            //var groups = new List<Group> 
            //{
            //    new Group{GroupName = "IPA"},
            //    new Group{GroupName = "ACO"},
            //};


            //groups.ForEach(g => context.Groups.Add(g));

            //Plan p1 = new Plan {Title = "Humana", InsuranceCompanyID = 1 };
            //Plan p2 = new Plan { Title = "GHI", InsuranceCompanyID = 1 };
            //Plan p3 = new Plan { Title = "Plan 1", InsuranceCompanyID = 1 };
            //Plan p4 = new Plan { Title = "Plan 2", InsuranceCompanyID = 1 };
            //Plan p5 = new Plan { Title = "Plan 3", InsuranceCompanyID = 1 };

            //Plan p6 = new Plan { Title = "Plan 4", InsuranceCompanyID = 2 };
            //Plan p7 = new Plan { Title = "Plan 5", InsuranceCompanyID = 2 };
            //Plan p8 = new Plan { Title = "Plan 6", InsuranceCompanyID = 3 };
            //Plan p9 = new Plan { Title = "Plan 7", InsuranceCompanyID = 3 };
            //Plan p10 = new Plan { Title = "Plan 8", InsuranceCompanyID = 3 };
            //Plan p11 = new Plan { Title = "Plan 9", InsuranceCompanyID = 3 };
            //Plan p12 = new Plan { Title = "Plan 10", InsuranceCompanyID = 3 };
            //Plan p13 = new Plan { Title = "Plan 11", InsuranceCompanyID = 3 };
            //Plan p14 = new Plan { Title = "Plan 12", InsuranceCompanyID = 4 };
            //Plan p15 = new Plan { Title = "Plan 13", InsuranceCompanyID = 4 };
            //Plan p16 = new Plan { Title = "Plan 14", InsuranceCompanyID = 4 };
            //Plan p17 = new Plan { Title = "Plan 15", InsuranceCompanyID = 4 };

            //InsuranceCompany ic1 = new InsuranceCompany();
            //ic1.Title = "Insurance Company 1";
            //ic1.Plans = new List<Plan> { };
            //ic1.Plans.Add(p1);
            //ic1.Plans.Add(p2);
            //ic1.Plans.Add(p3);
            //ic1.Plans.Add(p4);

            //InsuranceCompany ic2 = new InsuranceCompany();
            //ic2.Title = "Insurance Company 2";
            //ic2.Plans = new List<Plan> { };
            //ic2.Plans.Add(p5);
            //ic2.Plans.Add(p6);

            //InsuranceCompany ic3 = new InsuranceCompany();
            //ic3.Title = "Insurance Company 3";
            //ic3.Plans = new List<Plan> { };
            //ic3.Plans.Add(p7);
            //ic3.Plans.Add(p8);

            //InsuranceCompany ic4 = new InsuranceCompany();
            //ic4.Title = "Insurance Company 4";
            //ic4.Plans = new List<Plan> { };
            //ic4.Plans.Add(p9);
            //ic4.Plans.Add(p10);

            //context.InsuranceCompanies.Add(ic1);
            //context.InsuranceCompanies.Add(ic2);
            //context.InsuranceCompanies.Add(ic3);
            //context.InsuranceCompanies.Add(ic4);
            
            
            //context.SaveChanges();

            // Individual Providers

            //AddressInfo ai1 = new AddressInfo { City = "City1", County = "County1", State = "State 1", Address = "Address", AddressType=AddressType.Home, DurationOfStayInMonths = 30, IsActive = true, ZipCode = 43434, Country = "Country", LastUpdatedDateTime = DateTime.Now};

            //PersonalInfo pi1 = new PersonalInfo {FirstName="First Name", LastName = "Last Name", Title="Title", Email="Email", MaritalStatus = MaritalStatus.Married };

            //Individual i1 = new Individual {FullName = "Dr. Jacob", Relation = ProviderRelation.InHouse, IsPartOfGroup = false,ProviderTypeID = 1, PersonalInfo=pi1};
            
            //i1.AddressInfos.Add(ai1);

            //Individual i2 = new Individual { FullName = "Dr. Liam", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 1, PersonalInfo = pi1 , GroupID=1};
            //i2.AddressInfos.Add(ai1);
            //Individual i3 = new Individual { FullName = "Dr. Mason", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 2, PersonalInfo = pi1,GroupID = 1 };
            //i3.AddressInfos.Add(ai1);
            //Individual i4 = new Individual { FullName = "Dr. William", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 2, PersonalInfo = pi1, GroupID = 1 };
            //i4.AddressInfos.Add(ai1);
            //Individual i5 = new Individual { FullName = "Dr. Michael", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 3, PersonalInfo = pi1, GroupID = 1 };
            //i5.AddressInfos.Add(ai1);
            //Individual i6 = new Individual { FullName = "Dr. Rajesh", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 3, PersonalInfo = pi1, GroupID = 1 };
            //i6.AddressInfos.Add(ai1);
            //Individual i7 = new Individual { FullName = "Dr. Girish", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 1, PersonalInfo = pi1, GroupID = 1 };
            //i7.AddressInfos.Add(ai1);
            //Individual i8 = new Individual { FullName = "Dr. Suresh", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 1, PersonalInfo = pi1, GroupID = 1 };
            //i8.AddressInfos.Add(ai1);
            //Individual i9 = new Individual { FullName = "Dr. Emma", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 3, PersonalInfo = pi1, GroupID = 1 };
            //i9.AddressInfos.Add(ai1);
            //Individual i10 = new Individual { FullName = "Dr. Ava", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 4, PersonalInfo = pi1, GroupID = 1 };
            //i10.AddressInfos.Add(ai1);
            //Individual i11 = new Individual { FullName = "Dr. Emily", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 4, PersonalInfo = pi1, GroupID = 1 };
            //i11.AddressInfos.Add(ai1);
            //Individual i12 = new Individual { FullName = "Dr. Madison", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 5, PersonalInfo = pi1, GroupID = 1 };
            //i12.AddressInfos.Add(ai1);
            //Individual i13 = new Individual { FullName = "Dr. Elizabeth", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 5, PersonalInfo = pi1, GroupID = 1 };
            //i13.AddressInfos.Add(ai1);
            //Individual i14 = new Individual { FullName = "Dr. Sofia", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 6, PersonalInfo = pi1, GroupID = 1 };
            //i14.AddressInfos.Add(ai1);
            //Individual i15 = new Individual { FullName = "Dr. Shreya", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 6, PersonalInfo = pi1, GroupID = 1 };
            //i15.AddressInfos.Add(ai1);
            //Individual i16 = new Individual { FullName = "Dr. Priya", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 1, PersonalInfo = pi1, GroupID = 1 };
            //i16.AddressInfos.Add(ai1);
            //Individual i17 = new Individual { FullName = "Dr. Anitha", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 1, PersonalInfo = pi1, GroupID = 1 };
            //i17.AddressInfos.Add(ai1);
            //Individual i18 = new Individual { FullName = "Dr. Venkat", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 6, PersonalInfo = pi1, GroupID = 1 };
            //i18.AddressInfos.Add(ai1);


            //Individual i19 = new Individual { FullName = "Dr. Rohith", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 1, PersonalInfo = pi1, GroupID = 1 };
            //i19.AddressInfos.Add(ai1);
            //Individual i20 = new Individual { FullName = "Dr. Krishna", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 2, PersonalInfo = pi1, GroupID = 1 };
            //i20.AddressInfos.Add(ai1);
            //Individual i21 = new Individual { FullName = "Dr. Subramanian", Relation = ProviderRelation.InHouse, IsPartOfGroup = false, ProviderTypeID = 2, PersonalInfo = pi1, GroupID = 1 };
            //i21.AddressInfos.Add(ai1);

            
            //ContactInfo ci1 = new ContactInfo { };
            ////AddressInfo ai1 = new AddressInfo {City = "City1", County = "County1", State = "State 1" };

            //context.Providers.Add(i1);
            //context.Providers.Add(i2);
            //context.Providers.Add(i3);
            //context.Providers.Add(i4);
            //context.Providers.Add(i5);
            //context.Providers.Add(i6);
            //context.Providers.Add(i7);
            //context.Providers.Add(i8);
            //context.Providers.Add(i9);
            //context.Providers.Add(i10);
            //context.Providers.Add(i11);
            //context.Providers.Add(i12);
            //context.Providers.Add(i13);
            //context.Providers.Add(i14);
            //context.Providers.Add(i15);
            //context.Providers.Add(i16);
            //context.Providers.Add(i17);
            //context.Providers.Add(i18);
            //context.Providers.Add(i19);
            //context.Providers.Add(i20);
            //context.Providers.Add(i21);

            ////context.SaveChanges(); // OK


            ////Credentialed Plans

            //IndividualPlan iPlan1 = new IndividualPlan();
            //iPlan1.DateActivated = DateTime.Now.AddDays(-60);
            //iPlan1.ExpiryDate = DateTime.Now.AddDays(300);
            //iPlan1.PlanID = 1;
            ////iPlan1.Plan = p1;
            //iPlan1.CredentialingHistory = new List<CredentialingInfo>();
            //iPlan1.CredentialingHistory.Add(new CredentialingInfo {CredentialingStatus = CredentialingStatus.Completed, PlanID = 1 });

            //IndividualPlan iPlan2 = new IndividualPlan();
            //iPlan2.DateActivated = DateTime.Now.AddDays(-60);
            //iPlan2.ExpiryDate = DateTime.Now.AddDays(300);
            //iPlan2.PlanID = 2;
            ////iPlan1.Plan = p2;
            //iPlan2.CredentialingHistory = new List<CredentialingInfo>();

            //iPlan2.CredentialingHistory.Add(new CredentialingInfo { CredentialingStatus = CredentialingStatus.Completed, PlanID = 2});


            //IndividualPlan iPlan3 = new IndividualPlan();
            //iPlan3.DateActivated = DateTime.Now.AddDays(-60);
            //iPlan3.ExpiryDate = DateTime.Now.AddDays(300);
            //iPlan3.PlanID = 3;
            ////iPlan1.Plan = p3;
            //iPlan3.CredentialingHistory = new List<CredentialingInfo>();

            //iPlan3.CredentialingHistory.Add(new CredentialingInfo { CredentialingStatus = CredentialingStatus.Completed, PlanID = 3});


            //IndividualPlan iPlan4 = new IndividualPlan();
            //iPlan4.DateActivated = DateTime.Now.AddDays(-60);
            //iPlan4.ExpiryDate = DateTime.Now.AddDays(300);
            //iPlan4.PlanID = 4;
            ////iPlan1.Plan = p4;
            //iPlan4.CredentialingHistory = new List<CredentialingInfo>();

            //iPlan4.CredentialingHistory.Add(new CredentialingInfo { CredentialingStatus = CredentialingStatus.Completed, PlanID = 4});


            //IndividualPlan iPlan5 = new IndividualPlan();
            //iPlan5.DateActivated = DateTime.Now.AddDays(-60);
            //iPlan5.ExpiryDate = DateTime.Now.AddDays(300);
            //iPlan5.PlanID = 5;
            ////iPlan1.Plan = p5;
            //iPlan5.CredentialingHistory = new List<CredentialingInfo>();

            //iPlan5.CredentialingHistory.Add(new CredentialingInfo { CredentialingStatus = CredentialingStatus.Completed , PlanID = 5});



            //IndividualPlan iPlan6 = new IndividualPlan();
            //iPlan6.DateActivated = DateTime.Now.AddDays(-60);
            //iPlan6.ExpiryDate = DateTime.Now.AddDays(300);
            //iPlan6.PlanID = 1;
            ////iPlan1.Plan = p1;
            //iPlan6.CredentialingHistory = new List<CredentialingInfo>();
            //iPlan6.CredentialingHistory.Add(new CredentialingInfo { CredentialingStatus = CredentialingStatus.Completed, PlanID = 1 });

            //IndividualPlan iPlan7 = new IndividualPlan();
            //iPlan7.DateActivated = DateTime.Now.AddDays(-60);
            //iPlan7.ExpiryDate = DateTime.Now.AddDays(300);
            //iPlan7.PlanID = 2;
            ////iPlan1.Plan = p2;
            //iPlan7.CredentialingHistory = new List<CredentialingInfo>();

            //iPlan7.CredentialingHistory.Add(new CredentialingInfo { CredentialingStatus = CredentialingStatus.Completed, PlanID = 2 });


            //IndividualPlan iPlan8 = new IndividualPlan();
            //iPlan8.DateActivated = DateTime.Now.AddDays(-60);
            //iPlan8.ExpiryDate = DateTime.Now.AddDays(300);
            //iPlan8.PlanID = 3;
            ////iPlan1.Plan = p3;
            //iPlan8.CredentialingHistory = new List<CredentialingInfo>();

            //iPlan8.CredentialingHistory.Add(new CredentialingInfo { CredentialingStatus = CredentialingStatus.Completed, PlanID = 3 });


            //IndividualPlan iPlan9 = new IndividualPlan();
            //iPlan9.DateActivated = DateTime.Now.AddDays(-60);
            //iPlan9.ExpiryDate = DateTime.Now.AddDays(300);
            //iPlan9.PlanID = 4;
            ////iPlan1.Plan = p4;
            //iPlan9.CredentialingHistory = new List<CredentialingInfo>();

            //iPlan9.CredentialingHistory.Add(new CredentialingInfo { CredentialingStatus = CredentialingStatus.Completed, PlanID = 4 });


            //IndividualPlan iPlan10 = new IndividualPlan();
            //iPlan10.DateActivated = DateTime.Now.AddDays(-60);
            //iPlan10.ExpiryDate = DateTime.Now.AddDays(300);
            //iPlan10.PlanID = 5;
            ////iPlan1.Plan = p5;
            //iPlan10.CredentialingHistory = new List<CredentialingInfo>();

            //iPlan10.CredentialingHistory.Add(new CredentialingInfo { CredentialingStatus = CredentialingStatus.Completed, PlanID = 5 });


            ////context.

            //i1.IndividualPlans.Add(iPlan1);
            //i2.IndividualPlans.Add(iPlan2);
            //i3.IndividualPlans.Add(iPlan4);
            //i4.IndividualPlans.Add(iPlan5);
            //i5.IndividualPlans.Add(iPlan6);
            //i6.IndividualPlans.Add(iPlan7);
            //i7.IndividualPlans.Add(iPlan8);
            //i8.IndividualPlans.Add(iPlan9);
            //i9.IndividualPlans.Add(iPlan10);
            //i10.IndividualPlans.Add(iPlan3);           
            //context.SaveChanges();

        }
    }
}
