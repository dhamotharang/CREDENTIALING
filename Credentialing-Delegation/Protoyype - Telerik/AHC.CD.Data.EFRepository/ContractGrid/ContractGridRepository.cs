using AHC.CD.Data.Repository.ContractGrid;
using AHC.CD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
