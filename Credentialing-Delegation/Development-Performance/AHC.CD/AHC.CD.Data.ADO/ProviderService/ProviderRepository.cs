using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Resources.DatabaseQueries;
using AHC.UtilityService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }

}

