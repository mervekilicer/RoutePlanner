using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROLAB2.Models
{

    public interface IOdeme
    {
        double Bakiye { get; set; }
        bool OdemeYap(double tutar);
    }

    public class KrediKartiOdeme : IOdeme
    {
        public double Bakiye { get; set; }
        public bool OdemeYap(double tutar)
        {
            if (Bakiye >= tutar)
            {
                Bakiye -= tutar;
                return true;
            }
            return false;
        }
    }

    public class NakitOdeme : IOdeme
    {
        public double Bakiye { get; set; }
        public bool OdemeYap(double tutar)
        {
            if (Bakiye >= tutar)
            {
                Bakiye -= tutar;
                return true;
            }
            return false;
        }
    }

    public class KentKartOdeme : IOdeme
    {
        public double Bakiye { get; set; }
        public bool OdemeYap(double tutar)
        {
            if (Bakiye >= tutar)
            {
                Bakiye -= tutar;
                return true;
            }
            return false;
        }
    }
}
