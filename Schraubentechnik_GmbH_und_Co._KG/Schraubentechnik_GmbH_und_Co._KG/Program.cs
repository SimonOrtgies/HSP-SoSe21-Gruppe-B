using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class Program
    {
        [STAThread]

        static void Main(string[] args)
        {
            Schraube s = new Schraube();
            s.metrischeGewindegroesse = null;  //Um zu überprüfen ob schon eine Gewindegröße gesetzt wurde
            new GUI_Zugriff(s);
        }





        static void Main2(string[] args)
        {

            //new GUI_Zugriff();  //Gui wird Gestartet
                   
            StartAusgabe();

            int anzahl;
            anzahl = Schraubenanzahl();

            Schraube[] sALT = new Schraube[anzahl];    //Schraubenarray

            for (int i = 0; i < anzahl; i++)
            {
                Console.WriteLine("Parameter Für Schraube " + (i + 1) + " eingeben:");
                sALT[i] = frageNachMassen();
                sALT[i].volumen = getVolumen(sALT[i]);
                sALT[i].masse = getMasse(sALT[i]);
                sALT[i].preis = getPreis(sALT[i]);
                Console.WriteLine("");
                //Objekt Schraube führt UNterprogramm aus
            }

            for (int i = 0; i < anzahl; i++)
            {
                sALT[i].printSchraube(i);
                Console.WriteLine("");        //Objekt Schraube führt UNterprogramm aus
            }

            Console.ReadKey();

        }



        static void StartAusgabe()
        {
            Console.WriteLine("Willkommen beim Schraubenprogramm von Schraubentechnik GmbH und Co. KG.");
            Console.WriteLine("");
            Console.WriteLine("Hier können Sie Ihre individuellen Schrauben mit folgenden Parametern erstellen:");
            Console.WriteLine("-Anzahl individueller Schrauben");
            Console.WriteLine("-Gewinderichtung");
            Console.WriteLine("-Gewindegröße");
            Console.WriteLine("-Schaftlänge");
            Console.WriteLine("-Gewindelänge");
            Console.WriteLine("-Festigkeit");
            Console.WriteLine("-Anzahl");
            Console.WriteLine("");
        }

        static int Schraubenanzahl()
        {
            int anzahl = 0;
            Boolean gueltig;
            Console.WriteLine("Wie viele individuell verschiedene Schrauben sollen erstellt werden?");
            do
            {
                gueltig = true;
                try
                {
                    anzahl = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Ungültige Eingabe!");
                    gueltig = false;
                }
            } while (!gueltig);
            Console.WriteLine("");

            return anzahl;
        }

        static public Schraube frageNachMassen()
        {

            Schraube schraube = new Schraube(); //Neues Objekt des Typ Schraube wird erstellt 

            schraube.gewinderichtung = UserAbfrage.getGewinderichtung();

            schraube.schraubenkopf = UserAbfrage.getSchraubenkopf();

            schraube.metrischeGewindegroesse = UserAbfrage.getMetrischegewindegroesse();    //Objekt Schraube wird die gewindegröße

            schraube.schaftLaenge = UserAbfrage.getSchaftlaenge(schraube);

            schraube.gewindeLaenge = UserAbfrage.getGewindelaenge(schraube);

            schraube.festigkeitsklasse = UserAbfrage.getFestigkeit();

            schraube.anzahl = UserAbfrage.getAnzahl();

            return schraube;    //Rückgabe des Objekt Schraube an Main (mit allen Informationen)
        }

        static public double getVolumen(Schraube schraube)  //NIcht löschen
        {

            double pi = Math.PI;
            double faktorschraubenkopf = 1.5;
            double schraubenkopf = schraube.metrischeGewindegroesse.mutterhoehe * schraube.metrischeGewindegroesse.bezeichnung * faktorschraubenkopf * schraube.metrischeGewindegroesse.bezeichnung * faktorschraubenkopf * pi / 4; //Volumen Schraubenkopf
            double V = schraubenkopf + schraube.anzahl * (schraube.gewindeLaenge.gewindeLaenge * schraube.metrischeGewindegroesse.flanken * schraube.metrischeGewindegroesse.flanken * pi / 4 + (schraube.schaftLaenge.schaftlaenge - schraube.gewindeLaenge.gewindeLaenge) * schraube.metrischeGewindegroesse.bezeichnung * schraube.metrischeGewindegroesse.bezeichnung * pi / 4); // Volumenberechnung
            V = Math.Round(V, 3);
            return V;
        }

        static public double getMasse(Schraube schraube)    //nicht Löschen
        {
            double dichte = 0.00000785;
            double m = dichte * schraube.volumen;
            m = Math.Round(m, 3);
            return m;
        }

        static public double getPreis(Schraube schraube)    //nicht löschen
        {
            double p = schraube.masse * 5 +5; // Preisberechnung
            p = Math.Round(p, 2);
            return p;
        }
    }
}