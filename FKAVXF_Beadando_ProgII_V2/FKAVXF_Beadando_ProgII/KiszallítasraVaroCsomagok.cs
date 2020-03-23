using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FKAVXF_Beadando_ProgII
{
    class KiszallítasraVaroCsomagok : Csomag
    {
        public KiszallítasraVaroCsomagok(string fajta, int prioritas, int meret,int ertek) : base(fajta, prioritas, meret,ertek)
        {
        }
        public static LancoltLista<Csomag> CsakBeolvas(string honnan)
        {
            string [] sor;
            LancoltLista<Csomag> csomagok = new LancoltLista<Csomag>();
            Csomag csomag;
            StreamReader sr = new StreamReader(honnan);
            while(!sr.EndOfStream)
            {
                sor = sr.ReadLine().Split(';');
                csomag = new Csomag(sor[0], int.Parse(sor[1]), int.Parse(sor[2]), int.Parse(sor[3]));
                csomagok.PrioritasBerakNovekvo(csomag,csomag.Prioritas);                   
            }
            sr.Close();
            return csomagok;

        }
        public static int SorokSzama(string honnan)
        {
            int db = 0;
            StreamReader sr = new StreamReader(honnan);
            while (!sr.EndOfStream)
            {
                sr.ReadLine();
                db++;
            }
            sr.Close();
            return db;
        }
    }
}
