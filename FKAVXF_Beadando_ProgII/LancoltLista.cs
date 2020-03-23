using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKAVXF_Beadando_ProgII
{
    class LancoltLista<T> : IEnumerable<T>
    {
        public int Count(LancoltLista<Csomag> csomag)
        {
            int db = 0;
            foreach (var akt in csomag)
            {
                if (akt != null)
                {
                    db++;
                }
            }
            return db;
        }
        class ListaElem
        {
            public int kulcs;
            public T tartalom;
            public ListaElem kovetkezo;
        }
        class ListaEnumereter : IEnumerator<T>
        {
            ListaElem fej;
            ListaElem akt;
            public ListaEnumereter(ListaElem elso)
            {
                fej = elso;
                akt = null;
            }
            public void Dispose()
            {
                fej = null;
                akt = null;
            }
            public T Current
            {
                get { return akt.tartalom; }
            }
            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }
            public bool MoveNext()
            {
                if (akt == null)
                {
                    akt = fej;
                }
                else
                {
                    akt = akt.kovetkezo;
                }
                return akt != null;
            }
            public void Reset()
            {
                akt = null;
            }
        }
        ListaElem fej;
        public void EgeszListaTorles()
        {
            fej.kovetkezo = null;
            fej = null;
        }
        public void Torles(T elem)
        {
            ListaElem p = fej;
            ListaElem e = null;
            while (p!= null && !p.tartalom.Equals(elem))
            {
                e = p;
                p = p.kovetkezo;
            }
            if (p != null)
            {
                if(e==null)
                {
                    fej = p.kovetkezo;
                }
                else
                {
                    e.kovetkezo = p.kovetkezo;
                }
            }
        }
        public void Bejaras(DoSomething operation)//valamit csinál vele bejárás során
        {
            ListaElem p = fej;
            while (p != null)
            {
                operation(p.tartalom);
                p = p.kovetkezo;
            }
        }
        public delegate void DoSomething(T elem);
        public void PrioritasBerakNovekvo(T elem, int kulcs)
        {
            ListaElem uj = new ListaElem();
            uj.tartalom = elem;
            uj.kulcs = kulcs;
            if (fej == null)
            {
                fej = uj;
                uj.kovetkezo = null;
            }
            else
            {
                if (uj.kulcs <= fej.kulcs)
                {
                    uj.kovetkezo = fej;
                    fej = uj;
                }
                else
                {
                    ListaElem p = fej;
                    ListaElem e = null;
                    while (p != null && p.kulcs < uj.kulcs)
                    {
                        e = p;
                        p = p.kovetkezo;
                    }
                    if (p == null)
                    {
                        uj.kovetkezo = null;
                        e.kovetkezo = uj;
                    }
                    else
                    {
                        uj.kovetkezo = p;
                        e.kovetkezo = uj;
                    }
                }
            }
        }
        public void PrioritasBerakCsokkeno(T elem, int kulcs)
        {
            ListaElem uj = new ListaElem();
            uj.tartalom = elem;
            uj.kulcs = kulcs;
            if (fej == null)
            {
                fej = uj;
                uj.kovetkezo = null;
            }
            else
            {
                if (uj.kulcs >= fej.kulcs)
                {
                    uj.kovetkezo = fej;
                    fej = uj;
                }
                else
                {
                    ListaElem p = fej;
                    ListaElem e = null;
                    while (p != null && p.kulcs > uj.kulcs)
                    {
                        e = p;
                        p = p.kovetkezo;
                    }
                    if (p == null)
                    {
                        uj.kovetkezo = null;
                        e.kovetkezo = uj;
                    }
                    else
                    {
                        uj.kovetkezo = p;
                        e.kovetkezo = uj;
                    }
                }
            }
        }
        public T VisszaElsoElem()
        {
            if (fej != null)
            {
                ListaElem seged = fej;
                fej = fej.kovetkezo;
                return seged.tartalom;
            }
            else
            {
                throw new ElfogytakACsomagok("Nincs több kiszállítandó elem");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListaEnumereter(fej);
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
