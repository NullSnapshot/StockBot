using System;
using System.Threading.Tasks;
using Logger;
using Logger.Adapter;
using Logger.Service;
using Discord;


namespace Logger.Adapter.Handler
{
    /// <summary>
    ///     handler to assists <c>LoggingService</c> to capture Discord.net <c>LogMessage</c> and
    ///     adapt them to the <c>IstandardLog</c> Interface.
    /// </summary>
    public class DiscordLogMessageHandler : ILogHandler<LogMessage>
    {
        private LoggerService _logService;

        /// <summary>
        ///     constructor for <c>DiscordLogMessageHandler</c>
        /// </summary>
        /// <param name='logService'><c>LoggerService</c> that the handler will be calling to print Discord.net
        ///     </c>LogMessage</c> objects. </param>
        public DiscordLogMessageHandler(LoggerService logService)
        {
            _logService = logService;
        }

        /// <summary>
        ///     Task that will convert <c>LogMessage</c> to <c>DiscordLogMessageAdapter</c> and print
        ///     to the <c>LogService</c> the handler is attached to.
        /// </summary>
        /// <param name='message'> <c>LogMessage</c> object that will be adapted and 
        ///     sent to the <c>LogService</c></param>
        public Task Log(LogMessage message)
        {
            DiscordLogMessageAdapter discordMessage = new DiscordLogMessageAdapter(message);
            PushLog?.Invoke(discordMessage);
            return Task.CompletedTask;
        }

        public event Func<IStandardLog, Task> PushLog;
    }
}