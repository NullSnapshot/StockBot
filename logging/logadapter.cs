using System;
using System.Threading.Tasks;
using Logger;

namespace Logger.Adapter
{
    public class DiscordLogMessageAdapter : IStandardLog
    {
        public LogSeverity Severity { private set; get; }
        public string Source { private set; get; }
        public string Message { private set; get; }
        public Exception Exception { private set; get; }

        public DiscordLogMessageAdapter(Discord.LogMessage message)
        {
            Severity = (LogSeverity)message.Severity;
            Source = message.Source;
            Message = message.Message;
            Exception = message.Exception;
        }
    }
}