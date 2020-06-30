using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Frekwencja
{
    abstract class Komponent
    {
        protected string Nazwa;
        public Komponent(string nazwa)
        {
            Nazwa = nazwa;
        }
        public abstract List<string> Wypisz(int stopien, List<string> lista);
        public abstract double PoliczFrekwencje();
        public abstract double SumaZarejestrowanych();
        public abstract double SumaGlosujacych();
    }
    class Kompozyt : Komponent
    {
        public Kompozyt(string nazwa) : base(nazwa) { }
        private List<Komponent> Komponenty = new List<Komponent>();
        public override List<string> Wypisz(int stopien, List<string> lista)
        {
            lista.Add(new String('-', stopien) + Nazwa + ", Frekwencja: " + PoliczFrekwencje() + "%");
            foreach (var k in Komponenty)
               lista=k.Wypisz(stopien + 1,lista);
            return lista;
        }
        public void Dodaj(Komponent komponent)
        {
            Komponenty.Add(komponent);
        }

        public override double PoliczFrekwencje()
        {
            double frekwencja;
            frekwencja = Math.Round((SumaGlosujacych() / SumaZarejestrowanych()) * 100,2);
            return frekwencja;
        }
        public override double SumaGlosujacych()
        {
            double suma = 0;
            foreach (var k in Komponenty)
            {
                suma += k.SumaGlosujacych();
            }
            return suma;
        }
        public override double SumaZarejestrowanych()
        {
            double suma = 0;
            foreach (var k in Komponenty)
            {
                suma += k.SumaZarejestrowanych();
            }
            return suma;
        }
    }
    class Gmina : Komponent //liść
    {
        private double Zarejestrowani;
        private double Zaglosowali;
        public Gmina(string nazwa, double zarejestrowani, double zaglosowali) : base(nazwa)
        {
            Zarejestrowani = zarejestrowani;
            Zaglosowali = zaglosowali;
        }
        public override List<string> Wypisz(int stopien, List<string> lista)
        {
            lista.Add(new String('-', stopien) + Nazwa + ", Frekwencja: " + PoliczFrekwencje() + "%");
            return lista;
        }
        public override double PoliczFrekwencje()
        {
            double frekwencja;
            frekwencja = Math.Round((SumaGlosujacych() / SumaZarejestrowanych()) * 100, 2);
            return frekwencja;
        }
        public override double SumaGlosujacych()
        {
            return Zaglosowali;
        }
        public override double SumaZarejestrowanych()
        {
            return Zarejestrowani;
        }
    }
}
