using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    /// <summary>
    /// Interaktionslogik für Grafikoberfläche.xaml
    /// </summary>
    public partial class Grafikoberfläche : UserControl
    {
        Schraube s;
        Boolean slGuelitg = false; //Kontrollboolean, ob die Schaftlänge gesetzt werden kann

        public Grafikoberfläche(Schraube s)
        {
            this.s = s; //Zuweisung der übergebenen Variable auf die memberVariable schrauben
            InitializeComponent();
        }

        private void tvi_Gewinderichtung_Selected(object sender, RoutedEventArgs e)
        {
            grd_Schraubenkopf.Visibility = Visibility.Hidden;
            grd_Dimensionen.Visibility = Visibility.Hidden;
            grd_Festigkeitsklasse.Visibility = Visibility.Hidden;
            grd_Gewinderichtung.Visibility = Visibility.Visible;
        }

        private void rBtn_Rechtsgewinde_Checked(object sender, RoutedEventArgs e)   //Funktioniert
        {
            RadioButton rB = (RadioButton)sender;
            //Console.WriteLine(rB.Uid);  ///ID für unterscheidung
            s.gewinderichtung = Gewinderichtung.Rechtsgewinde;
            
            
        }

        private void tvi_Schraubenkopf_Selected(object sender, RoutedEventArgs e)
        {
            grd_Gewinderichtung.Visibility = Visibility.Hidden; //Altes grid verstecken
            grd_Dimensionen.Visibility = Visibility.Hidden;
            grd_Festigkeitsklasse.Visibility = Visibility.Hidden;
            grd_Schraubenkopf.Visibility = Visibility.Visible;  //Neuesgrid sichtbar schalten
        }

        private void tvi_Dimensionen_Selected(object sender, RoutedEventArgs e)
        {
            grd_Gewinderichtung.Visibility = Visibility.Hidden; //Altes grid verstecken
            grd_Schraubenkopf.Visibility = Visibility.Hidden;
            grd_Festigkeitsklasse.Visibility = Visibility.Hidden;
            grd_Dimensionen.Visibility = Visibility.Visible;  //Neuesgrid sichtbar schalten


        }

        private void txB_Gewindegroesse_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string g = tb.Text;
        }

        private void tvi_Festigkeitsklasse_Selected(object sender, RoutedEventArgs e)
        {
            grd_Gewinderichtung.Visibility = Visibility.Hidden; //Altes grid verstecken
            grd_Schraubenkopf.Visibility = Visibility.Hidden;
            grd_Dimensionen.Visibility = Visibility.Hidden;
            grd_Festigkeitsklasse.Visibility = Visibility.Visible;   //Neuesgrid sichtbar schalten
        }

        private void btn_weiterDimensionen_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch
            {
                MessageBox.Show("Ungültige Eingabe bei Gewindegröße!");
            }
            try
            {
                float sl = Convert.ToSingle(txB_Schaftlaenge.Text);
            }
            catch
            {
                txB_Schaftlaenge.Background = Brushes.Red;
                MessageBox.Show("Ungültige Eingabe bei Schaftlänge!");
            }
        }


        //Gewindegrößen abspeichern ab hier:
        
        private void cBI_m1_Selected(object sender, RoutedEventArgs e)  //Funktioniert
        {
            float g = 1;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m1_2_Selected(object sender, RoutedEventArgs e)
        {
            float g = 1.2f;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m1_6_Selected(object sender, RoutedEventArgs e)
        {
            float g = 1.6f;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m2_Selected(object sender, RoutedEventArgs e)
        {
            float g = 2;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m2_5_Selected(object sender, RoutedEventArgs e)
        {
            float g = 2.5f;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m3_Selected(object sender, RoutedEventArgs e)
        {
            float g = 3;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m3_5_Selected(object sender, RoutedEventArgs e)
        {
            float g = 3.5f;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m4_Selected(object sender, RoutedEventArgs e)
        {
            float g = 4;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m5_Selected(object sender, RoutedEventArgs e)
        {
            float g = 5;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m6_Selected(object sender, RoutedEventArgs e)
        {
            float g = 6;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m7_Selected(object sender, RoutedEventArgs e)
        {
            float g = 7;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m8_Selected(object sender, RoutedEventArgs e)
        {
            float g = 8;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m10_Selected(object sender, RoutedEventArgs e)
        {
            float g = 10;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m12_Selected(object sender, RoutedEventArgs e)
        {
            float g = 12;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m14_Selected(object sender, RoutedEventArgs e)
        {
            float g = 14;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m16_Selected(object sender, RoutedEventArgs e)
        {
            float g = 16;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m20_Selected(object sender, RoutedEventArgs e)
        {
            float g = 20;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m24_Selected(object sender, RoutedEventArgs e)
        {
            float g = 24;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m30_Selected(object sender, RoutedEventArgs e)
        {
            float g = 30;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m36_Selected(object sender, RoutedEventArgs e)
        {
            float g = 36;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void cBI_m42_Selected(object sender, RoutedEventArgs e)
        {
            float g = 42;
            s.metrischeGewindegroesse = MassTabelle.getMetrischeGewindeG(g);
            GewindegroessenErgaenzung();
        }

        private void GewindegroessenErgaenzung()
        {
            txB_Schaftlaenge.Background = Brushes.White;    //Freigeben der Schaftlängenbox
            txB_Schaftlaenge.IsReadOnly = false;
            txB_Klemmlaenge.Background = Brushes.White; //Freigeben der KLemmlängenbox
            txB_Klemmlaenge.IsReadOnly = false;
            slGuelitg = true;

            Boolean gueltig = false;

            if (slGuelitg == true)  //Testen ob die Schaftlänge nach wechseln der Gewindegröße noch immer gültig ist
            {
                Schaftlaenge objSl = new Schaftlaenge(s.metrischeGewindegroesse);
                try
                {
                    float sl = Convert.ToSingle(txB_Schaftlaenge.Text);
                    gueltig = objSl.setSchaftlaenge(sl); //setSchaftlaenge überprüft ob die Eingabe im gültigen Bereich liegt. Berechnungen finden in Schaftlaenge.cs statt

                    if (gueltig == false && txB_Schaftlaenge.Text != "")    //Falls die Länge nicht mehr gültig ist
                    {
                        lab_SchaftlaengeHinweis.Content = "Die Länge der Schraube muss zwischen " + objSl.minSchaftlaenge + " mm und " + objSl.maxSchaftlaenge + " mm liegen";
                        lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                        txB_Schaftlaenge.Background = Brushes.Red;
                        txB_Schaftlaenge.Text = "";
                    }
                }
                catch (Exception)
                {
                    lab_SchaftlaengeHinweis.Content = "Bitte eine Zahl eingeben";
                }
            }
        }
        //Gewindegrößen abspeichern hier ende

        //Ab hier Schaftlänge 
        private void rBtn_SchaftlaengeJa_Checked(object sender, RoutedEventArgs e)
        {
            txB_Schaftlaenge.Visibility = Visibility.Visible;
            txB_Klemmlaenge.Visibility = Visibility.Hidden;
            lab_SchaftlaengeHinweis.Visibility = Visibility.Hidden;
        }

        private void rBtn_SchaftlaengeNein_Checked(object sender, RoutedEventArgs e)
        {
            txB_Schaftlaenge.Visibility = Visibility.Hidden;
            txB_Klemmlaenge.Visibility = Visibility.Visible;
            lab_SchaftlaengeHinweis.Visibility = Visibility.Hidden;
        }

            //Wenn schaftlänge bekannt:
        private void txB_Schaftlaenge_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txB_Schaftlaenge.Text == "Schaftlänge")
            {
                txB_Schaftlaenge.Text = "";
            }
            txB_Schaftlaenge.Background = Brushes.White;

            if (s.metrischeGewindegroesse == null)  //Wenn noch keine gewindegröße Festgelegt wurde
            {
                txB_Schaftlaenge.Background = Brushes.DarkGray;
                MessageBox.Show("Gewindegöße zuerst wählen!");
                txB_Schaftlaenge.IsReadOnly = true;
                slGuelitg = false;

            }
        }
        private void txB_Schaftlaenge_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string g = tb.Text;
            Boolean gueltig = false;
            
            if(slGuelitg == true)
            {
                Schaftlaenge objSl = new Schaftlaenge(s.metrischeGewindegroesse);
                try
                {
                    float sl = Convert.ToSingle(g);
                    gueltig = objSl.setSchaftlaenge(sl); //setSchaftlaenge überprüft ob die Eingabe im gültigen Bereich liegt. Berechnungen finden in Schaftlaenge.cs statt

                    if(gueltig == true)
                    {
                        s.schaftLaenge = objSl;
                        Console.WriteLine(s.schaftLaenge.schaftlaenge);
                        txB_Schaftlaenge.Background = Brushes.Green;
                        lab_SchaftlaengeHinweis.Visibility = Visibility.Hidden;
                        s.schaftLaenge.schaftlaenge = sl;   //Setzen der Schaftlänge in der Scharube s
                    }
                    else
                    {
                        lab_SchaftlaengeHinweis.Content = "Die Länge der Schraube muss zwischen " + objSl.minSchaftlaenge + " mm und " + objSl.maxSchaftlaenge + " mm liegen";
                        lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                        txB_Schaftlaenge.Background = Brushes.Red;
                        txB_Schaftlaenge.Text = "";
                    }
                
                }
                catch (Exception)
                {
                    lab_SchaftlaengeHinweis.Content = "Bitte eine Zahl eingeben";
                    lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                    txB_Schaftlaenge.Background = Brushes.Red;
                }
            }
            
        }

        //Wenn schaftlänge unbekannt:
        private void txB_Klemmlaenge_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txB_Klemmlaenge.Text == "Klemmlänge")
            {
                txB_Klemmlaenge.Text = "";
            }
            txB_Klemmlaenge.Background = Brushes.White;

            if (s.metrischeGewindegroesse == null)  //Wenn noch keine gewindegröße Festgelegt wurde
            {
                txB_Klemmlaenge.Background = Brushes.DarkGray;
                MessageBox.Show("Gewindegöße zuerst wählen!");
                txB_Klemmlaenge.IsReadOnly = true;
                slGuelitg = false;
            }
        }

        private void txB_Klemmlaenge_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string g = tb.Text;

            if(slGuelitg == true)
            {
                Schaftlaenge objSl = new Schaftlaenge(s.metrischeGewindegroesse);
                try     //Erneut try und catch zum fehler abfangen
                {
                    float kl = Convert.ToSingle(g);
                    objSl.schaftlaenge = Schaftlaenge.berechneSchaftlaenge(s.metrischeGewindegroesse.mutterhoehe, kl);   //Unterprogramm Schaftlänge in Schaftlaenge.cs aufrufen
                    if (objSl.schaftlaenge == -1)    //WEnn schaftlänge zu groß/klein kommt dieser Fehlercode wieder
                    {
                        lab_SchaftlaengeHinweis.Content = "Die Klemmänge der Schraube muss zwischen " + (objSl.minSchaftlaenge-1.25*s.metrischeGewindegroesse.mutterhoehe) + " mm und " + (objSl.maxSchaftlaenge - 1.25*s.metrischeGewindegroesse.mutterhoehe) + " mm liegen";
                        lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                        txB_Klemmlaenge.Background = Brushes.Red;
                        txB_Klemmlaenge.Text = "";
                    }
                    else
                    {
                        lab_SchaftlaengeHinweis.Content = "Die berechnete Schaftlänge beträtgt: " + objSl.schaftlaenge + " mm";
                        lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                        txB_Klemmlaenge.Background = Brushes.Green;
                        Console.WriteLine(objSl.schaftlaenge);
                        float sl = objSl.schaftlaenge;
                        s.schaftLaenge = objSl; //Schaftlänge der Schraube zuweisen
                        //Console.WriteLine(s.schaftLaenge);
                    }
                }
                catch (Exception) //Fehler werden abgefangen
                {
                    lab_SchaftlaengeHinweis.Content = "Bitte eine Zahl eingeben";
                    lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                    txB_Schaftlaenge.Background = Brushes.Red;
                }
            }
            
        }

        //Ende Schaftlänge




       



    }

}
