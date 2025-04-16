using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROLAB2.Models
{

    public abstract class Yolcu
    {
        public string Ad { get; set; }
        public abstract double IndirimOraniGetir();
    }


    public class GenelYolcu : Yolcu
    {
        public override double IndirimOraniGetir()
        {
            return 0.0;
        }
    }


    public class Ogrenci : Yolcu
    {
        public override double IndirimOraniGetir()
        {
            return 0.5;
        }
    }


    public class Yasli : Yolcu
    {
        public override double IndirimOraniGetir()
        {
            return 1.0;
        }
    }
}
