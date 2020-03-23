using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FKAVXF_Beadando_ProgII // a program 4 db txt-t használ ,Meghiúsult.txt ahova kigyűjti az aznapi sikertelen szállításokat, BeolvasniValoTxt.txt ahonnan beolvassa a szállítani való csomagokat, Kivitt.txt ahova kiírja a sikeres kiszállításokat és CuccFajta.txt ahonnan be tudja olvasni milyen típusú csomagokat szállít(Ágy, Szekrény, stb)
{
    delegate void SikertelenKiszallitas(Csomag csomag);
    class Program
    {
        static void TeljesListaTorles(LancoltLista<Csomag> csomag)
        {
            foreach (var akt in csomag)
            {
                csomag.Torles(akt);
            }
        } // teszteléshez
        static void KiirTxtbe(Csomag csomag)
        {
            KiszallitandoCsomagKeszit.Kiirat(csomag, "Meghiúsultszállítások.txt");
        }
        static void NapVegeTxtbeIr(LancoltLista<Csomag> Csomagok)
        {
            Csomagok.Bejaras(KiirTxtbe);
        }
        static bool SzimulacioVege()
        {
            Start:
            Console.WriteLine("Bezárja a céget? (y | n)");
            string valasz = Console.ReadLine();
            if (valasz == "y")
            {
                return true;
            }
            else if (valasz != "y" && valasz != "n")
            {
                Console.WriteLine("Rossz válaszlehetőséget adott meg");
                goto Start;
            }
            return false;
        }
        static LancoltLista<Csomag> RendezettenBeolvas(string honnan)
        {
            LancoltLista<Csomag> Csomagok;
            int maxindex = KiszallítasraVaroCsomagok.SorokSzama(honnan);
            Csomagok = KiszallítasraVaroCsomagok.CsakBeolvas(honnan);
            return Csomagok;
        }
        static void Kiir(Csomag csomag)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine(" Fajta:  " + csomag.Fajta);
            Console.WriteLine(" Prioritás: " + csomag.Prioritas);
            Console.WriteLine(" Méret: " + csomag.Meret);
            Console.WriteLine(" Érték: " + csomag.Ertek + " Ft");
            Console.WriteLine("------------------------------");
        }
        static void Main(string[] args)
        {
            SikertelenKiszallitas v = new SikertelenKiszallitas(KiirTxtbe);
            SikertelenKiszallitas esemeny = new SikertelenKiszallitas(KiirTxtbe);
            esemeny += Kiir;
            LancoltLista<Csomag> Csomagok = new LancoltLista<Csomag>();
            Csomag segedCsomag;
            Stopwatch sw = new Stopwatch();//stopper
                do
                {
                KiszallitandoCsomagKeszit.TxtTorles("Meghiúsultszállítások.txt"); // nehogy visszaolvasson régebbieket
                sw.Reset();
                sw.Start();
                int db = 1;
                while (sw.ElapsedMilliseconds < 24000 && db < 20)//24 mp egy nap
                {
                    Eleje:
                    KiszallitandoCsomagKeszit.KiszallitandoCsomag("BeolvasniValoTxt.txt", sw);
                    Csomagok = RendezettenBeolvas("BeolvasniValoTxt.txt");
                    try
                    {
                        segedCsomag = Csomagok.VisszaElsoElem();
                    }
                    catch(ElfogytakACsomagok e)
                    {
                        Console.WriteLine(e.Msg);
                        goto Eleje;
                    }
                    try
                    {
                        Kiszallitas.Ksz(segedCsomag, sw);
                    }
                    catch(NemSikerultAKisszallítas nem)
                    {
                        Console.WriteLine(nem.Msg);
                        esemeny(segedCsomag);
                    }
                    db++;
                }
                sw.Stop();
                KiszallitandoCsomagKeszit.TxtTorles("BeolvasniValoTxt.txt");
                NapVegeTxtbeIr(Csomagok);// a 24 mp leteltével a még listákban lévő maradék csomagokat kirakja a MeghhiúsúltSzállítások.txt-be
                Raktar.RaktarbaPakolasMoho();// a MeghiúsúltSzállítások.txt-ből elhelyezi a raktárba onnan meg vissza a BeolvasniValoTxt.txt-be a csomagokat
            } while (!SzimulacioVege());
            Console.WriteLine("Bezárta a céget.");
            Console.ReadLine();
        }
    }
}
