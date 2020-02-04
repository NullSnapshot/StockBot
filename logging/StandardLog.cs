using System;

namespace Logger
{
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
}