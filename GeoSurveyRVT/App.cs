using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using GeoSurveyRVT.Commands;
using GeoSurveyRVT.DockablePaneUI;
using GeoSurveyRVT.Model;
using GeoSurveyRVT.Model.XMLParser;
using GeoSurveyRVT.ViewModel;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace GeoSurveyRVT
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    
    public class App : IExternalApplication
    {
        public static UIApplication MainUIApplication;
        
        public Result OnStartup(UIControlledApplication application)
        {
            
            try
            {
                //리본 탭
                string tabName = "GeoMaker";
                application.CreateRibbonTab(tabName);
                Assembly assembly = Assembly.GetExecutingAssembly();
                string assemblyPath = assembly.Location;

                //리본 패널
                RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "My Panel");

                //XML열기 버튼
                PushButtonData openXMLButtonData = new PushButtonData("Import XML", "Import\nXML", assemblyPath, "GeoSurveyRVT.Commands.OpenXML");
                PushButton openXMLButton = ribbonPanel.AddItem(openXMLButtonData) as PushButton;

                //대화상자 열기 버튼
                PushButtonData showButtonData = new PushButtonData("Show Window", "Show", assemblyPath, "GeoSurveyRVT.Show");
                PushButton showButton = ribbonPanel.AddItem(showButtonData) as PushButton;

                //설정창 버튼
                PushButtonData settingButtonData = new PushButtonData("Boring Setting", "Setting", assemblyPath, "GeoSurveyRVT.Commands.BoringSetting");
                PushButton settingButton = ribbonPanel.AddItem(settingButtonData) as PushButton;

                //register dockablepane
                RegisterDockablePane(application);

                // return result
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                return Result.Failed;
            }
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result RegisterDockablePane(UIControlledApplication application)
        {
            // dockablepaneviewer (customcontrol)
            ShowBoringTable window = new ShowBoringTable();

            // register in application with a new guid
            DockablePaneId dockID = new DockablePaneId(new Guid("{2ab6b447-1be8-4439-bbd1-c3e9d4037d64}"));
            try
            {
                application.RegisterDockablePane(dockID, "Dock Pane Sample",
                    window as IDockablePaneProvider);

            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error Message", ex.Message);
                return Result.Failed;
            }
            return Result.Succeeded;
        }
    }

    public class CommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication app, CategorySet cate)
        {
            if (app.ActiveUIDocument == null)
            {
                //disable register btn
                return true;
            }
            //enable register btn
            return false;
        }
    }

    // external command class
    [Transaction(TransactionMode.Manual)]
    public class Show : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {

                // dockable window id
                DockablePaneId id = new DockablePaneId(new Guid("{2ab6b447-1be8-4439-bbd1-c3e9d4037d64}"));
                DockablePane dockableWindow = commandData.Application.GetDockablePane(id);
                dockableWindow.Show();
            }
            catch (Exception ex)
            {
                // show error info dialog
                TaskDialog.Show("Info Message", ex.Message);
            }
            // return result
            return Result.Succeeded;
        }
    }
}
