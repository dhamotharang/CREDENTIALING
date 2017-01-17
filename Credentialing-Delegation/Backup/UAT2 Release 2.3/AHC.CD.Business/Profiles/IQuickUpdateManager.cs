using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface IQuickUpdateManager
    {
        Task<object> GetProfileReviewDataAsync(int profileId);
    }
}
