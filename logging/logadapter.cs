using System;
using System.Threading.Tasks;
using Logger;

namespace Logger.Adapter
{
    //Struct for holding converted Discord Log Messages.
    public struct DiscordLogMessageAdapter : IStandardLog
    {
        /// <summary>
        /// Gets the severity of the log event.
        /// </summary>
        /// <returns> A Logger.LogSeverity enum to indicate the severity of the incident or event. </returns>
        public LogSeverity Severity { private set; get; }

        /// <summary>
        /// Gets the source of the log entry.
        /// </summary>
        /// <returns> A string representing the source of the log entry. </returns>
        public string Source { private set; get; }

        /// <summary>
        ///     Gets the message of the log entry.
        /// </summary>
        /// <returns> A string representing the message of the log entry. </returns>
        public string Message { private set; get; }

        /// <summary>
        ///     Gets the exception thrown from the log entry.
        /// </summary>
        /// <returns> An exception thrown by the log entry, if one exists. Otherwise, returns null. </returns>
        public Exception Exception { private set; get; }

        /// <summary>
        ///     Constructor that converts a <c>Discord.LogMessage</c> to the <c>IStandardLog</c> format.
        /// </summary>
        /// <param name='message'> <c>Discord.LogMessage</c> to convert to the <c>IStandardLog</c> format.
        public DiscordLogMessageAdapter(Discord.LogMessage message)
        {
            Severity = (LogSeverity)message.Severity;
            Source = message.Source;
            Message = message.Message;
            Exception = message.Exception;
        }
    }
}