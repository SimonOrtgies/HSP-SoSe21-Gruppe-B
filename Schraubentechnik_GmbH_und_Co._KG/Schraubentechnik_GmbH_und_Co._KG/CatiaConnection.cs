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
    class CatiaConnection
    {
        INFITF.Application hsp_catiaApp;
        MECMOD.PartDocument hsp_catiaPart;
        MECMOD.Sketch hsp_catiaProfil;
        HybridBody catHybridBody1;

        Pad SchaftPad;
        Chamfer SchaftFase;

        Pad KopfPad;
        EdgeFillet RadiusKopf;

        Pocket SchlitzPocket;

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


        #region Schaft
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

        public void ErzeugeProfil(Double r)
        {
            // Skizze umbenennen
            hsp_catiaProfil.set_Name("Grundfläche Schaft");

            // Kreis in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();


            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);


            // dann den Kreis
            Circle2D catCircle2D_1 = catFactory2D1.CreateCircle(0, 0, r, 0, 0);
            catCircle2D_1.CenterPoint = catPoint2D1;



            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }

        public void ErzeugeSchaft(Double l)
        {
            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            // Block(Schaft) erzeugen
            ShapeFactory catShapeFactory1 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;
            SchaftPad = catShapeFactory1.AddNewPad(hsp_catiaProfil, l);
             

            // Block umbenennen
            SchaftPad.set_Name("Schaft");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }
        #endregion

        #region Kopf
        public void ErzeugeKopf(MetrischeGewindegroesse m, String sk)
        {
            sk = "Zylinderkopf mit Schlitz";                   //Test mit Sechskant 


            if (sk == "Sechskant")
            {
                Sechskant(m);
            }
            else if (sk == "Zylinderkopf mit Schlitz")
            {
                Zylinderkopf(m);
            }
            else if (sk == "Senkkopf mit Torx") 
            {
                Senkkopf(m);
            }
            else if (sk == "Linsenkopf mit Kreuz-Schlitz")
            {
                Linsenkopf(m);
            }

        }

        public void Sechskant(MetrischeGewindegroesse m)
        {
            #region SKizze bauen
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            Sketches catSketches1 = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaProfil = catSketches1.Add(catReference1);

            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();
            hsp_catiaProfil.set_Name("Sechskantkopf");
            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            

            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

            // Sechskant erzeugen
            double tan30 = Math.Sqrt(3)/3;
            double cos30 = Math.Sqrt(3)/2;
            //double mSW = m.schluesselweite /2;
            double mSW = 16;                              //Test mit Schlüsselweite 16

            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(mSW, tan30*mSW);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(mSW, -(tan30*mSW));
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(0, -(mSW/cos30));
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

            #endregion

            #region Pad
            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            // Block(Balken) erzeugen
            ShapeFactory catShapeFactory2 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;

            KopfPad = catShapeFactory2.AddNewPad(hsp_catiaProfil, -m.mutterhoehe);
            //Pad catPad2 = catShapeFactory2.AddNewPad(hsp_catiaProfil, -12);                   // Test mit Mutterhoehe 12

            // Block umbenennen
            KopfPad.set_Name("Kopf");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion
        }


        public void Zylinderkopf(MetrischeGewindegroesse m)
        {
            #region SKizze bauen
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            Sketches catSketches1 = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaProfil = catSketches1.Add(catReference1);

            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();
            hsp_catiaProfil.set_Name("Zylinderkopf");
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            

            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);

            // dann den Kreis
            Circle2D catCircle2D_1 = catFactory2D1.CreateCircle(0, 0, m.kopfdurchmesser/2, 0, 0);
            catCircle2D_1.CenterPoint = catPoint2D1;

            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();

            // Part aktualisieren
            hsp_catiaPart.Part.Update();

            #endregion

            #region Pad
            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            // Block(Schaft) erzeugen
            ShapeFactory catShapeFactory1 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;
            KopfPad = catShapeFactory1.AddNewPad(hsp_catiaProfil, -m.mutterhoehe);

            // Block umbenennen
            KopfPad.set_Name("Kopf");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();



            #endregion

            #region Schlitz

            #region Offsetebene
            Reference RefmyPlaneYZ = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part;
            HybridShapeFactory hybridShapeFactory1 = (HybridShapeFactory)hsp_catiaPart.Part.HybridShapeFactory;
            HybridShapePlaneOffset OffsetEbene = hybridShapeFactory1.AddNewPlaneOffset(RefmyPlaneYZ, m.mutterhoehe, true);
            OffsetEbene.set_Name("OffsetEbene");
            Reference RefOffsetEbene = hsp_catiaPart.Part.CreateReferenceFromObject(OffsetEbene);
            HybridBodies hybridBodies1 = hsp_catiaPart.Part.HybridBodies;
            HybridBody hybridBody1 = hybridBodies1.Item("Profile");
            hybridBody1.AppendHybridShape(OffsetEbene);


            hsp_catiaPart.Part.Update();
            Sketches catSketches2 = catHybridBody1.HybridSketches;
            Sketch SkizzeaufOffset = catSketches2.Add(RefOffsetEbene);
            hsp_catiaPart.Part.InWorkObject = SkizzeaufOffset;
            SkizzeaufOffset.set_Name("OffsetSkizze");

            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();
            // Skizzierer verlassen
            SkizzeaufOffset.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();

            #endregion


            // Skizze oeffnen
            Factory2D catFactory2D2 = SkizzeaufOffset.OpenEdition();


            // erst die Punkte
            Point2D catPoint2D2 = catFactory2D2.CreatePoint(m.kopfdurchmesser / 2, m.schlitzbreite / 2);
            Point2D catPoint2D3 = catFactory2D2.CreatePoint(-m.kopfdurchmesser / 2, m.schlitzbreite / 2);
            Point2D catPoint2D4 = catFactory2D2.CreatePoint(-m.kopfdurchmesser / 2, -m.schlitzbreite / 2);
            Point2D catPoint2D5 = catFactory2D2.CreatePoint(m.kopfdurchmesser / 2, -m.schlitzbreite / 2);

            Line2D catLine2D1 = catFactory2D2.CreateLine(m.kopfdurchmesser / 2, m.schlitzbreite / 2, -m.kopfdurchmesser / 2, m.schlitzbreite / 2);
            catLine2D1.StartPoint = catPoint2D2;
            catLine2D1.EndPoint = catPoint2D3;

            Line2D catLine2D2 = catFactory2D2.CreateLine(-m.kopfdurchmesser / 2, m.schlitzbreite / 2, -m.kopfdurchmesser / 2, -m.schlitzbreite / 2);
            catLine2D2.StartPoint = catPoint2D3;
            catLine2D2.EndPoint = catPoint2D4;

            Line2D catLine2D3 = catFactory2D2.CreateLine(-m.kopfdurchmesser / 2, -m.schlitzbreite / 2, m.kopfdurchmesser / 2, -m.schlitzbreite / 2);
            catLine2D3.StartPoint = catPoint2D4;
            catLine2D3.EndPoint = catPoint2D5;

            Line2D catLine2D4 = catFactory2D2.CreateLine(m.kopfdurchmesser / 2, -m.schlitzbreite / 2, m.kopfdurchmesser / 2, m.schlitzbreite / 2);
            catLine2D4.StartPoint = catPoint2D5;
            catLine2D4.EndPoint = catPoint2D2;

            hsp_catiaPart.Part.Update();

            #endregion

            #region Verrundung
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            ShapeFactory catshapeFactoryRadius = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;

            Reference reference1 = hsp_catiaPart.Part.CreateReferenceFromBRepName(  //Hier scheint der Fehler drin zu stecken, er erkennt nicht die richtige kante--wenn nicht die Kante, sondern die Fläche ausgewählt wird, scheint der Fehler behpoben zu sein
                "RSur:(Face:(Brp:(Pad.2;2);None:();Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", KopfPad);
            // "REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;1)));None:();Cf11:());Face:(Brp:(Pad.1;2);None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SchaftPad);
            RadiusKopf = catshapeFactoryRadius.AddNewEdgeFilletWithConstantRadius(reference1, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, 2);


            RadiusKopf.set_Name("Radius");
            hsp_catiaPart.Part.Update();

            #endregion

            #region Tasche Schlitz
            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            // Tasche erzeugen erzeugen
            ShapeFactory catShapeFactory2 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;

            SchlitzPocket = catShapeFactory2.AddNewPocket(SkizzeaufOffset, -m.schlitztiefe);

            // Block umbenennen
            SchlitzPocket.set_Name("Schlitz");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion

          


        }

        public void Linsenkopf(MetrischeGewindegroesse m)
        {
            #region SKizze bauen
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            Sketches catSketches1 = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaProfil = catSketches1.Add(catReference1);

            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();
            hsp_catiaProfil.set_Name("Linsenkopf");
            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion

        }

        public void Senkkopf(MetrischeGewindegroesse m)
        {
            #region SKizze bauen
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            Sketches catSketches1 = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaProfil = catSketches1.Add(catReference1);

            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();
            hsp_catiaProfil.set_Name("Senkkopf");
            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion

        }
        #endregion


        public void ErzeugeFase()           // Fase am Ende des Schraubenschaftes
        {
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            ShapeFactory catshapeFactoryFase = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;

            Reference reference1 = hsp_catiaPart.Part.CreateReferenceFromBRepName(
                "REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;1)));None:();Cf11:());Face:(Brp:(Pad.1;2);None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SchaftPad);

            SchaftFase = catshapeFactoryFase.AddNewChamfer(reference1, CatChamferPropagation.catTangencyChamfer, CatChamferMode.catLengthAngleChamfer, CatChamferOrientation.catNoReverseChamfer, 1, 45);

            SchaftFase.set_Name("Fase");
            hsp_catiaPart.Part.Update();
        }


        // Erzeugt ein Gewindefeature auf dem vorher erzeugten Schaft.
        internal void ErzeugeGewindeFeature(Gewinderichtung gr, Double bezeichnung, double gewindelaenge)
        {
            // Gewinde...
            // ... Referenzen lateral und limit erzeugen
            Reference RefMantelflaeche = hsp_catiaPart.Part.CreateReferenceFromBRepName(
                "RSur:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;1)));None:();Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SchaftFase);
            Reference RefFrontflaeche = hsp_catiaPart.Part.CreateReferenceFromBRepName(
                "RSur:(Face:(Brp:(Pad.1;2);None:();Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SchaftFase);

            ShapeFactory catshapeFactoryThread = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;

            // ... Gewinde erzeugen, Parameter setzen
            PARTITF.Thread thread1 = catshapeFactoryThread.AddNewThreadWithOutRef();
            if(gr == Gewinderichtung.Rechtsgewinde)     //Gewinderichtung festlegen
            {
                thread1.Side = CatThreadSide.catRightSide;
            }
            else
            {
                thread1.Side = CatThreadSide.catLeftSide;
            }
            thread1.Diameter = bezeichnung; //Gewindegröße
            thread1.Depth = gewindelaenge;

            thread1.LateralFaceElement = RefMantelflaeche; // Referenz lateral
            thread1.LimitFaceElement = RefFrontflaeche; // Referenz limit

            // ... Standardgewinde gesteuert über eine Konstruktionstabelle
            thread1.CreateUserStandardDesignTable("Metric_Thick_Pitch", @"C:\Program Files\Dassault Systemes\B28\win_b64\resources\standard\thread\Metric_Thick_Pitch.xml");

            thread1.set_Name("Gewinde");

            // Part update und fertig
            hsp_catiaPart.Part.Update();
        }

    }
}
