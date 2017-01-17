using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Data.Repository.Profile;


namespace AHC.CD.Data.EFRepository.Profile
{
    internal class ContactDetailEFRepository:EFGenericRepository<ContactDetail>,IContactDetailRepository
    {
    }
}
