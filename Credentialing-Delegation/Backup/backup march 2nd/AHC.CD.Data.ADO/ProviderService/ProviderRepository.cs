using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.Credentialing.Loading;
using System.Diagnostics;
using AHC.UtilityService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Diagnostics;
using AHC.CD.Resources.DatabaseQueries;


namespace AHC.CD.Data.ADO.ProviderService
{
    internal class ProviderRepository : IProviderRepository
    {
        public ProviderRepository()
        {

        }


        public IEnumerable<ProviderDTO> getAllProviderData()
        {
            try
            {
                DataTable dataTable = new DataTable();
                List<ProviderDTO> providerDTO = new List<ProviderDTO>();
                using (SqlConnection dBConnection = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
                {
                    using (var dBCommand = dBConnection.CreateCommand())
                    {
                        dBCommand.CommandText = AdoQueries.PROVIDERSERVICE_QUERY;
                        dataTable = ADORepository.GetData(dBCommand);
                    }
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        var tempProviderDTO = new ProviderDTO();
                        tempProviderDTO.NPI = String.IsNullOrEmpty(dataRow["NPINumber"].ToString()) ? null : dataRow["NPINumber"].ToString();
                        tempProviderDTO.SSN = String.IsNullOrEmpty(dataRow["SocialSecurityNumber"].ToString()) ? null : EncryptorDecryptor.Decrypt(dataRow["SocialSecurityNumber"].ToString());
                        tempProviderDTO.FirstName = String.IsNullOrEmpty(dataRow["FirstName"].ToString()) ? null : dataRow["FirstName"].ToString();
                        tempProviderDTO.MiddleName = String.IsNullOrEmpty(dataRow["MiddleName"].ToString()) ? null : dataRow["MiddleName"].ToString();
                        tempProviderDTO.LastName = String.IsNullOrEmpty(dataRow["LastName"].ToString()) ? null : dataRow["LastName"].ToString();
                        tempProviderDTO.Type = String.IsNullOrEmpty(dataRow["Type"].ToString()) ? null : dataRow["Type"].ToString();
                        tempProviderDTO.PhoneNumber = String.IsNullOrEmpty(dataRow["PhoneNumber"].ToString()) ? null : dataRow["PhoneNumber"].ToString();
                        tempProviderDTO.FaxNumber = String.IsNullOrEmpty(dataRow["FaxNumber"].ToString()) ? null : dataRow["FaxNumber"].ToString();
                        tempProviderDTO.ContactName = String.IsNullOrEmpty(dataRow["ContactName"].ToString()) ? null : dataRow["ContactName"].ToString();
                        tempProviderDTO.Speciality = String.IsNullOrEmpty(dataRow["SpecialtyName"].ToString()) ? null : dataRow["SpecialtyName"].ToString();
                        providerDTO.Add(tempProviderDTO);
                    }
                }
                return providerDTO.AsEnumerable<ProviderDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProfileAndPlanDTO> getAllProviderAndPalns()
        {
            DataTable newTable = new DataTable();
            List<ProfileAndPlanDTO> ProvidersAndPlans = new List<ProfileAndPlanDTO>();
            using (SqlConnection dBConnection = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
            {
                using (SqlCommand dBCommand = dBConnection.CreateCommand())
                {
                    dBCommand.CommandText = AdoQueries.PlanAndProvidersService_QUERY;
                    newTable = ADORepository.GetData(dBCommand);
                }
                foreach (DataRow dataRow in newTable.Rows)
                {
                    var tempProviderPlanDTO = new ProfileAndPlanDTO();
                    tempProviderPlanDTO.NPINumber = dataRow["NPINumber"].ToString();
                    tempProviderPlanDTO.FirstName = dataRow["FirstName"].ToString();
                    tempProviderPlanDTO.MiddleName = dataRow["MiddleName"].ToString();
                    tempProviderPlanDTO.LastName = dataRow["LastName"].ToString();

                    tempProviderPlanDTO.PlanNames = dataRow["PlanName"].ToString().Split(',').ToList();

                    ProvidersAndPlans.Add(tempProviderPlanDTO); 
                }
            }
                     
            return ProvidersAndPlans.AsEnumerable<ProfileAndPlanDTO>();
        }
      
           
      public List<ProviderDTOForUM> NewUMService()
        {
            
            string Query = @"SELECT  [First_Name] as FirstName ,[NPI],[Gender] ,[Languages]
      ,[Middle_Name]  as MiddleName
      ,[Last_Name] as LastName
      ,[Practitioner_Type]
      ,[Location]
      ,[Address]
      ,[City]
      ,[County]
      ,[State]
      ,[Zip]
      ,[Phone]
      ,[Fax]
      ,[In_Directory]
      ,[Accepting_Patients]
      
      
 
     
      ,[Group_Tax_ID]
      ,[Specialty]
      ,[Practice_As] from [UltimateProviders] ORDER BY [NPI]";
            List<ProviderDTOForUM> providers = new List<ProviderDTOForUM>();
            ProviderDTOForUM provider = null;
           
            using(IDbConnection connection=new SqlConnection(ConfigurationManager.ConnectionStrings["UMService"].ConnectionString.ToString()) )
            {
              
               var resultset= connection.Query<ProviderDTOForUM, LocationsDTO, FacilityDTO, SpecialtyDTO, ProviderDTOForUM>(Query,
                    (Demographics, Location, Facility, Speciality) =>
                    {
                        if ((!providers.Any(x => x.NPI == Demographics.NPI)))
                        {
                            providers.Add(Demographics);
                            //provider = Demographics;
                        }
                        if ( (!providers.Any(x => x.NPI == Demographics.NPI && x.ProviderPracticeLocationAddress.Any(y => y.Location == Location.Location))))
                        {
                            providers.Find(x => x.NPI == Demographics.NPI).ProviderPracticeLocationAddress.Add(Location);
                            // provider.Locations=Location;
                            //provider.Locations.Add(Location);
                        }
                        if (  (!providers.Any(x => x.NPI == Demographics.NPI && x.ProviderPracticeLocationAddress.Any(y => y.Location == Location.Location && x.ProviderPracticeLocationAddress.Any(w => w.Facility.Any(z => z.Address == Facility.Address))))))
                        {
                            providers.Find(x => x.NPI == Demographics.NPI).ProviderPracticeLocationAddress.FirstOrDefault(y => y.Location == Location.Location).Facility.Add(Facility);
                            //provider.Locations.Find(x=>x.Location==Location.Location).Facility.Add(Facility);
                        }
                        //if (!providers.Any(x => x.NPI == Demographics.NPI && x.Locations.Any(y => y.Location == Location.Location && x.Locations.Any(w => w.Facility.Any(z => z.Address == Facility.Address))) && x.Locations.Any(y => y.Facility.Any(z => z.Speciality.Any(w => w.Specialty == Speciality.Specialty)))))
                        if(!providers.Any(x=>x.NPI==Demographics.NPI && x.ProviderPracticeLocationAddress.Any(y=>y.Location==Location.Location && y.Facility.Any(z=>z.Address==Facility.Address && z.Speciality.Any(w=>w.Specialty==Speciality.Specialty)))))
                        {
                            providers.Find(x => x.NPI == Demographics.NPI).ProviderPracticeLocationAddress.Find(y => y.Location == Location.Location).Facility.Find(z => z.Address == Facility.Address).Speciality.Add(Speciality);
                            //provider.Locations.Find(x=>x.Location==Location.Location).Facility.Find(z=>z.Address==Facility.Address).Speciality.Add(Speciality);
                        }
                        return provider;
                    },
                    splitOn: "Location,Address,Specialty"
                    ).ToList();
             
               return providers;

            }
        }
      public List<ProviderDetailsDTO> GetAllProviders1()
      {
          
          try
          {
              List<ProviderDetailsDTO> Result = new List<ProviderDetailsDTO>();
              string Query = @"select distinct FirstName,LastName,MiddleName,oin.NPInumber as NPI,s.Name as Speciality,sli.LicenseNumber as SLN,f.MobileNumber,f.FaxNumber,
                            pld.PracticeLocationCorporateName as PracticeLocationName,f.Building,f.City,f.Country,f.County,f.EmailAddress as EmailAddress
                             from Profiles p 
                            left join 
                            OtherIdentificationNumbers oin on
                            p.OtherIdentificationNumber_OtherIdentificationNumberID=oin.OtherIdentificationNumberID 
                            left join
                            ContactDetails cd on p.ContactDetail_ContactDetailID=cd.ContactDetailID 
                            left join
                            PhoneDetails phd on phd.ContactDetail_ContactDetailID=cd.ContactDetailID
                            left join 
                            SpecialtyDetails sd on sd.Profile_ProfileID=p.ProfileID
                            left join 
                            Specialties s on sd.SpecialtyID=s.SpecialtyID
                            left join 
                            PersonalDetails pd on p.PersonalDetail_PersonalDetailID=pd.PersonalDetailID
                            left join 
                            PersonalIdentifications pid on p.PersonalIdentification_PersonalIdentificationID=pid.PersonalIdentificationID
                            left join 
                            PracticeLocationDetails pld on pld.Profile_ProfileID=p.ProfileID
                            left join 
                            Facilities f on f.FacilityID=pld.FacilityId 
							left join 
							(select LicenseNumber,Profile_ProfileID from [dbo].[StateLicenseInformations] where [Status]='Active' and  
                            [StateLicenseStatusID] in (select [StateLicenseStatusID] from StateLicenseStatus where [Status]='Active')) as  sli on p.ProfileID=sli.[Profile_ProfileID]
						      
							
						 ";

             
              ProviderDetailsDTO Provider = null;
              Stopwatch st=new System.Diagnostics.Stopwatch();
              st.Start();
              using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString))
              {

                  var resultset = connection.Query<ProviderDetailsDTO, PracticeLocationDTO, ProviderDetailsDTO>(Query,
                       (Demographics, Location) =>
                       {
                           if (!Result.Any(x => x.NPI == Demographics.NPI))
                           {

                               Result.Add(Demographics);
                           }
                           if (Result.Any(x => x.NPI == Demographics.NPI  && !(x.StateLicenseNumbers.Contains(Demographics.SLN))) &&  Demographics.SLN!=null)
                           {
                               Result.Find(x => x.NPI == Demographics.NPI).StateLicenseNumbers.Add(Demographics.SLN);
                           }
                           System.Diagnostics.Debug.WriteLine(Result);
                           if (Result.Any(x => x.NPI == Demographics.NPI  && x.PracticeLocations.Any(y=>y.PracticeLocationName==Location.PracticeLocationName)))
                           {
                              
                           }
                          
                           return Provider;
                       }, splitOn: "MobileNumber"
                       ).ToList();
                  long Elapsed = st.ElapsedMilliseconds;
                  return Result;
                  
              }
         
              
          }

          
          catch (Exception e)
          {
              throw;
          }
        
          
      }
       



        public int GetProviderCountByProviderLevel(Entities.MasterData.Enums.ProviderLevelEnum level)
        {
            
            int providerCount=0;

            string Query = @"";

            switch (level)
            {
                case AHC.CD.Entities.MasterData.Enums.ProviderLevelEnum.ALL:

                    Query = AHC.CD.Resources.DatabaseQueries.AdoQueries.PROVIDERSERVICE_ALLACTIVEPROVIDERS;
                    break;
                case AHC.CD.Entities.MasterData.Enums.ProviderLevelEnum.PCP:
                    Query=AHC.CD.Resources.DatabaseQueries.AdoQueries.PROVIDERSERVICE_ALLACTIVEPCP;
                    break;
                case AHC.CD.Entities.MasterData.Enums.ProviderLevelEnum.NURSE:
                    Query = AHC.CD.Resources.DatabaseQueries.AdoQueries.PROVIDERSERVICE_ALLACTIVENURSE;
                    break;
                case AHC.CD.Entities.MasterData.Enums.ProviderLevelEnum.MIDLEVEL:
                    Query = AHC.CD.Resources.DatabaseQueries.AdoQueries.PROVIDERSERVICE_ALLMIDLEVEL;
                    break;
                //default:

                //    Query = AHC.CD.Resources.DatabaseQueries.AdoQueries.PROVIDERSERVICE_ALLACTIVEPROVIDERS;
                   
            }

            DataTable table = new DataTable();
           
            using (SqlConnection conn = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = Query;
                    table = ADORepository.GetData(cmd);
                }
                foreach (DataRow row in table.Rows)
                {
                    providerCount = int.Parse(row["totalCount"].ToString());
                }
            }
             
               return providerCount;
        }
    }

}

