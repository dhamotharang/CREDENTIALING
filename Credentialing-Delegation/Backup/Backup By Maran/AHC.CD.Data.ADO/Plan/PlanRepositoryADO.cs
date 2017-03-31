using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Data.ADO.DTO.Plan;
using AHC.CD.Resources.DatabaseQueries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Plan
{
    internal class PlanRepositoryADO : IPlanRepositoryADO
    {
        public PlanRepositoryADO()
        {

        }
        public async Task<object> GetAllPlans()
        {
            try
            {
                DataTable dataTable = new DataTable();
                List<PlanDTO> planDTO = new List<PlanDTO>();
                using (SqlConnection dBConnection = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
                {
                    using (var dBCommand = dBConnection.CreateCommand())
                    {
                        dBCommand.CommandText = AdoQueries.PLANS_QUERY;
                        dataTable = ADORepository.GetData(dBCommand);
                    }
                    
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        var tempPlanDTO = new PlanDTO();
                        tempPlanDTO.PlanID = Convert.ToInt32(dataRow["PlanID"].ToString());
                        tempPlanDTO.PlanName = dataRow["PlanName"].ToString();
                        tempPlanDTO.PlanLogo = dataRow["PlanLogoPath"].ToString();
                        tempPlanDTO.PlanContactPersonName = dataRow["ContactPersonName"].ToString();
                        tempPlanDTO.PlanEmailID = dataRow["EmailAddress"].ToString();
                        tempPlanDTO.PlanAddress = dataRow["PlanAddress"].ToString();
                        tempPlanDTO.PlanStatus = dataRow["PlanStatus"].ToString();
                        planDTO.Add(tempPlanDTO);
                    }
                }
                return new
                {
                    ActivePlanRecords = planDTO.Where(a => a.PlanStatus == "Active").ToList(),
                    InactivePlanRecords = planDTO.Where(a => a.PlanStatus == "Inactive").ToList()
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public async Task<PlanDTO> GetPlanDataByIDAsync(int PlanID)
        {
            try
            {
                DataTable dataTable = new DataTable();
                List<PlanDTO> planDTO = new List<PlanDTO>();
                using (SqlConnection dBConnection = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
                {
                    using (var dBCommand = dBConnection.CreateCommand())
                    {
                        dBCommand.CommandText = AdoQueries.PLANS_QUERY_BYID;
                        SqlParameter PlanIDParameter = new SqlParameter();
                        PlanIDParameter.ParameterName = "@PlanID";
                        PlanIDParameter.Value = PlanID;
                        dBCommand.Parameters.Add(PlanIDParameter);
                        dataTable = ADORepository.GetData(dBCommand);
                    }

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        var tempPlanDTO = new PlanDTO();
                        tempPlanDTO.PlanID = Convert.ToInt32(dataRow["PlanID"].ToString());
                        tempPlanDTO.PlanName = dataRow["PlanName"].ToString();
                        tempPlanDTO.PlanLogo = dataRow["PlanLogoPath"].ToString();
                        tempPlanDTO.PlanContactPersonName = dataRow["ContactPersonName"].ToString();
                        tempPlanDTO.PlanEmailID = dataRow["EmailAddress"].ToString();
                        tempPlanDTO.PlanAddress = dataRow["PlanAddress"].ToString();
                        tempPlanDTO.PlanStatus = dataRow["PlanStatus"].ToString();
                        planDTO.Add(tempPlanDTO);
                    }
                }
                return planDTO.FirstOrDefault(a => a.PlanStatus == "Active");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
