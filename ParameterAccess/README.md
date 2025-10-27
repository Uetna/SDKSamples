# ParameterAccess

## Описание / Description

Пример чтения и записи параметров элементов Revit.

_Example of reading and writing Revit element parameters._

## Возможности / Features

- Чтение всех параметров элемента / Reading all element parameters
- Запись значений параметров / Writing parameter values
- Работа с пользовательскими параметрами / Working with custom parameters
- Обработка различных типов параметров / Handling different parameter types
- Интерактивный выбор элементов / Interactive element selection

## Примеры команд / Example Commands

### ReadParametersCommand
Читает и отображает все параметры выбранного элемента.

_Reads and displays all parameters of the selected element._

### WriteParametersCommand
Изменяет параметр "Comments" выбранного элемента.

_Modifies the "Comments" parameter of the selected element._

### CustomParametersCommand
Отображает список всех пользовательских параметров проекта.

_Displays a list of all custom project parameters._

## Основные концепции / Key Concepts

- `Parameter` - доступ к параметрам элемента / accessing element parameters
- `Parameter.StorageType` - типы хранения данных параметров / parameter data storage types
- `Parameter.Set()` - установка значения параметра / setting parameter value
- `LookupParameter()` - поиск параметра по имени / finding parameter by name
- `ParameterBindings` - параметры проекта / project parameters
- `Selection.PickObject()` - интерактивный выбор элементов / interactive element selection

## Типы параметров / Parameter Types

- `String` - текстовые значения / text values
- `Double` - числовые значения с плавающей точкой / floating point numbers
- `Integer` - целые числа / integers
- `ElementId` - ссылки на элементы / element references

## Использование / Usage

1. Откройте проект Revit с элементами / Open a Revit project with elements
2. Запустите команду / Run the command
3. Выберите элемент в модели / Select an element in the model
4. Просмотрите или измените параметры / View or modify parameters
