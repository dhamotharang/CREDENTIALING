using AHC.CD.Entities.MasterProfile.CredentialingRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface ICredentilingRequestManager
    {
        void InitiateCredentialingRequestAsync(CredentialingRequest credentialingRequest);
        void CredentialingRequestInactiveAsync(CredentialingRequest credentialingRequest);
        Task<CredentialingRequest> GetCredentialingRequestByID(int credRequestID);
        
    }
}
