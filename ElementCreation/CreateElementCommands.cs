using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace ElementCreation
{
    /// <summary>
    /// Пример создания стены в Revit
    /// Example of creating a wall in Revit
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CreateWallCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;

                // Начинаем транзакцию для создания элементов
                // Start transaction to create elements
                using (Transaction trans = new Transaction(doc, "Create Wall"))
                {
                    trans.Start();

                    // Получаем уровень для размещения стены
                    // Get a level for wall placement
                    Level level = new FilteredElementCollector(doc)
                        .OfClass(typeof(Level))
                        .FirstOrDefault() as Level;

                    if (level == null)
                    {
                        message = "Не найден уровень / No level found";
                        return Result.Failed;
                    }

                    // Создаем линию для стены
                    // Create a line for the wall
                    XYZ start = new XYZ(0, 0, 0);
                    XYZ end = new XYZ(10, 0, 0);
                    Line wallLine = Line.CreateBound(start, end);

                    // Создаем стену
                    // Create the wall
                    Wall wall = Wall.Create(doc, wallLine, level.Id, false);

                    trans.Commit();

                    TaskDialog.Show("Успех / Success", 
                        $"Стена создана!\nWall created!\n\n" +
                        $"ID: {wall.Id}\n" +
                        $"Уровень / Level: {level.Name}");

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

    /// <summary>
    /// Пример создания колонны в Revit
    /// Example of creating a column in Revit
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CreateColumnCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;

                using (Transaction trans = new Transaction(doc, "Create Column"))
                {
                    trans.Start();

                    // Получаем уровень
                    // Get level
                    Level level = new FilteredElementCollector(doc)
                        .OfClass(typeof(Level))
                        .FirstOrDefault() as Level;

                    if (level == null)
                    {
                        message = "Не найден уровень / No level found";
                        return Result.Failed;
                    }

                    // Получаем тип семейства колонны
                    // Get column family symbol
                    FamilySymbol columnType = new FilteredElementCollector(doc)
                        .OfClass(typeof(FamilySymbol))
                        .OfCategory(BuiltInCategory.OST_StructuralColumns)
                        .FirstOrDefault() as FamilySymbol;

                    if (columnType == null)
                    {
                        message = "Не найден тип колонны / No column type found";
                        return Result.Failed;
                    }

                    // Активируем тип, если необходимо
                    // Activate the type if necessary
                    if (!columnType.IsActive)
                    {
                        columnType.Activate();
                    }

                    // Создаем точку размещения
                    // Create placement point
                    XYZ point = new XYZ(5, 5, 0);

                    // Создаем колонну
                    // Create the column
                    FamilyInstance column = doc.Create.NewFamilyInstance(
                        point, 
                        columnType, 
                        level, 
                        StructuralType.Column);

                    trans.Commit();

                    TaskDialog.Show("Успех / Success", 
                        $"Колонна создана!\nColumn created!\n\n" +
                        $"ID: {column.Id}\n" +
                        $"Тип / Type: {columnType.Name}");

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
