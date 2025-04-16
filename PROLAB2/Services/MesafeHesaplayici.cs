using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROLAB2.Services
{
    public static class MesafeHesaplayici
    {
        public static double MesafeHesapla(Models.Koordinat koordinat1, Models.Koordinat koordinat2)
        {
            double R = 6371; 
            double lat1Rad = DereceyiRadyanaCevir(koordinat1.Lat);
            double lat2Rad = DereceyiRadyanaCevir(koordinat2.Lat);
            double deltaLat = DereceyiRadyanaCevir(koordinat2.Lat - koordinat1.Lat);
            double deltaLon = DereceyiRadyanaCevir(koordinat2.Lon - koordinat1.Lon);

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double mesafe = R * c;
            return mesafe;
        }

        private static double DereceyiRadyanaCevir(double derece)
        {
            return derece * Math.PI / 180;
        }
    }
}
