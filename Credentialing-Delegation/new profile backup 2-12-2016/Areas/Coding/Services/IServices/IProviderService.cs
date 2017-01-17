using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Coding.Services.IServices
{
    public interface IProviderService
    {
        void GetProviderDataByNPI(string NPI);
    }
}
