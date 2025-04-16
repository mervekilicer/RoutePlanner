using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROLAB2.Models
{
    public class Durak
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public bool sonDurak { get; set; }
        public List<DurakBaglantisi> nextStops { get; set; }
        public Transfer transfer { get; set; }
    }
}
