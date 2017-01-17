using AHC.CD.Data.Repository;
using AHC.CD.Entities.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.PlanForms
{
    public  class PlanFormManager : IPlanFormManager
    {
        private IUnitOfWork uow = null;

        public PlanFormManager(IUnitOfWork uow)
        {
            this.uow = uow;   
        }

        public async Task<List<PlanFormDetail>> GetAllPlanFormData()
        {
            try
            {
                var PlanFormData = await uow.GetGenericRepository<PlanFormDetail>().GetAllAsync("PlanFormPayer,PlanFormRegion");
                return PlanFormData.ToList();
            }
            catch (Exception)
            {   
                throw;
            }
        }
    }
}
