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
        Boolean FinishGewinderichtung = false;
        Boolean FinishSchraubenkopf = false;
        Boolean FinishGewindegroesse = false;
        Boolean FinishSchaftlaenge = false;
        Boolean FinishGewindelaenge = false;
        Boolean FinishFestigkeitsklasse = false;
        Boolean FinishAnzahl = false;
        Boolean hatWertSchaftlaenge = false;   //prüft ob Schaftlänge einen Wert hat, um den WeiterButton für die Dimensionen einzuschalten
        Boolean hatWertGewindelaenge = false;  //prüft ob Gewindelänge einen Wert hat, um den WeiterButton für die Dimensionen einzuschalten
        String Gewindedarstellung = "";     //Gewindedarstellung für Catia festlegen


        public Grafikoberfläche(Schraube s)
        {
            this.s = s; //Zuweisung der übergebenen Variable auf die memberVariable schrauben
            InitializeComponent();
        }


        #region Treeview
        private void tvi_Gewinderichtung_Selected(object sender, RoutedEventArgs e)
        {
            HideAllTreviewItem();
            grd_Gewinderichtung.Visibility = Visibility.Visible;
           
        }
        private void tvi_Schraubenkopf_Selected(object sender, RoutedEventArgs e)
        {
            HideAllTreviewItem();
            grd_Schraubenkopf.Visibility = Visibility.Visible;  //Neuesgrid sichtbar schalten
        }
        private void tvi_Dimensionen_Selected(object sender, RoutedEventArgs e)
        {
            HideAllTreviewItem();
            grd_Dimensionen.Visibility = Visibility.Visible;
            img_Mase.Visibility = Visibility.Visible; 
        }
        private void tvi_Anzahl_Selected(object sender, RoutedEventArgs e)
        {
            HideAllTreviewItem();
            grd_Anzahl.Visibility = Visibility.Visible;
        }
        private void tvi_Festigkeitsklasse_Selected(object sender, RoutedEventArgs e)
        {
            HideAllTreviewItem();
            grd_Festigkeitsklasse.Visibility = Visibility.Visible;   //Neuesgrid sichtbar schalten
        }

        private void HideAllTreviewItem()
        {
            grd_Gewinderichtung.Visibility = Visibility.Hidden;
            grd_Schraubenkopf.Visibility = Visibility.Hidden;
            grd_Dimensionen.Visibility = Visibility.Hidden;
            grd_Festigkeitsklasse.Visibility = Visibility.Hidden;
            grd_Anzahl.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Gewinderichtung
        private void rBtn_Rechtsgewinde_Checked(object sender, RoutedEventArgs e)   //Funktioniert
        {
            s.gewinderichtung = Gewinderichtung.Rechtsgewinde;
            lab_GewinderichtungHinweis.Visibility = Visibility.Hidden;
            FinishGewinderichtung = true;
        }

        private void rBtn_Linksgewinde_Checked(object sender, RoutedEventArgs e)
        {
            s.gewinderichtung = Gewinderichtung.Linksgewinde;
            lab_GewinderichtungHinweis.Visibility = Visibility.Hidden;
            FinishGewinderichtung = true;
        }

        #endregion

        #region Schraubenkopf Auswahl
        private void cBI_Sechskant_Selected(object sender, RoutedEventArgs e)
        {
            lab_SchraubenkopfHinweis.Visibility = Visibility.Hidden;
            s.schraubenkopf = "Sechskant";
            FinishSchraubenkopf = true;
            img_Sechskant.Visibility = Visibility.Visible;
            img_Zylinder_Innen.Visibility = Visibility.Hidden;
            img_Senkkopf.Visibility = Visibility.Hidden;
            img_Zylinder.Visibility = Visibility.Hidden;
        }

        private void cBI_Zylinderkopf_Selected(object sender, RoutedEventArgs e)
        {
            lab_SchraubenkopfHinweis.Visibility = Visibility.Hidden;
            s.schraubenkopf = "Zylinderkopf mit Schlitz";
            FinishSchraubenkopf = true;
            img_Sechskant.Visibility = Visibility.Hidden;
            img_Zylinder_Innen.Visibility = Visibility.Hidden;
            img_Senkkopf.Visibility = Visibility.Hidden;
            img_Zylinder.Visibility = Visibility.Visible;
        }

        private void cBI_Senkkopf_Selected(object sender, RoutedEventArgs e)
        {
            lab_SchraubenkopfHinweis.Visibility = Visibility.Hidden;
            s.schraubenkopf = "Senkkopf mit Torx";
            FinishSchraubenkopf = true;
            img_Sechskant.Visibility = Visibility.Hidden;
            img_Zylinder_Innen.Visibility = Visibility.Hidden;
            img_Senkkopf.Visibility = Visibility.Visible;
            img_Zylinder.Visibility = Visibility.Hidden;
        }

        private void cBI_Linsenkopf_Selected(object sender, RoutedEventArgs e)
        {
            lab_SchraubenkopfHinweis.Visibility = Visibility.Hidden;
            s.schraubenkopf = "Linsenkopf mit Kreuz-Schlitz";
            FinishSchraubenkopf = true;
            img_Sechskant.Visibility = Visibility.Hidden;
            img_Zylinder_Innen.Visibility = Visibility.Visible;
            img_Senkkopf.Visibility = Visibility.Hidden;
            img_Zylinder.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Gewindegroessen Auswahl

        private void cBI_m1_Selected(object sender, RoutedEventArgs e)
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
            
            slGuelitg = true;   //Wird auf true gesetzt, sobald eine Gewindegröße gesetzt wird, verhindert das man eine Schaftlänge4 vor dem Gewinde setzt
            FinishGewindegroesse = true;
            grd_Schaftlaenge.Visibility = Visibility.Visible;
            txB_Schaftlaenge.Background = Brushes.White;    //Freigeben der Schaftlängenbox
            txB_Schaftlaenge.IsReadOnly = false;
            txB_Klemmlaenge.Background = Brushes.White; //Freigeben der KLemmlängenbox
            txB_Klemmlaenge.IsReadOnly = false;

            Boolean gueltig = false;

            if (rBtn_SchaftlaengeJa.IsChecked == true)
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
                        FinishSchaftlaenge = false;
                    }
                }
                catch (Exception)
                {
                    lab_SchaftlaengeHinweis.Content = "Bitte eine Zahl eingeben";
                    FinishSchaftlaenge = false;
                }
            }
            else if (rBtn_SchaftlaengeNein.IsChecked == true)   //WEnn eingabe immer noch gültig ist, muss dennoch die Schaftlänge neu aus der KLemmlänge berechnet werden
            {
                txB_Klemmlaenge_LostFocus(txB_Klemmlaenge, null);   //Berechnung durch lostFocus funktion
            }

        }
        #endregion

        #region Schaftlaenge
        private void rBtn_SchaftlaengeJa_Checked(object sender, RoutedEventArgs e)
        {
            txB_Schaftlaenge.Visibility = Visibility.Visible;
            txB_Klemmlaenge.Visibility = Visibility.Hidden;
            lab_SchaftlaengeHinweis.Visibility = Visibility.Hidden;
            hatWertSchaftlaenge = false;

            if (txB_Schaftlaenge.Text != "Schaftlänge")     //Wenn zwischen den rBtn gewechselt wird, soll wieder ein leeres fehlt bereitgestellt werden
            {
                txB_Schaftlaenge.Text = "Schaftlänge";
                txB_Schaftlaenge.Background = Brushes.White;
            }

        }

        private void rBtn_SchaftlaengeNein_Checked(object sender, RoutedEventArgs e)
        {
            txB_Schaftlaenge.Visibility = Visibility.Hidden;
            txB_Klemmlaenge.Visibility = Visibility.Visible;
            lab_SchaftlaengeHinweis.Visibility = Visibility.Hidden;
            hatWertSchaftlaenge = false;

            if (txB_Klemmlaenge.Text != "Klemmlänge")
            {
                txB_Klemmlaenge.Text = "Klemmlänge";
                txB_Klemmlaenge.Background = Brushes.White;
            }


        }

        //Wenn schaftlänge bekannt:
        private void txB_Schaftlaenge_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txB_Schaftlaenge.Text == "Schaftlänge")
            {
                txB_Schaftlaenge.Text = "";
            }
            txB_Schaftlaenge.Background = Brushes.White;
        }

        private void txB_Schaftlaenge_LostFocus(object sender, RoutedEventArgs e)
        {
            Boolean gueltig = false;

            if (slGuelitg == true)
            {
                Schaftlaenge objSl = new Schaftlaenge(s.metrischeGewindegroesse);
                try
                {
                    float sl = Convert.ToSingle(txB_Schaftlaenge.Text);
                    gueltig = objSl.setSchaftlaenge(sl); //setSchaftlaenge überprüft ob die Eingabe im gültigen Bereich liegt. Berechnungen finden in Schaftlaenge.cs statt

                    if (gueltig == true)
                    {
                        s.schaftLaenge = objSl;
                        txB_Schaftlaenge.Background = Brushes.Green;
                        lab_SchaftlaengeHinweis.Visibility = Visibility.Hidden;
                        s.schaftLaenge.schaftlaenge = sl;   //Setzen der Schaftlänge in der Scharube s
                        FinishSchaftlaenge = true;
                        hatWertSchaftlaenge = true;
                        lab_Gewindelaenge.Visibility = Visibility.Visible;
                        rBtn_gesamte_Schaftlaenge.Visibility = Visibility.Visible;
                        rBtn_benutzerdefiniert.Visibility = Visibility.Visible;                        
                        txB_Gewindelaenge.Background = Brushes.White;    //Freigeben der Schaftlängenbox
                        txB_Gewindelaenge.IsReadOnly = false;
                    }
                    else
                    {
                        lab_SchaftlaengeHinweis.Content = "Die Länge der Schraube muss zwischen " + objSl.minSchaftlaenge + " mm und " + objSl.maxSchaftlaenge + " mm liegen";
                        lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                        txB_Schaftlaenge.Background = Brushes.Red;
                        txB_Schaftlaenge.Text = "";
                        FinishSchaftlaenge = false;
                    }

                }
                catch (Exception)
                {
                    lab_SchaftlaengeHinweis.Content = "Bitte eine Zahl eingeben";
                    lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                    txB_Schaftlaenge.Background = Brushes.Red;
                    txB_Schaftlaenge.Text = "";
                    FinishSchaftlaenge = false;
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

            if (slGuelitg == true)
                if (slGuelitg == true)

                {
                    Schaftlaenge objSl = new Schaftlaenge(s.metrischeGewindegroesse);
                    try     //Erneut try und catch zum fehler abfangen
                    {
                        float kl = Convert.ToSingle(txB_Klemmlaenge.Text);
                        objSl.schaftlaenge = Schaftlaenge.berechneSchaftlaenge(s.metrischeGewindegroesse.mutterhoehe, kl);   //Unterprogramm Schaftlänge in Schaftlaenge.cs aufrufen
                        if (objSl.schaftlaenge == -1)    //WEnn schaftlänge zu groß/klein kommt dieser Fehlercode wieder
                        {

                            double minsl = objSl.minSchaftlaenge - 1.25 * s.metrischeGewindegroesse.mutterhoehe;
                            minsl = Math.Round(objSl.minSchaftlaenge, 2);
                            double maxsl = (objSl.maxSchaftlaenge - 1.25 * s.metrischeGewindegroesse.mutterhoehe);
                            maxsl = Math.Round(maxsl, 2);
                            lab_SchaftlaengeHinweis.Content = "Die Klemmlänge der Schraube muss zwischen " + minsl + " mm und " + maxsl + " mm liegen";
                            lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                            txB_Klemmlaenge.Background = Brushes.Red;
                            txB_Klemmlaenge.Text = "";
                            FinishSchaftlaenge = false;
                        }
                        else
                        {
                            lab_SchaftlaengeHinweis.Content = "Die berechnete Schaftlänge beträtgt: " + objSl.schaftlaenge + " mm";
                            lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                            txB_Klemmlaenge.Background = Brushes.Green;
                            float sl = objSl.schaftlaenge;
                            s.schaftLaenge = objSl; //Schaftlänge der Schraube zuweisen
                            FinishSchaftlaenge = true;
                            hatWertSchaftlaenge = true;
                            lab_Gewindelaenge.Visibility = Visibility.Visible;
                            rBtn_gesamte_Schaftlaenge.Visibility = Visibility.Visible;
                            rBtn_benutzerdefiniert.Visibility = Visibility.Visible;                          
                            txB_Gewindelaenge.Background = Brushes.White;    //Freigeben der Schaftlängenbox
                            txB_Gewindelaenge.IsReadOnly = false;
                        }
                    }
                    catch (Exception) //Fehler werden abgefangen
                    {
                        lab_SchaftlaengeHinweis.Content = "Bitte eine Zahl eingeben";
                        lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                        txB_Schaftlaenge.Background = Brushes.Red;
                        FinishSchaftlaenge = false;
                    }
                }

        }
        #endregion

        #region Gewindelaenge

        // wenn Schaftlänge gleich Gewindelänge
        private void rBtn_gesamte_Schaftlaenge_Checked(object sender, RoutedEventArgs e)
        {
            lab_GewindelaengeHinweis.Visibility = Visibility.Hidden;
            txB_Gewindelaenge.Visibility = Visibility.Hidden;
            txB_Gewindelaenge.Text = "Gewindelänge";
            txB_Gewindelaenge.Background = Brushes.White;


            Gewindelaenge g = new Gewindelaenge(s.schaftLaenge, s.metrischeGewindegroesse);
            g.gewindeLaenge = Gewindelaenge.maxGewindeLaengeRechnung(s.schaftLaenge.schaftlaenge);
            s.gewindeLaenge = g;
            FinishGewindelaenge = true;
            hatWertGewindelaenge = true;
        }

        // Benutzer kann Gewindelänge eingeben
        private void rBtn_benutzerdefiniert_Checked(object sender, RoutedEventArgs e)
        {
            txB_Gewindelaenge.Visibility = Visibility.Visible;
            hatWertGewindelaenge = false;

        }


        private void txB_Gewindelaenge_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txB_Gewindelaenge.Text == "Gewindelänge")
            {
                txB_Gewindelaenge.Text = "";
            }
            txB_Gewindelaenge.Background = Brushes.White;

            if (s.metrischeGewindegroesse == null)
            {
                txB_Gewindelaenge.Background = Brushes.DarkGray;
                MessageBox.Show("Gewindegröße zuerst wählen!");
                txB_Gewindelaenge.IsReadOnly = true;

            }
            else if (s.schaftLaenge == null)  //Wenn noch keine Schaftlänge Festgelegt wurde
            {
                txB_Gewindelaenge.Background = Brushes.DarkGray;
                MessageBox.Show("Schaftlänge zuerst wählen!");
                txB_Gewindelaenge.IsReadOnly = true;

            }
        }

        private void txB_Gewindelaenge_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string g = tb.Text;
            float test;



            try     //Erneut try und catch zum fehler abfangen
            {
                float gl = Convert.ToSingle(g);
                Gewindelaenge objGL = new Gewindelaenge(s.schaftLaenge, s.metrischeGewindegroesse);
                test = Gewindelaenge.berechneGewindeLaenge(s.metrischeGewindegroesse.mutterhoehe, s.schaftLaenge.schaftlaenge, gl);
                if (test == -1)
                {
                    lab_GewindelaengeHinweis.Visibility = Visibility.Visible;
                    lab_GewindelaengeHinweis.Content = "Gewindelänge muss minimal " + 2*s.metrischeGewindegroesse.mutterhoehe +" mm betragen";
                    txB_Gewindelaenge.Background = Brushes.Red;
                    FinishGewindelaenge = false;
                }
                else if (test == -2)
                {
                    lab_GewindelaengeHinweis.Visibility = Visibility.Visible;
                    lab_GewindelaengeHinweis.Content = "Gewindelänge darf maximal " + s.schaftLaenge.schaftlaenge + " mm betragen";
                    txB_Gewindelaenge.Background = Brushes.Red;
                    FinishGewindelaenge = false;
                }
                else
                {
                    lab_GewindelaengeHinweis.Visibility = Visibility.Hidden;
                    objGL.gewindeLaenge = gl;
                    s.gewindeLaenge = objGL;
                    txB_Gewindelaenge.Background = Brushes.Green;
                    FinishGewindelaenge = true;
                    hatWertGewindelaenge = true;
                }
            }
            catch (Exception) //Fehler werden abgefangen
            {
                lab_GewindelaengeHinweis.Content = "Bitte eine Zahl eingeben";
                lab_GewindelaengeHinweis.Visibility = Visibility.Visible;
                txB_Gewindelaenge.Background = Brushes.Red;
                FinishGewindelaenge = false;
            }
        }

        #endregion

        #region Festigkeit
        private void rBtn_5_8_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "5.8";
            FinishFestigkeitsklasse = true;
            lab_FestigkeitsklasseHinweis.Visibility = Visibility.Hidden;
        }

        private void rBtn_6_8_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "6.8";
            FinishFestigkeitsklasse = true;
            lab_FestigkeitsklasseHinweis.Visibility = Visibility.Hidden;
        }

        private void rBtn_8_8_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "8.8";
            FinishFestigkeitsklasse = true;
            lab_FestigkeitsklasseHinweis.Visibility = Visibility.Hidden;
        }

        private void rBtn_9_8_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "9.8";
            FinishFestigkeitsklasse = true;
            lab_FestigkeitsklasseHinweis.Visibility = Visibility.Hidden;
        }

        private void rBtn_10_8_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "10.8";
            FinishFestigkeitsklasse = true;
            lab_FestigkeitsklasseHinweis.Visibility = Visibility.Hidden;
        }

        private void rBtn_12_9_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "12.9";
            FinishFestigkeitsklasse = true;
            lab_FestigkeitsklasseHinweis.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Anzahl
        private void tBx_Anzahl_LostFocus(object sender, RoutedEventArgs e)
        {
            int max = 1000000;
            int min = 25;
            try
            {
                int a = Convert.ToInt32(tBx_Anzahl.Text);
                if (a > max)    //Test ob zu viele Schrauben gewollt werden
                {
                    lab_Anzahl_Warnung.Content = "Es sind maximal " + max + " Schrauben möglich!";
                    lab_Anzahl_Warnung.Visibility = Visibility.Visible;
                    tBx_Anzahl.Background = Brushes.Red;
                    tBx_Anzahl.Text = "";
                    FinishAnzahl = false;
                }
                else if (a < min)   //Test ob zu wenig Schrauben gewollt werden
                {
                    lab_Anzahl_Warnung.Content = "Es sind minimal " + min + " Schrauben nötig!";
                    lab_Anzahl_Warnung.Visibility = Visibility.Visible;
                    tBx_Anzahl.Background = Brushes.Red;
                    tBx_Anzahl.Text = "";
                    FinishAnzahl = false;
                }
                else
                {
                    tBx_Anzahl.Background = Brushes.Green;
                    lab_Anzahl_Warnung.Visibility = Visibility.Hidden;
                    s.anzahl = a; // Setzen der Anzahl, wenn die vorherigen Tests negativ waren
                    FinishAnzahl = true;
                }


            }
            catch
            {
                lab_Anzahl_Warnung.Content = "Bitte geben Sie eine ganze Zahl ein";
                lab_Anzahl_Warnung.Visibility = Visibility.Visible;
                tBx_Anzahl.Background = Brushes.Red;
                tBx_Anzahl.Text = "";
                FinishAnzahl = false;
            }
        }
        #endregion

        #region Btn Fertigstellen
        private void Btn_Fertigstellen_Click(object sender, RoutedEventArgs e)
        {
            if (hatWertSchaftlaenge == true && hatWertGewindelaenge == true)
            {
                if (s.schaftLaenge.schaftlaenge >= s.gewindeLaenge.gewindeLaenge)
                {
                    FinishGewindelaenge = true;

                }
                else if (s.schaftLaenge.schaftlaenge < s.gewindeLaenge.gewindeLaenge)
                {
                    FinishGewindelaenge = false;
                    lab_EingabenUeberpruefen.Content = "Bitte Gewindelänge Überprüfen";
                    lab_EingabenUeberpruefen.Visibility = Visibility.Visible;
                    txB_Gewindelaenge.Background = Brushes.Red;
                }
            }
            // Prüft, ob alle Eingaben korekt sind
            if (FinishGewinderichtung == false ||
                FinishSchraubenkopf == false ||
                FinishGewindegroesse == false ||
                FinishSchaftlaenge == false ||
                FinishGewindelaenge == false ||
                FinishFestigkeitsklasse == false ||
                FinishAnzahl == false)
            {
                lab_EingabenUeberpruefen.Content = "Bitte Eingaben Überprüfen";
                lab_EingabenUeberpruefen.Visibility = Visibility.Visible;
            }
            else
            {
                // Ausgabefenster anzeigen
                lab_EingabenUeberpruefen.Visibility = Visibility.Hidden;
                s.volumen = Program.getVolumen(s);
                s.masse = Program.getMasse(s);
                s.preis = Program.getPreis(s);

                txB_BerechungenGewinderichtung.Text = Convert.ToString(s.gewinderichtung);
                txB_BerechungenSchraubenkopf.Text = s.schraubenkopf;
                txB_BerechungenGewindegroesse.Text = "M" +Convert.ToString(s.metrischeGewindegroesse.bezeichnung);
                txB_BerechungenSchaftlaenge.Text = Convert.ToString(s.schaftLaenge.schaftlaenge) + " mm";
                txB_BerechungenGewindelaenge.Text = Convert.ToString(s.gewindeLaenge.gewindeLaenge) + " mm";
                txB_BerechungenFestigkeitsklasse.Text = s.festigkeitsklasse;
                txB_BerechungenAnzahl.Text = Convert.ToString(s.anzahl);

                txB_BerechungenVolumen.Text = Convert.ToString(Math.Round(s.volumen / 1000, 3)) + " cm^3";
                txB_BerechungenMasse.Text = Convert.ToString(s.masse) + " kg";
                txB_BerechungenPreis.Text = Convert.ToString(s.preis) + "€";


                grd_Berechnungen.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region Btn Weiter
        private void btn_FestigkeitsklasseWeiter_Click(object sender, RoutedEventArgs e)
        {
            if (FinishFestigkeitsklasse == true)
            {
                tvi_Anzahl_Selected(tvi_Anzahl, null);
                lab_FestigkeitsklasseHinweis.Visibility = Visibility.Hidden;
            }
            else
            {
                lab_FestigkeitsklasseHinweis.Content = "Bitte Festigkeitsklasse auswählen";
                lab_FestigkeitsklasseHinweis.Visibility = Visibility.Visible;
            }

        }

        private void btn_SchraubenkopfWeiter_Click(object sender, RoutedEventArgs e)
        {
            if (FinishSchraubenkopf == true)
            {
                tvi_Dimensionen_Selected(tvi_Dimensionen, null);
                lab_SchraubenkopfHinweis.Visibility = Visibility.Hidden;
           
            }
            else
            {
                lab_SchraubenkopfHinweis.Content = "Bitte Schraubenkopf auswählen";
                lab_SchraubenkopfHinweis.Visibility = Visibility.Visible;
            }

        }

        private void btn_GewinderichtungWeiter_Click(object sender, RoutedEventArgs e)
        {
            if (FinishGewinderichtung == true)
            {
                tvi_Schraubenkopf_Selected(tvi_Schraubenkopf, null);
                lab_GewinderichtungHinweis.Visibility = Visibility.Hidden;
            }
            else
            {
                lab_GewinderichtungHinweis.Content = "Bitte Gewinderichtung auswählen";
                lab_GewinderichtungHinweis.Visibility = Visibility.Visible;
            }

        }

        private void btn_weiterDimensionen_Click(object sender, RoutedEventArgs e)
        {
            
            if (hatWertSchaftlaenge ==true && hatWertGewindelaenge == true)
            {
                if (s.schaftLaenge.schaftlaenge >= s.gewindeLaenge.gewindeLaenge)
                {
                    FinishGewindelaenge = true;

                }
                else if (s.schaftLaenge.schaftlaenge < s.gewindeLaenge.gewindeLaenge)
                {
                    FinishGewindelaenge = false;
                    lab_GewindelaengeHinweis.Visibility = Visibility.Visible;
                    lab_GewindelaengeHinweis.Content = "Die Gewindelänge darf maximal " + s.schaftLaenge.schaftlaenge + " mm betragen";
                }
            }
            if (FinishGewindegroesse == true && FinishSchaftlaenge == true && FinishGewindelaenge == true)
            {
                tvi_Festigkeitsklasse_Selected(tvi_Festigkeitsklasse, null);
            }


        }
        #endregion

       

        private void btn_Zurueck_Click(object sender, RoutedEventArgs e)
        {
            grd_Berechnungen.Visibility = Visibility.Hidden;
        }

        private void btn_AnCatiaUebertragen_Click(object sender, RoutedEventArgs e)
        {
            if(Gewindedarstellung == "technisch")
            {
                CatiaControl.CatiaStarten(s, Gewindedarstellung);
                
            }
            else if(Gewindedarstellung == "optisch")
            {
                CatiaControl.CatiaStarten(s, Gewindedarstellung);
            }
            else if(Gewindedarstellung == "")
            {
                lab_GewindedarstellungsHinweis.Content = "Bitte zuerst Gewindedarstellung auswählen!";
                lab_GewindedarstellungsHinweis.Visibility = Visibility.Visible;
            }
            
        }

        #region Gewindedarstellung
        private void rBtn_TechnischesGewinde_Checked(object sender, RoutedEventArgs e)
        {
            lab_GewindedarstellungsHinweis.Visibility = Visibility.Hidden;
            Gewindedarstellung = "technisch";
        }
        private void rBtn_OptischesGewinde_Checked(object sender, RoutedEventArgs e)
        {
            lab_GewindedarstellungsHinweis.Visibility = Visibility.Hidden;
            Gewindedarstellung = "optisch";
        }
        #endregion


    }
}
