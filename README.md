В репозитории представлено два проекта:
1. TestTaskPushPostRequest - микросервис, отправляющий post запросы на указанный URL с определенным интервалом.
2. TestPostJson - Небольшое приложение Web API, способное принять запрос (отправленный TestTaskPushPostRequest) и иммитировать время ожидания ответа
от сервера. Используется для тестирования микросервиса TestTaskPushPostRequest.

Как работать с TestTaskPushPostRequest.
В файле конфигурации проекта (расположен в TestTaskPushPostRequest\TestTaskPushPostRequest\bin\Debug\net10.0\appsettings.json)
в секции SettingSendMessage можно задать следующие настройки:
1. URL - определяет URL ресурса, куда отправляются запросы.
2. PeriodSendMessage - период, с которым отправляются запросы на Web ресурс (в тестовом задании он указан как 2 минуты).
3. WaitingPeriodForResponse - время, которое микросервис TestTaskPushPostRequest будет ждать ответа от Web ресурса. Если ответа
указанное время не получено, то в консоле появится ошибка: "Для сообщения с Id={message.Id} ожидание ответа превысило таймаут {Timeout}".
4. Message - текст сообщения, которое отправляем.

Пример настроек может выглядеть так:

"SettingSendMessage": {
  "Url": "https://localhost:7146/api/TestPost/testPost",
  "PeriodSendMessage": "00:00:10",
  "WaitingPeriodForResponse": "00:00:10",
  "Message": "test_message"
}

После указания этих настоек, можно запускать микросервис TestTaskPushPostRequest при помощи TestTaskPushPostRequest.exe, который
расположен по пути TestTaskPushPostRequest\TestTaskPushPostRequest\bin\Debug\net10.0\TestTaskPushPostRequest.exe



Как работать с TestPostJson.
В файле конфигурации проекта (расположен в TestPostJson\TestPostJson\bin\Debug\net10.0\appsettings.json)
в секции SettingSendMessage можно определить одну единственную настройку: Timeout - имитационная задержка ответа от сервера.

Пример настройки может выглядеть так: "Timeout": { "Timeout":1000 }

После указания данной настройки, можно запускать приложение TestPostJson при помощи TestPostJson.exe, которое
расположено по пути TestPostJson\TestPostJson\bin\Debug\net10.0\TestPostJson.exe

Далее, можно запустить TestTaskPushPostRequest, а затем запускать TestPostJson, затем выключать TestPostJson, менять Timeout, затем опять запускать 
TestPostJson, чтобы видеть, как реагирует на различные ситуации микросервис TestTaskPushPostRequest.

Примеры работы TestTaskPushPostRequest представлены ниже. Показаны различные ситуации:
1. Успешный ответ.
2. Отсутствие подключения к ресурсу.
3. Превышение таймаута.
4. Не найден метод ресурса.

<img width="979" height="512" alt="image" src="https://github.com/user-attachments/assets/6819542f-f9d1-4da5-933d-999c416c02a4" />

<img width="979" height="512" alt="image" src="https://github.com/user-attachments/assets/a952fc4a-640b-4c40-847b-a0ee9c079c7b" />

<img width="979" height="512" alt="image" src="https://github.com/user-attachments/assets/a8b32b9e-1abe-4e16-81a5-6c10991fd294" />




