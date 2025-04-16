using PROLAB2.Models;

namespace PROLAB2.Graph
{

    public class GrafKenar
    {
        public GrafDugumu Kaynak { get; }
        public GrafDugumu Hedef { get; }
        public double Mesafe { get; }
        public int Sure { get; }
        public double Ucret { get; }
        public string Mod { get; }

        public GrafKenar(GrafDugumu kaynak, GrafDugumu hedef, double mesafe, int sure, double ucret, string mod)
        {
            Kaynak = kaynak;
            Hedef = hedef;
            Mesafe = mesafe;
            Sure = sure;
            Ucret = ucret;
            Mod = mod;
        }
    }
}
