using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Schraubentechnik_GmbH_und_Co._KG
{

    class Schraube
    {
        public Gewinderichtung gewinderichtung;
        public MetrischeGewindegroesse metrischeGewindegroesse;
        public float schaftLaenge;
        public float gewindeLaenge;

        public Schraube (Gewinderichtung g, float schaftL, float gewindeL)
        {
            gewinderichtung = g;
            schaftLaenge = schaftL;
            gewindeLaenge = gewindeL;
        }

        public Schraube(float schaftL, float gewindeL)
        {
            gewinderichtung = Gewinderichtung.Rechtsgewinde;
            schaftLaenge = schaftL;
            gewindeLaenge = gewindeL;
        }
        public Schraube()
        {
            
        }

        public void printSchraube()
        {
            Console.WriteLine("INFOS über die Schraube");
            //...
        }
    }
}
