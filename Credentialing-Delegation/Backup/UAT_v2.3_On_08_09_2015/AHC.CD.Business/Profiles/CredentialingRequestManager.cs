using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Exceptions.Credentialing;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public class CredentialingRequestManager : ICredentilingRequestManager
    {
        ICredentialingRequestRepository credentialingRequestRepository = null;  
        private IRepositoryManager repositoryManager = null;
        IUnitOfWork uow = null;

        public CredentialingRequestManager(IUnitOfWork uow, IRepositoryManager repositoryManager)
        {
            this.credentialingRequestRepository = uow.GetCredentialingrequestRepository();
            this.repositoryManager = repositoryManager;
            this.uow = uow;
        }

        public void InitiateCredentialingRequestAsync(Entities.MasterProfile.CredentialingRequest.CredentialingRequest credentialingRequest)
        {
            try
            {
                credentialingRequestRepository.Create(credentialingRequest);
                credentialingRequestRepository.Save();
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CredentialingRequestException(ExceptionMessage.CREDENTIALING_REQUEST_EXCEPTION, ex);
            }
        }

        public void CredentialingRequestInactiveAsync(Entities.MasterProfile.CredentialingRequest.CredentialingRequest credentialingRequest)
        {
            try
            {
                credentialingRequestRepository.Update(credentialingRequest);
                credentialingRequestRepository.Save();
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CredentialingRequestException(ExceptionMessage.CREDENTIALING_REQUEST_EXCEPTION, ex);
            }
        }
    }
}
