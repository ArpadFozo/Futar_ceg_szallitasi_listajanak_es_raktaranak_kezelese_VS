using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKAVXF_Beadando_ProgII
{
    class Csomag
    {
        string fajta;
        int prioritas;
        int meret;
        int ertek;
        public Csomag(string fajta,int prioritas,int meret,int ertek)
        {
            this.fajta = fajta;
            this.prioritas = prioritas;
            this.meret = meret;
            this.ertek = ertek;

        }
        public string Fajta
        {
            get
            {
                return fajta;
            }
        }

        public int Prioritas
        {
            get
            {
                return prioritas;
            }
        }

        public int Meret
        {
            get
            {
                return meret;
            }
        }

        public int Ertek
        {
            get
            {
                return ertek;
            }
        }
    }
}
