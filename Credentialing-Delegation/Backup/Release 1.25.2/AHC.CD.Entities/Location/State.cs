using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Location
{
    public class State
    {
        public int StateID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Coordinates
        {
            set
            {
                if (value != null)
                {
                    var data = value.Split(',');
                    this.Latitute = data[0];
                    this.Longitute = data[1];
                }
            }
        }

        public string Latitute { get; set; }

        public string Longitute { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
