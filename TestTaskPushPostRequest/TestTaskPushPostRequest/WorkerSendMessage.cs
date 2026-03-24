using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using TestTaskPushPostRequest.Data;

namespace TestTaskPushPostRequest
{
    public class WorkerSendMessage : BackgroundService
    {
        private readonly ILogger<WorkerSendMessage> _logger;
        private readonly IHttpClientFactory _httpClient;
        private readonly SettingSendMessage _settingSendMessage;
        private LinkedList<Task> tasks = new LinkedList<Task>();

        public WorkerSendMessage(ILogger<WorkerSendMessage> loger, IHttpClientFactory httpClient, IOptions<SettingSendMessage> options)
        {
            _logger = loger;
            _httpClient = httpClient;
            _settingSendMessage = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Сервис WorkerSendMessage запустился в: {DateTimeOffset.UtcNow}");
                
            using var timer = new PeriodicTimer(_settingSendMessage.PeriodSendMessage);

            while (!stoppingToken.IsCancellationRequested &&
               await timer.WaitForNextTickAsync(stoppingToken))
            {
                Message message = new Message
                {
                    Id = Guid.NewGuid(),
                    TimeSend = DateTime.UtcNow,
                    Msg = $"{_settingSendMessage.Message}_{DateTime.UtcNow.ToString("yyyy-MM-dd")}"
                };

                tasks.AddLast(SendMessage(stoppingToken, message));

                if (tasks.Count > 10)
                {
                     var completed = tasks.Where(t => t.IsCompleted).ToArray();
                     foreach (var t in completed) tasks.Remove(t);
                }
            }
            _logger.LogInformation($"Сервис WorkerSendMessage завершает работу {DateTimeOffset.UtcNow}. Ожидаем получение ответов на отправленные запросы...");
            await Task.WhenAll(tasks);
            _logger.LogInformation($"Сервис WorkerSendMessage остановился в: {DateTimeOffset.UtcNow}");
        }


        private async Task SendMessage(CancellationToken cancellationToken, Message message)
        {
            try
            {
                var client = _httpClient.CreateClient();
                client.Timeout = _settingSendMessage.WaitingPeriodForResponse;
                _logger.LogInformation($"Сообщение с Id={message.Id} отправлено. Время: {DateTimeOffset.UtcNow}");
                var responce = await client.PostAsJsonAsync(_settingSendMessage.Url, message, cancellationToken);
                _logger.LogInformation($"Для сообщения с Id={message.Id} получен ответ: {responce.StatusCode}. Время: {DateTimeOffset.UtcNow}");
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogInformation($"Для сообщения с Id={message.Id} ожидание ответа превысило таймаут {_settingSendMessage.WaitingPeriodForResponse.Seconds} секунд");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogInformation($"Для сообщения с Id={message.Id} произошла HTTP ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Для сообщения с Id={message.Id} произошла неожиданная ошибка: {ex.Message}");
            }
        }
    }
}
