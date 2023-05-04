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
using FamilyFac = Autodesk.Revit.Creation;
using GeoSurveyRVT.UIViewModel;

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
                string familyTemplatePath = $@"C:\Users\Jake\OneDrive\JAKK3 Lab\03_DEV\01_Project\05_BIM Addin\01_토목BIM\03_디지털 주상도 제작\Repo\_FamilyTemplate\BoringTemplate.rft";
                Document familyDoc = app.NewFamilyDocument(familyTemplatePath);

                using (var tr = new Transaction(familyDoc, "Create Solid Family"))
                {
                    tr.Start();
                    double start = 0;
                    foreach(var layer in boringData.GroundLayers)
                    {
                        double depth = layer.Depth;
                        CreateLayerPost(familyDoc, start, depth, layer.LayerName);
                        start -= depth;
                    }

                    //트랜잭션 시작
                    tr.Commit();
                }

                // Save the family to a temporary file and load it into the current document
                string tempFamilyPath = System.IO.Path.GetTempPath() + $"{boringData.BoringName}.rfa";
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

        public Form CreateLayerPost(Autodesk.Revit.DB.Document doc, double start, double height, string layerName)
        {
            Form layerForm = null;

            //프로파일 생성
            ReferenceArray baseArr = new ReferenceArray();
            XYZ baseCenter = new XYZ(0, 0, start);
            double radius = UnitUtils.Convert(1000, UnitTypeId.Millimeters, UnitTypeId.Feet);
            double startAngle = 0;
            double endAngle = 2 * Math.PI;
            XYZ xAis = XYZ.BasisX;
            XYZ yAis = XYZ.BasisY;
            XYZ norm = XYZ.BasisZ;
            Arc boringBase = Arc.Create(baseCenter, radius, startAngle, endAngle, xAis, yAis);
            Plane plane = Plane.CreateByNormalAndOrigin(norm, baseCenter);
            SketchPlane skplane = SketchPlane.Create(doc, plane);
            ModelCurve modelArc = doc.FamilyCreate.NewModelCurve(boringBase, skplane);

            //베이스 생성
            baseArr.Append(modelArc.GeometryCurve.Reference);

            //Extrusion 생성
            XYZ heightNorm = new XYZ(0, 0, height);
            layerForm = doc.FamilyCreate.NewExtrusionForm(true, baseArr, -heightNorm);
            var layerFormId = layerForm.Id;

            
            //재질 생성 및 색상 지정
            var materialId = Material.Create(doc, $"보링_{layerName}");
            var layerColor = BoringSettingViewModel.Instance.LayerInfos.Single(x => x.LayerName == layerName).LayerColor;
            Color color = new Color (layerColor.R, layerColor.G, layerColor.B);
            Material material = doc.GetElement(materialId) as Material;
            material.Color = color;

            //재질 입히기
            GeometryElement geoElem = layerForm.get_Geometry(new Options());
            foreach (GeometryObject geoObj in geoElem)
            {
                if (geoObj is Solid)
                {
                    Solid solid = geoObj as Solid;
                    foreach (Face face in solid.Faces)
                    {
                        doc.Paint(layerFormId, face, materialId);
                    }
                }
            }
            return layerForm;
        }
    }
}
