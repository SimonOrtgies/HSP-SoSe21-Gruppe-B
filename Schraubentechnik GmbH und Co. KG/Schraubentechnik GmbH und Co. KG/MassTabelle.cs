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
           { new MetrischeGewindegroesse(1, 0.25f, 0.84f, 0.61f, 3),
             new MetrischeGewindegroesse(2, 0.25f, 0.84f, 0.61f, 4),//Bisher nur Nonesense-WErte für Funktionstest
             new MetrischeGewindegroesse(4f, 0.25f, 0.84f, 0.63f, 5),
             new MetrischeGewindegroesse(6f, 0.25f, 0.84f, 0.63f, 6),
             new MetrischeGewindegroesse(6.5f, 0.25f, 0.84f, 0.63f, 7)};




        static public MetrischeGewindegroesse getMetrischeGewindeG(float name)  //Unterprogramm zum Durchsuchen der obigen Tabelle
        {
            //Durchsuchen der Tabelle nach der Bezeichnung
            for(int i = 0; i < 5; i++)  //Bereich von i anpassen wenn Tabelle erweitert wird
            {
                if(tabelle[i].bezeichnung == name)
                {
                    return tabelle[i];  //Rückgabe der Tabellenzeile, wenn Eingabe gefunden wurde
                }
            }

            return null;    //Wenn die EIngabe nicht gefunden wird, wird NULL zurückgegeben
        }
    }
}
