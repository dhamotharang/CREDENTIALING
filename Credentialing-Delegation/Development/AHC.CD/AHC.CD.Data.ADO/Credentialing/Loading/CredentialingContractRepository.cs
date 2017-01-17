using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Data.ADO.DTO;
using AHC.CD.Entities.Credentialing.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Credentialing.Loading
{
    public class CredentialingContractRepository : ICredentialingContractRepository
    {

        public CredentialingContractRepository()
        {

        }

        public async Task<IEnumerable<AHC.CD.Data.ADO.DTO.ContractGridDTO>> GetAllContractGrid()
        {
            DataTable dt = new DataTable();
            List<AHC.CD.Data.ADO.DTO.ContractGridDTO> ContractGridDTOList = new List<AHC.CD.Data.ADO.DTO.ContractGridDTO>();

            using (SqlConnection conn = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
Select ureca.[ProfileID],ureca.[FirstName],ureca.[MiddleName],ureca.[LastName],ureca.[PlanName],ureca.BE, ureca.[LOBCode],ureca.[LOBName],
ureca.[ContractGridID],ureca.[PracticeLocationCorporateName],ureca.[ContractGridStatus],ureca.[ProviderID],ureca.[TerminationDate],ureca.PanelStatus,ureca.[ParticipatingStatus],ureca.[InitiatedDate],
ureca.[GroupID],ureca.[Report_CredentialingContractInfoFromPlanID],ureca.[Status],NPI.[NPINumber]  
from (select prt.[OtherIdentificationNumber_OtherIdentificationNumberID], prodata.[ProfileID],prodata.[FirstName],
prodata.[MiddleName],prodata.[LastName],prodata.[PlanName],prodata.BE, prodata.[LOBCode],prodata.[LOBName],prodata.[PracticeLocationCorporateName],prodata.[ContractGridID],prodata.[ContractGridStatus],
prodata.[ProviderID],prodata.[TerminationDate],prodata.[ParticipatingStatus],prodata.PanelStatus,prodata.[InitiatedDate],prodata.[GroupID],
prodata.[Report_CredentialingContractInfoFromPlanID],prodata.[Status]   
from (SELECT sdf.[ProfileID],sdf.[FirstName],sdf.[MiddleName],sdf.[LastName],sdf.[PlanName],Groups.[Name] as BE, 
sdf.[LOBCode],sdf.[LOBName],sdf.[PracticeLocationCorporateName],sdf.[ContractGridID],sdf.[ContractGridStatus],sdf.[ProviderID],sdf.[TerminationDate],sdf.PanelStatus,sdf.[ParticipatingStatus],sdf.[InitiatedDate],
sdf.[GroupID],sdf.[Report_CredentialingContractInfoFromPlanID],sdf.[Status]  FROM (SELECT we.[LOBCode],we.[LOBName],ty.[ProfileID],
ty.[BusinessEntityID],ty.[PracticeLocationCorporateName],ty.[ContractGridID],ty.[ContractGridStatus],ty.[ProviderID],ty.[TerminationDate],ty.PanelStatus,ty.[ParticipatingStatus],ty.[InitiatedDate],
ty.[GroupID],ty.[Status],ty.[Report_CredentialingContractInfoFromPlanID],ty.[FirstName],ty.[MiddleName],ty.[LastName],
ty.[PlanName] FROM (SELECT Report.[BusinessEntityID],Report.[LOBID],Report.[PracticeLocationCorporateName],Report.[ContractGridID],Report.[ContractGridStatus],CR.[ProviderID],
CR.[TerminationDate],
CR.[ParticipatingStatus],CR.PanelStatus,CR.[InitiatedDate],CR.[GroupID],Report.[Report_CredentialingContractInfoFromPlanID],
Report.[ProfileID],Report.[FirstName],Report.[MiddleName],Report.[LastName],Report.[PlanName],Report.[Status] FROM 
(SELECT m.[ContractGridID],m.[ContractGridStatus],m.[Status],m.[LOBID],m.[BusinessEntityID],m.[Report_CredentialingContractInfoFromPlanID],
m.[CredentialingInfoID],CREDETAIL.[ProfileID],CREDETAIL.[FirstName],CREDETAIL.[MiddleName],CREDETAIL.[LastName], 
CREDETAIL.[PlanName],pld.[PracticeLocationCorporateName] FROM 
[ContractGrids] as m 
LEFT JOIN
[dbo].[PracticeLocationDetails] as pld
ON
pld.[PracticeLocationDetailID]=m.[ProfilePracticeLocationID]

LEFT JOIN
(SELECT b.[FirstName],b.[MiddleName],b.[LastName], a.[PlanName],a.[CredentialingInfoID],a.[ProfileID] FROM
(SELECT k.[PersonalDetail_PersonalDetailID],l.[PlanName],l.[CredentialingInfoID],l.[ProfileID] FROM 
(SELECT w.[ProfileID],r.[PlanName],w.[CredentialingInfoID] 
FROM (SELECT p.[PlanID],p.[ProfileID],q.[CredentialingInfoID] 
FROM (SELECT DISTINCT [CredentialingInfoID] FROM 
[ContractGrids])as q 
LEFT JOIN   [CredentialingInfoes] as p ON 
q.[CredentialingInfoID] =p.[CredentialingInfoID]) as w LEFT JOIN [Plans] as
r ON w.[PlanID]=r.[PlanID]) as l LEFT JOIN [Profiles] as k ON l.[ProfileID]=k.[ProfileID]) as a LEFT JOIN [PersonalDetails] as b 
ON a.[PersonalDetail_PersonalDetailID]=b.[PersonalDetailID]) as CREDETAIL 
ON m.[CredentialingInfoID]=CREDETAIL.[CredentialingInfoID]) as Report LEFT JOIN [CredentialingContractInfoFromPlans] as CR 
ON Report.[Report_CredentialingContractInfoFromPlanID]=CR.[CredentialingContractInfoFromPlanID]) as ty LEFT JOIN [LOBs] as we 
ON ty.[LOBID]=we.[LOBID]) as sdf LEFT JOIN [Groups] as Groups ON sdf.[BusinessEntityID]=Groups.[GroupID]) as prodata 
left join [Profiles] as prt  on prodata.[ProfileID]=prt.[ProfileID]) as ureca left join  [OtherIdentificationNumbers] as NPI 
on ureca.[OtherIdentificationNumber_OtherIdentificationNumberID]= NPI.[OtherIdentificationNumberID]";



                    //cmd.CommandText = @"select cg.ContractGridID, l.LOBName, ci.CredentialingInfoID, p.PlanName, pl.CurrentlyPracticingAtThisAddress  from ContractGrids cg join LOBs l on cg.LOBID==l.LOBID join PracticeLocationDetails pl on pl.PracticeLocationDetailID=cg.PracticeLocationDetailID join CredentialingInfos ci on ci.CredentialingInfoID==cg.CredentialingInfoID";
                    dt = ADORepository.GetData(cmd);
                }
                try
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        AHC.CD.Data.ADO.DTO.ContractGridDTO ContractGridDTOObj = new AHC.CD.Data.ADO.DTO.ContractGridDTO();
                        ContractGridDTOObj.ContractGridID = int.Parse(data["ContractGridID"].ToString());
                        ContractGridDTOObj.PlanName = data["PlanName"].ToString();
                        ContractGridDTOObj.ProviderFirstName = data["FirstName"].ToString();
                        ContractGridDTOObj.ProviderMiddleName = data["MiddleName"].ToString();
                        ContractGridDTOObj.ProviderLastName = data["LastName"].ToString();
                        ContractGridDTOObj.PlanName = data["PlanName"].ToString();
                        ContractGridDTOObj.LOBCode = data["LOBCode"].ToString();
                        ContractGridDTOObj.LOBName = data["LOBName"].ToString();
                        ContractGridDTOObj.ProviderID = data["ProviderID"].ToString();
                        ContractGridDTOObj.ParticipatingStatus = data["ParticipatingStatus"].ToString();
                        ContractGridDTOObj.TerminationDate = data["TerminationDate"].ToString();
                        ContractGridDTOObj.InitiatedDate = data["InitiatedDate"].ToString();
                        ContractGridDTOObj.GroupID = data["GroupID"].ToString();
                        ContractGridDTOObj.CredentialingContractInfoPlanID = data["Report_CredentialingContractInfoFromPlanID"].ToString();
                        ContractGridDTOObj.Status = data["Status"].ToString();
                        ContractGridDTOObj.ContractGridStatus = data["ContractGridStatus"].ToString();
                        ContractGridDTOObj.ProfileID = data["ProfileID"].ToString();
                        ContractGridDTOObj.NPINumber = data["NPINumber"].ToString();
                        ContractGridDTOObj.ProviderName = ContractGridDTOObj.ProviderFirstName + " " + ContractGridDTOObj.ProviderMiddleName.Trim() + " " + ContractGridDTOObj.ProviderLastName;
                        ContractGridDTOObj.PracticeLocationCorporateName = data["PracticeLocationCorporateName"].ToString();
                        ContractGridDTOObj.PanelStatus = data["PanelStatus"].ToString();
                        //ContractGridDTOObj.InitiatedDate = ContractGridDTOObj.InitiatedDate == "" ? ContractGridDTOObj.InitiatedDate : ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[1] + "-" + ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[0] + "-" + ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[2];
                        //ContractGridDTOObj.TerminationDate = ContractGridDTOObj.TerminationDate == "" ? ContractGridDTOObj.TerminationDate : ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[1] + "-" + ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[0] + "-" + ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[2];
                        ContractGridDTOList.Add(ContractGridDTOObj);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                var FilteredData = new List<AHC.CD.Data.ADO.DTO.ContractGridDTO>();
                FilteredData = ContractGridDTOList.Where(x => x.ContractGridStatus == "Active" && x.Status == "Active").ToList();
                return FilteredData;
            }
        }

        public List<ContractGridForProfileDTO> GetAllActiveContractGridByID(int ProfileID)
        {
            DataTable dt = new DataTable();
            List<ContractGridForProfileDTO> ContractGridDTOList = new List<ContractGridForProfileDTO>();

            using (SqlConnection conn = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"With c as(
Select ureca.[ProfileID],ureca.[FirstName],ureca.[MiddleName],ureca.[LastName],ureca.[PlanName],ureca.BE, ureca.[LOBCode],ureca.[LOBName],
ureca.[ContractGridID],ureca.[PracticeLocationCorporateName],ureca.[ContractGridStatus],ureca.[ProviderID],ureca.[TerminationDate],ureca.PanelStatus,ureca.[ParticipatingStatus],ureca.[InitiatedDate],
ureca.[GroupID],ureca.[Report_CredentialingContractInfoFromPlanID],ureca.[Status],NPI.[NPINumber]  
from (select prt.[OtherIdentificationNumber_OtherIdentificationNumberID], prodata.[ProfileID],prodata.[FirstName],
prodata.[MiddleName],prodata.[LastName],prodata.[PlanName],prodata.BE, prodata.[LOBCode],prodata.[LOBName],prodata.[PracticeLocationCorporateName],prodata.[ContractGridID],prodata.[ContractGridStatus],
prodata.[ProviderID],prodata.[TerminationDate],prodata.[ParticipatingStatus],prodata.PanelStatus,prodata.[InitiatedDate],prodata.[GroupID],
prodata.[Report_CredentialingContractInfoFromPlanID],prodata.[Status]   
from (SELECT sdf.[ProfileID],sdf.[FirstName],sdf.[MiddleName],sdf.[LastName],sdf.[PlanName],Groups.[Name] as BE, 
sdf.[LOBCode],sdf.[LOBName],sdf.[PracticeLocationCorporateName],sdf.[ContractGridID],sdf.[ContractGridStatus],sdf.[ProviderID],sdf.[TerminationDate],sdf.PanelStatus,sdf.[ParticipatingStatus],sdf.[InitiatedDate],
sdf.[GroupID],sdf.[Report_CredentialingContractInfoFromPlanID],sdf.[Status]  FROM (SELECT we.[LOBCode],we.[LOBName],ty.[ProfileID],
ty.[BusinessEntityID],ty.[PracticeLocationCorporateName],ty.[ContractGridID],ty.[ContractGridStatus],ty.[ProviderID],ty.[TerminationDate],ty.PanelStatus,ty.[ParticipatingStatus],ty.[InitiatedDate],
ty.[GroupID],ty.[Status],ty.[Report_CredentialingContractInfoFromPlanID],ty.[FirstName],ty.[MiddleName],ty.[LastName],
ty.[PlanName] FROM (SELECT Report.[BusinessEntityID],Report.[LOBID],Report.[PracticeLocationCorporateName],Report.[ContractGridID],Report.[ContractGridStatus],CR.[ProviderID],
CR.[TerminationDate],
CR.[ParticipatingStatus],CR.PanelStatus,CR.[InitiatedDate],CR.[GroupID],Report.[Report_CredentialingContractInfoFromPlanID],
Report.[ProfileID],Report.[FirstName],Report.[MiddleName],Report.[LastName],Report.[PlanName],Report.[Status] FROM 
(SELECT m.[ContractGridID],m.[ContractGridStatus],m.[Status],m.[LOBID],m.[BusinessEntityID],m.[Report_CredentialingContractInfoFromPlanID],
m.[CredentialingInfoID],CREDETAIL.[ProfileID],CREDETAIL.[FirstName],CREDETAIL.[MiddleName],CREDETAIL.[LastName], 
CREDETAIL.[PlanName],pld.[PracticeLocationCorporateName] FROM 
[ContractGrids] as m 
LEFT JOIN
[dbo].[PracticeLocationDetails] as pld
ON
pld.[PracticeLocationDetailID]=m.[ProfilePracticeLocationID]

LEFT JOIN
(SELECT b.[FirstName],b.[MiddleName],b.[LastName], a.[PlanName],a.[CredentialingInfoID],a.[ProfileID] FROM
(SELECT k.[PersonalDetail_PersonalDetailID],l.[PlanName],l.[CredentialingInfoID],l.[ProfileID] FROM 
(SELECT w.[ProfileID],r.[PlanName],w.[CredentialingInfoID] 
FROM (SELECT p.[PlanID],p.[ProfileID],q.[CredentialingInfoID] 
FROM (SELECT DISTINCT [CredentialingInfoID] FROM 
[ContractGrids])as q 
LEFT JOIN   [CredentialingInfoes] as p ON 
q.[CredentialingInfoID] =p.[CredentialingInfoID]) as w LEFT JOIN [Plans] as
r ON w.[PlanID]=r.[PlanID]) as l LEFT JOIN [Profiles] as k ON l.[ProfileID]=k.[ProfileID]) as a LEFT JOIN [PersonalDetails] as b 
ON a.[PersonalDetail_PersonalDetailID]=b.[PersonalDetailID]) as CREDETAIL 
ON m.[CredentialingInfoID]=CREDETAIL.[CredentialingInfoID]) as Report LEFT JOIN [CredentialingContractInfoFromPlans] as CR 
ON Report.[Report_CredentialingContractInfoFromPlanID]=CR.[CredentialingContractInfoFromPlanID]) as ty LEFT JOIN [LOBs] as we 
ON ty.[LOBID]=we.[LOBID]) as sdf LEFT JOIN [Groups] as Groups ON sdf.[BusinessEntityID]=Groups.[GroupID]) as prodata 
left join [Profiles] as prt  on prodata.[ProfileID]=prt.[ProfileID]) as ureca left join  [OtherIdentificationNumbers] as NPI 
on ureca.[OtherIdentificationNumber_OtherIdentificationNumberID]= NPI.[OtherIdentificationNumberID])
select * from c where ProfileID=@profileid";



                    //cmd.CommandText = @"select cg.ContractGridID, l.LOBName, ci.CredentialingInfoID, p.PlanName, pl.CurrentlyPracticingAtThisAddress  from ContractGrids cg join LOBs l on cg.LOBID==l.LOBID join PracticeLocationDetails pl on pl.PracticeLocationDetailID=cg.PracticeLocationDetailID join CredentialingInfos ci on ci.CredentialingInfoID==cg.CredentialingInfoID";
                    cmd.Parameters.Add(new SqlParameter("@profileid", ProfileID));
                    dt = ADORepository.GetData(cmd);
                }
                try
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        ContractGridForProfileDTO ContractGridDTOObj = new ContractGridForProfileDTO();
                        ContractGridDTOObj.ContractGridID = int.Parse(data["ContractGridID"].ToString());
                        ContractGridDTOObj.PlanName = data["PlanName"].ToString();

                        ContractGridDTOObj.PlanName = data["PlanName"].ToString();
                        ContractGridDTOObj.LOBCode = data["LOBCode"].ToString();
                        ContractGridDTOObj.LOBName = data["LOBName"].ToString();
                        ContractGridDTOObj.IndividualID = data["ProviderID"].ToString();
                        ContractGridDTOObj.ParticiPationStatus = data["ParticipatingStatus"].ToString();
                        ContractGridDTOObj.TerminationDate = data["TerminationDate"].ToString().Split(' ')[0];
                        ContractGridDTOObj.InitiatedDate = data["InitiatedDate"].ToString().Split(' ')[0];
                        ContractGridDTOObj.EffectiveDate = data["InitiatedDate"].ToString().Split(' ')[0];
                        ContractGridDTOObj.GroupID = data["GroupID"].ToString();
                        ContractGridDTOObj.CredentialingContractInfoPlanID = data["Report_CredentialingContractInfoFromPlanID"].ToString();
                        ContractGridDTOObj.Status = data["Status"].ToString();
                        ContractGridDTOObj.ContractGridStatus = data["ContractGridStatus"].ToString();
                        ContractGridDTOObj.ProfileID = data["ProfileID"].ToString();
                        ContractGridDTOObj.PracticeLocationCorporateName = data["PracticeLocationCorporateName"].ToString();
                        ContractGridDTOObj.PanelStatus = data["PanelStatus"].ToString();
                        ContractGridDTOObj.NPINumber = data["NPINumber"].ToString();
                        //   ContractGridDTOObj.InitiatedDate = ContractGridDTOObj.InitiatedDate == "" ? ContractGridDTOObj.InitiatedDate : ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[1]+"-"+ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[0]+"-"+ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[2];
                        // ContractGridDTOObj.TerminationDate = ContractGridDTOObj.TerminationDate == "" ? ContractGridDTOObj.TerminationDate : ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[1] + "-" + ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[0] + "-" + ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[2];
                        ContractGridDTOList.Add(ContractGridDTOObj);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                var FilteredData = new List<ContractGridForProfileDTO>();
                FilteredData = ContractGridDTOList.Where(x => x.ContractGridStatus == "Active" && x.Status == "Active").ToList();
                return FilteredData;
            }
        }




        public async Task<IEnumerable<AHC.CD.Data.ADO.DTO.ContractGridDTO>> GetAllInactiveContractGrid()
        {
            DataTable dt = new DataTable();
            List<AHC.CD.Data.ADO.DTO.ContractGridDTO> ContractGridDTOList = new List<AHC.CD.Data.ADO.DTO.ContractGridDTO>();

            using (SqlConnection conn = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
Select ureca.[ProfileID],ureca.[FirstName],ureca.[MiddleName],ureca.[LastName],ureca.[PlanName],ureca.BE, ureca.[LOBCode],ureca.[LOBName],
ureca.[ContractGridID],ureca.[PracticeLocationCorporateName],ureca.[ContractGridStatus],ureca.[ProviderID],ureca.[TerminationDate],ureca.PanelStatus,ureca.[ParticipatingStatus],ureca.[InitiatedDate],
ureca.[GroupID],ureca.[Report_CredentialingContractInfoFromPlanID],ureca.[Status],NPI.[NPINumber]  
from (select prt.[OtherIdentificationNumber_OtherIdentificationNumberID], prodata.[ProfileID],prodata.[FirstName],
prodata.[MiddleName],prodata.[LastName],prodata.[PlanName],prodata.BE, prodata.[LOBCode],prodata.[LOBName],prodata.[PracticeLocationCorporateName],prodata.[ContractGridID],prodata.[ContractGridStatus],
prodata.[ProviderID],prodata.[TerminationDate],prodata.[ParticipatingStatus],prodata.PanelStatus,prodata.[InitiatedDate],prodata.[GroupID],
prodata.[Report_CredentialingContractInfoFromPlanID],prodata.[Status]   
from (SELECT sdf.[ProfileID],sdf.[FirstName],sdf.[MiddleName],sdf.[LastName],sdf.[PlanName],Groups.[Name] as BE, 
sdf.[LOBCode],sdf.[LOBName],sdf.[PracticeLocationCorporateName],sdf.[ContractGridID],sdf.[ContractGridStatus],sdf.[ProviderID],sdf.[TerminationDate],sdf.PanelStatus,sdf.[ParticipatingStatus],sdf.[InitiatedDate],
sdf.[GroupID],sdf.[Report_CredentialingContractInfoFromPlanID],sdf.[Status]  FROM (SELECT we.[LOBCode],we.[LOBName],ty.[ProfileID],
ty.[BusinessEntityID],ty.[PracticeLocationCorporateName],ty.[ContractGridID],ty.[ContractGridStatus],ty.[ProviderID],ty.[TerminationDate],ty.PanelStatus,ty.[ParticipatingStatus],ty.[InitiatedDate],
ty.[GroupID],ty.[Status],ty.[Report_CredentialingContractInfoFromPlanID],ty.[FirstName],ty.[MiddleName],ty.[LastName],
ty.[PlanName] FROM (SELECT Report.[BusinessEntityID],Report.[LOBID],Report.[PracticeLocationCorporateName],Report.[ContractGridID],Report.[ContractGridStatus],CR.[ProviderID],
CR.[TerminationDate],
CR.[ParticipatingStatus],CR.PanelStatus,CR.[InitiatedDate],CR.[GroupID],Report.[Report_CredentialingContractInfoFromPlanID],
Report.[ProfileID],Report.[FirstName],Report.[MiddleName],Report.[LastName],Report.[PlanName],Report.[Status] FROM 
(SELECT m.[ContractGridID],m.[ContractGridStatus],m.[Status],m.[LOBID],m.[BusinessEntityID],m.[Report_CredentialingContractInfoFromPlanID],
m.[CredentialingInfoID],CREDETAIL.[ProfileID],CREDETAIL.[FirstName],CREDETAIL.[MiddleName],CREDETAIL.[LastName], 
CREDETAIL.[PlanName],pld.[PracticeLocationCorporateName] FROM 
[ContractGrids] as m 
LEFT JOIN
[dbo].[PracticeLocationDetails] as pld
ON
pld.[PracticeLocationDetailID]=m.[ProfilePracticeLocationID]

LEFT JOIN
(SELECT b.[FirstName],b.[MiddleName],b.[LastName], a.[PlanName],a.[CredentialingInfoID],a.[ProfileID] FROM
(SELECT k.[PersonalDetail_PersonalDetailID],l.[PlanName],l.[CredentialingInfoID],l.[ProfileID] FROM 
(SELECT w.[ProfileID],r.[PlanName],w.[CredentialingInfoID] 
FROM (SELECT p.[PlanID],p.[ProfileID],q.[CredentialingInfoID] 
FROM (SELECT DISTINCT [CredentialingInfoID] FROM 
[ContractGrids])as q 
LEFT JOIN   [CredentialingInfoes] as p ON 
q.[CredentialingInfoID] =p.[CredentialingInfoID]) as w LEFT JOIN [Plans] as
r ON w.[PlanID]=r.[PlanID]) as l LEFT JOIN [Profiles] as k ON l.[ProfileID]=k.[ProfileID]) as a LEFT JOIN [PersonalDetails] as b 
ON a.[PersonalDetail_PersonalDetailID]=b.[PersonalDetailID]) as CREDETAIL 
ON m.[CredentialingInfoID]=CREDETAIL.[CredentialingInfoID]) as Report LEFT JOIN [CredentialingContractInfoFromPlans] as CR 
ON Report.[Report_CredentialingContractInfoFromPlanID]=CR.[CredentialingContractInfoFromPlanID]) as ty LEFT JOIN [LOBs] as we 
ON ty.[LOBID]=we.[LOBID]) as sdf LEFT JOIN [Groups] as Groups ON sdf.[BusinessEntityID]=Groups.[GroupID]) as prodata 
left join [Profiles] as prt  on prodata.[ProfileID]=prt.[ProfileID]) as ureca left join  [OtherIdentificationNumbers] as NPI 
on ureca.[OtherIdentificationNumber_OtherIdentificationNumberID]= NPI.[OtherIdentificationNumberID]";
                    //cmd.CommandText = @"select cg.ContractGridID, l.LOBName, ci.CredentialingInfoID, p.PlanName, pl.CurrentlyPracticingAtThisAddress  from ContractGrids cg join LOBs l on cg.LOBID==l.LOBID join PracticeLocationDetails pl on pl.PracticeLocationDetailID=cg.PracticeLocationDetailID join CredentialingInfos ci on ci.CredentialingInfoID==cg.CredentialingInfoID";
                    dt = ADORepository.GetData(cmd);
                }
                try
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        AHC.CD.Data.ADO.DTO.ContractGridDTO ContractGridDTOObj = new AHC.CD.Data.ADO.DTO.ContractGridDTO();
                        ContractGridDTOObj.ContractGridID = int.Parse(data["ContractGridID"].ToString());
                        ContractGridDTOObj.PlanName = data["PlanName"].ToString();
                        ContractGridDTOObj.ProviderFirstName = data["FirstName"].ToString();
                        ContractGridDTOObj.ProviderMiddleName = data["MiddleName"].ToString();
                        ContractGridDTOObj.ProviderLastName = data["LastName"].ToString();
                        ContractGridDTOObj.PlanName = data["PlanName"].ToString();
                        ContractGridDTOObj.LOBCode = data["LOBCode"].ToString();
                        ContractGridDTOObj.LOBName = data["LOBName"].ToString();
                        ContractGridDTOObj.ProviderID = data["ProviderID"].ToString();
                        ContractGridDTOObj.ParticipatingStatus = data["ParticipatingStatus"].ToString();
                        ContractGridDTOObj.TerminationDate = data["TerminationDate"].ToString();
                        ContractGridDTOObj.InitiatedDate = data["InitiatedDate"].ToString();
                        ContractGridDTOObj.GroupID = data["GroupID"].ToString();
                        ContractGridDTOObj.CredentialingContractInfoPlanID = data["Report_CredentialingContractInfoFromPlanID"].ToString();
                        ContractGridDTOObj.Status = data["Status"].ToString();
                        ContractGridDTOObj.ContractGridStatus = data["ContractGridStatus"].ToString();
                        ContractGridDTOObj.ProfileID = data["ProfileID"].ToString();
                        ContractGridDTOObj.NPINumber = data["NPINumber"].ToString();
                        ContractGridDTOObj.PracticeLocationCorporateName = data["PracticeLocationCorporateName"].ToString();
                        ContractGridDTOObj.ProviderName = ContractGridDTOObj.ProviderFirstName + " " + ContractGridDTOObj.ProviderMiddleName + " " + ContractGridDTOObj.ProviderLastName;
                        ContractGridDTOObj.PanelStatus = data["PanelStatus"].ToString();
                        //ContractGridDTOObj.InitiatedDate = ContractGridDTOObj.InitiatedDate == "" ? ContractGridDTOObj.InitiatedDate : ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[1] + "-" + ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[0] + "-" + ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[2];
                        //ContractGridDTOObj.TerminationDate = ContractGridDTOObj.TerminationDate == "" ? ContractGridDTOObj.TerminationDate : ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[1] + "-" + ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[0] + "-" + ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[2];
                        ContractGridDTOList.Add(ContractGridDTOObj);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                var FilteredData = new List<AHC.CD.Data.ADO.DTO.ContractGridDTO>();
                FilteredData = ContractGridDTOList.Where(x => x.ContractGridStatus == "Inactive" && x.Status == "Active").ToList();
                return FilteredData;
            }
        }

        public List<ContractGridForProfileDTO> GetAllInactiveContractGridByID(int ProfileID)
        {
            DataTable dt = new DataTable();
            List<ContractGridForProfileDTO> ContractGridDTOList = new List<ContractGridForProfileDTO>();

            using (SqlConnection conn = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"With c as(
Select ureca.[ProfileID],ureca.[FirstName],ureca.[MiddleName],ureca.[LastName],ureca.[PlanName],ureca.BE, ureca.[LOBCode],ureca.[LOBName],
ureca.[ContractGridID],ureca.[PracticeLocationCorporateName],ureca.[ContractGridStatus],ureca.[ProviderID],ureca.[TerminationDate],ureca.PanelStatus,ureca.[ParticipatingStatus],ureca.[InitiatedDate],
ureca.[GroupID],ureca.[Report_CredentialingContractInfoFromPlanID],ureca.[Status],NPI.[NPINumber]  
from (select prt.[OtherIdentificationNumber_OtherIdentificationNumberID], prodata.[ProfileID],prodata.[FirstName],
prodata.[MiddleName],prodata.[LastName],prodata.[PlanName],prodata.BE, prodata.[LOBCode],prodata.[LOBName],prodata.[PracticeLocationCorporateName],prodata.[ContractGridID],prodata.[ContractGridStatus],
prodata.[ProviderID],prodata.[TerminationDate],prodata.[ParticipatingStatus],prodata.PanelStatus,prodata.[InitiatedDate],prodata.[GroupID],
prodata.[Report_CredentialingContractInfoFromPlanID],prodata.[Status]   
from (SELECT sdf.[ProfileID],sdf.[FirstName],sdf.[MiddleName],sdf.[LastName],sdf.[PlanName],Groups.[Name] as BE, 
sdf.[LOBCode],sdf.[LOBName],sdf.[PracticeLocationCorporateName],sdf.[ContractGridID],sdf.[ContractGridStatus],sdf.[ProviderID],sdf.[TerminationDate],sdf.PanelStatus,sdf.[ParticipatingStatus],sdf.[InitiatedDate],
sdf.[GroupID],sdf.[Report_CredentialingContractInfoFromPlanID],sdf.[Status]  FROM (SELECT we.[LOBCode],we.[LOBName],ty.[ProfileID],
ty.[BusinessEntityID],ty.[PracticeLocationCorporateName],ty.[ContractGridID],ty.[ContractGridStatus],ty.[ProviderID],ty.[TerminationDate],ty.PanelStatus,ty.[ParticipatingStatus],ty.[InitiatedDate],
ty.[GroupID],ty.[Status],ty.[Report_CredentialingContractInfoFromPlanID],ty.[FirstName],ty.[MiddleName],ty.[LastName],
ty.[PlanName] FROM (SELECT Report.[BusinessEntityID],Report.[LOBID],Report.[PracticeLocationCorporateName],Report.[ContractGridID],Report.[ContractGridStatus],CR.[ProviderID],
CR.[TerminationDate],
CR.[ParticipatingStatus],CR.PanelStatus,CR.[InitiatedDate],CR.[GroupID],Report.[Report_CredentialingContractInfoFromPlanID],
Report.[ProfileID],Report.[FirstName],Report.[MiddleName],Report.[LastName],Report.[PlanName],Report.[Status] FROM 
(SELECT m.[ContractGridID],m.[ContractGridStatus],m.[Status],m.[LOBID],m.[BusinessEntityID],m.[Report_CredentialingContractInfoFromPlanID],
m.[CredentialingInfoID],CREDETAIL.[ProfileID],CREDETAIL.[FirstName],CREDETAIL.[MiddleName],CREDETAIL.[LastName], 
CREDETAIL.[PlanName],pld.[PracticeLocationCorporateName] FROM 
[ContractGrids] as m 
LEFT JOIN
[dbo].[PracticeLocationDetails] as pld
ON
pld.[PracticeLocationDetailID]=m.[ProfilePracticeLocationID]

LEFT JOIN
(SELECT b.[FirstName],b.[MiddleName],b.[LastName], a.[PlanName],a.[CredentialingInfoID],a.[ProfileID] FROM
(SELECT k.[PersonalDetail_PersonalDetailID],l.[PlanName],l.[CredentialingInfoID],l.[ProfileID] FROM 
(SELECT w.[ProfileID],r.[PlanName],w.[CredentialingInfoID] 
FROM (SELECT p.[PlanID],p.[ProfileID],q.[CredentialingInfoID] 
FROM (SELECT DISTINCT [CredentialingInfoID] FROM 
[ContractGrids])as q 
LEFT JOIN   [CredentialingInfoes] as p ON 
q.[CredentialingInfoID] =p.[CredentialingInfoID]) as w LEFT JOIN [Plans] as
r ON w.[PlanID]=r.[PlanID]) as l LEFT JOIN [Profiles] as k ON l.[ProfileID]=k.[ProfileID]) as a LEFT JOIN [PersonalDetails] as b 
ON a.[PersonalDetail_PersonalDetailID]=b.[PersonalDetailID]) as CREDETAIL 
ON m.[CredentialingInfoID]=CREDETAIL.[CredentialingInfoID]) as Report LEFT JOIN [CredentialingContractInfoFromPlans] as CR 
ON Report.[Report_CredentialingContractInfoFromPlanID]=CR.[CredentialingContractInfoFromPlanID]) as ty LEFT JOIN [LOBs] as we 
ON ty.[LOBID]=we.[LOBID]) as sdf LEFT JOIN [Groups] as Groups ON sdf.[BusinessEntityID]=Groups.[GroupID]) as prodata 
left join [Profiles] as prt  on prodata.[ProfileID]=prt.[ProfileID]) as ureca left join  [OtherIdentificationNumbers] as NPI 
on ureca.[OtherIdentificationNumber_OtherIdentificationNumberID]= NPI.[OtherIdentificationNumberID])
select * from c where ProfileID=@profileid";
                    //cmd.CommandText = @"select cg.ContractGridID, l.LOBName, ci.CredentialingInfoID, p.PlanName, pl.CurrentlyPracticingAtThisAddress  from ContractGrids cg join LOBs l on cg.LOBID==l.LOBID join PracticeLocationDetails pl on pl.PracticeLocationDetailID=cg.PracticeLocationDetailID join CredentialingInfos ci on ci.CredentialingInfoID==cg.CredentialingInfoID";
                    cmd.Parameters.Add(new SqlParameter("@profileid", ProfileID));
                    dt = ADORepository.GetData(cmd);
                }
                try
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        ContractGridForProfileDTO ContractGridDTOObj = new ContractGridForProfileDTO();
                        ContractGridDTOObj.ContractGridID = int.Parse(data["ContractGridID"].ToString());
                        ContractGridDTOObj.PlanName = data["PlanName"].ToString();
                        //ContractGridDTOObj.ProviderFirstName = data["FirstName"].ToString();
                        //ContractGridDTOObj.ProviderMiddleName = data["MiddleName"].ToString();
                        //ContractGridDTOObj.ProviderLastName = data["LastName"].ToString();
                        ContractGridDTOObj.PlanName = data["PlanName"].ToString();
                        ContractGridDTOObj.LOBCode = data["LOBCode"].ToString();
                        ContractGridDTOObj.LOBName = data["LOBName"].ToString();
                        ContractGridDTOObj.IndividualID = data["ProviderID"].ToString();
                        // ContractGridDTOObj.ProviderID = data["ProviderID"].ToString();
                        ContractGridDTOObj.EffectiveDate = data["InitiatedDate"].ToString().Split(' ')[0];
                        ContractGridDTOObj.ParticiPationStatus = data["ParticipatingStatus"].ToString();
                        ContractGridDTOObj.TerminationDate = data["TerminationDate"].ToString().Split(' ')[0];
                        ContractGridDTOObj.InitiatedDate = data["InitiatedDate"].ToString().Split(' ')[0];
                        ContractGridDTOObj.GroupID = data["GroupID"].ToString();
                        ContractGridDTOObj.CredentialingContractInfoPlanID = data["Report_CredentialingContractInfoFromPlanID"].ToString();
                        ContractGridDTOObj.Status = data["Status"].ToString();
                        ContractGridDTOObj.ContractGridStatus = data["ContractGridStatus"].ToString();
                        ContractGridDTOObj.ProfileID = data["ProfileID"].ToString();
                        ContractGridDTOObj.PracticeLocationCorporateName = data["PracticeLocationCorporateName"].ToString();
                        ContractGridDTOObj.PanelStatus = data["PanelStatus"].ToString();
                        // ContractGridDTOObj.NPINumber = data["NPINumber"].ToString();
                        //ContractGridDTOObj.InitiatedDate = ContractGridDTOObj.InitiatedDate == "" ? ContractGridDTOObj.InitiatedDate : ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[1] + "-" + ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[0] + "-" + ContractGridDTOObj.InitiatedDate.Split(' ')[0].Split('-')[2];
                        //ContractGridDTOObj.TerminationDate = ContractGridDTOObj.TerminationDate == "" ? ContractGridDTOObj.TerminationDate : ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[1] + "-" + ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[0] + "-" + ContractGridDTOObj.TerminationDate.Split(' ')[0].Split('-')[2];
                        ContractGridDTOList.Add(ContractGridDTOObj);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                var FilteredData = new List<ContractGridForProfileDTO>();
                FilteredData = ContractGridDTOList.Where(x => x.ContractGridStatus == "Inactive" && x.Status == "Active").ToList();
                return FilteredData;
            }
        }
    }
}
