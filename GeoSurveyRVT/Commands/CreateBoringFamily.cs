using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Structure;
using GeoSurveyRVT.Model;

namespace GeoSurveyRVT.Commands
{
    public class CreateBoringFamily : IExternalEventHandler
    {
        public static Boring boringData; 
        public void Execute(UIApplication uiApp)
        {
            //기본 설정
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Application app = uiApp.Application;
            Document doc = uiDoc.Document;

            try
            {
                //Onedrive에 포함되어 있기때문에 사용하는것
                //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                //string user;
                //if (userName.Contains("sjak3"))
                //{
                //    user = "sjak3";
                //}
                //else
                //{
                //    user = "jake";
                //}
                string familyPath = $@"C:\Users\sjak3\OneDrive\JAKK3 Lab\03_DEV\01_Project\05_BIM Addin\01_토목BIM\03_디지털 주상도 제작\Repo\_FamilyTemplate\BoringTemplate.rft";
                Document familyDoc = app.NewFamilyDocument(familyPath);
                using (var tr = new Transaction(familyDoc, "Create Solid Family"))
                {
                    tr.Start();

                    XYZ baseCenter = new XYZ(0, 0, 0);



                    double heightMilimeter = GetLength(boringData);
                    double widthMilimeter = 1000;
                    double height = UnitUtils.Convert(heightMilimeter, UnitTypeId.Millimeters, UnitTypeId.Feet); ;
                    double width = UnitUtils.Convert(widthMilimeter, UnitTypeId.Millimeters, UnitTypeId.Feet); ;

                    List<Curve> profile = new List<Curve>
                    {
                        Line.CreateBound(baseCenter.Add(new XYZ(-width / 2, -width / 2, 0)), baseCenter.Add(new XYZ(width / 2, -width / 2, 0))),
                        Line.CreateBound(baseCenter.Add(new XYZ(width / 2, -width / 2, 0)), baseCenter.Add(new XYZ(width / 2, width / 2, 0))),
                        Line.CreateBound(baseCenter.Add(new XYZ(width / 2, width / 2, 0)), baseCenter.Add(new XYZ(-width / 2, width / 2, 0))),
                        Line.CreateBound(baseCenter.Add(new XYZ(-width / 2, width / 2, 0)), baseCenter.Add(new XYZ(-width / 2, -width / 2, 0)))
                    };

                    CurveLoop baseProfile = CurveLoop.Create(profile);
                    SolidOptions options = new SolidOptions(ElementId.InvalidElementId, ElementId.InvalidElementId);
                    Solid solid = GeometryCreationUtilities.CreateExtrusionGeometry(new List<CurveLoop> { baseProfile }, new XYZ(0, 0, 1), height, options);

                    // Create a FreeForm element with the solid geometry
                    FreeFormElement.Create(familyDoc, solid);

                    tr.Commit();
                }


                // Save the family to a temporary file and load it into the current document
                string tempFamilyPath = System.IO.Path.GetTempPath() + "TestElement.rfa";
                familyDoc.SaveAs(tempFamilyPath);
                familyDoc.Close(false);

                Family family;
                using (Transaction trans = new Transaction(doc, "Load Family"))
                {
                    trans.Start();
                    doc.LoadFamily(tempFamilyPath, out family);
                    trans.Commit();
                }

                //임시파일 삭제
                if(System.IO.File.Exists(tempFamilyPath))
                {
                    try
                    {
                        System.IO.File.Delete(tempFamilyPath);
                    }
                    catch(Exception ex)
                    {

                    }
                }

                // Place the family instance at (0,0,0)
                using (Transaction trans = new Transaction(doc, "Place Family Instance"))
                {
                    trans.Start();

                    Level level = new FilteredElementCollector(doc)
                        .OfClass(typeof(Level))
                        .Cast<Level>()
                        .FirstOrDefault(lvl => lvl.Elevation == 0);

                    if (level != null)
                    {
                        var familyElementId = family.GetFamilySymbolIds().FirstOrDefault();
                        var symbol = doc.GetElement(familyElementId) as FamilySymbol;
                        if (symbol != null)
                        {
                            if (!symbol.IsActive) symbol.Activate();

                            XYZ origin = new XYZ(0, 0, 0);
                            FamilyInstance instance = doc.Create.NewFamilyInstance(origin, symbol, level, StructuralType.NonStructural);
                        }
                    }

                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetName()
        {
            return "CreateModelTextEventHandler";
        }

        public double GetLength(Boring boring)
        {
            return boring.GroundLayers.First().Depth;
        }
    }
}
