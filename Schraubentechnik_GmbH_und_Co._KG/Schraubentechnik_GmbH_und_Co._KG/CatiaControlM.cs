using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class CatiaControlM
    {
        CatiaControlM(Mutter m, String Gewindedarstellung)
        {
            try
            {

                CatiaConnectionM cc = new CatiaConnectionM();

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
                    cc.ErzeugeSechskant(m.metrischeGewindegroesse);
                    //cc.ErzeugeProfil(10);                     //Test für M20
                    Console.WriteLine("3");

                    //Loch erzeugen
                    cc.ErzeugeLoch(m.metrischeGewindegroesse.bezeichnung / 2, m.metrischeGewindegroesse);
                    Console.WriteLine("4");

                    //ErzeugeGewindeFeature
                    if (Gewindedarstellung == "technisch")
                    {
                       // cc.ErzeugeGewindeFeature(m.gewinderichtung, m.metrischeGewindegroesse.bezeichnung, m.gewindeLaenge.gewindeLaenge);
                        //cc.ErzeugeGewindeFeature(20, 75);         //Test mit d=20, gewindelänge=75
                        Console.WriteLine("5");
                    }
                    else if (Gewindedarstellung == "optisch")
                    {
                        cc.ErzeugeGewindeHelix(m);
                        Console.WriteLine("5");
                    }
                    
                    
                    
                    MessageBox.Show("Modell erfolgreich erstellt!" ,
                    "Erfolgreiche Erstellung", MessageBoxButton.OK, MessageBoxImage.Information);
         
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

        public static void CatiaStartenM(Mutter m, String Gd)
        {
            Mutter mut = m;
            String Gewindedarstellung = Gd;

            new CatiaControlM(mut, Gewindedarstellung);
        }
    }
}
