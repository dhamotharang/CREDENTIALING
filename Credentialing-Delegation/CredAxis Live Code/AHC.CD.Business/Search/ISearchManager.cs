using AHC.CD.Business.Credentialing;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Search
{
    /// <summary>
    /// Author: Venkat
    /// Date: 04/03/2015
    /// Providing all Profile Searching features here
    /// </summary>
    public interface ISearchManager
    {
        List<ProviderSearchResultDTO> SearchProviderProfileForViewEdit(string NPINumber = null, string firstName = null, string lastName = null, string providerRelationship = null, string IPAGroupName = null, string providerLevel = null, string providerType = null);

        List<Profile> SearchProfileForViewEdit(string NPINumber = null, string firstName = null, string lastName = null, string providerRelationship = null, string IPAGroupName = null, string providerLevel = null, string providerType = null);

        List<SearchResultForCred> SearchProviderProfileForCred(string NPINumber = null, string firstName = null, string lastName = null, string CAQH = null, string IPAGroupName = null, string specialty = null, string providerType = null);

        List<CredentialingInfo> SearchProviderProfileForReCred(string NPINumber = null, string firstName = null, string lastName = null, string CAQH = null, string IPAGroupName = null, string specialty = null, string providerType = null);
        List<CredentialingInfo> SearchProviderProfileForDeCred(string NPINumber = null, string firstName = null, string lastName = null, string CAQH = null, string IPAGroupName = null, string specialty = null, string providerType = null);
        Task<List<SearchUserforGroupMailDTO>> SearchUserforGroupMail(string firstName = null, string IPAGroupName = null, string providerLevel = null);
        Task<List<SearchUserforGroupMailDTO>> GetAllUsersForGroupMailAsync();
    }
}
