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
        public string schraubenkopf;
        public MetrischeGewindegroesse metrischeGewindegroesse;
        public Schaftlaenge schaftLaenge;
        public Gewindelaenge gewindeLaenge;
        public string festigkeitsklasse;
        public int anzahl;
        public float volumen;
        public float preis;

        public Schraube()   //Konstruktor kann gelöscht werden, weil es diesen standardmäßig existiert, solange es keinen anderen konstruktor (der gefüllt ist) gibt
        {

        }

        public void printSchraube(int name) //Unterprogramm zur Ausgabe der Schraubeninformationen
        {
            Console.WriteLine("");
            Console.WriteLine("Informationen über die Schraube " + (name+1) + ":");
            Console.WriteLine("Gewinderichtung: " + gewinderichtung);
            Console.WriteLine("Schraubenkopfart:" + schraubenkopf);
            Console.WriteLine("Gewindegröße: M" + metrischeGewindegroesse.bezeichnung);
            Console.WriteLine("Schaftlänge: " + schaftLaenge.schaftlaenge + " mm");
            Console.WriteLine("Gewindelänge: " + gewindeLaenge.gewindeLaenge + " mm");
            Console.WriteLine("Festigkeitsklasse: " + festigkeitsklasse);
            Console.WriteLine("Anzahl: " + anzahl);
        }
    }
}
