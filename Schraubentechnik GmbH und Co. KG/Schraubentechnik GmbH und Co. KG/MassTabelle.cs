using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    static class MassTabelle    //Masstabelle
    {

        static MetrischeGewindegroesse[] tabelle =  //Tabelle mit den Informationen die ein Gewinde enthält (Reihenfolge in MetrischeGewindegroesse.cs festgelegt)

           { new MetrischeGewindegroesse(1, 0.25f, 0.84f, 0.69f, 0.8f),
             new MetrischeGewindegroesse(1.2f, 0.25f, 1.04f, 0.89f, 1),
             new MetrischeGewindegroesse(1.6f, 0.35f, 1.38f, 1.17f, 1.2f),
             new MetrischeGewindegroesse(2, 0.4f, 1.74f, 1.51f, 1.6f),
             new MetrischeGewindegroesse(2.5f, 0.45f, 2.21f, 1.95f, 2),
             new MetrischeGewindegroesse(3, 0.5f, 2.68f, 2.39f, 2.4f),
             new MetrischeGewindegroesse(3.5f, 0.6f, 3.11f, 2.76f, 2.8f),
             new MetrischeGewindegroesse(4, 0.7f, 3.55f, 3.14f, 3.2f),
             new MetrischeGewindegroesse(5, 0.8f, 4.48f, 4.02f, 4),
             new MetrischeGewindegroesse(6, 1f, 5.35f, 4.77f, 5),
             new MetrischeGewindegroesse(7, 1f, 6.35f, 5.77f, 7),
             new MetrischeGewindegroesse(8, 1.25f, 7.19f, 6.47f, 6.5f),
             new MetrischeGewindegroesse(10, 1.5f, 9.03f, 8.16f, 8),
             new MetrischeGewindegroesse(12, 1.75f, 10.86f, 9.85f, 10),
             new MetrischeGewindegroesse(14, 2f, 12.7f, 11.55f, 11),
             new MetrischeGewindegroesse(16, 2f, 14.7f, 13.55f, 13),
             new MetrischeGewindegroesse(20, 2.5f, 18.38f, 16.93f, 16),
             new MetrischeGewindegroesse(24, 3f, 22.05f, 20.32f, 19),
             new MetrischeGewindegroesse(30, 3.5f, 27.73f, 25.71f, 24),
             new MetrischeGewindegroesse(36, 4f, 33.4f, 31.09f, 29),
             new MetrischeGewindegroesse(42, 4.5f, 39.08f, 36.48f, 34),    //Alle Werte Stimmen nun


        };
        




        static public MetrischeGewindegroesse getMetrischeGewindeG(float input)  //Unterprogramm zum Durchsuchen der obigen Tabelle
        {
            //Durchsuchen der Tabelle nach der Bezeichnung
            for(int i = 0; i < 21; i++)  //Bereich von i anpassen wenn Tabelle erweitert wird
            {
                if(tabelle[i].bezeichnung == input)
                {
                    return tabelle[i];  //Rückgabe der Tabellenzeile, wenn Eingabe gefunden wurde
                }
            }

            return null;    //Wenn die EIngabe nicht gefunden wird, wird NULL zurückgegeben
        }
    }
}
