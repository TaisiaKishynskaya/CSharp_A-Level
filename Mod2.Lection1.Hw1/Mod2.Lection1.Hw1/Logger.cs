using System.Text;

namespace Mod2.Lection1.Hw1
{
    internal class Logger
    {
        private static readonly Logger instance = new();

        static Logger() { }
        private Logger() { }

        readonly List<string> loggers = new();

        internal static Logger GetInstance => instance;

        internal void Error(string message)
        {
            var logErrorText = $"{DateTime.Now}: Error: {message}";

            loggers.Add(logErrorText);
            Console.WriteLine(logErrorText);
        }

        internal void Info(string message)
        {
            var logInfoText = $"{DateTime.Now}: Info: {message}";

            loggers.Add(logInfoText);
            Console.WriteLine(logInfoText);
        }

        internal void Warning(string message)
        {
            var logWarningText = $"{DateTime.Now}: Warning: {message}";

            loggers.Add(logWarningText);
            Console.WriteLine(logWarningText);
        }

        internal string GetLogs()
        {
            var sb = new StringBuilder();

            foreach (var log in loggers)
            {
                sb.AppendLine(log);
            }

            return sb.ToString();
        }
    }
}
