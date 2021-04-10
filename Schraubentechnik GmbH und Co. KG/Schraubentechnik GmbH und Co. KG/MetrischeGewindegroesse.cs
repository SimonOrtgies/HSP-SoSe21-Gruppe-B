using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class MetrischeGewindegroesse
    {
        public float bezeichnung;
        public float steigung;
        public float flanken;
        public float aussengewinde;
        
        public MetrischeGewindegroesse(float b, float st, float f, float au)
        {
            bezeichnung = b;
            steigung = st;
            flanken = f;
            aussengewinde = au;
        }

        public String printGewinde()
        {
            return "M " + bezeichnung + " steigung " + steigung + " flanken " + flanken + " aussengewinde " + aussengewinde;    //um es immer ausführen zu können
        }
    }
}
