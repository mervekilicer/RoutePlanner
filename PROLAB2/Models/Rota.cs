using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROLAB2.Models
{
    public class Rota
    {
        public List<RotaSegmenti> Segmentler { get; set; }
        public double ToplamMesafe { get; set; }
        public int ToplamSure { get; set; }
        public double ToplamUcret { get; set; }
    }
}
