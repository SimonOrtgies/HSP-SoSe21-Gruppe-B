using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    public class Mutter
    {
        public MetrischeGewindegroesse metrischeGewindegroesse;

        public Mutter()   //Konstruktor kann gelöscht werden, weil es diesen standardmäßig existiert, solange es keinen anderen konstruktor (der gefüllt ist) gibt
        {

        }
        public void printSchraube(int name) //Unterprogramm zur Ausgabe der Schraubeninformationen
        {
            Console.WriteLine("");
            Console.WriteLine("Informationen über die Mutter " + (name + 1) + ":");
            Console.WriteLine("Gewindegröße: M" + metrischeGewindegroesse.bezeichnung);
        }

    }
}
