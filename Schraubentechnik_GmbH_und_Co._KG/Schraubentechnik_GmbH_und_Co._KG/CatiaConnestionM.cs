using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MECMOD;
using INFITF;
using PARTITF;
using System.Windows;
using HybridShapeTypeLib;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class CatiaConnectionM
    {

        //MUTTERN WERDEN 0.2 mm ZU GRO? GEFERTIGT (FÜR TOLERANZ)
        INFITF.Application hsp_catiaApp;
        MECMOD.PartDocument hsp_catiaPart;
        MECMOD.Sketch hsp_catiaProfil;
        HybridBody catHybridBody1;

        Pad KopfPad;
        Groove SechskantGroove;
        Pocket LochPocket;

        public bool CATIALaeuft()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject(
                    "CATIA.Application");
                hsp_catiaApp = (INFITF.Application)catiaObject;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean ErzeugePart()
        {
            INFITF.Documents catDocuments1 = hsp_catiaApp.Documents;
            hsp_catiaPart = catDocuments1.Add("Part") as MECMOD.PartDocument;
            return true;
        }

        public void ErstelleLeereSkizze()
        {
            // geometrisches Set auswaehlen und umbenennen
            HybridBodies catHybridBodies1 = hsp_catiaPart.Part.HybridBodies;

            try
            {
                catHybridBody1 = catHybridBodies1.Item("Geometrisches Set.1");
            }
            catch (Exception)
            {
                MessageBox.Show("Kein geometrisches Set gefunden! " + Environment.NewLine +
                    "Ein PART manuell erzeugen und darauf achten, dass 'Geometrisches Set' aktiviert ist.",
                    "Fehler", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            catHybridBody1.set_Name("Profile");
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            Sketches catSketches1 = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaProfil = catSketches1.Add(catReference1);

            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }

        private void ErzeugeAchsensystem()
        {
            object[] arr = new object[] {0.0, 0.0, 0.0,
                                         0.0, 1.0, 0.0,
                                         0.0, 0.0, 1.0 };
            hsp_catiaProfil.SetAbsoluteAxisData(arr);
        }

        public void ErzeugeSechskant(MetrischeGewindegroesse m)
        {
            // Skizze umbenennen
            hsp_catiaProfil.set_Name("Grundfläche Sechskant");

            // Kreis in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();


            // Sechskant erzeugen
            double tan30 = Math.Sqrt(3) / 3;
            double cos30 = Math.Sqrt(3) / 2;
            double mSW = m.schluesselweite / 2;
            //double mSW = 16;                              //Test mit Schlüsselweite 16

            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(mSW, tan30 * mSW);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(mSW, -(tan30 * mSW));
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(0, -(mSW / cos30));
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(-mSW, -(tan30 * mSW));
            Point2D catPoint2D5 = catFactory2D1.CreatePoint(-mSW, tan30 * mSW);
            Point2D catPoint2D6 = catFactory2D1.CreatePoint(0, mSW / cos30);

            // dann die Linien
            Line2D catLine2D1 = catFactory2D1.CreateLine(mSW, tan30 * mSW, mSW, -(tan30 * mSW));
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(mSW, -(tan30 * mSW), 0, -(mSW / cos30));
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(0, -(mSW / cos30), -mSW, -(tan30 * mSW));
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(-mSW, -(tan30 * mSW), -mSW, (tan30 * mSW));
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D5;

            Line2D catLine2D5 = catFactory2D1.CreateLine(-mSW, (tan30 * mSW), 0, mSW / cos30);
            catLine2D5.StartPoint = catPoint2D5;
            catLine2D5.EndPoint = catPoint2D6;

            Line2D catLine2D6 = catFactory2D1.CreateLine(0, mSW / cos30, mSW, tan30 * mSW);
            catLine2D6.StartPoint = catPoint2D6;
            catLine2D6.EndPoint = catPoint2D1;

            // Part aktualisieren
            hsp_catiaPart.Part.Update();

            #region Pad
            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            // Block(Balken) erzeugen
            ShapeFactory catShapeFactory2 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;

            KopfPad = catShapeFactory2.AddNewPad(hsp_catiaProfil, -m.kopfhoehesechs);
            //Pad catPad2 = catShapeFactory2.AddNewPad(hsp_catiaProfil, -12);                   // Test mit Mutterhoehe 12

            // Block umbenennen
            KopfPad.set_Name("Kopf");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion

            Sechskantverrundung(m);
            /*
            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            */
        }

        public void Sechskantverrundung(MetrischeGewindegroesse m)
        {
            #region SKizze bauen
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            Sketches catSketches1 = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneZX;
            hsp_catiaProfil = catSketches1.Add(catReference1);


            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();
            hsp_catiaProfil.set_Name("Kopffase");
            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();

            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();
            #endregion

            #region Profil zeichnen

            double tan30 = Math.Sqrt(3) / 3;
            double cos30 = Math.Sqrt(3) / 2;
            double mSW = (m.schluesselweite / 2);
            double Breite = (mSW / cos30);

            Point2D catPoint2D1 = catFactory2D1.CreatePoint(m.kopfhoehesechs, mSW * 0.95);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(m.kopfhoehesechs, Breite);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(m.kopfhoehesechs - (tan30 * (Breite - mSW)), Breite);

            Line2D catLine2D1 = catFactory2D1.CreateLine(m.kopfhoehesechs, mSW * 0.95, m.kopfhoehesechs, Breite);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(m.kopfhoehesechs, Breite, m.kopfhoehesechs - (tan30 * (Breite - mSW)), Breite);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(m.kopfhoehesechs - (tan30 * (Breite - mSW)), Breite, m.kopfhoehesechs, mSW * 0.95);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D1;

            Point2D AxisPoint1 = catFactory2D1.CreatePoint(0, 0);
            Point2D AxisPoint2 = catFactory2D1.CreatePoint(m.kopfhoehesechs, 0);

            Line2D AxisLine1 = catFactory2D1.CreateLine(0, 0, m.kopfhoehesechs, 0);
            AxisLine1.StartPoint = AxisPoint1;
            AxisLine1.EndPoint = AxisPoint2;

            Reference Axisreference1 = hsp_catiaPart.Part.CreateReferenceFromObject(AxisPoint1);
            GeometricElements geometricElements1 = hsp_catiaProfil.GeometricElements;
            Axis2D catAxis2D1 = (Axis2D)geometricElements1.Item("Absolute Achse");
            Line2D AxisLine2 = (Line2D)catAxis2D1.GetItem("H-Richtung");

            Reference Axisreference2 = hsp_catiaPart.Part.CreateReferenceFromObject(AxisLine2);
            Constraints constraints1 = hsp_catiaProfil.Constraints;
            Constraint constraint1 = constraints1.AddBiEltCst(CatConstraintType.catCstTypeOn, Axisreference1, Axisreference2);
            constraint1.Mode = CatConstraintMode.catCstModeDrivingDimension;

            Reference Axisreference3 = hsp_catiaPart.Part.CreateReferenceFromObject(AxisLine1);
            Reference Axisreference4 = hsp_catiaPart.Part.CreateReferenceFromObject(AxisLine2);
            Constraint constraint2 = constraints1.AddBiEltCst(CatConstraintType.catCstTypeVerticality, Axisreference3, Axisreference4);
            constraint2.Mode = CatConstraintMode.catCstModeDrivingDimension;

            hsp_catiaProfil.CenterLine = AxisLine1;

            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion

            #region Nut
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;


            ShapeFactory catShapeFactory1 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;
            Reference reference1 = hsp_catiaPart.Part.CreateReferenceFromObject(hsp_catiaProfil);
            SechskantGroove = catShapeFactory1.AddNewGrooveFromRef(reference1);
            SechskantGroove.SetProfileElement(reference1);

            SechskantGroove.set_Name("Kopfnut");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion
        }

        public void ErzeugeLoch(double r, MetrischeGewindegroesse m)
        {
            #region SKizze bauen
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            Sketches catSketches1 = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaProfil = catSketches1.Add(catReference1);

            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();
            hsp_catiaProfil.set_Name("Loch");
            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();

            // Skizze oeffnen
            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();


            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);


            // dann den Kreis
            Circle2D catCircle2D_1 = catFactory2D1.CreateCircle(0, 0, r + 0.2, 0, 0);
            catCircle2D_1.CenterPoint = catPoint2D1;



            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();

            #endregion
            #region Pocket
            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            // Block(Balken) erzeugen
            ShapeFactory catShapeFactory2 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;

            LochPocket = catShapeFactory2.AddNewPocket(hsp_catiaProfil, m.kopfhoehesechs);
            //Pad catPad2 = catShapeFactory2.AddNewPad(hsp_catiaProfil, -12);                   // Test mit Mutterhoehe 12

            // Block umbenennen
            LochPocket.set_Name("Loch");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion

        }



        #region Gewinde Optisch
        internal void ErzeugeGewindeHelix(Mutter m)
        {
            Double P = m.metrischeGewindegroesse.steigung;
            Double Ri = m.metrischeGewindegroesse.bezeichnung / 2;
            HybridShapeFactory HSF = (HybridShapeFactory)hsp_catiaPart.Part.HybridShapeFactory;

            Sketch myGewinde = makeGewindeSkizze(m);

            HybridShapeDirection HelixDir = HSF.AddNewDirectionByCoord(1, 0, 0);
            Reference RefHelixDir = hsp_catiaPart.Part.CreateReferenceFromObject(HelixDir);

            HybridShapePointCoord HelixStartpunkt = HSF.AddNewPointCoord(0, 0, Ri + 0.2);
            Reference RefHelixStartpunkt = hsp_catiaPart.Part.CreateReferenceFromObject(HelixStartpunkt);

            Boolean DrehrichtungLinks = false;

            /*if (s.gewinderichtung == Gewinderichtung.Rechtsgewinde)
            {
                DrehrichtungLinks = false;
            }
            else
            {
                DrehrichtungLinks = true;
            }*/

            HybridShapeHelix Helix = HSF.AddNewHelix(RefHelixDir, true, RefHelixStartpunkt, P, m.metrischeGewindegroesse.mutterhoehe, DrehrichtungLinks, 0, 0, false);


            Reference RefHelix = hsp_catiaPart.Part.CreateReferenceFromObject(Helix);
            Reference RefmyGewinde = hsp_catiaPart.Part.CreateReferenceFromObject(myGewinde);

            hsp_catiaPart.Part.Update();

            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            OriginElements catOriginElements = this.hsp_catiaPart.Part.OriginElements;
            Reference RefmyPlaneZX = (Reference)catOriginElements.PlaneZX;

            #region Fase
            Sketches catSketchesChamferHelix = catHybridBody1.HybridSketches;
            /*Sketch ChamferSkizze = catSketchesChamferHelix.Add(RefmyPlaneZX);
            hsp_catiaPart.Part.InWorkObject = ChamferSkizze;
            ChamferSkizze.set_Name("Fase");

            double H_links = Ri;
            double V_links = - 0.65 * P;

            double H_rechts = Ri;
            double V_rechts = 0;

            double H_unten = Ri - 0.65 * P;
            double V_unten = 0;

            Factory2D catFactory2D3 = ChamferSkizze.OpenEdition();

            Point2D links = catFactory2D3.CreatePoint(H_links, V_links);
            Point2D rechts = catFactory2D3.CreatePoint(H_rechts, V_rechts);
            Point2D unten = catFactory2D3.CreatePoint(H_unten, V_unten);

            Line2D Oben = catFactory2D3.CreateLine(H_links, V_links, H_rechts, V_rechts);
            Oben.StartPoint = links;
            Oben.EndPoint = rechts;

            Line2D hypo = catFactory2D3.CreateLine(H_links, V_links, H_unten, V_unten);
            hypo.StartPoint = links;
            hypo.EndPoint = unten;

            Line2D seite = catFactory2D3.CreateLine(H_unten, V_unten, H_rechts, V_rechts);
            seite.StartPoint = unten;
            seite.EndPoint = rechts;

            ChamferSkizze.CloseEdition();
            */
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;
            hsp_catiaPart.Part.Update();

            ShapeFactory catshapeFactoryHelix = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;
            //Groove myChamfer = catshapeFactoryHelix.AddNewGroove(ChamferSkizze);
            //myChamfer.RevoluteAxis = RefHelixDir;

            hsp_catiaPart.Part.Update();
            #endregion

            //Slot GewindeRille = catshapeFactoryHelix.AddNewSlotFromRef(RefmyGewinde, RefHelix);
            Rib GewindeRippe = catshapeFactoryHelix.AddNewRibFromRef(RefmyGewinde, RefHelix);

            Reference RefmyPad = hsp_catiaPart.Part.CreateReferenceFromObject(LochPocket);
            HybridShapeSurfaceExplicit GewindestangenSurface = HSF.AddNewSurfaceDatum(RefmyPad);
            Reference RefGewindestangenSurface = hsp_catiaPart.Part.CreateReferenceFromObject(GewindestangenSurface);

            GewindeRippe.ReferenceSurfaceElement = RefGewindestangenSurface;

            Reference RefGewindeRippe = hsp_catiaPart.Part.CreateReferenceFromObject(GewindeRippe);

            hsp_catiaPart.Part.Update();
        }



        private Sketch makeGewindeSkizze(Mutter m)
        {
            Double P = m.metrischeGewindegroesse.steigung;
            Double Ri = m.metrischeGewindegroesse.bezeichnung / 2;


            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference RefmyPlaneZX = (Reference)catOriginElements.PlaneZX;

            Sketches catSketchesGewinde = catHybridBody1.HybridSketches;
            Sketch myGewinde = catSketchesGewinde.Add(RefmyPlaneZX);
            hsp_catiaPart.Part.InWorkObject = myGewinde;
            myGewinde.set_Name("Gewinde");

            double V_oben_links = -(((((Math.Sqrt(3) / 2) * P) / 6) + 0.6134 * P) * Math.Tan((30 * Math.PI) / 180));
            double H_oben_links = Ri+0.2;

            double V_oben_rechts = (((((Math.Sqrt(3) / 2) * P) / 6) + 0.6134 * P) * Math.Tan((30 * Math.PI) / 180));
            double H_oben_rechts = Ri + 0.2;

            double V_unten_links = -((0.1443 * P) * Math.Sin((60 * Math.PI) / 180));
            double H_unten_links = Ri + 0.2 - (0.6134 * P - 0.1443 * P) - Math.Sqrt(Math.Pow((0.1443 * P), 2) - Math.Pow((0.1443 * P) * Math.Sin((60 * Math.PI) / 180), 2));

            double V_unten_rechts = (0.1443 * P) * Math.Sin((60 * Math.PI) / 180);
            double H_unten_rechts = Ri + 0.2 - (0.6134 * P - 0.1443 * P) - Math.Sqrt(Math.Pow((0.1443 * P), 2) - Math.Pow((0.1443 * P) * Math.Sin((60 * Math.PI) / 180), 2));

            double V_Mittelpunkt = 0;
            double H_Mittelpunkt = Ri + 0.2 - ((0.6134 * P) - 0.1443 * P);

            Factory2D catFactory2D2 = myGewinde.OpenEdition();

            Point2D Oben_links = catFactory2D2.CreatePoint(H_oben_links, V_oben_links);
            Point2D Oben_rechts = catFactory2D2.CreatePoint(H_oben_rechts, V_oben_rechts);
            Point2D Unten_links = catFactory2D2.CreatePoint(H_unten_links, V_unten_links);
            Point2D Unten_rechts = catFactory2D2.CreatePoint(H_unten_rechts, V_unten_rechts);
            Point2D Mittelpunkt = catFactory2D2.CreatePoint(H_Mittelpunkt, V_Mittelpunkt);

            Line2D Oben = catFactory2D2.CreateLine(H_oben_links, V_oben_links, H_oben_rechts, V_oben_rechts);
            Oben.StartPoint = Oben_links;
            Oben.EndPoint = Oben_rechts;

            Line2D Rechts = catFactory2D2.CreateLine(H_oben_rechts, V_oben_rechts, H_unten_rechts, V_unten_rechts);
            Rechts.StartPoint = Oben_rechts;
            Rechts.EndPoint = Unten_rechts;

            Circle2D Verrundung = catFactory2D2.CreateCircle(H_Mittelpunkt, V_Mittelpunkt, 0.1443 * P, 0, 0);
            Verrundung.CenterPoint = Mittelpunkt;
            Verrundung.StartPoint = Unten_rechts;
            Verrundung.EndPoint = Unten_links;

            Line2D Links = catFactory2D2.CreateLine(H_oben_links, V_oben_links, H_unten_links, V_unten_links);
            Links.StartPoint = Unten_links;
            Links.EndPoint = Oben_links;

            myGewinde.CloseEdition();
            hsp_catiaPart.Part.Update();

            return myGewinde;
        }
        #endregion



    }

}