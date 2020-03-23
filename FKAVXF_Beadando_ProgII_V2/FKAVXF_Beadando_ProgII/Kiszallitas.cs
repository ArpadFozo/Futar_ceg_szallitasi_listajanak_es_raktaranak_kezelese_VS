using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FKAVXF_Beadando_ProgII
{
    static class Kiszallitas
    {
        private static Random rnd = new Random();
        private static void FutarIdo()
        {
            Stopwatch kiszalitasIdo = new Stopwatch();
            kiszalitasIdo.Start();
            while (kiszalitasIdo.ElapsedMilliseconds < rnd.Next(500, 3000)) // fél óra és 3 óra közötti időt határoz meg
            {
            }
            kiszalitasIdo.Stop();
        }//Szimulálja a futár úton tölött idejét
        public static void Ksz(Csomag Csomag,Stopwatch sw) // sw kell mert leellenőrzi egyáltalán van-e még ideje kiszállítani
        {
            FutarIdo();
            if (sw.ElapsedMilliseconds / 1000 < Csomag.Prioritas && Csomag.Prioritas != 100 && Csomag.Prioritas != -1 && rnd.Next(0, 101) < 90)
            {
                KiszallitandoCsomagKeszit.Kiirat(Csomag, "Kivitt.txt");
                Console.WriteLine("A csomag sikeresen kézbesítve lett: " + Csomag.Fajta);
            }
            else if (Csomag.Prioritas == -1 && rnd.Next(0, 101) < 90)
            {
                KiszallitandoCsomagKeszit.Kiirat(Csomag, "Kivitt.txt");
                Console.WriteLine("A csomag sikeresen kézbesítve lett: " + Csomag.Fajta);
            }
            else if (Csomag.Prioritas == 100 && rnd.Next(0, 101) < 90)
            {
                KiszallitandoCsomagKeszit.Kiirat(Csomag, "Kivitt.txt");
                Console.WriteLine("A csomag sikeresen kézbesítve lett: " + Csomag.Fajta);
            }
            else
            {
                throw new NemSikerultAKisszallítas(Csomag, "Nem sikerült a kiszállítás az alábbi csomagnál: " + Csomag.Fajta + " " + Csomag.Prioritas + " " + Csomag.Meret + " " + Csomag.Ertek);
            }
        }//Kiszállítást szimláló metódus

    }
}
   
