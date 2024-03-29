﻿using System;
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


        Shaft SenkkopfShaft;
        Groove SechskantGroove;


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
            //sk = "Zylinderkopf mit Schlitz";                   //Test mit Sechskant 
   


            if (sk == "Sechskant")
            {
                Sechskant(m);
            }
            else if (sk == "Zylinderkopf mit Schlitz")
            {
                ZylinderkopfSchlitz(m);
            }
            else if (sk == "Senkkopf mit Torx") 
            {
                Senkkopf(m);
            }
            else if (sk == "Linsenkopf mit Kreuz-Schlitz")
            {
                ZylinderkopfInnensechskant(m);
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
            double mSW = m.schluesselweite /2;
            //double mSW = 16;                              //Test mit Schlüsselweite 16

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

            KopfPad = catShapeFactory2.AddNewPad(hsp_catiaProfil, -m.kopfhoehesechs);
            //Pad catPad2 = catShapeFactory2.AddNewPad(hsp_catiaProfil, -12);                   // Test mit Mutterhoehe 12

            // Block umbenennen
            KopfPad.set_Name("Kopf");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion

            Sechskantverrundung(m);
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

            Point2D catPoint2D1 = catFactory2D1.CreatePoint(m.kopfhoehesechs, mSW*0.95);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(m.kopfhoehesechs, Breite);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(m.kopfhoehesechs-(tan30*(Breite-mSW)),Breite);

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


        public void ZylinderkopfSchlitz(MetrischeGewindegroesse m)
        {
            #region SKizze bauen
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            Sketches catSketches1 = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaProfil = catSketches1.Add(catReference1);

            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();
            hsp_catiaProfil.set_Name("Zylinderkopf mit Schlitz");
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
            RadiusKopf = catshapeFactoryRadius.AddNewEdgeFilletWithConstantRadius(reference1, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, m.fase);


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

        public void ZylinderkopfInnensechskant(MetrischeGewindegroesse m)
        {
            #region SKizze bauen
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            Sketches catSketches1 = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaProfil = catSketches1.Add(catReference1);

            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();
            hsp_catiaProfil.set_Name("Zylinderkopf mit Innensechskannt");
            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();

            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);

            // dann den Kreis
            Circle2D catCircle2D_1 = catFactory2D1.CreateCircle(0, 0, m.kopfdurchmesser / 2, 0, 0);
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

            #region Innensechskannt
            Factory2D catFactory2D2 = SkizzeaufOffset.OpenEdition();
            
            // Sechskant erzeugen
            double tan30 = Math.Sqrt(3) / 3;
            double cos30 = Math.Sqrt(3) / 2;
            double mSW = m.innensechskant /2;
            
            // erst die Punkte
            Point2D catPoint2D2 = catFactory2D2.CreatePoint(mSW, tan30 * mSW);
            Point2D catPoint2D3 = catFactory2D2.CreatePoint(mSW, -(tan30 * mSW));
            Point2D catPoint2D4 = catFactory2D2.CreatePoint(0, -(mSW / cos30));
            Point2D catPoint2D5 = catFactory2D2.CreatePoint(-mSW, -(tan30 * mSW));
            Point2D catPoint2D6 = catFactory2D2.CreatePoint(-mSW, tan30 * mSW);
            Point2D catPoint2D7 = catFactory2D2.CreatePoint(0, mSW / cos30);

            // dann die Linien
            Line2D catLine2D1 = catFactory2D2.CreateLine(mSW, tan30 * mSW, mSW, -(tan30 * mSW));
            catLine2D1.StartPoint = catPoint2D2;
            catLine2D1.EndPoint = catPoint2D3;

            Line2D catLine2D2 = catFactory2D2.CreateLine(mSW, -(tan30 * mSW), 0, -(mSW / cos30));
            catLine2D2.StartPoint = catPoint2D3;
            catLine2D2.EndPoint = catPoint2D4;

            Line2D catLine2D3 = catFactory2D2.CreateLine(0, -(mSW / cos30), -mSW, -(tan30 * mSW));
            catLine2D3.StartPoint = catPoint2D4;
            catLine2D3.EndPoint = catPoint2D5;

            Line2D catLine2D4 = catFactory2D2.CreateLine(-mSW, -(tan30 * mSW), -mSW, (tan30 * mSW));
            catLine2D4.StartPoint = catPoint2D5;
            catLine2D4.EndPoint = catPoint2D6;

            Line2D catLine2D5 = catFactory2D2.CreateLine(-mSW, (tan30 * mSW), 0, mSW / cos30);
            catLine2D5.StartPoint = catPoint2D6;
            catLine2D5.EndPoint = catPoint2D7;

            Line2D catLine2D6 = catFactory2D2.CreateLine(0, mSW / cos30, mSW, tan30 * mSW);
            catLine2D6.StartPoint = catPoint2D7;
            catLine2D6.EndPoint = catPoint2D2;

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion
            
            #region Verrundung
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            ShapeFactory catshapeFactoryRadius = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;

            Reference reference1 = hsp_catiaPart.Part.CreateReferenceFromBRepName(  //Hier scheint der Fehler drin zu stecken, er erkennt nicht die richtige kante--wenn nicht die Kante, sondern die Fläche ausgewählt wird, scheint der Fehler behpoben zu sein
                "RSur:(Face:(Brp:(Pad.2;2);None:();Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", KopfPad);
            // "REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;1)));None:();Cf11:());Face:(Brp:(Pad.1;2);None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SchaftPad);
            RadiusKopf = catshapeFactoryRadius.AddNewEdgeFilletWithConstantRadius(reference1, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, m.fase);


            RadiusKopf.set_Name("Radius");
            hsp_catiaPart.Part.Update();
            #endregion

            #region Tasche Innensechskannt
            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            // Tasche erzeugen erzeugen
            ShapeFactory catShapeFactory2 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;

            SchlitzPocket = catShapeFactory2.AddNewPocket(SkizzeaufOffset, -m.innensktiefe);

            // Block umbenennen
            SchlitzPocket.set_Name("Innensechskant");

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
            Reference catReference1 = (Reference)catOriginElements.PlaneZX;
            hsp_catiaProfil = catSketches1.Add(catReference1);


            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();
            hsp_catiaProfil.set_Name("Senkkopf");
            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            
            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();
            #endregion

            #region Profil
            double tan45 = 1;
            double tan30 = Math.Sqrt(3) / 3;
            double winkel;
            if (m.bezeichnung <= 20)
            {
                winkel = tan45;
            }
            else
            {
                winkel = tan30;
            }

            double HoeheP4 = ((m.innenskkopfdurchmesser / 2) - (m.bezeichnung / 2)) / winkel;
            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(m.innenskkopfhöhe, 0);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(m.innenskkopfhöhe, m.innenskkopfdurchmesser / 2);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(HoeheP4, m.innenskkopfdurchmesser / 2);
            Point2D catPoint2D5 = catFactory2D1.CreatePoint(0, m.bezeichnung / 2);

            // dann das Profil
            Line2D catLine2D1 = catFactory2D1.CreateLine(0, 0, m.innenskkopfhöhe, 0);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(m.innenskkopfhöhe, 0, m.innenskkopfhöhe, m.innenskkopfdurchmesser / 2);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(m.innenskkopfhöhe, m.innenskkopfdurchmesser / 2, HoeheP4, m.innenskkopfdurchmesser / 2);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(HoeheP4, m.innenskkopfdurchmesser / 2, 0, m.bezeichnung / 2);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D5;

            Line2D catLine2D5 = catFactory2D1.CreateLine(0, m.bezeichnung / 2, 0, 0);
            catLine2D5.StartPoint = catPoint2D5;
            catLine2D5.EndPoint = catPoint2D1;

            Point2D AxisPoint1 = catFactory2D1.CreatePoint(0, 0);
            Point2D AxisPoint2 = catFactory2D1.CreatePoint(m.innenskkopfhöhe, 0);

            Line2D AxisLine1 = catFactory2D1.CreateLine(0, 0, m.innenskkopfhöhe, 0);
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

            #region Shaft
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;


            ShapeFactory catShapeFactory1 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;
            Reference reference1 = hsp_catiaPart.Part.CreateReferenceFromObject(hsp_catiaProfil);
            SenkkopfShaft = catShapeFactory1.AddNewShaftFromRef(reference1);
            SenkkopfShaft.SetProfileElement(reference1);

            SenkkopfShaft.set_Name("Kopf");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion

            #region Offsetebene
            Reference RefmyPlaneYZ = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part;
            HybridShapeFactory hybridShapeFactory1 = (HybridShapeFactory)hsp_catiaPart.Part.HybridShapeFactory;
            HybridShapePlaneOffset OffsetEbene = hybridShapeFactory1.AddNewPlaneOffset(RefmyPlaneYZ, m.innenskkopfhöhe, true);
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

            #region Innensechskant
            Factory2D catFactory2D2 = SkizzeaufOffset.OpenEdition();

            // Sechskant erzeugen

            double cos30 = Math.Sqrt(3) / 2;
            double mSW = m.innensechskant / 2;

            // erst die Punkte
            Point2D catPoint2D6 = catFactory2D2.CreatePoint(mSW, tan30 * mSW);
            Point2D catPoint2D7 = catFactory2D2.CreatePoint(mSW, -(tan30 * mSW));
            Point2D catPoint2D8 = catFactory2D2.CreatePoint(0, -(mSW / cos30));
            Point2D catPoint2D9 = catFactory2D2.CreatePoint(-mSW, -(tan30 * mSW));
            Point2D catPoint2D10 = catFactory2D2.CreatePoint(-mSW, tan30 * mSW);
            Point2D catPoint2D11 = catFactory2D2.CreatePoint(0, mSW / cos30);

            // dann die Linien
            Line2D catLine2D6 = catFactory2D2.CreateLine(mSW, tan30 * mSW, mSW, -(tan30 * mSW));
            catLine2D6.StartPoint = catPoint2D6;
            catLine2D6.EndPoint = catPoint2D7;

            Line2D catLine2D7 = catFactory2D2.CreateLine(mSW, -(tan30 * mSW), 0, -(mSW / cos30));
            catLine2D7.StartPoint = catPoint2D7;
            catLine2D7.EndPoint = catPoint2D8;

            Line2D catLine2D8 = catFactory2D2.CreateLine(0, -(mSW / cos30), -mSW, -(tan30 * mSW));
            catLine2D8.StartPoint = catPoint2D8;
            catLine2D8.EndPoint = catPoint2D9;

            Line2D catLine2D9 = catFactory2D2.CreateLine(-mSW, -(tan30 * mSW), -mSW, (tan30 * mSW));
            catLine2D9.StartPoint = catPoint2D9;
            catLine2D9.EndPoint = catPoint2D10;

            Line2D catLine2D10 = catFactory2D2.CreateLine(-mSW, (tan30 * mSW), 0, mSW / cos30);
            catLine2D10.StartPoint = catPoint2D10;
            catLine2D10.EndPoint = catPoint2D11;

            Line2D catLine2D11 = catFactory2D2.CreateLine(0, mSW / cos30, mSW, tan30 * mSW);
            catLine2D11.StartPoint = catPoint2D11;
            catLine2D11.EndPoint = catPoint2D6;

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion

            #region Tasche Innensechskant
            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            // Tasche erzeugen erzeugen
            ShapeFactory catShapeFactory2 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;

            SchlitzPocket = catShapeFactory2.AddNewPocket(SkizzeaufOffset, -m.schlitztiefe);

            // Block umbenennen
            SchlitzPocket.set_Name("Innensechskannt");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion
        }
        #endregion

        // Fase am Ende des Schraubenschaftes
        public void ErzeugeFase(MetrischeGewindegroesse m)
        {
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            ShapeFactory catshapeFactoryFase = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;

            Reference reference1 = hsp_catiaPart.Part.CreateReferenceFromBRepName(
                "REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;1)));None:();Cf11:());Face:(Brp:(Pad.1;2);None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SchaftPad);

            SchaftFase = catshapeFactoryFase.AddNewChamfer(reference1, CatChamferPropagation.catTangencyChamfer, CatChamferMode.catLengthAngleChamfer, CatChamferOrientation.catNoReverseChamfer, m.fase, 45);

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

        #region Gewinde Optisch
        internal void ErzeugeGewindeHelix(Schraube  s)
        {
            Double P = s.metrischeGewindegroesse.steigung;
            Double Ri = s.metrischeGewindegroesse.bezeichnung/2;
            HybridShapeFactory HSF = (HybridShapeFactory)hsp_catiaPart.Part.HybridShapeFactory;

            Sketch myGewinde = makeGewindeSkizze(s);

            HybridShapeDirection HelixDir = HSF.AddNewDirectionByCoord(1, 0, 0);
            Reference RefHelixDir = hsp_catiaPart.Part.CreateReferenceFromObject(HelixDir);

            HybridShapePointCoord HelixStartpunkt = HSF.AddNewPointCoord(s.schaftLaenge.schaftlaenge, 0, Ri);
            Reference RefHelixStartpunkt = hsp_catiaPart.Part.CreateReferenceFromObject(HelixStartpunkt);

            Boolean DrehrichtungLinks;

            if (s.gewinderichtung == Gewinderichtung.Rechtsgewinde)
            {
                DrehrichtungLinks = false;
            }
            else
            {
                DrehrichtungLinks = true;
            }

            HybridShapeHelix Helix = HSF.AddNewHelix(RefHelixDir, true, RefHelixStartpunkt, P, s.gewindeLaenge.gewindeLaenge, DrehrichtungLinks, 0, 0, false);


            Reference RefHelix = hsp_catiaPart.Part.CreateReferenceFromObject(Helix);
            Reference RefmyGewinde = hsp_catiaPart.Part.CreateReferenceFromObject(myGewinde);

            hsp_catiaPart.Part.Update();

            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            OriginElements catOriginElements = this.hsp_catiaPart.Part.OriginElements;
            Reference RefmyPlaneZX = (Reference)catOriginElements.PlaneZX;
            
            Sketches catSketchesChamferHelix = catHybridBody1.HybridSketches;
            Sketch ChamferSkizze = catSketchesChamferHelix.Add(RefmyPlaneZX);
            hsp_catiaPart.Part.InWorkObject = ChamferSkizze;
            ChamferSkizze.set_Name("Fase");

            double H_links = Ri;
            double V_links = s.schaftLaenge.schaftlaenge-0.65 * P;

            double H_rechts = Ri;
            double V_rechts = s.schaftLaenge.schaftlaenge;

            double H_unten = Ri - 0.65 * P;
            double V_unten = s.schaftLaenge.schaftlaenge;

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
            
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;
            hsp_catiaPart.Part.Update();

            ShapeFactory catshapeFactoryHelix = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;
            Groove myChamfer = catshapeFactoryHelix.AddNewGroove(ChamferSkizze);
            myChamfer.RevoluteAxis = RefHelixDir;
            
            hsp_catiaPart.Part.Update();

            Slot GewindeRille = catshapeFactoryHelix.AddNewSlotFromRef(RefmyGewinde, RefHelix);

            Reference RefmyPad = hsp_catiaPart.Part.CreateReferenceFromObject(SchaftPad);
            HybridShapeSurfaceExplicit GewindestangenSurface = HSF.AddNewSurfaceDatum(RefmyPad);
            Reference RefGewindestangenSurface = hsp_catiaPart.Part.CreateReferenceFromObject(GewindestangenSurface);

            GewindeRille.ReferenceSurfaceElement = RefGewindestangenSurface;

            Reference RefGewindeRille = hsp_catiaPart.Part.CreateReferenceFromObject(GewindeRille);

            hsp_catiaPart.Part.Update();
        }

        
      
        private Sketch makeGewindeSkizze(Schraube s)
        {
            Double P = s.metrischeGewindegroesse.steigung;
            Double Ri = s.metrischeGewindegroesse.bezeichnung/2;
           

            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference RefmyPlaneZX = (Reference)catOriginElements.PlaneZX;

            Sketches catSketchesGewinde = catHybridBody1.HybridSketches;
            Sketch myGewinde = catSketchesGewinde.Add(RefmyPlaneZX);
            hsp_catiaPart.Part.InWorkObject = myGewinde;
            myGewinde.set_Name("Gewinde");

            double V_oben_links = -(((((Math.Sqrt(3) / 2) * P) / 6) + 0.6134 * P) * Math.Tan((30 * Math.PI) / 180)-s.schaftLaenge.schaftlaenge);
            double H_oben_links = Ri;

            double V_oben_rechts = (((((Math.Sqrt(3) / 2) * P) / 6) + 0.6134 * P) * Math.Tan((30 * Math.PI) / 180)+s.schaftLaenge.schaftlaenge);
            double H_oben_rechts = Ri;

            double V_unten_links = -((0.1443 * P) * Math.Sin((60 * Math.PI) / 180)-s.schaftLaenge.schaftlaenge);
            double H_unten_links = Ri - (0.6134 * P - 0.1443 * P) - Math.Sqrt(Math.Pow((0.1443 * P), 2) - Math.Pow((0.1443 * P) * Math.Sin((60 * Math.PI) / 180), 2));

            double V_unten_rechts = (0.1443 * P) * Math.Sin((60 * Math.PI) / 180)+s.schaftLaenge.schaftlaenge;
            double H_unten_rechts = Ri - (0.6134 * P - 0.1443 * P) - Math.Sqrt(Math.Pow((0.1443 * P), 2) - Math.Pow((0.1443 * P) * Math.Sin((60 * Math.PI) / 180), 2));

            double V_Mittelpunkt = s.schaftLaenge.schaftlaenge;
            double H_Mittelpunkt = Ri - ((0.6134 * P) - 0.1443 * P);

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

        public void ErzeugeExportDatei(Schraube s)
        {
            string Dokumentname = s.schraubenkopf +" M" + s.metrischeGewindegroesse.bezeichnung + "x" + s.schaftLaenge.schaftlaenge;
            hsp_catiaPart.Activate();
            hsp_catiaPart.ExportData("C:\\Windows\\Temp\\" + Dokumentname, "stp");
            //partDocument1.SaveAs "D:\Users\Simon Ortgies\Google Drive\Me2 Conaxkupplung\Part7.CATPart"
        }

        public void ErzeugeScreenshot(Schraube s)
        {
            // Dateiname festlegen
            string bildname = s.schraubenkopf + " M" + s.metrischeGewindegroesse.bezeichnung + "x" + s.schaftLaenge.schaftlaenge;

            //Standarthintergrung speichern
            object[] arr1 = new object[3];
            hsp_catiaApp.ActiveWindow.ActiveViewer.GetBackgroundColor(arr1);

            // Hintergrung auf weiß setzen
            object[] arr2 = new object[] { 1, 1, 1 };
            hsp_catiaApp.ActiveWindow.ActiveViewer.PutBackgroundColor(arr2);

            // 3D Kompass ausblenden
            hsp_catiaApp.StartCommand("CompassDisplayOff");
            hsp_catiaApp.ActiveWindow.ActiveViewer.Reframe();

            INFITF.SettingControllers settingControllers1 = hsp_catiaApp.SettingControllers;

            // Screenshot wird erstellt und gespeichert
            hsp_catiaApp.ActiveWindow.ActiveViewer.CaptureToFile(CatCaptureFormat.catCaptureFormatBMP, "C:\\Windows\\Temp\\" + bildname + ".bmp");

            // 3D Kompass eingeblendet
            hsp_catiaApp.StartCommand("CompassDisplayOn");

            // Setzt die Hintergrundfarbe auf Standart zurück
            hsp_catiaApp.ActiveWindow.ActiveViewer.PutBackgroundColor(arr1);
            
        }
    }
}
