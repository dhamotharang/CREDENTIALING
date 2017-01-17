using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.ApplicationTemplateLibrary
{
    public interface IApplicationTemplateLibraryManager
    {
        Task<string> GetProfileDataByIdAsync(int profileId);
    }
}
