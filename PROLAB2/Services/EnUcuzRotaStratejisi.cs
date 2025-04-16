using System;
using System.Collections.Generic;
using System.Linq;
using PROLAB2.Models;

namespace PROLAB2.Services
{
    public class EnUcuzRotaStratejisi : IRotaStratejisi
    {

        private class KenarDetayi
        {
            public double Mesafe { get; set; }
            public int Sure { get; set; }
            public double Ucret { get; set; }
            public string Mod { get; set; }
        }
     
        private const double YurumeSuresi = 12;      
        private const double EsikDegeri = 2; 

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

            double distanceStart = MesafeHesaplayici.MesafeHesapla(baslangic, new Koordinat { Lat = enYakinBaslangicDurak.lat, Lon = enYakinBaslangicDurak.lon });
            RotaSegmenti startSegment = new RotaSegmenti();
            if (distanceStart <= EsikDegeri)
            {
                int walkTime = (int)Math.Ceiling(distanceStart * YurumeSuresi);
                startSegment = new RotaSegmenti
                {
                    Baslangic = new Durak { name = "Başlangıç Noktası (Yürüme)", lat = baslangic.Lat, lon = baslangic.Lon },
                    Varis = enYakinBaslangicDurak,
                    Mesafe = distanceStart,
                    Sure = walkTime,
                    Ucret = 0, 
                    Mod = "Yürüme"
                };
            }
            else
            {
                int taxiTime = (int)Math.Ceiling(distanceStart * 2); 
                double taxiCost = veri.Taxi.OpeningFee + (veri.Taxi.CostPerKm * distanceStart);
                startSegment = new RotaSegmenti
                {
                    Baslangic = new Durak { name = "Başlangıç Noktası (Taksi)", lat = baslangic.Lat, lon = baslangic.Lon },
                    Varis = enYakinBaslangicDurak,
                    Mesafe = distanceStart,
                    Sure = taxiTime,
                    Ucret = taxiCost,
                    Mod = "taxi"
                };
            }
            rota.Segmentler.Add(startSegment);


            List<(string durakId, KenarDetayi kenar)> yolKenarDetaylari;
            List<string> yolDurakIds = DijkstraAlgoritmasi(duraklarSozluk, enYakinBaslangicDurak.id, enYakinVarisDurak.id, out yolKenarDetaylari);

            if (yolDurakIds == null || yolDurakIds.Count == 0)
            {
                double distanceBetween = MesafeHesaplayici.MesafeHesapla(
                    new Koordinat { Lat = enYakinBaslangicDurak.lat, Lon = enYakinBaslangicDurak.lon },
                    new Koordinat { Lat = enYakinVarisDurak.lat, Lon = enYakinVarisDurak.lon });
                RotaSegmenti middleSegment;
                if (distanceBetween <= EsikDegeri)
                {
                    int walkTime = (int)Math.Ceiling(distanceBetween * YurumeSuresi);
                    middleSegment = new RotaSegmenti
                    {
                        Baslangic = enYakinBaslangicDurak,
                        Varis = enYakinVarisDurak,
                        Mesafe = distanceBetween,
                        Sure = walkTime,
                        Ucret = 0,
                        Mod = "Yürüme"
                    };
                }
                else
                {
                    int taxiTime = (int)Math.Ceiling(distanceBetween * 2);
                    double taxiCost = veri.Taxi.OpeningFee + (veri.Taxi.CostPerKm * distanceBetween);
                    middleSegment = new RotaSegmenti
                    {
                        Baslangic = enYakinBaslangicDurak,
                        Varis = enYakinVarisDurak,
                        Mesafe = distanceBetween,
                        Sure = taxiTime,
                        Ucret = taxiCost,
                        Mod = "taxi"
                    };
                }
                rota.Segmentler.Add(middleSegment);
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

            double distanceEnd = MesafeHesaplayici.MesafeHesapla(new Koordinat { Lat = enYakinVarisDurak.lat, Lon = enYakinVarisDurak.lon }, varis);
            RotaSegmenti endSegment = new RotaSegmenti();
            if (distanceEnd <= EsikDegeri)
            {
                int walkTime = (int)Math.Ceiling(distanceEnd * YurumeSuresi);
                endSegment = new RotaSegmenti
                {
                    Baslangic = enYakinVarisDurak,
                    Varis = new Durak { name = "Hedef Noktası (Yürüme)", lat = varis.Lat, lon = varis.Lon },
                    Mesafe = distanceEnd,
                    Sure = walkTime,
                    Ucret = 0,
                    Mod = "Yürüme"
                };
            }
            else
            {
                int taxiTime = (int)Math.Ceiling(distanceEnd * 2);
                double taxiCost = veri.Taxi.OpeningFee + (veri.Taxi.CostPerKm * distanceEnd);
                endSegment = new RotaSegmenti
                {
                    Baslangic = enYakinVarisDurak,
                    Varis = new Durak { name = "Hedef Noktası (Taxi)", lat = varis.Lat, lon = varis.Lon },
                    Mesafe = distanceEnd,
                    Sure = taxiTime,
                    Ucret = taxiCost,
                    Mod = "taxi"
                };
            }
            rota.Segmentler.Add(endSegment);
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
                        double alternatif = mesafe[suanki] + sonraki.ucret;
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
                        double alternatif = mesafe[suanki] + suankiDurak.transfer.TransferUcret;
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
