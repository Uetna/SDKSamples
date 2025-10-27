# HelloRevit

## Описание / Description

Базовый пример, демонстрирующий создание простого аддона для Revit. Показывает информацию о текущем документе и версии Revit.

_Basic example demonstrating creation of a simple Revit add-in. Displays information about the current document and Revit version._

## Возможности / Features

- Базовая структура команды Revit API / Basic Revit API command structure
- Доступ к документу и приложению / Access to document and application
- Вывод информации через TaskDialog / Display information via TaskDialog

## Использование / Usage

1. Соберите проект / Build the project
2. Скопируйте `.dll` и `.addin` файлы в папку аддонов Revit / Copy `.dll` and `.addin` files to Revit add-ins folder
3. Запустите Revit / Start Revit
4. Выполните команду "Hello Revit" / Execute "Hello Revit" command

## Основные концепции / Key Concepts

- `IExternalCommand` - интерфейс для создания команды / interface for creating a command
- `Transaction` - управление транзакциями / transaction management
- `UIApplication` и `Document` - доступ к приложению и документу / access to application and document
- `TaskDialog` - диалоговые окна / dialog boxes
