using AHC.CD.Entities.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository
{
    public class LocationInitializer
    {
        public void Seed(List<Country> countries)
        {
            EFEntityContext context = new EFEntityContext();

            context.Countries.AddRange(countries);

            context.SaveChanges();
        }
    }
}
