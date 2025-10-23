using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Linq;
using System.Text;

namespace ParameterAccess
{
    /// <summary>
    /// Пример чтения параметров элемента
    /// Example of reading element parameters
    /// </summary>
    [Transaction(TransactionMode.ReadOnly)]
    public class ReadParametersCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;

                // Предлагаем пользователю выбрать элемент
                // Prompt user to select an element
                Reference reference = uiDoc.Selection.PickObject(
                    ObjectType.Element, 
                    "Выберите элемент / Select an element");

                Element element = doc.GetElement(reference);

                if (element == null)
                {
                    message = "Элемент не найден / Element not found";
                    return Result.Failed;
                }

                // Собираем информацию о параметрах
                // Collect parameter information
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Элемент / Element: {element.Name}");
                sb.AppendLine($"Категория / Category: {element.Category?.Name}");
                sb.AppendLine($"ID: {element.Id}");
                sb.AppendLine("\n--- Параметры / Parameters ---\n");

                foreach (Parameter param in element.Parameters)
                {
                    string paramName = param.Definition.Name;
                    string paramValue = GetParameterValue(param);
                    string paramType = param.StorageType.ToString();

                    sb.AppendLine($"{paramName}:");
                    sb.AppendLine($"  Значение / Value: {paramValue}");
                    sb.AppendLine($"  Тип / Type: {paramType}");
                    sb.AppendLine($"  Только чтение / Read-only: {param.IsReadOnly}");
                    sb.AppendLine();
                }

                TaskDialog dialog = new TaskDialog("Параметры элемента / Element Parameters");
                dialog.MainInstruction = "Информация о параметрах / Parameter Information";
                dialog.MainContent = sb.ToString();
                dialog.Show();

                return Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }

        private string GetParameterValue(Parameter param)
        {
            if (!param.HasValue)
                return "<нет значения / no value>";

            switch (param.StorageType)
            {
                case StorageType.Double:
                    return param.AsDouble().ToString("F3");
                case StorageType.Integer:
                    return param.AsInteger().ToString();
                case StorageType.String:
                    return param.AsString() ?? "<пусто / empty>";
                case StorageType.ElementId:
                    return param.AsElementId().IntegerValue.ToString();
                default:
                    return "<неизвестно / unknown>";
            }
        }
    }

    /// <summary>
    /// Пример записи параметров элемента
    /// Example of writing element parameters
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class WriteParametersCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;

                // Предлагаем пользователю выбрать элемент
                // Prompt user to select an element
                Reference reference = uiDoc.Selection.PickObject(
                    ObjectType.Element,
                    "Выберите элемент для изменения / Select an element to modify");

                Element element = doc.GetElement(reference);

                if (element == null)
                {
                    message = "Элемент не найден / Element not found";
                    return Result.Failed;
                }

                using (Transaction trans = new Transaction(doc, "Write Parameters"))
                {
                    trans.Start();

                    // Пример: изменение параметра "Comments" (Комментарии)
                    // Example: modifying the "Comments" parameter
                    Parameter commentsParam = element.LookupParameter("Comments");
                    
                    if (commentsParam != null && !commentsParam.IsReadOnly)
                    {
                        string newValue = $"Изменено через API / Modified via API - {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                        commentsParam.Set(newValue);
                        
                        trans.Commit();

                        TaskDialog.Show("Успех / Success",
                            $"Параметр 'Comments' обновлен!\n" +
                            $"Parameter 'Comments' updated!\n\n" +
                            $"Новое значение / New value:\n{newValue}");

                        return Result.Succeeded;
                    }
                    else
                    {
                        trans.RollBack();
                        TaskDialog.Show("Информация / Information",
                            "Параметр 'Comments' не найден или доступен только для чтения.\n" +
                            "Parameter 'Comments' not found or is read-only.");
                        return Result.Failed;
                    }
                }
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }

    /// <summary>
    /// Пример работы с пользовательскими параметрами
    /// Example of working with custom parameters
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class CustomParametersCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;

                using (Transaction trans = new Transaction(doc, "List Custom Parameters"))
                {
                    trans.Start();

                    // Получаем все параметры проекта
                    // Get all project parameters
                    BindingMap bindingMap = doc.ParameterBindings;
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Пользовательские параметры проекта / Custom Project Parameters:\n");

                    DefinitionBindingMapIterator iterator = bindingMap.ForwardIterator();
                    int count = 0;

                    while (iterator.MoveNext())
                    {
                        Definition definition = iterator.Key;
                        ElementBinding binding = iterator.Current as ElementBinding;

                        if (binding != null)
                        {
                            count++;
                            sb.AppendLine($"{count}. {definition.Name}");
                            sb.AppendLine($"   Тип / Type: {definition.ParameterType}");
                            sb.AppendLine($"   Группа / Group: {definition.ParameterGroup}");
                            sb.AppendLine();
                        }
                    }

                    trans.RollBack();

                    if (count == 0)
                    {
                        sb.AppendLine("Пользовательские параметры не найдены.");
                        sb.AppendLine("No custom parameters found.");
                    }

                    TaskDialog dialog = new TaskDialog("Пользовательские параметры / Custom Parameters");
                    dialog.MainInstruction = $"Найдено параметров / Parameters found: {count}";
                    dialog.MainContent = sb.ToString();
                    dialog.Show();

                    return Result.Succeeded;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
