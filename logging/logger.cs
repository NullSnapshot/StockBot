using System;
using Logger.Adapter;
using Logger.Adapter.Handler;
using System.Threading.Tasks;

namespace Logger
{
    public interface IStandardLog
    {
        LogSeverity Severity { get; }
        string Source { get; }
        string Message { get; }
        Exception Exception { get; }
    }

    public struct StandardLog : IStandardLog
    {
        public LogSeverity Severity { get; }
        public string Source { get; }
        public string Message { get; }
        public Exception Exception { get; }

        public StandardLog(LogSeverity severity, string source, string message, Exception exception = null)
        {
            Severity = severity;
            Source = source;
            Message = message;
            Exception = exception;
        }
    }

    public class LoggerService
    {
        public DiscordLogMessageHandler DiscordHandler;

        public LoggerService()
        {
            DiscordHandler = new DiscordLogMessageHandler(this);
        }
        public void PrintLog(IStandardLog message)
        {
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message} {message.Exception}");
            Console.ResetColor();
        }

        public Task StandardLog(StandardLog message)
        {
            PrintLog(message);
            return Task.CompletedTask;
        }
    }

    public enum LogSeverity
    {
        //
        // Summary:
        //     Logs that contain the most severe level of error. This type of error indicate
        //     that immediate attention may be required.
        Critical = 0,
        //
        // Summary:
        //     Logs that highlight when the flow of execution is stopped due to a failure.
        Error = 1,
        //
        // Summary:
        //     Logs that highlight an abnormal activity in the flow of execution.
        Warning = 2,
        //
        // Summary:
        //     Logs that track the general flow of the application.
        Info = 3,
        //
        // Summary:
        //     Logs that are used for interactive investigation during development.
        Verbose = 4,
        //
        // Summary:
        //     Logs that contain the most detailed messages.
        Debug = 5
    }
}