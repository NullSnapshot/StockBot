using System;
using Logger.Adapter;
using Logger.Adapter.Handler;
using System.Threading.Tasks;

namespace Logger
{
    /// <summary>
    ///     Interface used for all logs coming into <c>LoggingService</c>
    /// </summary>
    public interface IStandardLog
    {
        /// <summary>
        /// Gets the severity of the log event.
        /// </summary>
        /// <returns> A Logger.LogSeverity enum to indicate the severity of the incident or event. </returns>
        LogSeverity Severity { get; }

        /// <summary>
        /// Gets the source of the log entry.
        /// </summary>
        /// <returns> A string representing the source of the log entry. </returns>
        string Source { get; }

        /// <summary>
        ///     Gets the message of the log entry.
        /// </summary>
        /// <returns> A string representing the message of the log entry. </returns>
        string Message { get; }

        /// <summary>
        ///     Gets the exception thrown from the log entry.
        /// </summary>
        /// <returns> An exception thrown by the log entry, if one exists. Otherwise, returns null. </returns>
        Exception Exception { get; }
    }

    /// <summary>
    ///     Structure that can be used with <c>LoggingService</c>
    /// </summary>
    public struct StandardLog : IStandardLog
    {
        /// <summary>
        /// Gets the severity of the log event.
        /// </summary>
        /// <returns> A Logger.LogSeverity enum to indicate the severity of the incident or event. </returns>
        public LogSeverity Severity { get; }

        /// <summary>
        /// Gets the source of the log entry.
        /// </summary>
        /// <returns> A string representing the source of the log entry. </returns>
        public string Source { get; }

        /// <summary>
        ///     Gets the message of the log entry.
        /// </summary>
        /// <returns> A string representing the message of the log entry. </returns>
        public string Message { get; }

        /// <summary>
        ///     Gets the exception thrown from the log entry.
        /// </summary>
        /// <returns> An exception thrown by the log entry, if one exists. Otherwise, returns null. </returns>
        public Exception Exception { get; }

        /// <summary>
        ///     Initializes a new <c>Logger.StandardLog</c> struct with the
        ///     severity, source of the event, message to be logged, and optionally, 
        ///     the exception thrown.
        /// </summary>
        /// <param name="severity">The severity of the event.</param>
        /// <param name="source">The source of the log entry.</param>
        /// <param name="message">The message of the log entry to be printed to the console.</param>
        /// <param name="exception">The optional exception thrown by the event to be logged.</param>
        public StandardLog(LogSeverity severity, string source, string message,
            Exception exception = null)
        {
            Severity = severity;
            Source = source;
            Message = message;
            Exception = exception;
        }
    }

    //LoggerService
    //Class that processes all Standard Logs. Contains the Handlers for any third parties that do not use
    //the IStandardLog format.
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

    /// <summary>
    ///     Enumerator Specifying the severity of the Log Message
    /// </summary>
    public enum LogSeverity
    {
        /// <summary>
        ///     Logs that contain the most severe level of error. This type of error indicate
        ///     that immediate attention may be required.
        /// </summary>
        Critical = 0,

        /// <summary>
        ///     Logs that highlight when the flow of execution is stopped due to a failure.
        /// </summary>
        Error = 1,

        /// <summary>
        ///     Logs that highlight an abnormal activity in the flow of execution.
        /// </summary>
        Warning = 2,

        /// <summary>
        ///     Logs that track the general flow of the application.
        /// </summary>
        Info = 3,

        /// <summary>
        ///     Logs that are used for interactive investigation during development.
        /// </summary>
        Verbose = 4,

        /// <summary>
        ///     Logs that contain the most detailed messages.
        /// </summary>
        Debug = 5
    }
}