using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class GUI_Zugriff
    {
        Schraube s;

        public GUI_Zugriff(Schraube s)
        {
            this.s = s;     //Zuweisung der übergebenen Variable auf die memberVariable schrauben
            Window fenster = new Window();         //using System.Window von oben wird aufgerufen
            Grafikoberfläche meineGUI = new Grafikoberfläche(s);     //neues graphische Oberfläche Objekt wird erzeugt
            fenster.Title = "Schraubenprofis";        //Name des Fensters
            fenster.SizeToContent = SizeToContent.WidthAndHeight; //Größe des Fensters soll auf die Größe des Inhalts skaliert werden
            fenster.ResizeMode = ResizeMode.NoResize;   //gibt die Möglichkeit, dass man das Fenster von der Größe her verändern kann (NoResize verhindert folgende Erklärung)
            fenster.Content = meineGUI;     //Inhalt des fensters soll durch meineGUI festgelegt werden
            fenster.ShowDialog();       //ShowDialog sorgt dafür dass zuerst das Fenster ausgeführt werden muss

            Console.WriteLine("Press any key...");
            //Console.ReadKey();
        }
        
        
    }
}
