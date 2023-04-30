using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GeoSurveyRVT.Model;
using GeoSurveyRVT.Model.XMLParser;
using GeoSurveyRVT.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeoSurveyRVT.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class OpenXML : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            XMLParser xmlParser = new XMLParser();
            var loadedBorings = xmlParser.XMLRead();
            var duplicateBorings = new List<Boring>();
            var container = Container.BoringSetContainer;
            foreach (var boring in loadedBorings)
            {
                //중복되지 않는것만 우선 추가
                if (container.Borings.Find(x => x.BoringName == boring.BoringName) == null)
                {
                    container.AddBoring(boring);
                }
                else
                {
                     duplicateBorings.Add(boring);
                }
            }
            
            //중복된것 있을 시 덮어쓰기, 취소 확인하기
            if(duplicateBorings.Count != 0)
            {
                object[] duplicateBoringNames = duplicateBorings.Select(x => x.BoringName).ToArray();
                string duplicatesToShow = string.Join("\n", duplicateBoringNames);

                if (duplicateBoringNames.Length > 5)
                {
                    duplicatesToShow += "\n...";
                }
                var msgBoxResult = MessageBox.Show($"{duplicateBorings.Count}개의 보링이 이미 있습니다\n덮어쓰시겠습니까?\n중복 항목 : \n{duplicatesToShow}", "보링 추가", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(msgBoxResult == MessageBoxResult.Yes)
                {
                    foreach (var item in duplicateBorings)
                    {
                        int oldIndex = container.Borings.FindIndex(x => x.BoringName == item.BoringName);
                        container.UpdateBoring(oldIndex, item);
                    }
                }
            }
            return Result.Succeeded;
        }
    }
}
