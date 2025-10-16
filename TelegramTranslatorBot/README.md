# Telegram Translator Bot
---
- Тг бот который переводит текст между языками с помощью **LibreTranslate** и **MyMemory API**
---
- Tg bot that translates text between languages using **LibreTranslate** and **MyMemory API**
---

## Что в боте
- Перевод текста между языками
- Выбор целевого языка
- Автоматическое определение исходного языка  
- Поддержка нескольких API для перевода

---

## What's in the bot
- Text translation between languages
- Target language selection
- Automatic source language detection  
- Support for multiple translation APIs

---

## NuGet Packages
- dotnet add package Telegram.Bot
- dotnet add package Microsoft.Extensions.Hosting
- dotnet add package Microsoft.Extensions.Configuration
- dotnet add package Microsoft.Extensions.Configuration.Json
- dotnet add package Microsoft.Extensions.DependencyInjection
- dotnet add package Newtonsoft.Json
- dotnet add package RestSharp

---

## Как запустить
- Склонируйте репозиторий
- Измените `appsettings.json`, указав токен вашего бота и URL LibreTranslate (если используете)
- Запустите проект
- Напиши `/start` в боте
- Есть кнопки "Перевести текст" и "Выбрать язык"
- Выберите язык для начала(это язык на который вы будете переводить)
- Нажмите на Перевести текст и выберите нужный язык с которого хотите перевести
- Введите текст для перевода
- Бот вернет переведенный текст

---

## How to launch
- Clone the repository
- Change the `appsettings.json`, specifying your bot's token and the LibreTranslate URL (if using it)
, Launch the project
- Write `/start` in the bot
- There are buttons for "Translate text" and "Select language"
- Select the language to start (this is the language you will translate into)
- Click on Translate text and select the desired language from which you want to translate
- Enter the text to translate
- The bot will return the translated text.

---

## Архитектура

- Handlers - обработчики команд и сообщений
- CommandHandler.cs - показывает меню и варианты выбора языка, работает с KeyboardHelper
---
- KeyboardHelper.cs - UI клавиатура для выбора языка и команд
---
- MessageHandler.cs:
- Обрабатывает обычные текстовые сообщения, не являющиеся командами
- Определяет, что пользователь хочет сделать (например, перевести текст)
- Обращается к TranslatorService для получения результата перевода
- Хранит текущий контекст пользователя через UserState
---
- Models - содержит простые классы для хранения и передачи данных
- Language.cs - представляет язык перевода и используется для создания меню выбора языка
---
- UserState.cs - отвечает за хранение текущего состояния пользователя: выбранный язык, этап диалога, контекст последней команды
---
- Services - содержит логику для взаимодействия с внешними API
- BotService.cs - настраивает обработку обновлений,  передаёт сообщения в CommandHandler и MessageHandler
---
- TranslatorService.cs - логика перевода текста, работает через LibreTranslate и MyMemory API, возвращает результ в MessageHandler
---
- Program.cs - настраивает зависимости (IConfiguration), загружает appsettings.json, создаёт экземпляр BotService и запускает бота.
---
- appsettings.json - хранит токен тг бота и настройки апи переводчика:
---

---

## Architecture

- Handlers - handlers of commands and messages
- CommandHandler.cs - shows menus and language options, works with KeyboardHelper
---
- KeyboardHelper.cs - UI keyboard for language selection and commands
---
- MessageHandler.cs:
- Handles regular text messages that are not commands
- Determines what the user wants to do (for example, translate the text)
- Accesses the TranslatorService to get the translation result
- Stores the current user context via userState
---
- Models - contains simple classes for storing and transmitting data
- Language.cs - represents the translation language and is used to create a language selection menu
---
- userState.cs - is responsible for storing the current state of the user: the selected language, the stage of the dialog, the context of the last command
---
- Services - contains logic for interacting with external APIs
- BotService.cs - configures update processing, transmits messages to CommandHandler and MessageHandler
---
- TranslatorService.cs - text translation logic, works via LibreTranslate and MyMemory API, returns the result to MessageHandler
---
- Program.cs - configures dependencies (IConfiguration), loads appsettings.json, creates an instance of BotService and launches the bot.
---
- appsettings.json - stores the token of the tg bot and the settings of the API translator:
---