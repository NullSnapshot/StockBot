using System;

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
}