namespace BloodDonor.Mvc.Logger
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger(categoryName);
        }

        public void Dispose() { }
    }

    public class CustomLogger : ILogger
    {
        private readonly string _categoryName;

        public CustomLogger(string categoryName)
        {
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            File.AppendAllText("logs/custom-log.txt", $"[{DateTime.Now}] {_categoryName} [{logLevel}] {message}{Environment.NewLine}");
        }
    }
}
