# GeometryManipulation

## Описание / Description

Пример работы с геометрией элементов Revit.

_Example of working with Revit element geometry._

## Возможности / Features

- Анализ геометрии элементов / Element geometry analysis
- Работа с Solid объектами / Working with Solid objects
- Получение граней и их свойств / Getting faces and their properties
- Работа с BoundingBox / Working with BoundingBox
- Вычисление объемов и площадей / Computing volumes and areas
- Получение нормалей поверхностей / Getting surface normals

## Примеры команд / Example Commands

### AnalyzeGeometryCommand
Анализирует геометрию выбранного элемента, показывает объем, площадь поверхности, количество граней и ребер.

_Analyzes geometry of the selected element, shows volume, surface area, number of faces and edges._

### GetFacesCommand
Получает все грани элемента и их свойства (площадь, нормали).

_Gets all element faces and their properties (area, normals)._

### GetBoundingBoxCommand
Получает BoundingBox элемента и вычисляет его размеры.

_Gets element BoundingBox and computes its dimensions._

## Основные концепции / Key Concepts

- `GeometryElement` - контейнер геометрии элемента / element geometry container
- `Solid` - твердотельная геометрия / solid geometry
- `Face` - грань геометрии / geometry face
- `Edge` - ребро геометрии / geometry edge
- `BoundingBoxXYZ` - ограничивающий параллелепипед / bounding box
- `Options` - настройки получения геометрии / geometry options
- `ComputeReferences` - вычисление ссылок на геометрию / computing geometry references
- `DetailLevel` - уровень детализации / level of detail

## Геометрические типы / Geometry Types

- `Solid` - твердое тело с объемом / solid with volume
- `GeometryInstance` - экземпляр геометрии / geometry instance
- `Curve` - кривая / curve
- `Mesh` - сетка / mesh

## Использование / Usage

1. Откройте проект Revit с 3D элементами / Open a Revit project with 3D elements
2. Запустите команду / Run the command
3. Выберите элемент для анализа / Select an element to analyze
4. Просмотрите геометрическую информацию / View geometric information
