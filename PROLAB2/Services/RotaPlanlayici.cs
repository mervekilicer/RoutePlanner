using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROLAB2.Services
{
    public class RotaPlanlayici : IRotaPlanlayici
    {
        private readonly IRotaStratejisi _rotaStratejisi;
        public RotaPlanlayici(IRotaStratejisi rotaStratejisi)
        {
            _rotaStratejisi = rotaStratejisi;
        }

        public Models.Rota RotaPlanla(
            Models.UlasimVerisi veri,
            Models.Koordinat baslangic,
            Models.Koordinat varis,
            Models.Yolcu yolcu)
        {
            return _rotaStratejisi.RotaHesapla(veri, baslangic, varis, yolcu);
        }
    }
}
