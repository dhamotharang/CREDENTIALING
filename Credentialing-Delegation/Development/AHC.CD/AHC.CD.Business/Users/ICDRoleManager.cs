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
        Task AssignRoleForUser(CDUser cdUser, int id);
        void ChangeRoleofaUser(string newRoleCode, string authId, string oldrole);
        bool RemoveRoleofaUser(string RoleCode, string authId);
        Task<bool> AddNewRoletoaUser(string RoleCode, string authId);
    }
}
