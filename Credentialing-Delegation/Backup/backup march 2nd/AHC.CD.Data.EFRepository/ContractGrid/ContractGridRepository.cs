using AHC.CD.Data.Repository.ContractGrid;
using AHC.CD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.Credentialing.DTO;

namespace AHC.CD.Data.EFRepository.ContractGrid
{
    internal class ContractGridRepository : IContractGridRepository
    {
        IGenericRepository<Entities.Credentialing.LoadingInformation.ContractGrid> contractrepo = null;

        public ContractGridRepository()
        {
            this.contractrepo = new EFGenericRepository<Entities.Credentialing.LoadingInformation.ContractGrid>();
        }

        public List<Entities.Credentialing.DTO.ContractGridDTO> GetAllContractGridInfoes(int profileid)
        {
            try
            {
                string IncludeProperties = "CredentialingInfo,LOB,Report,CredentialingInfo.Plan";
                var result = contractrepo.GetAll(IncludeProperties).Where<Entities.Credentialing.LoadingInformation.ContractGrid>(a => a.CredentialingInfo.ProfileID == profileid && a.Status=="Active").ToList();
                var ListOfcontractGrid = new List<Entities.Credentialing.DTO.ContractGridDTO>();
                foreach (var data in result)
                {
                    string TermDate = "";
                    if (data.Report.TerminationDate!=null)
                    {
                        TermDate=data.Report.TerminationDate.Value.ToShortDateString();
                    }

                    string EffectDate = "";

                    if (data.Report.InitiatedDate != null)
                    {
                        EffectDate = data.Report.InitiatedDate.Value.ToShortDateString();
                    }
                    
                    if (data.Report!=null)
                    {
                        Entities.Credentialing.DTO.ContractGridDTO ContractGridDTO = new Entities.Credentialing.DTO.ContractGridDTO();
                        ContractGridDTO.ContractGridID = data.ContractGridID;
                        ContractGridDTO.PlanName = data.CredentialingInfo.Plan.PlanName;
                        ContractGridDTO.ParticiPationStatus = data.Report.ParticipatingStatus;
                        ContractGridDTO.IndividualID = data.Report.ProviderID;
                        ContractGridDTO.GroupID = data.Report.GroupID;
                        ContractGridDTO.EffectiveDate = EffectDate;
                        ContractGridDTO.TerminationDate = TermDate;
                        ContractGridDTO.ContractGridStatusType = data.ContractGridStatusType;
                        if (data.LOB == null)
                        {
                            ContractGridDTO.LOB = "";
                        }
                        else
                        {
                            if (data.LOB.LOBCode == null)
                            {
                                data.LOB.LOBCode = "";
                                ContractGridDTO.LOB = "";
                            }
                            else
                            {
                                ContractGridDTO.LOB = data.LOB.LOBCode;
                            }
                        }
                        if (ListOfcontractGrid.Count == 0)
                        {
                            ListOfcontractGrid.Add(ContractGridDTO);
                        }
                        else
                        {
                            var flag = 0;
                            foreach (var item in ListOfcontractGrid)
                            {
                                
                                if (data.LOB != null)
                                {
                                    if (item.PlanName == data.CredentialingInfo.Plan.PlanName && item.GroupID == data.Report.GroupID && item.ParticiPationStatus == data.Report.ParticipatingStatus && item.IndividualID == data.Report.ProviderID && item.GroupID == data.Report.GroupID && item.EffectiveDate == EffectDate && item.TerminationDate == TermDate && !(item.LOB.Contains(data.LOB.LOBCode)) && data.LOB.LOBCode != null)
                                    {
                                        item.LOB+=","+data.LOB.LOBCode;
                                        flag = 1;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (item.PlanName == data.CredentialingInfo.Plan.PlanName && item.GroupID == data.Report.GroupID && item.ParticiPationStatus == data.Report.ParticipatingStatus && item.IndividualID == data.Report.ProviderID && item.GroupID == data.Report.GroupID && item.EffectiveDate == EffectDate && item.TerminationDate == TermDate)
                                    {
                                        item.LOB += "," + "";
                                        flag = 1;
                                        break;
                                    }
                                }
                            }
                            if (flag==0)
                            {
                                ListOfcontractGrid.Add(ContractGridDTO);
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                    
                }
                return ListOfcontractGrid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ContractForServiceDto> GetContractsForAprovider(int profileID)
        {
            try
            {
                string IncludeProperties = "CredentialingInfo,LOB,Report,CredentialingInfo.Plan,ProfileSpecialty,ProfileSpecialty,ProfileSpecialty.Specialty,ProfilePracticeLocation,ProfilePracticeLocation.Facility";
                var result = contractrepo.Get(x => x.CredentialingInfo.ProfileID == profileID && x.Status == "Active", IncludeProperties);//IncludeProperties).Where<Entities.Credentialing.LoadingInformation.ContractGrid>(a => a.CredentialingInfo.ProfileID == profileID && a.Status == "Active").ToList();
                List<ContractForServiceDto> Result = new List<ContractForServiceDto>();
                foreach (var contract in result)
                {
                    ContractForServiceDto cfs = new ContractForServiceDto();
                    cfs.Speciality = contract.ProfileSpecialty != null ? contract.ProfileSpecialty.Specialty != null ? contract.ProfileSpecialty.Specialty.Name : null : null;
                    cfs.EffectiveDate = contract.Report.InitiatedDate;
                    cfs.TerminationDate = contract.Report.TerminationDate;
                    cfs.Status = contract.ContractGridStatus;
                    cfs.PlanName = contract.CredentialingInfo.Plan.PlanName;
                    cfs.LOB = contract.LOB != null ? contract.LOB.LOBCode : null;
                    cfs.LOBName = contract.LOB != null ? contract.LOB.LOBName : null;
                    cfs.Facility = contract.ProfilePracticeLocation != null ? (contract.ProfilePracticeLocation.Facility != null ? new FacilityDTOForMobileApp
                    {
                        City = contract.ProfilePracticeLocation.Facility.City,
                        State = contract.ProfilePracticeLocation.Facility.State,
                        Country = contract.ProfilePracticeLocation.Facility.Country,
                        County = contract.ProfilePracticeLocation.Facility.County,
                        FacilityName = contract.ProfilePracticeLocation.Facility.Name,
                        Building = contract.ProfilePracticeLocation.Facility.Building,
                        Street = contract.ProfilePracticeLocation.Facility.Street,
                        ZipCode = contract.ProfilePracticeLocation.Facility.ZipCode
                    } : null) : null;
                    Result.Add(cfs);
                }
                return Result;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public object GetContractsForAproviderGropedbyZipCode(int profileID)
        {
            try
            {
                string IncludeProperties = "CredentialingInfo,LOB,Report,CredentialingInfo.Plan,ProfileSpecialty,ProfileSpecialty,ProfileSpecialty.Specialty,ProfilePracticeLocation,ProfilePracticeLocation.Facility,Report";
                var result = contractrepo.Get(x => x.CredentialingInfo.ProfileID == profileID && x.Status == "Active", IncludeProperties);//IncludeProperties).Where<Entities.Credentialing.LoadingInformation.ContractGrid>(a => a.CredentialingInfo.ProfileID == profileID && a.Status == "Active").ToList();
                List<ContractForServiceDto> Result = new List<ContractForServiceDto>();
                foreach (var contract in result)
                {
                    ContractForServiceDto cfs = new ContractForServiceDto();
                    cfs.Speciality = contract.ProfileSpecialty != null ? contract.ProfileSpecialty.Specialty != null ? contract.ProfileSpecialty.Specialty.Name : null : null;
                    cfs.EffectiveDate = contract.Report.InitiatedDate;
                    cfs.TerminationDate = contract.Report.TerminationDate;
                    cfs.Status = contract.ContractGridStatus;
                    cfs.PlanName = contract.CredentialingInfo.Plan.PlanName;
                    cfs.LOB = contract.LOB != null ? contract.LOB.LOBCode : null;
                    cfs.LOBName = contract.LOB != null ? contract.LOB.LOBName : null;
                    cfs.PanelStatus = contract.Report != null ? contract.Report.PanelStatus : null;
                    cfs.Facility = contract.ProfilePracticeLocation != null ? (contract.ProfilePracticeLocation.Facility != null ? new FacilityDTOForMobileApp()
                    {
                        City = contract.ProfilePracticeLocation.Facility.City,
                        State = contract.ProfilePracticeLocation.Facility.State,
                        Country = contract.ProfilePracticeLocation.Facility.Country,
                        County = contract.ProfilePracticeLocation.Facility.County,
                        FacilityName = contract.ProfilePracticeLocation.Facility.Name,
                        Building = contract.ProfilePracticeLocation.Facility.Building,
                        Street = contract.ProfilePracticeLocation.Facility.Street,
                        ZipCode = contract.ProfilePracticeLocation.Facility.ZipCode
                    } : null) : null;
                    Result.Add(cfs);
                }
                //List<object> data = new List<object>();
                //var WithZip = (from res in Result.Where(x=>x.Facility!=null) group res by res.Facility.ZipCode into z select new { ZipCode=z.Key,contracts = z.ToList<ContractForServiceDto>() }).ToList();
                //var WithOutZip = Result.Where(x => x.Facility == null).ToList();
                var WithZip = (from res in Result.Where(x => x.Facility != null?x.Facility.ZipCode!=null:false) group res by res.Facility.ZipCode into z select new { ZipCode = z.Key, contracts = z.ToList<ContractForServiceDto>() }).ToList();
                var WithOutZip = Result.Where(x => x.Facility == null?true:x.Facility.ZipCode==null).ToList();
                return new
                {
                    ContractsGroupedByZip = WithZip,
                    ContractsWithOutZip = WithOutZip
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int GetCountOfActiveContractsForaProvider(int ProfiileID)
        {
            try
            {
                EFEntityContext context = new EFEntityContext();
                return context.ContractGrids.Where(x => x.CredentialingInfo.ProfileID == ProfiileID && x.ContractGridStatus == "Active" && x.CredentialingInfo.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString() && x.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).Count();
            }
            catch (Exception)
            {

                return 0;
            }
        }
        
        




     

    }
}
