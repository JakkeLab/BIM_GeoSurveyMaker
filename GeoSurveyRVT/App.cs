using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using GeoSurveyRVT.TabUI;
using GeoSurveyRVT.Model;
using System;
using System.Reflection;

namespace GeoSurveyRVT
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    
    public class App : IExternalApplication
    {
        public BoringSet boringSet = new BoringSet();
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                
                // Create a new Ribbon Tab
                string tabName = "GeoMaker";
                application.CreateRibbonTab(tabName);
                Assembly assembly = Assembly.GetExecutingAssembly();
                string assemblyPath = assembly.Location;

                // Create a new Ribbon Panel
                RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "My Panel");

                // Create a new Push Button
                PushButtonData openXMLBtnData = new PushButtonData("MyButton", "MyButton", assemblyPath, "GeoSurveyRVT.Commands.OpenXML");

                // Add the Push Button to the Ribbon Panel
                PushButton btnOpenXML = ribbonPanel.AddItem(openXMLBtnData) as PushButton;

                //Create Show Button
                PushButtonData showButtonData = new PushButtonData("Show Window", "Show", assemblyPath, "GeoSurveyRVT.Show");
                PushButton showButton = ribbonPanel.AddItem(showButtonData) as PushButton;

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
            MyDockablePane window = new MyDockablePane();

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
