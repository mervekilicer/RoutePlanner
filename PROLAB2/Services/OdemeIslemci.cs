using System;
using PROLAB2.Models;

namespace PROLAB2.Services
{
    public class OdemeIslemci
    {
        public IOdeme KrediKarti { get; }
        public IOdeme Nakit { get; }
        public IOdeme KentKart { get; }

        public OdemeIslemci(IOdeme krediKarti, IOdeme nakit, IOdeme kentKart)
        {
            KrediKarti = krediKarti;
            Nakit = nakit;
            KentKart = kentKart;
        }


        public bool OdemeYap(Rota rota, Yolcu yolcu)
        {
            foreach (var segment in rota.Segmentler)
            {
                if (segment.Ucret == 0)
                    continue;

                bool odemeBasarili = false;
                double tutar = segment.Ucret; 

                if (segment.Mod.Equals("taxi", StringComparison.OrdinalIgnoreCase))
                {
                    if (Nakit.OdemeYap(tutar))
                        odemeBasarili = true;
                    else if (KrediKarti.OdemeYap(tutar))
                        odemeBasarili = true;
                }
                else if (segment.Mod.Equals("bus", StringComparison.OrdinalIgnoreCase) ||
                         segment.Mod.Equals("tram", StringComparison.OrdinalIgnoreCase) ||
                         segment.Mod.Equals("transfer", StringComparison.OrdinalIgnoreCase))
                {

                    double tutarIndirimli = tutar;
                    if (!segment.Mod.Equals("transfer", StringComparison.OrdinalIgnoreCase))
                    {
                        tutarIndirimli = tutar * (1 - yolcu.IndirimOraniGetir());
                    }
                    if (KentKart.OdemeYap(tutarIndirimli))
                        odemeBasarili = true;
                    else if (KrediKarti.OdemeYap(tutar))
                        odemeBasarili = true;
                }
                if (!odemeBasarili)
                {
                    return false; 
                }
            }
            return true;
        }

    }
}
