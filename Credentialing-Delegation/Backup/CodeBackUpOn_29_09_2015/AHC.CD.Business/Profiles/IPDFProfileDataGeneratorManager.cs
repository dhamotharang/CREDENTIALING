using AHC.CD.Business.BusinessModels.PDFGenerator;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface IPDFProfileDataGeneratorManager
    {
        Task<string> GetProfileDataByIdAsync(int profileId);
        Task<string> GetProfileDetailByIdAsync(int profileId);
        Task<Profile> GetProfileList(int profileID);
    }
}
