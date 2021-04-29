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
            Window fenster = new Window();
            GUI_Zugriff meineGUI = new GUI_Zugriff();
            fenster.Title = "Schraubenprofis";
            fenster.SizeToContent = SizeToContent.WidthAndHeight;
            fenster.ResizeMode = ResizeMode.NoResize;
            fenster.Content = meineGUI;
            fenster.ShowDialog();

        }
        [STAThread]
        static void Main(string[] args)
        {
            new GUI_Zugriff();
        }
        
    }
}
