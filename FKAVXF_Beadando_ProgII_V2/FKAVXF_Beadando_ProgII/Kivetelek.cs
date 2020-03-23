using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FKAVXF_Beadando_ProgII
{
    class Kivetelek : Exception
    {
        string msg;
        public Kivetelek(string msg) : base (msg)
        {
            this.msg = msg;
        }

        public string Msg
        {
            get
            {
                return msg;
            }
        }
    }
    class NemSikerultAKisszallítas : Kivetelek
    {
        public NemSikerultAKisszallítas(Csomag csomag,string msg) : base(msg)
        {
        }
    }
    class ElfogytakACsomagok : Kivetelek
    {
        public ElfogytakACsomagok(string msg) : base (msg)
        {

        }
    }
}
