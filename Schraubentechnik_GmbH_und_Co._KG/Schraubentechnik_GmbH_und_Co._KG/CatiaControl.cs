using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class CatiaControl
    {
        CatiaControl(Schraube s)
        {
            try
            {

                CatiaConnection cc = new CatiaConnection();

                // Finde Catia Prozess
                if (cc.CATIALaeuft())
                {
                    Console.WriteLine("0");

                    // Öffne ein neues Part
                    cc.ErzeugePart();
                    Console.WriteLine("1");

                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();
                    Console.WriteLine("2");

                    // Generiere ein Profil
                    //cc.ErzeugeProfil(s.metrischeGewindegroesse.bezeichnung/2);
                    cc.ErzeugeProfil(10);                     //Test für M20
                    Console.WriteLine("3");

                    // Extrudiere Schaft
                    //cc.ErzeugeSchaft(s.schaftLaenge.schaftlaenge);
                    cc.ErzeugeSchaft(100);                   // Test mit Schaftlänge 100
                    Console.WriteLine("4");

                    //Erzeuge Scharubenkopf
                    cc.ErzeugeKopf(s.metrischeGewindegroesse, s.schraubenkopf);
                    Console.WriteLine("5");

                    //Erzeugt eine Fase am ende des Schafts
                    cc.ErzeugeFase();
                    Console.WriteLine("6");

                    //ErzeugeGewindeFeature
                    //cc.ErzeugeGewindeFeature(s.metrischeGewindegroesse.bezeichnung, s.gewindeLaenge.gewindeLaenge);
                    cc.ErzeugeGewindeFeature(20, 75);         //Test mit d=20, gewindelänge=75
                    Console.WriteLine("6");

                }
                else
                {               
                    MessageBox.Show("Laufende Catia Application nicht gefunden!" + Environment.NewLine + "Zum Fortfahren Catia zuerst öffnen",
                    "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception aufgetreten");
            }

        }

        public static void CatiaStarten(Schraube s)
        {
            Schraube schr = s;
            new CatiaControl(schr);
        }
    }
}

