using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.DataMigration
{
    public class Processor
    {
        public void Process()
        {
            new Transformer().Process();
        }
    }
}
