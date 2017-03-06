using AHC.CD.Entities.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.PlanForms
{
    public interface IPlanFormManager
    {
        Task<List<PlanFormDetail>> GetAllPlanFormData();
    }
}
