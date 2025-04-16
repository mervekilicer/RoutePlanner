using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROLAB2.Models
{
    public class RotaSegmenti
    {
        public Durak Baslangic { get; set; }
        public Durak Varis { get; set; }
        public double Mesafe { get; set; }
        public int Sure { get; set; }
        public double Ucret { get; set; }

        public string Mod { get; set; }
    }
}
