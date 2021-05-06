﻿using System;
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


        public Grafikoberfläche(Schraube s)
        {
            this.s = s; //Zuweisung der übergebenen Variable auf die memberVariable schrauben
            InitializeComponent();
        }
        #region Treeview
        private void tvi_Gewinderichtung_Selected(object sender, RoutedEventArgs e)
        {
            grd_Schraubenkopf.Visibility = Visibility.Hidden;
            grd_Dimensionen.Visibility = Visibility.Hidden;
            grd_Festigkeitsklasse.Visibility = Visibility.Hidden;
            grd_Gewinderichtung.Visibility = Visibility.Visible;
            grd_Anzahl.Visibility = Visibility.Hidden;
        }
        private void tvi_Schraubenkopf_Selected(object sender, RoutedEventArgs e)
        {
            grd_Gewinderichtung.Visibility = Visibility.Hidden; //Altes grid verstecken
            grd_Dimensionen.Visibility = Visibility.Hidden;
            grd_Festigkeitsklasse.Visibility = Visibility.Hidden;
            grd_Schraubenkopf.Visibility = Visibility.Visible;  //Neuesgrid sichtbar schalten
            grd_Anzahl.Visibility = Visibility.Hidden;
        }
        private void tvi_Dimensionen_Selected(object sender, RoutedEventArgs e)
        {
            grd_Gewinderichtung.Visibility = Visibility.Hidden; //Altes grid verstecken
            grd_Schraubenkopf.Visibility = Visibility.Hidden;
            grd_Festigkeitsklasse.Visibility = Visibility.Hidden;
            grd_Dimensionen.Visibility = Visibility.Visible;  //Neuesgrid sichtbar schalten
            grd_Anzahl.Visibility = Visibility.Hidden;
        }
        private void tvi_Anzahl_Selected(object sender, RoutedEventArgs e)
        {
            grd_Anzahl.Visibility = Visibility.Visible;
            grd_Festigkeitsklasse.Visibility = Visibility.Hidden;
            grd_Dimensionen.Visibility = Visibility.Hidden;
            grd_Schraubenkopf.Visibility = Visibility.Hidden;
            grd_Gewinderichtung.Visibility = Visibility.Hidden;
        }
        private void tvi_Festigkeitsklasse_Selected(object sender, RoutedEventArgs e)
        {
            grd_Gewinderichtung.Visibility = Visibility.Hidden; //Altes grid verstecken
            grd_Schraubenkopf.Visibility = Visibility.Hidden;
            grd_Dimensionen.Visibility = Visibility.Hidden;
            grd_Festigkeitsklasse.Visibility = Visibility.Visible;   //Neuesgrid sichtbar schalten
            grd_Anzahl.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Gewinderichtung
        private void rBtn_Rechtsgewinde_Checked(object sender, RoutedEventArgs e)   //Funktioniert
        {
            s.gewinderichtung = Gewinderichtung.Rechtsgewinde;
            FinishGewinderichtung = true;
        }

        private void rBtn_Linksgewinde_Checked(object sender, RoutedEventArgs e)
        {
            s.gewinderichtung = Gewinderichtung.Linksgewinde;
            FinishGewinderichtung = true;
        }

        #endregion

        private void txB_Schaftlaenge_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void cBx_Gewindegroesse_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        #region Schraubenkopf Auswahl
        private void cBI_Sechskant_Selected(object sender, RoutedEventArgs e)
        {
            s.schraubenkopf = "Sechskant";
            FinishSchraubenkopf = true;
        }

        private void cBI_Zylinderkopf_Selected(object sender, RoutedEventArgs e)
        {
            s.schraubenkopf = "Zylinderkopf mit Innensechskant";
            FinishSchraubenkopf = true;
        }

        private void cBI_Senkkopf_Selected(object sender, RoutedEventArgs e)
        {
            s.schraubenkopf = "Senkkopf mit Innensechskant";
            FinishSchraubenkopf = true;
        }

        private void cBI_Linsenkopf_Selected(object sender, RoutedEventArgs e)
        {
            s.schraubenkopf = "Linsenkopf mit Schlitz";
            FinishSchraubenkopf = true;
        }
        #endregion

        #region Gewindegroessen Auswahl
        private void txB_Gewindegroesse_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string g = tb.Text;
        }


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
            txB_Schaftlaenge.Background = Brushes.White;    //Freigeben der Schaftlängenbox
            txB_Schaftlaenge.IsReadOnly = false;
            txB_Klemmlaenge.Background = Brushes.White; //Freigeben der KLemmlängenbox
            txB_Klemmlaenge.IsReadOnly = false;
            slGuelitg = true;
            FinishGewindegroesse = true;

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
                        FinishSchaftlaenge = false;
                    }
                }
                catch (Exception)
                {
                    lab_SchaftlaengeHinweis.Content = "Bitte eine Zahl eingeben";
                    FinishSchaftlaenge = false;
                }
            }
        }
        #endregion
 
        #region Schaftlaenge
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
                        FinishSchaftlaenge = true;
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
                        FinishSchaftlaenge = false;
                    }
                    else
                    {
                        lab_SchaftlaengeHinweis.Content = "Die berechnete Schaftlänge beträtgt: " + objSl.schaftlaenge + " mm";
                        lab_SchaftlaengeHinweis.Visibility = Visibility.Visible;
                        txB_Klemmlaenge.Background = Brushes.Green;
                        Console.WriteLine(objSl.schaftlaenge);
                        float sl = objSl.schaftlaenge;
                        s.schaftLaenge = objSl; //Schaftlänge der Schraube zuweisen
                        FinishSchaftlaenge = true;
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

        #region Festigkeit
        private void rBtn_5_8_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "5.8";
            FinishFestigkeitsklasse = true;
        }

        private void rBtn_6_8_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "6.8";
            FinishFestigkeitsklasse = true;
        }

        private void rBtn_8_8_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "8.8";
            FinishFestigkeitsklasse = true;
        }

        private void rBtn_9_8_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "9.8";
            FinishFestigkeitsklasse = true;
        }

        private void rBtn_10_8_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "10.8";
            FinishFestigkeitsklasse = true;
        }

        private void rBtn_12_9_Checked(object sender, RoutedEventArgs e)
        {
            s.festigkeitsklasse = "12.9";
            FinishFestigkeitsklasse = true;
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
            Gewindelaenge gl = new Gewindelaenge(s.schaftLaenge, s.metrischeGewindegroesse);
            gl.gewindeLaenge = 10;
            s.gewindeLaenge = gl;


            FinishGewindelaenge = true;

            if(FinishGewinderichtung == false || 
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
                lab_EingabenUeberpruefen.Visibility = Visibility.Hidden;
                s.volumen = Program.getVolumen(s);
                s.masse = Program.getMasse(s);
                s.preis = Program.getPreis(s);
                Console.WriteLine(s.preis);

                txB_BerechungenGewinderichtung.Text = Convert.ToString(s.gewinderichtung);
                txB_BerechungenSchraubenkopf.Text = s.schraubenkopf;
                txB_BerechungenGewindegroesse.Text = Convert.ToString(s.metrischeGewindegroesse.bezeichnung);
                txB_BerechungenSchaftlaenge.Text = Convert.ToString(s.schaftLaenge.schaftlaenge);
                txB_BerechungenGewindelaenge.Text = Convert.ToString(s.gewindeLaenge.gewindeLaenge);
                txB_BerechungenFestigkeitsklasse.Text = s.festigkeitsklasse;
                txB_BerechungenAnzahl.Text = Convert.ToString(s.anzahl);

                txB_BerechungenVolumen.Text = Convert.ToString(Math.Round(s.volumen/1000,3));
                txB_BerechungenMasse.Text = Convert.ToString(s.masse);
                txB_BerechungenPreis.Text = Convert.ToString(s.preis);


                grd_Berechnungen.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region Btn Weiter
        private void btn_FestigkeitsklasseWeiter_Click(object sender, RoutedEventArgs e)
        {
            if(FinishFestigkeitsklasse == true)
            {
                tvi_Anzahl_Selected(tvi_Anzahl, null);
            }
            
        }

        private void btn_SchraubenkopfWeiter_Click(object sender, RoutedEventArgs e)
        {
            if(FinishSchraubenkopf == true)
            {
                tvi_Dimensionen_Selected(tvi_Dimensionen, null);
            }
            
        }

        private void btn_GewinderichtungWeiter_Click(object sender, RoutedEventArgs e)
        {
            if(FinishGewinderichtung == true)
            {
                tvi_Schraubenkopf_Selected(tvi_Schraubenkopf, null);
            }
            
        }

        private void btn_weiterDimensionen_Click(object sender, RoutedEventArgs e)
        {
            if(FinishGewindegroesse == true && FinishSchaftlaenge == true && FinishGewindelaenge == true)
            {
                tvi_Festigkeitsklasse_Selected(tvi_Festigkeitsklasse, null);
            }
            
        }
        #endregion

    }

}
