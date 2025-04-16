using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROLAB2.Models
{
    public abstract class Arac
    {
        public string Plaka { get; set; }
        public abstract void Calistir();
    }

    public class Otobus : Arac
    {
        public override void Calistir()
        {

        }
    }

    public class Tramvay : Arac
    {
        public override void Calistir()
        {

        }
    }

    public class TaksiAraci : Arac
    {
        public override void Calistir()
        {

        }
    }
}
