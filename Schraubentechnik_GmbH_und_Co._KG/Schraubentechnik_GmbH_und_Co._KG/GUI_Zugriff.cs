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

        GUI_Zugriff()
        {
            Window fenster = new Window();         //using System.Window von oben wird aufgerufen
            GUI_Zugriff meineGUI = new GUI_Zugriff();     //neues graphische Oberfläche Objekt wird erzeugt
            fenster.Title = "Schraubenprofis";        //Name des Fensters
            fenster.SizeToContent = SizeToContent.WidthAndHeight; //Größe des Fensters soll auf die Größe des Inhalts skaliert werden
            fenster.ResizeMode = ResizeMode.NoResize;   //gibt die Möglichkeit, dass man das Fenster von der Größe her verändern kann (NoResize verhindert folgende Erklärung)
            fenster.Content = meineGUI;     //Inhalt des fensters soll durch meineGUI festgelegt werden
            fenster.ShowDialog();       //ShowDialog sorgt dafür dass zuerst das Fenster ausgeführt werden muss

            Console.WriteLine("Press any key...");
            //Console.ReadKey();
        }
        [STAThread] //GUI-Komponenten müssen in einem Prozess laufen
        static void Main(string[] args)
        {
            new GUI_Zugriff();  //neues Objekt
        }
        
    }
}
