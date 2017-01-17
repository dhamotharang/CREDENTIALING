using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.CustomField;
using AHC.CD.Entities.CustomField.CustomFieldTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.CustomFieldGeneration
{
    internal class CustomFieldGenerationManager : ICustomFieldGenerationManager
    {
        IProfileRepository profileRepository = null;
        private IUnitOfWork uow = null;
        public CustomFieldGenerationManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.uow = uow;
        }
        public async Task<IEnumerable<CustomField>> getAllCustomField()
        {
            var CustomFields = uow.GetGenericRepository<CustomField>();

            return await CustomFields.GetAllAsync();
            
        }

        public async Task<CustomField> AddCustomField(CustomField customField)
        {
            try
            {
                customField.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                var CustomFields = uow.GetGenericRepository<CustomField>();
                CustomFields.Create(customField);
                await CustomFields.SaveAsync();
                return customField;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }


        public async Task<CustomFieldTransaction> getCustomFieldTransaction(int ProfileID)
        {

            try
            {
                var includeProperties = new string[]
                {
                    //Custom Field Transaction
                    "CustomFieldTransaction",
                    "CustomFieldTransaction.CustomFieldTransactionDatas",
                    "CustomFieldTransaction.CustomFieldTransactionDatas.CustomField"
                   
                };
                var profile = await profileRepository.FindAsync(p => p.ProfileID == ProfileID, includeProperties);
                CustomFieldTransaction customFieldTransaction = null;
                customFieldTransaction = profile.CustomFieldTransaction;
                return customFieldTransaction;
            }
            catch (Exception)
            {
                
                throw;
            }

        }


        public async Task<int> AddCustomFieldTansaction(int ProfileID, CustomFieldTransaction CustomFieldTransaction)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Custom Field Transaction
                    "CustomFieldTransaction",
                    "CustomFieldTransaction.CustomFieldTransactionDatas",
                    "CustomFieldTransaction.CustomFieldTransactionDatas.CustomField"
                   
                };
                var profile = await profileRepository.FindAsync(p => p.ProfileID == ProfileID, includeProperties);

                if (profile.CustomFieldTransaction == null)
                {
                    profile.CustomFieldTransaction = new CustomFieldTransaction();
                }
                //profile.CustomFieldTransaction.CustomFieldTransactionDatas = null;
                profile.CustomFieldTransaction = AutoMapper.Mapper.Map<CustomFieldTransaction, CustomFieldTransaction>(CustomFieldTransaction, profile.CustomFieldTransaction);
                profile.CustomFieldTransaction.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;

                profileRepository.Update(profile);
                await profileRepository.SaveAsync();
                return profile.CustomFieldTransaction.CustomFieldTransactionID;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
