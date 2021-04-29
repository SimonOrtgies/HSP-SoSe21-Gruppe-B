using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    public class MetrischeGewindegroesse   //Objekt/Klasse metrischeGewindegröße
    {
        public float bezeichnung;   //Variablen/Infomrationen die die Klasse enthält
        public float steigung;
        public float flanken;       //Für Volumenberechnung nötig
        public float aussengewinde;
        public float mutterhoehe;   //Für mindest Schaftlänge und Längenrechner notwendig

        public MetrischeGewindegroesse(float b, float st, float f, float au, float mu)    //Aufbau der KLasse
        {
            bezeichnung = b;
            steigung = st;
            flanken = f;
            aussengewinde = au;
            mutterhoehe = mu;
        }

        public String printGewinde()    //Unterprogramm zur Ausgabe der Daten als Übersicht
        {
            return "M" + bezeichnung + " Steigung: " + steigung + " Flankendurchmesser: " + flanken + " Aussengewindedurchmesser " + aussengewinde;
        }
    }
}
