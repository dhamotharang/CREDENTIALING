using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Data.Repository.Profiles;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Profile
{
    internal class ProfileUpdateADORepository : IProfileUpdateRepository
    {
        private DAPPERRepository _genericDapperRepo = null;

        public ProfileUpdateADORepository()
        {
            this._genericDapperRepo = new DAPPERRepository();
        }

        public List<Entities.DTO.ProfileUpdatesTrackerDTO> GetAllUpdates()
        {
            List<Entities.DTO.ProfileUpdatesTrackerDTO> updatesList = null;
            try
            {
                updatesList = _genericDapperRepo.ExecuteQuery<Entities.DTO.ProfileUpdatesTrackerDTO>("select * from GetAllUpdatesRenewals").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return updatesList;
        }


        public List<Entities.DTO.ProfileUpdatesTrackerDTO> GetAllUpdatesByID(int profileID)
        {
            List<Entities.DTO.ProfileUpdatesTrackerDTO> updatesList = null;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@profileID", profileID);
            try
            {
                updatesList = _genericDapperRepo.ExecuteStoredProcedure<Entities.DTO.ProfileUpdatesTrackerDTO>("GetAllUpdatesByID", parameters).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return updatesList;
        }


        public List<Entities.DTO.ProfileUpdatesTrackerDTO> GetAllUpdatesHistory()
        {
            List<Entities.DTO.ProfileUpdatesTrackerDTO> updatesHistoryList = null;
            try
            {
                updatesHistoryList = _genericDapperRepo.ExecuteQuery<Entities.DTO.ProfileUpdatesTrackerDTO>("select * from GetAllUpdatesRenewalsHistory").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return updatesHistoryList;
        }


        public List<Entities.DTO.ProfileUpdatesTrackerDTO> GetAllUpdatesHistoryByID(int profileID)
        {
            List<Entities.DTO.ProfileUpdatesTrackerDTO> updatesHistoryList = null;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@profileID", profileID);
            try
            {
                updatesHistoryList = _genericDapperRepo.ExecuteStoredProcedure<Entities.DTO.ProfileUpdatesTrackerDTO>("GetAllUpdatesHistoryByID", parameters).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return updatesHistoryList;
        }


        //public List<AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeDetail> GetAllhospitalpreviligesByID(int hospitalpreviligeinfoid)
        //{
        //    List<AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeDetail> hospitaldetailList = null;
        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("@hospitalpreviligeinfoid", hospitalpreviligeinfoid);
        //    try
        //    {
        //        hospitaldetailList = _genericDapperRepo.ExecuteStoredProcedure<AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeDetail>("spGetAllHospitalPreviliges", parameters).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return hospitaldetailList;
        //}
    }
}
