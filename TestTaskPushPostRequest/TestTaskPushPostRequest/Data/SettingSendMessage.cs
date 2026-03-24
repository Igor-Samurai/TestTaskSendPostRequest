namespace TestTaskPushPostRequest.Data
{
    public class SettingSendMessage
    {
        public string Url { get; set; }
        public TimeSpan PeriodSendMessage { get; set; }
        public TimeSpan WaitingPeriodForResponse { get; set; }
        public string Message { get; set; }
    }
}
