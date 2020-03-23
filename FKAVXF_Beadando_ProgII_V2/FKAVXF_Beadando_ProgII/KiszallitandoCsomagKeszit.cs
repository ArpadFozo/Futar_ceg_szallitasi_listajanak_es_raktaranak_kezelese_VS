using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FKAVXF_Beadando_ProgII
{
    static class KiszallitandoCsomagKeszit
    {
        static Random rnd = new Random();
        private static string[] csomagFajta;
        
        public static void KiszallitandoCsomag(string honnan, Stopwatch stw)
        {
            StreamReader sr = new StreamReader("CuccFajta.txt");
                csomagFajta = sr.ReadLine().Split(',');//egy sorba kell írni az egészet
            sr.Close();
            StreamWriter sw = new StreamWriter(honnan);
            for (int i = 0; i < rnd.Next(1, 3); i++)//hány csomag érkezzen nap közben
            {
                int csomagesely = rnd.Next(0, 100);
                if (csomagesely <= 33)// 50-50 az esélye h azonnal szállítandó/nem sürgős vagy határidős
                {
                    // csomagfajta; prioritás; méret 
                    sw.WriteLine(csomagFajta[rnd.Next(0, csomagFajta.Length)] + ";" + -1 + ";" + rnd.Next(10, 100) + ";" + rnd.Next(999,999999));
                }
                else if (33 < csomagesely && csomagesely <= 66)
                {
                    sw.WriteLine(csomagFajta[rnd.Next(0, csomagFajta.Length)] + ";" + 100 + ";" + rnd.Next(10, 100) + ";" + rnd.Next(999, 999999));
                }
                else
                {
                    // csomagfajta; mikorra; méret
                    sw.WriteLine(csomagFajta[rnd.Next(0, csomagFajta.Length)] + ";" + rnd.Next((Convert.ToInt32(stw.ElapsedMilliseconds) / 1000) + 2, 24) + ";" + rnd.Next(10, 100) + ";" + rnd.Next(999, 999999));
                }
            }
            sw.Close();

        }
        public static void TxtTorles(string hova)
        {
            StreamWriter sw = new StreamWriter(hova);
            sw.Write("");
            sw.Close();
        }
        public static void Kiirat(Csomag csomag, string hova)
        {
            StreamWriter sw = new StreamWriter(hova,true);
            sw.WriteLine(csomag.Fajta + ";" + csomag.Prioritas + ";" + csomag.Meret + ";" + csomag.Ertek);
            sw.Close();
        }
    }
}
