using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Frekwencja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Kompozyt kraj = new Kompozyt("Polska");
        Kompozyt s = new Kompozyt("Slaskie");
        Gmina d = new Gmina("Dabrowa Gornicza", 92329, 60027);
        public MainWindow()
        {
            
            InitializeComponent();
        }
        private void Otwarcie(object sender, RoutedEventArgs e) 
        {
            Kompozyt za = new Kompozyt("Zawiercianski");
            Gmina zal = new Gmina("Lazy", 12694, 8245);
            Gmina k = new Gmina("Katowice", 222937, 146330);
            kraj.Dodaj(s);
            s.Dodaj(k);
            s.Dodaj(za);
            s.Dodaj(d);
            za.Dodaj(zal);
            za.Dodaj(new Gmina("Ogrodzieniec", 7512, 4925));
            Kompozyt o = new Kompozyt("Opolskie");
            Kompozyt kr = new Kompozyt("Krapkowicki");
            kraj.Dodaj(o);
            o.Dodaj(kr);
            kr.Dodaj(new Gmina("Walce", 4453, 2044));
            /*
             Dzięki wzorcowi kompozyt nie trzeba tworzyć osobnych list obiektów, dla każdego województwa i dla każdego powiatu.
            Frekwencja dla każdej jednostki administracyjnej liczy i zapisuje się za wywołaniem jednej funkcji 
            obsługującej zarówno kompozyty (województwa, powiaty) jak równieć liście (gminy, miasta). 
            
             */
            List<string> lista = new List<string>();
            lista = kraj.Wypisz(1, lista);
            Odswiez(lista);
        }

        public void Odswiez(List<string>lista)
        {
            listBox.Items.Clear();
            foreach (string das in lista)
                listBox.Items.Add(das);
            listBox.Items.Refresh();
            //Wykorzystując te same funckje mamy dostęp do każdego elementu hierarhii Od pierwszego kompozytu do ostatniego liścia
            textBlockUprawnieniPolska.Text = kraj.SumaZarejestrowanych().ToString();
            textBlockZaglosowaliPolska.Text = kraj.SumaGlosujacych().ToString();
            textBlockUprawnieniSlaskie.Text = s.SumaZarejestrowanych().ToString();
            textBlockZaglosowaliSlaskie.Text = s.SumaGlosujacych().ToString();
            textBlockUprawnieniDG.Text = d.SumaZarejestrowanych().ToString();
            textBlockZaglosowaliDG.Text = d.SumaGlosujacych().ToString();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            
            //Można łatwo dopisać kolejne liście i kompozyty zarówno przed "podpięciem" pod wyższy kompozyt
            Kompozyt malopolskie = new Kompozyt("Małopolskie");
            malopolskie.Dodaj(new Gmina("Kraków", 606178, 433674));
            kraj.Dodaj(malopolskie);
            
            //Jak i po:
            
            Kompozyt nowotarski = new Kompozyt("Nowotarski");
            malopolskie.Dodaj(nowotarski);
            malopolskie.Dodaj(new Gmina("Tarnów", 86573, 55302));
            nowotarski.Dodaj(new Gmina("Czarny Dunajec", 17314, 9020));
            nowotarski.Dodaj(new Gmina("Krościenko nad Dunajcem", 5551, 3604));
            nowotarski.Dodaj(new Gmina("Szczawnica", 6335, 4067));
            
            List<string> lista = new List<string>();
            lista = kraj.Wypisz(1, lista);
            Odswiez(lista);
            Dodaj.IsEnabled=false;
        }

        private void DodajSlaskie_Click(object sender, RoutedEventArgs e)
        {
            //Podczas dodawania wystarczy tylko dodać obiekt do kompozytu o jeden wyżej w hierarhii, nie trzeba dodawac do każdego pokolei
            Kompozyt zywiecki =new Kompozyt ("Żywiecki");
            zywiecki.Dodaj(new Gmina("Ujsoły", 3702, 2373));
            zywiecki.Dodaj(new Gmina("Milówka", 7991, 5170));
            zywiecki.Dodaj(new Gmina("Rajcza", 7137, 4439));
            s.Dodaj(zywiecki);
            List<string> lista = new List<string>();
            lista = kraj.Wypisz(1, lista);
            Odswiez(lista);
            DodajSlaskie.IsEnabled = false;
        }
    }
}
