using AHC.CD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository.Profiles
{
    public interface IProfileUpdateRepository
    {
        List<ProfileUpdatesTrackerDTO> GetAllUpdates();
        List<ProfileUpdatesTrackerDTO> GetAllUpdatesByID(int profileID);
        List<ProfileUpdatesTrackerDTO> GetAllUpdatesHistory();
        List<ProfileUpdatesTrackerDTO> GetAllUpdatesHistoryByID(int profileID);
    }
}
