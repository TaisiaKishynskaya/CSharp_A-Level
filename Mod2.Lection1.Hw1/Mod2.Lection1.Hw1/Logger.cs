using System.Text;

namespace Mod2.Lection1.Hw1
{
    internal class Logger
    {
        private static readonly Logger _instance = new();

        static Logger() { }
        private Logger() { }

        readonly List<string> logs = new();

        internal static Logger Instance => _instance;

        internal void Error(string message)
        {
            var logErrorText = $"{DateTime.Now}: Error: {message}";

            logs.Add(logErrorText);
            Console.WriteLine(logErrorText);
        }

        internal void Info(string message)
        {
            var logInfoText = $"{DateTime.Now}: Info: {message}";

            logs.Add(logInfoText);
            Console.WriteLine(logInfoText);
        }

        internal void Warning(string message)
        {
            var logWarningText = $"{DateTime.Now}: Warning: {message}";

            logs.Add(logWarningText);
            Console.WriteLine(logWarningText);
        }

        internal string GetLogs()
        {
            var sb = new StringBuilder();

            foreach (var log in logs)
            {
                sb.AppendLine(log);
            }

            return sb.ToString();
        }
    }
}
