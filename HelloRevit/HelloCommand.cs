using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace HelloRevit
{
    /// <summary>
    /// Базовый пример команды Revit API
    /// Basic Revit API command example
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class HelloCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Получаем доступ к приложению и документу Revit
                // Get access to Revit application and document
                UIApplication uiApp = commandData.Application;
                UIDocument uiDoc = uiApp.ActiveUIDocument;
                Document doc = uiDoc.Document;

                // Выводим приветственное сообщение
                // Display welcome message
                string msg = $"Привет, Revit API!\n" +
                             $"Hello, Revit API!\n\n" +
                             $"Текущий документ / Current document: {doc.Title}\n" +
                             $"Путь к файлу / File path: {doc.PathName}\n" +
                             $"Версия Revit / Revit version: {uiApp.Application.VersionNumber}";

                TaskDialog.Show("Hello Revit", msg);

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
