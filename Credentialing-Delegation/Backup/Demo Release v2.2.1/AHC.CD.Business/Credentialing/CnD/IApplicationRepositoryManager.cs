using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.CnD
{
    public interface IApplicationRepositoryManager
    {
        Task<string> GetProfileDataByIdAsync(int profileId, string template);
    }
}
