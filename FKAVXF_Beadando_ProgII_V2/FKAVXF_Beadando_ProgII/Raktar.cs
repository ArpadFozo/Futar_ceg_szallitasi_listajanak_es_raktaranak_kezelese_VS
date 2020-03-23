using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FKAVXF_Beadando_ProgII
{
    class Raktar : Csomag
    {
        static int raktarmeret = 1000; // raktar merete
        static Random rnd = new Random();
        public Raktar(string fajta, int prioritas, int meret, int ertek) : base(fajta, prioritas, meret,ertek)
        {
        }
        private static int OsszMeret(LancoltLista<Csomag> Csomagok)
        {
            int osszmeret=0;
            foreach (Csomag akt in Csomagok)
            {
                osszmeret += akt.Meret;
            }
            return osszmeret;
        }
        private static LancoltLista<Csomag> ListaRendezErtek(LancoltLista<Csomag> Csomagok) // érték alapján rendezzük buborék rendezéssel
        {
            LancoltLista<Csomag> SegedCsomag = new LancoltLista<Csomag>();
            foreach (Csomag akt in Csomagok)
            {
                SegedCsomag.PrioritasBerakCsokkeno(akt, akt.Ertek);
            }
            return SegedCsomag;
        }
        public static void RaktarbaPakolasMoho()
        {
            LancoltLista<Csomag> Kimenet = new LancoltLista<Csomag>();
            LancoltLista<Csomag> SegedCsomag = KiszallítasraVaroCsomagok.CsakBeolvas("Meghiúsultszállítások.txt");
            if (OsszMeret(Kimenet) <= raktarmeret) // megnézzük h az összes csomag befér-e a raktárba
            {
                foreach (Csomag akt in SegedCsomag)
                {
                    Kimenet.PrioritasBerakCsokkeno(akt, akt.Meret);
                }
            }
            else // ha nem, mohó algoritmussal bepakoljuk amit tudunk a többit pedig visszavisszük a küldőnek
            {
                Kimenet = ListaRendezErtek(SegedCsomag);
                SegedCsomag.EgeszListaTorles();
                foreach (Csomag akt in SegedCsomag)
                {
                    if (OsszMeret(Kimenet) < raktarmeret)
                        SegedCsomag.PrioritasBerakCsokkeno(akt, akt.Ertek);
                }
            }
            Kimenet = SegedCsomag;
            foreach (Csomag akt in Kimenet)
            {
                KiszallitandoCsomagKeszit.Kiirat(akt, "BeolvasniValoTxt.txt");
            }
        }
    }
}
