using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    static class MassTabelle
    {

        static MetrischeGewindegroesse[] tabelle =
           { new MetrischeGewindegroesse(1, 0.25f, 0.84f, 0.61f),
             new MetrischeGewindegroesse(2, 0.25f, 0.84f, 0.61f),
             new MetrischeGewindegroesse(4f, 0.25f, 0.84f, 0.63f),
             new MetrischeGewindegroesse(6f, 0.25f, 0.84f, 0.63f),
             new MetrischeGewindegroesse(6.5f, 0.25f, 0.84f, 0.63f)};




        static public MetrischeGewindegroesse getMetrischeGewindeG(float name)
        {
            //Durchsuchen der Tabelle nach der Bezeichnung
            for(int i = 0; i < 5; i++)
            {
                if(tabelle[i].bezeichnung == name)
                {
                    return tabelle[i];
                }
            }

            return null;
        }
    }
}
