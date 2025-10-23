# ElementCreation

## Описание / Description

Пример создания различных элементов модели Revit (стены, колонны и другие элементы).

_Example of creating various Revit model elements (walls, columns, and other elements)._

## Возможности / Features

- Создание стен / Wall creation
- Создание колонн / Column creation  
- Работа с уровнями / Working with levels
- Использование типов семейств / Using family types
- Управление транзакциями / Transaction management

## Примеры команд / Example Commands

### CreateWallCommand
Создает простую стену на первом доступном уровне.

_Creates a simple wall on the first available level._

### CreateColumnCommand
Создает структурную колонну, используя первый доступный тип колонны.

_Creates a structural column using the first available column type._

## Основные концепции / Key Concepts

- `Transaction` - управление изменениями в документе / managing changes to the document
- `FilteredElementCollector` - поиск элементов в документе / finding elements in the document
- `Level` - уровни в проекте / levels in the project
- `FamilySymbol` - типы семейств / family types
- `Wall.Create()` - создание стен / creating walls
- `NewFamilyInstance()` - создание экземпляров семейств / creating family instances

## Использование / Usage

1. Откройте проект Revit / Open a Revit project
2. Убедитесь, что в проекте есть уровни / Ensure the project has levels
3. Выполните команду создания элемента / Execute an element creation command
4. Элемент будет создан в модели / The element will be created in the model
