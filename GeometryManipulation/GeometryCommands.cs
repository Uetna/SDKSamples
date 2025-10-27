using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Linq;
using System.Text;

namespace GeometryManipulation
{
    /// <summary>
    /// Пример анализа геометрии элемента
    /// Example of analyzing element geometry
    /// </summary>
    [Transaction(TransactionMode.ReadOnly)]
    public class AnalyzeGeometryCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;

                // Выбираем элемент
                // Select element
                Reference reference = uiDoc.Selection.PickObject(
                    ObjectType.Element,
                    "Выберите элемент для анализа геометрии / Select element to analyze geometry");

                Element element = doc.GetElement(reference);

                if (element == null)
                {
                    message = "Элемент не найден / Element not found";
                    return Result.Failed;
                }

                // Получаем геометрию элемента
                // Get element geometry
                Options geoOptions = new Options
                {
                    ComputeReferences = true,
                    DetailLevel = ViewDetailLevel.Fine
                };

                GeometryElement geometryElement = element.get_Geometry(geoOptions);

                if (geometryElement == null)
                {
                    message = "Геометрия не найдена / Geometry not found";
                    return Result.Failed;
                }

                // Анализируем геометрию
                // Analyze geometry
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Элемент / Element: {element.Name}");
                sb.AppendLine($"Категория / Category: {element.Category?.Name}");
                sb.AppendLine($"ID: {element.Id}\n");
                sb.AppendLine("--- Геометрия / Geometry ---\n");

                int solidCount = 0;
                int instanceCount = 0;
                int curveCount = 0;
                double totalVolume = 0;

                foreach (GeometryObject geoObj in geometryElement)
                {
                    if (geoObj is Solid solid)
                    {
                        if (solid.Volume > 0)
                        {
                            solidCount++;
                            totalVolume += solid.Volume;
                            sb.AppendLine($"Solid #{solidCount}:");
                            sb.AppendLine($"  Объем / Volume: {solid.Volume:F3} куб.фут / cu.ft");
                            sb.AppendLine($"  Площадь / Surface Area: {solid.SurfaceArea:F3} кв.фут / sq.ft");
                            sb.AppendLine($"  Граней / Faces: {solid.Faces.Size}");
                            sb.AppendLine($"  Ребер / Edges: {solid.Edges.Size}");
                            sb.AppendLine();
                        }
                    }
                    else if (geoObj is GeometryInstance instance)
                    {
                        instanceCount++;
                    }
                    else if (geoObj is Curve)
                    {
                        curveCount++;
                    }
                }

                sb.AppendLine($"\nИтого / Summary:");
                sb.AppendLine($"Solid объектов / Solid objects: {solidCount}");
                sb.AppendLine($"Экземпляров / Instances: {instanceCount}");
                sb.AppendLine($"Кривых / Curves: {curveCount}");
                sb.AppendLine($"Общий объем / Total volume: {totalVolume:F3} куб.фут / cu.ft");

                TaskDialog dialog = new TaskDialog("Анализ геометрии / Geometry Analysis");
                dialog.MainInstruction = "Информация о геометрии / Geometry Information";
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
    }

    /// <summary>
    /// Пример получения граней элемента
    /// Example of getting element faces
    /// </summary>
    [Transaction(TransactionMode.ReadOnly)]
    public class GetFacesCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;

                Reference reference = uiDoc.Selection.PickObject(
                    ObjectType.Element,
                    "Выберите элемент / Select element");

                Element element = doc.GetElement(reference);

                Options geoOptions = new Options
                {
                    ComputeReferences = true,
                    DetailLevel = ViewDetailLevel.Fine
                };

                GeometryElement geometryElement = element.get_Geometry(geoOptions);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Элемент / Element: {element.Name}\n");
                sb.AppendLine("--- Грани / Faces ---\n");

                int faceCount = 0;

                foreach (GeometryObject geoObj in geometryElement)
                {
                    if (geoObj is Solid solid && solid.Volume > 0)
                    {
                        foreach (Face face in solid.Faces)
                        {
                            faceCount++;
                            sb.AppendLine($"Грань / Face #{faceCount}:");
                            sb.AppendLine($"  Площадь / Area: {face.Area:F3} кв.фут / sq.ft");
                            
                            // Получаем нормаль в центре грани
                            // Get normal at face center
                            UV centerUV = new UV(0.5, 0.5);
                            try
                            {
                                XYZ normal = face.ComputeNormal(centerUV);
                                sb.AppendLine($"  Нормаль / Normal: ({normal.X:F3}, {normal.Y:F3}, {normal.Z:F3})");
                            }
                            catch
                            {
                                sb.AppendLine($"  Нормаль / Normal: <не удалось вычислить / cannot compute>");
                            }
                            
                            sb.AppendLine();
                        }
                    }
                }

                sb.AppendLine($"\nВсего граней / Total faces: {faceCount}");

                TaskDialog dialog = new TaskDialog("Грани элемента / Element Faces");
                dialog.MainInstruction = $"Найдено граней / Faces found: {faceCount}";
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
    }

    /// <summary>
    /// Пример работы с BoundingBox
    /// Example of working with BoundingBox
    /// </summary>
    [Transaction(TransactionMode.ReadOnly)]
    public class GetBoundingBoxCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uiDoc = commandData.Application.ActiveUIDocument;
                Document doc = uiDoc.Document;

                Reference reference = uiDoc.Selection.PickObject(
                    ObjectType.Element,
                    "Выберите элемент / Select element");

                Element element = doc.GetElement(reference);

                // Получаем BoundingBox элемента
                // Get element BoundingBox
                BoundingBoxXYZ bbox = element.get_BoundingBox(null);

                if (bbox == null)
                {
                    message = "BoundingBox не найден / BoundingBox not found";
                    return Result.Failed;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Элемент / Element: {element.Name}\n");
                sb.AppendLine("--- BoundingBox ---\n");
                sb.AppendLine($"Минимальная точка / Min point:");
                sb.AppendLine($"  X: {bbox.Min.X:F3}");
                sb.AppendLine($"  Y: {bbox.Min.Y:F3}");
                sb.AppendLine($"  Z: {bbox.Min.Z:F3}");
                sb.AppendLine();
                sb.AppendLine($"Максимальная точка / Max point:");
                sb.AppendLine($"  X: {bbox.Max.X:F3}");
                sb.AppendLine($"  Y: {bbox.Max.Y:F3}");
                sb.AppendLine($"  Z: {bbox.Max.Z:F3}");
                sb.AppendLine();

                double width = bbox.Max.X - bbox.Min.X;
                double depth = bbox.Max.Y - bbox.Min.Y;
                double height = bbox.Max.Z - bbox.Min.Z;

                sb.AppendLine($"Размеры / Dimensions:");
                sb.AppendLine($"  Ширина / Width: {width:F3}");
                sb.AppendLine($"  Глубина / Depth: {depth:F3}");
                sb.AppendLine($"  Высота / Height: {height:F3}");

                TaskDialog.Show("BoundingBox", sb.ToString());

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
    }
}
