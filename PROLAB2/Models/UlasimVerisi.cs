using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROLAB2.Models
{
    public class UlasimVerisi
    {
        public string City { get; set; }
        public Taksi Taxi { get; set; }
        public List<Durak> Duraklar { get; set; }
    }
}
