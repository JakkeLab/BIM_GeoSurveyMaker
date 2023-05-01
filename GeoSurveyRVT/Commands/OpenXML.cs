using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GeoSurveyRVT.DockablePaneUI;
using GeoSurveyRVT.Model;
using GeoSurveyRVT.Model.XMLParser;
using GeoSurveyRVT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GeoSurveyRVT.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class OpenXML : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            BoringSetImport();
            App.MainUIApplication = commandData.Application;
            return Result.Succeeded;
        }

        public event EventHandler UIApplicationLoaded;

        public void BoringSetImport()
        {
            XMLParser xmlParser = new XMLParser();
            var loadedBorings = xmlParser.XMLRead();
            var duplicateBorings = new List<Boring>();
            foreach (var boring in loadedBorings)
            {
                //중복되지 않는것만 우선 추가
                if (BoringViewModel.Instance.ImportedBoringSet.Borings.FirstOrDefault(x => x.BoringName == boring.BoringName) == null)
                {
                    BoringViewModel.Instance.ImportedBoringSet.AddBoring(boring);
                }
                else
                {
                    duplicateBorings.Add(boring);
                }
            }

            //중복된것 있을 시 덮어쓰기, 취소 확인하기
            if (duplicateBorings.Count != 0)
            {
                object[] duplicateBoringNames = duplicateBorings.Select(x => x.BoringName).ToArray();
                string duplicatesToShow = string.Join("\n", duplicateBoringNames);

                if (duplicateBoringNames.Length > 5)
                {
                    duplicatesToShow += "\n...";
                }
                var msgBoxResult = MessageBox.Show($"{duplicateBorings.Count}개의 보링이 이미 있습니다\n덮어쓰시겠습니까?\n중복 항목 : \n{duplicatesToShow}", "보링 추가", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (msgBoxResult == MessageBoxResult.Yes)
                {
                    foreach (var item in duplicateBorings)
                    {
                        var oldItem = BoringViewModel.Instance.ImportedBoringSet.Borings.Single(x => x.BoringName == item.BoringName);
                        int oldIndex = BoringViewModel.Instance.ImportedBoringSet.Borings.IndexOf(oldItem);
                        BoringViewModel.Instance.ImportedBoringSet.UpdateBoring(oldIndex, item);
                    }
                }
            }
        }
    }
}
