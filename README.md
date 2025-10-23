# Revit SDK Samples

**Примеры кода для Revit API (C#, .NET Framework 4.8/.NET 8)**

Этот репозиторий содержит примеры проектов для Autodesk Revit API на платформе .NET. Каждый проект демонстрирует отдельную задачу или концепцию работы с Revit API.

---

**Sample Code for Revit API (C#, .NET Framework 4.8/.NET 8)**

This repository contains sample projects for Autodesk Revit API on the .NET platform. Each project demonstrates a separate task or concept of working with Revit API.

## Требования / Requirements

- Autodesk Revit 2024 или новее / Autodesk Revit 2024 or newer
- Visual Studio 2022 или новее / Visual Studio 2022 or newer
- .NET Framework 4.8 или .NET 8 / .NET Framework 4.8 or .NET 8
- Revit API SDK

## Структура проекта / Project Structure

```
SDKSamples/
├── HelloRevit/              - Базовый пример загрузки аддона / Basic add-in loading example
├── ElementCreation/         - Создание элементов / Element creation
├── ParameterAccess/         - Чтение и запись параметров / Reading and writing parameters
├── GeometryManipulation/    - Работа с геометрией / Geometry manipulation
└── README.md
```

## Примеры / Examples

### 1. HelloRevit
Базовый пример, демонстрирующий создание простого аддона для Revit.
_Basic example demonstrating creation of a simple Revit add-in._

### 2. ElementCreation
Пример создания различных элементов модели Revit (стены, двери, окна и т.д.).
_Example of creating various Revit model elements (walls, doors, windows, etc.)._

### 3. ParameterAccess
Пример чтения и записи параметров элементов.
_Example of reading and writing element parameters._

### 4. GeometryManipulation
Пример работы с геометрией элементов Revit.
_Example of working with Revit element geometry._

## Как использовать / How to Use

### Сборка проектов / Building Projects

1. Откройте решение `RevitSamples.sln` в Visual Studio
2. Восстановите NuGet пакеты
3. Соберите решение (Build → Build Solution)

_1. Open the solution `RevitSamples.sln` in Visual Studio_
_2. Restore NuGet packages_
_3. Build the solution (Build → Build Solution)_

### Загрузка в Revit / Loading in Revit

1. Скопируйте скомпилированные `.dll` и `.addin` файлы в папку аддонов Revit:
   - `%APPDATA%\Autodesk\Revit\Addins\2024\`
2. Запустите Revit
3. Найдите аддон в меню или на ленте

_1. Copy compiled `.dll` and `.addin` files to Revit add-ins folder:_
   _- `%APPDATA%\Autodesk\Revit\Addins\2024\`_
_2. Start Revit_
_3. Find the add-in in menu or ribbon_

## Документация / Documentation

- [Revit API Documentation](https://www.revitapidocs.com/)
- [Revit API Developer Guide](https://help.autodesk.com/view/RVT/2024/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html)
- [The Building Coder Blog](https://thebuildingcoder.typepad.com/)

## Лицензия / License

Примеры предоставлены "как есть" для образовательных целей.
_Examples are provided "as is" for educational purposes._

## Вклад / Contributing

Приветствуются предложения и улучшения!
_Suggestions and improvements are welcome!_
