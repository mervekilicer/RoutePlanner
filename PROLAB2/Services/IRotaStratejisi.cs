using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROLAB2.Services
{
    public interface IRotaStratejisi
    {
        Models.Rota RotaHesapla(
            Models.UlasimVerisi veri,
            Models.Koordinat baslangic,
            Models.Koordinat varis,
            Models.Yolcu yolcu);
    }
}
