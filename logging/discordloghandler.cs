using System;
using System.Threading.Tasks;
using Logger;
using Logger.Adapter;
using Discord;
namespace Logger.Adapter.Handler
{
    public class DiscordLogMessageHandler
    {
        private LoggerService _log;

        public DiscordLogMessageHandler(LoggerService log)
        {
            _log = log;
        }

        public Task Log(LogMessage message)
        {
            DiscordLogMessageAdapter discordMessage = new DiscordLogMessageAdapter(message);
            _log.PrintLog(discordMessage);
            return Task.CompletedTask;
        }
    }
}