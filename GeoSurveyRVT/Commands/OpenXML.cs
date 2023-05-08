using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GeoSurveyRVT.DockablePaneUI;
using GeoSurveyRVT.Model;
using GeoSurveyRVT.Model.XMLParser;
using GeoSurveyRVT.UIViewModel;
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
            BoringViewModel.Instance.ImportedBoringSet.Borings.Clear();
            foreach (var boring in loadedBorings)
            {
                BoringViewModel.Instance.ImportedBoringSet.AddBoring(boring);
            }
            BoringViewModel.Instance.LoadBoringNames();
            BoringSettingViewModel.Instance.LayerInfos.Clear();
        }
    }
}
