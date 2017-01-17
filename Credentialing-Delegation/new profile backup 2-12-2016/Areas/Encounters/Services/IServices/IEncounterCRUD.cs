using PortalTemplate.Areas.Encounters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Encounters.Services.IServices
{
    public interface IEncounterCRUD
    {
        EncounterViewModel ViewEncounter();
        EncounterViewModel EditEncounter();
    }
}
