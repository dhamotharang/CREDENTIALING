using AHC.CD.Data.Repository;
using AHC.CD.Entities.Notification;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository.Notification
{
    internal class ExpiryNotificationRepository : EFGenericRepository<ExpiryNotificationDetail>, IExpiryNotificationRepository
    {
        public void UpdateExpiryDetails(IEnumerable<ExpiryNotificationDetail> expiries)
        {
            var dbExpiryDetails = this.GetAll();

            if(dbExpiryDetails != null || dbExpiryDetails.Count() != 0)
            {
                foreach (var newExpiryDetail in expiries)
                {
                    var updateexpiry = dbExpiryDetails.FirstOrDefault(e => e.NPINumber.Equals(newExpiryDetail.NPINumber));
                    
                    if (updateexpiry != null)
                    {
                        updateexpiry = AutoMapper.Mapper.Map<ExpiryNotificationDetail, ExpiryNotificationDetail>(newExpiryDetail, updateexpiry);

                        //Update State License Expiry
                        UpdateStateLicenseExpiries(newExpiryDetail.StateLicenseExpiries, updateexpiry.StateLicenseExpiries);

                        //Update DEA License Expiry
                        UpdateDEALicenseExpiries(newExpiryDetail.DEALicenseExpiries, updateexpiry.DEALicenseExpiries);

                        //Update CDSC Info Expiry
                        UpdateCDSCInfoExpiries(newExpiryDetail.CDSCInfoExpiries, updateexpiry.CDSCInfoExpiries);

                        //Update Specialty Detail Expiry
                        UpdateSpecialtyDetailExpiries(newExpiryDetail.SpecialtyDetailExpiries, updateexpiry.SpecialtyDetailExpiries);

                        //Update Hospital Privilege Expiry
                        UpdateHospitalPrivilegeExpiries(newExpiryDetail.HospitalPrivilegeExpiries, updateexpiry.HospitalPrivilegeExpiries);

                        //Update Hospital Privilege Expiry
                        UpdateProfessionalLiabilityExpiries(newExpiryDetail.ProfessionalLiabilityExpiries, updateexpiry.ProfessionalLiabilityExpiries);

                        //Update Hospital Privilege Expiry
                        UpdateWorkerCompensationExpiries(newExpiryDetail.WorkerCompensationExpiries, updateexpiry.WorkerCompensationExpiries);
                    }
                    else
                        this.Create(newExpiryDetail);
                }
            }
            else
                this.CreateRange(expiries);
        }

        private void UpdateStateLicenseExpiries(ICollection<StateLicenseExpiry> newExpiryList, ICollection<StateLicenseExpiry> dbExpiryList)
        {
            if (dbExpiryList != null || dbExpiryList.Count != 0)
            {
                foreach (var newExpiry in newExpiryList)
                {
                    var updateExpiry = dbExpiryList.FirstOrDefault(s => s.StateLicenseInformationID == newExpiry.StateLicenseInformationID);

                    if (updateExpiry != null)
                    {
                        updateExpiry = AutoMapper.Mapper.Map<StateLicenseExpiry, StateLicenseExpiry>(newExpiry, updateExpiry);
                    }
                    else
                        dbExpiryList.Add(newExpiry);
                }

                for (int i = 0; i < dbExpiryList.Count; i++)
                {
                    if (!newExpiryList.Any(e => e.StateLicenseInformationID == dbExpiryList.ElementAt(i).StateLicenseInformationID))
                    {
                        dbExpiryList.Remove(dbExpiryList.ElementAt(i));
                        i--;
                    }
                }
            }
            else
                dbExpiryList = newExpiryList;
        }

        private void UpdateDEALicenseExpiries(ICollection<DEALicenseExpiry> newExpiryList, ICollection<DEALicenseExpiry> dbExpiryList)
        {
            if (dbExpiryList != null || dbExpiryList.Count != 0)
            {
                foreach (var newExpiry in newExpiryList)
                {
                    var updateExpiry = dbExpiryList.FirstOrDefault(s => s.FederalDEAInformationID == newExpiry.FederalDEAInformationID);

                    if (updateExpiry != null)
                    {
                        updateExpiry = AutoMapper.Mapper.Map<DEALicenseExpiry, DEALicenseExpiry>(newExpiry, updateExpiry);
                    }
                    else
                        dbExpiryList.Add(newExpiry);
                }

                for (int i = 0; i < dbExpiryList.Count; i++)
                {
                    if (!newExpiryList.Any(e => e.FederalDEAInformationID == dbExpiryList.ElementAt(i).FederalDEAInformationID))
                    {
                        dbExpiryList.Remove(dbExpiryList.ElementAt(i));
                        i--;
                    }
                }
            }
            else
                dbExpiryList = newExpiryList;
        }

        private void UpdateCDSCInfoExpiries(ICollection<CDSCInfoExpiry> newExpiryList, ICollection<CDSCInfoExpiry> dbExpiryList)
        {
            if (dbExpiryList != null || dbExpiryList.Count != 0)
            {
                foreach (var newExpiry in newExpiryList)
                {
                    var updateExpiry = dbExpiryList.FirstOrDefault(s => s.CDSCInformationID == newExpiry.CDSCInformationID);

                    if (updateExpiry != null)
                    {
                        updateExpiry = AutoMapper.Mapper.Map<CDSCInfoExpiry, CDSCInfoExpiry>(newExpiry, updateExpiry);
                    }
                    else
                        dbExpiryList.Add(newExpiry);
                }

                for (int i = 0; i < dbExpiryList.Count; i++)
                {
                    if (!newExpiryList.Any(e => e.CDSCInformationID == dbExpiryList.ElementAt(i).CDSCInformationID))
                    {
                        dbExpiryList.Remove(dbExpiryList.ElementAt(i));
                        i--;
                    }
                }
            }
            else
                dbExpiryList = newExpiryList;
        }

        private void UpdateSpecialtyDetailExpiries(ICollection<SpecialtyDetailExpiry> newExpiryList, ICollection<SpecialtyDetailExpiry> dbExpiryList)
        {
            if (dbExpiryList != null || dbExpiryList.Count != 0)
            {
                foreach (var newExpiry in newExpiryList)
                {
                    var updateExpiry = dbExpiryList.FirstOrDefault(s => s.SpecialtyDetailID == newExpiry.SpecialtyDetailID);

                    if (updateExpiry != null)
                    {
                        updateExpiry = AutoMapper.Mapper.Map<SpecialtyDetailExpiry, SpecialtyDetailExpiry>(newExpiry, updateExpiry);
                    }
                    else
                        dbExpiryList.Add(newExpiry);
                }

                for (int i = 0; i < dbExpiryList.Count; i++)
                {
                    if (!newExpiryList.Any(e => e.SpecialtyDetailID == dbExpiryList.ElementAt(i).SpecialtyDetailID))
                    {
                        dbExpiryList.Remove(dbExpiryList.ElementAt(i));
                        i--;
                    }
                }
            }
            else
                dbExpiryList = newExpiryList;
        }

        private void UpdateHospitalPrivilegeExpiries(ICollection<HospitalPrivilegeExpiry> newExpiryList, ICollection<HospitalPrivilegeExpiry> dbExpiryList)
        {
            if (dbExpiryList != null || dbExpiryList.Count != 0)
            {
                foreach (var newExpiry in newExpiryList)
                {
                    var updateExpiry = dbExpiryList.FirstOrDefault(s => s.HospitalPrivilegeDetailID == newExpiry.HospitalPrivilegeDetailID);

                    if (updateExpiry != null)
                    {
                        updateExpiry = AutoMapper.Mapper.Map<HospitalPrivilegeExpiry, HospitalPrivilegeExpiry>(newExpiry, updateExpiry);
                    }
                    else
                        dbExpiryList.Add(newExpiry);
                }

                for (int i = 0; i < dbExpiryList.Count; i++)
                {
                    if (!newExpiryList.Any(e => e.HospitalPrivilegeDetailID == dbExpiryList.ElementAt(i).HospitalPrivilegeDetailID))
                    {
                        dbExpiryList.Remove(dbExpiryList.ElementAt(i));
                        i--;
                    }
                }
            }
            else
                dbExpiryList = newExpiryList;
        }

        private void UpdateProfessionalLiabilityExpiries(ICollection<ProfessionalLiabilityExpiry> newExpiryList, ICollection<ProfessionalLiabilityExpiry> dbExpiryList)
        {
            if (dbExpiryList != null || dbExpiryList.Count != 0)
            {
                foreach (var newExpiry in newExpiryList)
                {
                    var updateExpiry = dbExpiryList.FirstOrDefault(s => s.ProfessionalLiabilityInfoID == newExpiry.ProfessionalLiabilityInfoID);

                    if (updateExpiry != null)
                    {
                        updateExpiry = AutoMapper.Mapper.Map<ProfessionalLiabilityExpiry, ProfessionalLiabilityExpiry>(newExpiry, updateExpiry);
                    }
                    else
                        dbExpiryList.Add(newExpiry);
                }

                for (int i = 0; i < dbExpiryList.Count; i++)
                {
                    if (!newExpiryList.Any(e => e.ProfessionalLiabilityInfoID == dbExpiryList.ElementAt(i).ProfessionalLiabilityInfoID))
                    {
                        dbExpiryList.Remove(dbExpiryList.ElementAt(i));
                        i--;
                    }
                }
            }
            else
                dbExpiryList = newExpiryList;
        }

        private void UpdateWorkerCompensationExpiries(ICollection<WorkerCompensationExpiry> newExpiryList, ICollection<WorkerCompensationExpiry> dbExpiryList)
        {
            if (dbExpiryList != null || dbExpiryList.Count != 0)
            {
                foreach (var newExpiry in newExpiryList)
                {
                    var updateExpiry = dbExpiryList.FirstOrDefault(s => s.WorkersCompensationInformationID == newExpiry.WorkersCompensationInformationID);

                    if (updateExpiry != null)
                    {
                        updateExpiry = AutoMapper.Mapper.Map<WorkerCompensationExpiry, WorkerCompensationExpiry>(newExpiry, updateExpiry);
                    }
                    else
                        dbExpiryList.Add(newExpiry);
                }

                for (int i = 0; i < dbExpiryList.Count; i++)
                {
                    if (!newExpiryList.Any(e => e.WorkersCompensationInformationID == dbExpiryList.ElementAt(i).WorkersCompensationInformationID))
                    {
                        dbExpiryList.Remove(dbExpiryList.ElementAt(i));
                        i--;
                    }
                }
            }
            else
                dbExpiryList = newExpiryList;
        }
    }
}
