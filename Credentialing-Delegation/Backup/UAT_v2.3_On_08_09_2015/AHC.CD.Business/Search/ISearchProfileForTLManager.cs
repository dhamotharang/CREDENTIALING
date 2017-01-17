using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Search
{
    public interface ISearchProfileForTLManager
    {
        List<ProviderSearchResultDTO> SearchProviderProfileForViewEdit(string NPINumber = null, string firstName = null, string lastName = null, string providerRelationship = null, string IPAGroupName = null, string providerLevel = null, string providerType = null, string userId = null);
        
    }
}
