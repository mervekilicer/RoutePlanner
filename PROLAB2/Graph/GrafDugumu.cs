using System.Collections.Generic;
using PROLAB2.Models;

namespace PROLAB2.Graph
{

    public class GrafDugumu
    {
        public Durak Durak { get; }
        public List<GrafKenar> Kenarlar { get; }

        public GrafDugumu(Durak durak)
        {
            Durak = durak;
            Kenarlar = new List<GrafKenar>();
        }
    }
}
