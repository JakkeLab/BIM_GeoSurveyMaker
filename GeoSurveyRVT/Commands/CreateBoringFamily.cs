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
using System.Windows.Media.Media3D;
using Material = Autodesk.Revit.DB.Material;
using static Autodesk.Revit.DB.SpecTypeId;

namespace GeoSurveyRVT.Commands
{
    public class CreateBoringFamily : IExternalEventHandler
    {
        public static List<Boring> boringSet; 
        public void Execute(UIApplication uiApp)
        {
            //기본 설정
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Application app = uiApp.Application;
            Document doc = uiDoc.Document;

            foreach(var boring in boringSet)
            {
                try
                {
                    string familyTemplatePath = BoringSettingViewModel.Instance.TemplatePath;
                    Document familyDoc = app.NewFamilyDocument(familyTemplatePath);

                    using (var tr = new Transaction(familyDoc, "Create Solid Family"))
                    {
                        tr.Start();
                        CreateLayerPosts(familyDoc, boring);

                        //트랜잭션 시작
                        tr.Commit();
                    }

                    // Save the family to a temporary file and load it into the current document
                    string tempFamilyPath = System.IO.Path.GetTempPath() + $"{boring.BoringName}.rfa";
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
                    if (System.IO.File.Exists(tempFamilyPath))
                    {
                        try
                        {
                            System.IO.File.Delete(tempFamilyPath);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    //문서내에 배치
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

                                XYZ origin = new XYZ(boring.BoringLocation.X,
                                                     boring.BoringLocation.Y,
                                                     boring.TopLevel);
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
        }

        public string GetName()
        {
            return "CreateModelTextEventHandler";
        }

        /// <summary>
        /// 보링 시추공 한번에 생성
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="boring"></param>
        /// <returns></returns>
        public List<Form> CreateLayerPosts(Document doc, Boring boring)
        {
            var posts = new List<Form>();
            double start = 0;
            foreach (var layer in boring.GroundLayers)
            {
                var createdForm = CreateLayerPost(doc, start, layer.Depth, layer.LayerName, layer.Depth);
                start -= UnitUtils.Convert(layer.Depth, UnitTypeId.Meters, UnitTypeId.Feet);
                posts.Add(createdForm);
            }
            PlaceModelText(doc, 
                    $"보링이름_{boring.BoringName}", 
                    new XYZ(UnitUtils.Convert(550, UnitTypeId.Millimeters, UnitTypeId.Feet), 0,
                                                   UnitUtils.Convert(150, UnitTypeId.Millimeters, UnitTypeId.Feet)), 
                    boring.BoringName,
                    UnitUtils.Convert(600, UnitTypeId.Millimeters, UnitTypeId.Feet));

            PlaceModelText(doc, 
                    $"시추종료점 표시", 
                    new XYZ(UnitUtils.Convert(550, UnitTypeId.Millimeters, UnitTypeId.Feet), 0,
                                                   UnitUtils.Convert(-boring.GroundLayers.Sum(x => x.Depth) + 0.15, UnitTypeId.Meters, UnitTypeId.Feet)),
                                                          
                    $"시추종료 : {boring.GroundLayers.Sum(x => x.Depth).ToString("F2")}",
                    UnitUtils.Convert(400, UnitTypeId.Millimeters, UnitTypeId.Feet));

            //최하단 구분선 배치
            CreateModelCurveXAxis(doc, UnitUtils.Convert(-boring.GroundLayers.Sum(x => x.Depth), UnitTypeId.Meters, UnitTypeId.Feet), 5000, 500);

            //좌측 눈금선 배치
            double postLength = boring.GroundLayers.Select(x => x.Depth).Sum();
            var rulerEnd = (int)Math.Truncate(postLength / 0.25);
            for (int i = 0; i < rulerEnd; i++)
            {
                var zCoord = UnitUtils.Convert(-0.25 * i, UnitTypeId.Meters, UnitTypeId.Feet);
                var zCoordText = UnitUtils.Convert(-0.25 * i - 0.15, UnitTypeId.Meters, UnitTypeId.Feet);
                var rulerXCoord = UnitUtils.Convert(-3700, UnitTypeId.Millimeters, UnitTypeId.Feet);
                if(i != 0)
                {
                    //5의 배수가 아닌곳에서
                    if(i % 5 != 0)
                    {
                        CreateModelCurveXAxis(doc, zCoord, -500, -500);
                        
                    }
                    else
                    {
                        //5의배수인곳에서
                        if(i % 10 == 0)
                        {
                            //10의 배수이면
                            CreateModelCurveXAxis(doc, zCoord, -2000, -500);
                        }
                        else
                        {
                            //10의 배수는 아니면
                            CreateModelCurveXAxis(doc, zCoord, -1000, -500);
                        }
                        PlaceModelText(doc, $"레벨표시_{i*0.25}", new XYZ(rulerXCoord, 0, zCoordText), (i*0.25).ToString(), UnitUtils.Convert(300, UnitTypeId.Millimeters, UnitTypeId.Feet));
                    }
                }
                else
                {
                    //시작점에선 좌측으로 3500
                    CreateModelCurveXAxis(doc, zCoord, -3500, -500);
                }
            }
            CreateModelCurveXAxis(doc, UnitUtils.Convert(-boring.GroundLayers.Sum(x => x.Depth), UnitTypeId.Meters, UnitTypeId.Feet), -3500, -500);

            return posts;
        }
        /// <summary>
        /// 보링 시추공 층별로 생성
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="start"></param>
        /// <param name="height"></param>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public Form CreateLayerPost(Document doc, double start, double height, string layerName, double layerDepth)
        {
            Form layerForm = null;

            //프로파일 생성
            ReferenceArray baseArr = new ReferenceArray();
            XYZ baseCenter = new XYZ(0, 0, start);
            double radius = UnitUtils.Convert(500, UnitTypeId.Millimeters, UnitTypeId.Feet);
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
            XYZ heightNorm = new XYZ(0, 0, UnitUtils.Convert(height, UnitTypeId.Meters, UnitTypeId.Feet));
            layerForm = doc.FamilyCreate.NewExtrusionForm(true, baseArr, -heightNorm);
            var layerFormId = layerForm.Id;

            //재질 생성 및 색상 지정
            var mats = GetMatsOfDocument(doc);
            var matNames = mats.Select(x => x.Name);
            Material mat;
            if (matNames.Contains($"보링_{layerName}"))
            {
                mat = mats.Single(x => x.Name == $"보링_{layerName}");
            }
            else
            {
                var materialId = Material.Create(doc, $"보링_{layerName}");
                var layerColor = BoringSettingViewModel.Instance.LayerInfos.Single(x => x.LayerName == layerName).LayerColor;
                Color color = new Color(layerColor.R, layerColor.G, layerColor.B);
                mat = doc.GetElement(materialId) as Material;
                mat.Color = color;
            }

            //재질 입히기
            PaintToForm(layerForm, doc, mat);

            //주석 기준점
            double layerStart = UnitUtils.Convert(start, UnitTypeId.Feet, UnitTypeId.Millimeters);

            //레이어명 배치하기
            double zValueLayer = UnitUtils.Convert((layerStart - 750), UnitTypeId.Millimeters, UnitTypeId.Feet);
            PlaceModelText(doc, 
                    $"레이어명_{layerName}", 
                    new XYZ(UnitUtils.Convert(550, UnitTypeId.Millimeters, UnitTypeId.Feet), 0, zValueLayer),
                    layerName,
                    UnitUtils.Convert(600, UnitTypeId.Millimeters, UnitTypeId.Feet));
            
            //깊이 배치하기
            double zValueDepth = UnitUtils.Convert((layerStart - 1500), UnitTypeId.Millimeters, UnitTypeId.Feet);
            PlaceModelText(doc, 
                    $"심도_{layerName}", new XYZ(UnitUtils.Convert(550, UnitTypeId.Millimeters, UnitTypeId.Feet), 0, zValueDepth), 
                    $"심도 : {layerDepth.ToString("F2")}",
                    UnitUtils.Convert(400, UnitTypeId.Millimeters, UnitTypeId.Feet));

            //구분선 배치하기
            CreateModelCurveXAxis(doc, skplane.GetPlane().Origin.Z, 5000, 500);
            return layerForm;
        }

        /// <summary>
        /// Form 재질 입히기
        /// </summary>
        /// <param name="formElement"></param>
        /// <param name="doc"></param>
        /// <param name="mat"></param>
        public void PaintToForm(Form formElement, Document doc, Material mat)
        {
            try
            {
                var geomElement = formElement.get_Geometry(new Options());
                foreach (GeometryObject geoObj in geomElement)
                {
                    if (geoObj is Solid)
                    {
                        Solid solid = geoObj as Solid;
                        foreach (Face face in solid.Faces)
                        {
                            doc.Paint(formElement.Id, face, mat.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        /// <summary>
        /// Doc 내 모든 재질 리스트 반환
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public List<Material> GetMatsOfDocument(Document doc)
        {
            List<Material> mats = new List<Material>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            var matElmentCollection = collector.OfCategory(BuiltInCategory.OST_Materials);
            foreach (var elem in matElmentCollection)
            {
                Material mat = elem as Material;
                if(mat != null)
                {
                    mats.Add(mat);
                }
            }
            return mats;
        }
        /// <summary>
        /// Model Text 생성 및 배치
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="str"></param>
        public void PlaceModelText(Document doc, string str, XYZ placePoint, string textString, double textHeight)
        {
            //패밀리 템플릿 내에서 패밀리 이름이 TextOnXZ 인 패밀리 찾기
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_GenericModel);
            var elementIds = collector.ToElementIds();
            var sourceSymbol = elementIds
                .Select(elemId => doc.GetElement(elemId) as FamilySymbol)
                .FirstOrDefault(symbol => symbol != null && symbol.FamilyName == "TextOnXZ" && symbol.Name == "Default");

            if (sourceSymbol == null)
            {
                TaskDialog.Show("Error", "Default 유형을 찾을 수 없습니다.");
                return;
            }

            //유형을 복사하고 이름은 str로 하기
            FamilySymbol newSymbol = sourceSymbol.Duplicate(str) as FamilySymbol;

            //해당 유형의 매개변수중 TextString은 value로 지정하기
            Parameter textStringParam = newSymbol.LookupParameter("TextString");
            if (textStringParam != null)
            {
                textStringParam.Set(textString);
            }

            //해당 유형의 매개변수중 TextString은 value로 지정하기
            Parameter textHeightParam = newSymbol.LookupParameter("TextHeight");
            if (textHeightParam != null)
            {
                textHeightParam.Set(textHeight);
            }

            //doc 내에 placePoint에 놓기
            var instance = doc.FamilyCreate.NewFamilyInstance(placePoint, newSymbol, StructuralType.NonStructural);
        }

        /// <summary>
        /// X좌표가 xCoord(mm)인 지점에서 X축 방향으로 length만큼 연장한 라인 생성
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="zCoord"></param>
        /// <param name="length"></param>
        /// <param name="xCoord"></param>
        public void CreateModelCurveXAxis(Document doc, double zCoord, double length, double xCoord)
        {
            XYZ planeOrigin = new XYZ(0, 0, zCoord);
            Plane plane = Plane.CreateByNormalAndOrigin(XYZ.BasisZ, planeOrigin);
            SketchPlane skpPlane = SketchPlane.Create(doc, plane);

            double startX = UnitUtils.Convert(xCoord, UnitTypeId.Millimeters, UnitTypeId.Feet);
            XYZ pt1 = new XYZ(startX, 0, skpPlane.GetPlane().Origin.Z);
            double endX = UnitUtils.Convert(xCoord + length, UnitTypeId.Millimeters, UnitTypeId.Feet);
            XYZ pt2 = new XYZ(endX, 0, skpPlane.GetPlane().Origin.Z);

            Curve headerLine = Line.CreateBound(pt1, pt2);
            if (headerLine != null)
            {
                doc.FamilyCreate.NewModelCurve(headerLine, skpPlane);
            }
        }
    }
}
