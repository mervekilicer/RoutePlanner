using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using PROLAB2.Data;
using PROLAB2.Models;
using PROLAB2.Services;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using RotaModel = PROLAB2.Models.Rota;

namespace PROLAB2
{
    public partial class Form1 : Form
    {
        private UlasimVerisi ulasimVerisi;
        private bool secimBaslangic = false;
        private bool secimHedef = false;

        public Form1()
        {
            InitializeComponent();
            gMapControl1.MouseClick += gMapControl1_MouseClick;
            this.BackColor = System.Drawing.Color.Pink;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = OpenStreetMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.Position = new PointLatLng(40.781, 29.947); 
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 20;
            gMapControl1.Zoom = 14;

            VeriyiYukle();
            HaritadaDuraklariGoster();
            HaritadaHatlariGoster();

            cmbRouteType.Items.Add("En Hızlı Rota");
            cmbRouteType.Items.Add("En Ucuz Rota");
            cmbRouteType.SelectedIndex = 0; 
        }

        private void VeriyiYukle()
        {
            try
            {
                string dosyaYolu = "C:\\Users\\dmlis\\Desktop\\PROLAB2\\PROLAB2\\PROLAB2\\Data\\veriseti.json";
                string jsonVerisi = File.ReadAllText(dosyaYolu);
                ulasimVerisi = JsonConvert.DeserializeObject<UlasimVerisi>(jsonVerisi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri seti okunurken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointLatLng nokta = gMapControl1.FromLocalToLatLng(e.X, e.Y);
                if (secimBaslangic)
                {
                    txtStartLat.Text = nokta.Lat.ToString("F6", CultureInfo.InvariantCulture);
                    txtStartLon.Text = nokta.Lng.ToString("F6", CultureInfo.InvariantCulture);
                    secimBaslangic = false;
                    MessageBox.Show("Başlangıç konumu haritadan seçildi.");
                }
                else if (secimHedef)
                {
                    txtDestLat.Text = nokta.Lat.ToString("F6", CultureInfo.InvariantCulture);
                    txtDestLon.Text = nokta.Lng.ToString("F6", CultureInfo.InvariantCulture);
                    secimHedef = false;
                    MessageBox.Show("Hedef konumu haritadan seçildi.");
                }
            }
        }


        private void btnSetStartFromMap_Click(object sender, EventArgs e)
        {
            secimBaslangic = true;
            secimHedef = false;
            MessageBox.Show("Lütfen haritada başlangıç konumunu seçiniz.");
        }


        private void btnSetDestFromMap_Click(object sender, EventArgs e)
        {
            secimHedef = true;
            secimBaslangic = false;
            MessageBox.Show("Lütfen haritada hedef konumunu seçiniz.");
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {

            if (!double.TryParse(txtKrediKarti.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double krediKartiBakiye))
            {
                MessageBox.Show("Kredi Kartı bakiyesi geçersiz. Lütfen nokta (.) kullanarak giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!double.TryParse(txtNakit.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double nakitBakiye))
            {
                MessageBox.Show("Nakit bakiyesi geçersiz. Lütfen nokta (.) kullanarak giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!double.TryParse(txtKentKart.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double kentKartBakiye))
            {
                MessageBox.Show("KentKart bakiyesi geçersiz. Lütfen nokta (.) kullanarak giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!double.TryParse(txtStartLat.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double startLat))
            {
                MessageBox.Show("Başlangıç enlemi geçersiz. Lütfen nokta (.) kullanarak giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!double.TryParse(txtStartLon.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double startLon))
            {
                MessageBox.Show("Başlangıç boylamı geçersiz. Lütfen nokta (.) kullanarak giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!double.TryParse(txtDestLat.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double destLat))
            {
                MessageBox.Show("Hedef enlemi geçersiz. Lütfen nokta (.) kullanarak giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!double.TryParse(txtDestLon.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double destLon))
            {
                MessageBox.Show("Hedef boylamı geçersiz. Lütfen nokta (.) kullanarak giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            Koordinat baslangic = new Koordinat { Lat = startLat, Lon = startLon };
            Koordinat varis = new Koordinat { Lat = destLat, Lon = destLon };
            Yolcu yolcu;
            string seciliYolcu = cmbPassengerType.SelectedItem?.ToString();
            if (seciliYolcu == "Öğrenci")
            {
                yolcu = new Ogrenci();
            }
            else if (seciliYolcu == "Yaşlı")
            {
                yolcu = new Yasli();
            }
            else
            {
                yolcu = new GenelYolcu();
            }

            IRotaStratejisi strateji;
            string seciliRota = cmbRouteType.SelectedItem?.ToString();
            if (seciliRota == "En Ucuz Rota")
            {
                strateji = new EnUcuzRotaStratejisi();
            }
            else
            {
                strateji = new EnHizliRotaStratejisi();
            }

            IRotaPlanlayici rotaPlanlayici = new RotaPlanlayici(strateji);
            RotaModel rota = rotaPlanlayici.RotaPlanla(ulasimVerisi, baslangic, varis, yolcu);
            if (rota == null)
            {
                MessageBox.Show("Rota hesaplanamadı veya başlangıç/varış durakları bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string cikti = "Hesaplanan Rota:\r\n";
            foreach (var segment in rota.Segmentler)
            {
                cikti += $"{segment.Mod} ile: {segment.Baslangic.name} -> {segment.Varis.name} | " +
                         $"Mesafe: {segment.Mesafe:F2} km, Süre: {segment.Sure} dk, Ücret: {segment.Ucret:F2} TL\r\n";
            }
            cikti += $"\r\nToplam Mesafe: {rota.ToplamMesafe:F2} km";
            cikti += $"\r\nToplam Süre: {rota.ToplamSure} dk";
            cikti += $"\r\nToplam Ücret: {rota.ToplamUcret:F2} TL";
            txtOutput.Text = cikti;


            HaritadaRotaGoster(rota);


            var krediKartiOdeme = new KrediKartiOdeme { Bakiye = krediKartiBakiye };
            var nakitOdeme = new NakitOdeme { Bakiye = nakitBakiye };
            var kentKartOdeme = new KentKartOdeme { Bakiye = kentKartBakiye };

            var odemeIslemci = new OdemeIslemci(krediKartiOdeme, nakitOdeme, kentKartOdeme);
            bool odemeBasarili = odemeIslemci.OdemeYap(rota, yolcu);

            if (odemeBasarili)
            {
                MessageBox.Show("Ödeme başarıyla gerçekleştirildi.", "Ödeme Bilgisi");
            }
            else
            {
                MessageBox.Show("Ödeme yapılamadı. Cüzdan bakiyeniz yetersiz.", "Ödeme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HaritadaRotaGoster(RotaModel rota)
        {
            gMapControl1.Overlays.Clear();

            var rotaNoktalar = new List<PointLatLng>();
            if (rota.Segmentler.Count > 0)
            {
                rotaNoktalar.Add(new PointLatLng(rota.Segmentler[0].Baslangic.lat, rota.Segmentler[0].Baslangic.lon));
                foreach (var segment in rota.Segmentler)
                {
                    rotaNoktalar.Add(new PointLatLng(segment.Varis.lat, segment.Varis.lon));
                }
            }
            GMapRoute mapRota = new GMapRoute(rotaNoktalar, "Hesaplanan Rota")
            {
                Stroke = new System.Drawing.Pen(System.Drawing.Color.Blue, 3)
            };
            GMapOverlay rotaOverlay = new GMapOverlay("rotalar");
            rotaOverlay.Routes.Add(mapRota);
            gMapControl1.Overlays.Add(rotaOverlay);

            GMapOverlay markerOverlay = new GMapOverlay("markerlar");

            foreach (var nokta in rotaNoktalar)
            {
                GMapMarker marker = new GMarkerGoogle(nokta, GMarkerGoogleType.red_dot);
                markerOverlay.Markers.Add(marker);
            }

            if (ulasimVerisi != null && ulasimVerisi.Duraklar != null)
            {
                foreach (var durak in ulasimVerisi.Duraklar)
                {
                    PointLatLng point = new PointLatLng(durak.lat, durak.lon);
                    GMapMarker durakMarker = new GMarkerGoogle(point, GMarkerGoogleType.green_dot);
                    durakMarker.ToolTipText = durak.name;
                    markerOverlay.Markers.Add(durakMarker);
                }
            }
            gMapControl1.Overlays.Add(markerOverlay);


            if (rotaNoktalar.Any())
            {
                gMapControl1.Position = rotaNoktalar[0];
                gMapControl1.Zoom = 14;
            }
        }


        private void HaritadaDuraklariGoster()
        {
            if (ulasimVerisi == null || ulasimVerisi.Duraklar == null)
                return;

            GMapOverlay durakOverlay = new GMapOverlay("duraklar");
            foreach (var durak in ulasimVerisi.Duraklar)
            {
                PointLatLng point = new PointLatLng(durak.lat, durak.lon);
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.green_dot);
                marker.ToolTipText = durak.name;
                durakOverlay.Markers.Add(marker);
            }
            gMapControl1.Overlays.Add(durakOverlay);
        }


        private void HaritadaHatlariGoster()
        {
            if (ulasimVerisi == null || ulasimVerisi.Duraklar == null)
                return;

            var stopDict = ulasimVerisi.Duraklar.ToDictionary(s => s.id, s => s);

            GMapOverlay busOverlay = new GMapOverlay("busConnections");
            GMapOverlay tramOverlay = new GMapOverlay("tramConnections");

            foreach (var durak in ulasimVerisi.Duraklar)
            {
                if (durak.nextStops != null)
                {
                    foreach (var baglanti in durak.nextStops)
                    {
                        if (stopDict.ContainsKey(baglanti.stopId))
                        {
                            var hedefDurak = stopDict[baglanti.stopId];

                            if (durak.type.Equals("bus", StringComparison.OrdinalIgnoreCase) &&
                                hedefDurak.type.Equals("bus", StringComparison.OrdinalIgnoreCase))
                            {
                                var points = new List<PointLatLng>
                                {
                                    new PointLatLng(durak.lat, durak.lon),
                                    new PointLatLng(hedefDurak.lat, hedefDurak.lon)
                                };
                                GMapRoute route = new GMapRoute(points, "BusConnection")
                                {
                                    Stroke = new System.Drawing.Pen(System.Drawing.Color.Red, 2)
                                };
                                busOverlay.Routes.Add(route);
                            }
                            else if (durak.type.Equals("tram", StringComparison.OrdinalIgnoreCase) &&
                                     hedefDurak.type.Equals("tram", StringComparison.OrdinalIgnoreCase))
                            {
                                var points = new List<PointLatLng>
                                {
                                    new PointLatLng(durak.lat, durak.lon),
                                    new PointLatLng(hedefDurak.lat, hedefDurak.lon)
                                };
                                GMapRoute route = new GMapRoute(points, "TramConnection")
                                {
                                    Stroke = new System.Drawing.Pen(System.Drawing.Color.Blue, 2)
                                };
                                tramOverlay.Routes.Add(route);
                            }
                        }
                    }
                }
            }

            gMapControl1.Overlays.Add(busOverlay);
            gMapControl1.Overlays.Add(tramOverlay);
        }
    }
}
