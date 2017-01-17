using AHC.CD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Users
{
    public interface ICDRoleManager
    {
        Task AssignProviderRoleAsync(CDUser cdUser);
    }
}
