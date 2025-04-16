using System.Collections.Generic;
using System.Linq;
using PROLAB2.Models;

namespace PROLAB2.Graph
{

    public class DurakGrafi
    {

        private readonly Dictionary<string, GrafDugumu> dugumler;

        public DurakGrafi(List<Durak> duraklar)
        {
            dugumler = new Dictionary<string, GrafDugumu>();
            BuildGraph(duraklar);
        }


        private void BuildGraph(List<Durak> duraklar)
        {

            foreach (var d in duraklar)
            {
                if (!dugumler.ContainsKey(d.id))
                {
                    dugumler[d.id] = new GrafDugumu(d);
                }
            }


            foreach (var dugum in dugumler.Values)
            {

                if (dugum.Durak.nextStops != null)
                {
                    foreach (var baglanti in dugum.Durak.nextStops)
                    {
                        if (dugumler.ContainsKey(baglanti.stopId))
                        {
                            var hedefDugum = dugumler[baglanti.stopId];
                            var kenar = new GrafKenar(dugum, hedefDugum, baglanti.mesafe, baglanti.sure, baglanti.ucret, dugum.Durak.type);
                            dugum.Kenarlar.Add(kenar);
                        }
                    }
                }


                if (dugum.Durak.transfer != null)
                {
                    string transferId = dugum.Durak.transfer.TransferStopId;
                    if (dugumler.ContainsKey(transferId))
                    {
                        var hedefDugum = dugumler[transferId];

                        var kenar = new GrafKenar(dugum, hedefDugum, 0, dugum.Durak.transfer.TransferSure, dugum.Durak.transfer.TransferUcret, "transfer");
                        dugum.Kenarlar.Add(kenar);
                    }
                }
            }
        }


        public GrafDugumu GetDugum(string durakId)
        {
            return dugumler.ContainsKey(durakId) ? dugumler[durakId] : null;
        }


        public IEnumerable<GrafDugumu> Dugumler => dugumler.Values;


        public IEnumerable<GrafKenar> GetKomsuKenarlar(GrafDugumu dugum)
        {
            return dugum?.Kenarlar ?? Enumerable.Empty<GrafKenar>();
        }
    }
}
