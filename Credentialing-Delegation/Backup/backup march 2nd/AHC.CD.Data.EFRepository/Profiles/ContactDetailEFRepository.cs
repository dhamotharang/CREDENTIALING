using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Data.Repository.Profiles;


namespace AHC.CD.Data.EFRepository.Profiles
{
    internal class ContactDetailEFRepository:EFGenericRepository<ContactDetail>,IContactDetailRepository
    {
    }
}
