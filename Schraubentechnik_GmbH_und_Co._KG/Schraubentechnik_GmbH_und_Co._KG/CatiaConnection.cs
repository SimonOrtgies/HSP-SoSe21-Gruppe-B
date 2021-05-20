using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MECMOD;
using INFITF;
using PARTITF;
using System.Windows;

namespace Schraubentechnik_GmbH_und_Co._KG
{
    class CatiaConnection
    {
        INFITF.Application hsp_catiaApp;
        MECMOD.PartDocument hsp_catiaPart;
        MECMOD.Sketch hsp_catiaProfil;
HybridBody catHybridBody1;

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
                    "Ein PART manuell erzeugen und ein darauf achten, dass 'Geometisches Set' aktiviert ist.",
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

            // Rechteck erzeugen

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

            // Block(Balken) erzeugen
            ShapeFactory catShapeFactory1 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;
            Pad catPad1 = catShapeFactory1.AddNewPad(hsp_catiaProfil, l);

            // Block umbenennen
            catPad1.set_Name("Schaft");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }
        #endregion

        #region Kopf
        public void ErzeugeKopf(MetrischeGewindegroesse m, String sk)
        {
            sk = "Sechskant";

            if(sk == "Sechskant")
            {
                Sechskant(m);
            }else if (sk == "Zylinderkopf mit Schlitz")
            {

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
            hsp_catiaProfil.set_Name("Schraubenkopf");
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
            #endregion

            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

            // Sechskant erzeugen
            double tan30 = Math.Sqrt(3)/3;
            double cos30 = Math.Sqrt(3)/2;
            //double mSW = m.schluesselweite / 2;
            double mSW = 16;

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

            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();


            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            // Block(Balken) erzeugen
            ShapeFactory catShapeFactory2 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;
            //Pad catPad2 = catShapeFactory2.AddNewPad(hsp_catiaProfil, m.mutterhoehe);
            Pad catPad2 = catShapeFactory2.AddNewPad(hsp_catiaProfil, 12);

            // Block umbenennen
            catPad2.set_Name("Kopf");

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }
        #endregion

        public void ErzeugeFase()
        {
            ShapeFactory catshapeFactoryFase = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;
            // var reference1 = (Reference)hsp_catiaPart.Part.CreateReferenceFromBRepName("REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;1)));None:();Cf11:());Face:(Brp:(Pad.1;2);None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)");
            var reference1 = (Reference)hsp_catiaPart.Part.CreateReferenceFromName("");

            Chamfer catChamfer1 = catshapeFactoryFase.AddNewChamfer(reference1, CatChamferPropagation.catTangencyChamfer, CatChamferMode.catLengthAngleChamfer, CatChamferOrientation.catNoReverseChamfer, 1, 45);

           // Set reference1 = part1.CreateReferenceFromName("");



          // Set chamfer1 = shapeFactory1.AddNewChamfer(reference1, catTangencyChamfer, catLengthAngleChamfer, catNoReverseChamfer, 1.000000, 45.000000);
        }


    }
}
