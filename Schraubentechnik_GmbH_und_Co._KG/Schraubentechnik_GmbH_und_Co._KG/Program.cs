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