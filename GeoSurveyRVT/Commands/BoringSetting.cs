using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GeoSurveyRVT.RibbonUIForm.BoringSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSurveyRVT.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class BoringSetting : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            BoringSettingUI boringSettingUI = new BoringSettingUI();

            boringSettingUI.ShowDialog();

            return Result.Succeeded;
        }
    }
}
