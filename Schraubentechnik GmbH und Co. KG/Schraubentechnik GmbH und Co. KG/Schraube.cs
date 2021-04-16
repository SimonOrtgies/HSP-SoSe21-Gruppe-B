using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Schraubentechnik_GmbH_und_Co._KG
{

    public class Schraube  //Objekt SChraube
    {
        public Gewinderichtung gewinderichtung; //Variablen/Informationen die die Schraube beeinhaltet
        public MetrischeGewindegroesse metrischeGewindegroesse;
        public Schaftlaenge schaftLaenge;
        public Gewindelaenge gewindeLaenge;
        public string festigkeitsklasse;

        public Schraube (Gewinderichtung g, Schaftlaenge schaftL, Gewindelaenge gewindeL)  //Aufbau der Struktur
        {
            gewinderichtung = g;
            schaftLaenge = schaftL;
            gewindeLaenge = gewindeL;
        }

        public Schraube(Schaftlaenge schaftL, Gewindelaenge gewindeL)
        {
            gewinderichtung = Gewinderichtung.Rechtsgewinde;
            schaftLaenge = schaftL;
            gewindeLaenge = gewindeL;
        }
        public Schraube()   //Leeres Objekt, siehe Überladung und so
        {
          
        }

        public void printSchraube() //Unterprogramm zur Ausgabe der Schraubeninformationen
        {
            Console.WriteLine("");
            Console.WriteLine("INFOS über die Schraube:");
            Console.WriteLine("Gewinderichtung: " + gewinderichtung);
            Console.WriteLine("Gewindegröße: M" + metrischeGewindegroesse.bezeichnung);
            Console.WriteLine("Schaftlänge: " + schaftLaenge.schaftlaenge + " mm");
            Console.WriteLine("Gewindelänge: " + gewindeLaenge.gewindeLaenge + " mm");
            Console.WriteLine("Festigkeitsklasse: " + festigkeitsklasse);
        }
    }
}
