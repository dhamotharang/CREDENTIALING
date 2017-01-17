using AHC.CD.Entities.CustomField;
using AHC.CD.Entities.CustomField.CustomFieldTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.CustomFieldGeneration
{
    public interface ICustomFieldGenerationManager
    {
        Task<IEnumerable<CustomField>> getAllCustomField();
        Task<CustomField> AddCustomField(CustomField customField);
        Task<CustomFieldTransaction> getCustomFieldTransaction(int ProfileID);
        Task<int> AddCustomFieldTansaction(int ProfileID, CustomFieldTransaction CustomFieldTransaction);
    }
}
