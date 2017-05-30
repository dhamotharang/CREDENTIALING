using AHC.CD.Entities.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business
{
    /// <summary>
    /// Added By : Manideep Innamuri
    /// </summary>
    public interface IAssignToCCOorTLManager
    {
        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get the Assigned CCo of a Provider
        /// </summary>
        /// <param name="ProfileID"></param>
        /// <returns></returns>
        string GetAssignedCCOForaProvider(int ProfileID);

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get the Assigned TL of a Provider
        /// </summary>
        /// <param name="ProfileID"></param>
        /// <returns></returns>
        string GetAssignedTLForaProvider(int ProfileID);

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Interface Method for Getting All CCO's
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProfileUser>> GetAllCCOs();


        /// <summary>
        /// Author : Manideep Innamuri
        /// Description: Interface Method For Getting All TL's
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProfileUser>> GetAllTLs();

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to get the Number of Providers Assigned To a CCO/TL
        /// </summary>
        /// <param name="ProfileUserID"></param>
        /// <returns></returns>
        int NumberOfProvidersAssigned(int ProfileUserID);

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get the No fof Tasks Assigned to a CCO
        /// </summary>
        /// <param name="ProfileUserID"></param>
        /// <returns></returns>
        int GetNoofTasksAssigned(int? CDUserID);

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get the No of Tasks Pending for that CCo
        /// </summary>
        /// <param name="ProfileUserID"></param>
        /// <returns></returns>
        int GetNoofTasksPending(int? CDUserID);

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Assign Multiple Profiles To cco
        /// </summary>
        /// <param name="profileIds"></param>
        /// <param name="profileUserId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task AssignMultipleProfilesToCCo(List<int?> ProfileIDs, int profileUserId, string userId, string Status);

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Assign Multiple Profiles To TL
        /// </summary>
        /// <param name="profileIds"></param>
        /// <param name="profileUserId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task AssignMultipleProfilesToTL(List<int?> ProfileIDs, int profileUserId, string userId, string Status);

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get the Count of Providers AlreadyAssigned
        /// </summary>
        /// <param name="ProfileIDs"></param>
        /// <returns></returns>
        int GetCountOfAlreadyAssignedProviders(List<int?> ProfileIDs, string CCorTL);

        /// <summary>
        /// Author : Manideep Innamuri
        /// Date : 10th May 2017
        /// Description : Method to get the already Assigned ProfileIDs
        /// </summary>
        /// <param name="ProfileIDs"></param>
        /// <param name="CCorTL"></param>
        /// <returns></returns>
        List<int?> GetAlreadyAssignedProviders(List<int?> ProfileIDs, string CCorTL);

    }
}
