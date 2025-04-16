using System;
using System.Collections.Generic;
using System.Linq;
using PROLAB2.Models;

namespace PROLAB2.Services
{
    public class EnHizliRotaStratejisi : IRotaStratejisi
    {
        private class KenarDetayi
        {
            public double Mesafe { get; set; }
            public int Sure { get; set; }
            public double Ucret { get; set; }
            public string Mod { get; set; }
        }

        public Rota RotaHesapla(UlasimVerisi veri, Koordinat baslangic, Koordinat varis, Yolcu yolcu)
        {
            Rota rota = new Rota();
            rota.Segmentler = new List<RotaSegmenti>();

            Dictionary<string, Durak> duraklarSozluk = veri.Duraklar.ToDictionary(d => d.id, d => d);

            Durak enYakinBaslangicDurak = EnYakinDurakBul(veri.Duraklar, baslangic);
            if (enYakinBaslangicDurak == null)
            {
                Console.WriteLine("Başlangıç noktasına yakın durak bulunamadı.");
                return null;
            }
            Durak enYakinVarisDurak = EnYakinDurakBul(veri.Duraklar, varis);
            if (enYakinVarisDurak == null)
            {
                Console.WriteLine("Hedef noktasına yakın durak bulunamadı.");
                return null;
            }


            double taksiMesafeBaslangic = MesafeHesaplayici.MesafeHesapla(baslangic, new Koordinat { Lat = enYakinBaslangicDurak.lat, Lon = enYakinBaslangicDurak.lon });
            int taksiSureBaslangic = (int)Math.Ceiling(taksiMesafeBaslangic * 2); 
            double taksiUcretBaslangic = veri.Taxi.OpeningFee + (veri.Taxi.CostPerKm * taksiMesafeBaslangic);

            RotaSegmenti taksiSegmentBaslangic = new RotaSegmenti
            {
                Baslangic = new Durak { name = "Başlangıç Noktası (Taxi)", lat = baslangic.Lat, lon = baslangic.Lon },
                Varis = enYakinBaslangicDurak,
                Mesafe = taksiMesafeBaslangic,
                Sure = taksiSureBaslangic,
                Ucret = taksiUcretBaslangic,
                Mod = "taxi"
            };
            rota.Segmentler.Add(taksiSegmentBaslangic);


            List<(string durakId, KenarDetayi kenar)> yolKenarDetaylari;
            List<string> yolDurakIds = DijkstraAlgoritmasi(duraklarSozluk, enYakinBaslangicDurak.id, enYakinVarisDurak.id, out yolKenarDetaylari);

            if (yolDurakIds == null || yolDurakIds.Count == 0)
            {

                double taksiMesafeArasi = MesafeHesaplayici.MesafeHesapla(
                    new Koordinat { Lat = enYakinBaslangicDurak.lat, Lon = enYakinBaslangicDurak.lon },
                    new Koordinat { Lat = enYakinVarisDurak.lat, Lon = enYakinVarisDurak.lon });
                int taksiSureArasi = (int)Math.Ceiling(taksiMesafeArasi * 2);
                double taksiUcretArasi = veri.Taxi.OpeningFee + (veri.Taxi.CostPerKm * taksiMesafeArasi);

                RotaSegmenti taksiSegmentArasi = new RotaSegmenti
                {
                    Baslangic = enYakinBaslangicDurak,
                    Varis = enYakinVarisDurak,
                    Mesafe = taksiMesafeArasi,
                    Sure = taksiSureArasi,
                    Ucret = taksiUcretArasi,
                    Mod = "taxi"
                };
                rota.Segmentler.Add(taksiSegmentArasi);
            }
            else
            {

                for (int i = 0; i < yolKenarDetaylari.Count; i++)
                {
                    string baslangicDurakId = yolDurakIds[i];
                    string varisDurakId = yolDurakIds[i + 1];
                    Durak baslangicDurak = duraklarSozluk[baslangicDurakId];
                    Durak varisDurak = duraklarSozluk[varisDurakId];
                    KenarDetayi kenarDetay = yolKenarDetaylari[i].kenar;

                    RotaSegmenti segment = new RotaSegmenti
                    {
                        Baslangic = baslangicDurak,
                        Varis = varisDurak,
                        Mesafe = kenarDetay.Mesafe,
                        Sure = kenarDetay.Sure,
                        Ucret = kenarDetay.Ucret,
                        Mod = kenarDetay.Mod
                    };
                    rota.Segmentler.Add(segment);
                }
            }

            double taksiMesafeSon = MesafeHesaplayici.MesafeHesapla(new Koordinat { Lat = enYakinVarisDurak.lat, Lon = enYakinVarisDurak.lon }, varis);
            int taksiSureSon = (int)Math.Ceiling(taksiMesafeSon * 2);
            double taksiUcretSon = veri.Taxi.OpeningFee + (veri.Taxi.CostPerKm * taksiMesafeSon);

            RotaSegmenti taksiSegmentSon = new RotaSegmenti
            {
                Baslangic = enYakinVarisDurak,
                Varis = new Durak { name = "Hedef Noktası (Taxi)", lat = varis.Lat, lon = varis.Lon },
                Mesafe = taksiMesafeSon,
                Sure = taksiSureSon,
                Ucret = taksiUcretSon,
                Mod = "taxi"
            };
            rota.Segmentler.Add(taksiSegmentSon);
            rota.ToplamMesafe = rota.Segmentler.Sum(s => s.Mesafe);
            rota.ToplamSure = rota.Segmentler.Sum(s => s.Sure);
            rota.ToplamUcret = rota.Segmentler.Sum(s => s.Ucret);

            return rota;
        }


        private Durak EnYakinDurakBul(List<Durak> duraklar, Koordinat nokta)
        {
            Durak enYakin = null;
            double minMesafe = double.MaxValue;
            foreach (var durak in duraklar)
            {
                double mesafe = MesafeHesaplayici.MesafeHesapla(nokta, new Koordinat { Lat = durak.lat, Lon = durak.lon });
                if (mesafe < minMesafe)
                {
                    minMesafe = mesafe;
                    enYakin = durak;
                }
            }
            return enYakin;
        }


        private List<string> DijkstraAlgoritmasi(Dictionary<string, Durak> duraklarSozluk, string kaynakDurakId, string hedefDurakId, out List<(string durakId, KenarDetayi kenar)> yolKenarDetaylari)
        {
            var mesafe = new Dictionary<string, double>();
            var onceki = new Dictionary<string, (string oncekiDurak, KenarDetayi kenar)>();
            var ziyaretEdilmeyen = new HashSet<string>();

            foreach (var kvp in duraklarSozluk)
            {
                mesafe[kvp.Key] = double.MaxValue;
                ziyaretEdilmeyen.Add(kvp.Key);
            }
            mesafe[kaynakDurakId] = 0;

            while (ziyaretEdilmeyen.Count > 0)
            {
                string suanki = null;
                double minMesafe = double.MaxValue;
                foreach (var node in ziyaretEdilmeyen)
                {
                    if (mesafe[node] < minMesafe)
                    {
                        minMesafe = mesafe[node];
                        suanki = node;
                    }
                }
                if (suanki == null)
                    break;
                if (suanki == hedefDurakId)
                    break;

                ziyaretEdilmeyen.Remove(suanki);
                Durak suankiDurak = duraklarSozluk[suanki];


                if (suankiDurak.nextStops != null)
                {
                    foreach (var sonraki in suankiDurak.nextStops)
                    {
                        if (!duraklarSozluk.ContainsKey(sonraki.stopId))
                            continue;
                        double alternatif = mesafe[suanki] + sonraki.sure; 
                        if (alternatif < mesafe[sonraki.stopId])
                        {
                            mesafe[sonraki.stopId] = alternatif;
                            onceki[sonraki.stopId] = (suanki, new KenarDetayi
                            {
                                Mesafe = sonraki.mesafe,
                                Sure = sonraki.sure,
                                Ucret = sonraki.ucret,
                                Mod = suankiDurak.type
                            });
                        }
                    }
                }


                if (suankiDurak.transfer != null)
                {
                    string transferId = suankiDurak.transfer.TransferStopId;
                    if (duraklarSozluk.ContainsKey(transferId))
                    {
                        double alternatif = mesafe[suanki] + suankiDurak.transfer.TransferSure;
                        if (alternatif < mesafe[transferId])
                        {
                            mesafe[transferId] = alternatif;
                            onceki[transferId] = (suanki, new KenarDetayi
                            {
                                Mesafe = 0, 
                                Sure = suankiDurak.transfer.TransferSure,
                                Ucret = suankiDurak.transfer.TransferUcret,
                                Mod = "transfer"
                            });
                        }
                    }
                }
            }


            if (mesafe[hedefDurakId] == double.MaxValue)
            {
                yolKenarDetaylari = new List<(string, KenarDetayi)>();
                return null;
            }

            List<string> yol = new List<string>();
            List<(string durakId, KenarDetayi kenar)> kenarListesi = new List<(string, KenarDetayi)>();
            string suankiDugum = hedefDurakId;
            yol.Add(suankiDugum);
            while (suankiDugum != kaynakDurakId)
            {
                if (!onceki.ContainsKey(suankiDugum))
                    break; 
                var oncekiKayit = onceki[suankiDugum];
                suankiDugum = oncekiKayit.oncekiDurak;
                yol.Insert(0, suankiDugum);
                kenarListesi.Insert(0, (suankiDugum, oncekiKayit.kenar));
            }
            yolKenarDetaylari = kenarListesi;
            return yol;
        }
    }
}
