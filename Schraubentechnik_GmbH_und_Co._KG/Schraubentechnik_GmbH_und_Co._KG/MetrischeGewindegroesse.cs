using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    public class MetrischeGewindegroesse   //Objekt/Klasse metrischeGewindegröße
    {
        public float bezeichnung;       //Variablen/Infomrationen die die Klasse enthält
        public float steigung;
        public float flanken;           //Für Volumenberechnung nötig
        public float aussengewinde;
        public float mutterhoehe;       //Für mindest Schaftlänge und Längenrechner notwendig
        public float schluesselweite;   //Für Catia Sechskant
        public float kopfdurchmesser;   //Für Catia Zylinderkopf
        public float schlitztiefe;      //Für Catia Zylinderkopf
        public float schlitzbreite;     //Für Catia Zylinderkopf
        public float innensechskant;    //Für Catia Senkkopf die schlüsselweite
        public float innensktiefe;      //Für Catia Senkkopf
        public float innenskkopfhöhe;   //Für Catia Senkkopf
        public float innenskkopfdurchmesser; //Für Catia Senkkopf
        public float fase;              //Für Catia Schaftfase und Zylinderkopfradius

        public MetrischeGewindegroesse(float b, float st, float f, float au, float mu, float sw, float kd, float ts, float bs, float ins, float iskt, float iskh, float iskd, float fa)    //Aufbau der KLasse
        {
            bezeichnung = b;
            steigung = st;
            flanken = f;
            aussengewinde = au;
            mutterhoehe = mu;
            schluesselweite = sw;
            kopfdurchmesser = kd;
            schlitztiefe = ts;
            schlitzbreite = bs;
            innensechskant = ins;
            innensktiefe = iskt;
            innenskkopfhöhe = iskh;
            innenskkopfdurchmesser = iskd;
            fase = fa;

        }

        public String printGewinde()    //Unterprogramm zur Ausgabe der Daten als Übersicht
        {
            return "M" + bezeichnung + " Steigung: " + steigung + " Flankendurchmesser: " + flanken + " Aussengewindedurchmesser " + aussengewinde;
        }
    }
}
