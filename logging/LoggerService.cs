using System;
using Logger;
using Logger.Adapter.Handler;
using System.Threading.Tasks;

namespace Logger.Service
{
    /// <summary>
    ///     Service that can be subscribed to events that output <c>IStandardLog</c> objects.
    ///     The service will proceed to print log events to the console.
    /// </summary>
    public class LoggerService
    {
        /// <summary>
        ///     Handler for Discord.net logs. <c>Discord.LogMessage</c> outputing events can be subscribed
        ///     to DiscordHandler.Log.
        /// </summary>
        public DiscordLogMessageHandler DiscordHandler;

        /// <summary>
        ///     Constructor that creates all the handlers the <c>LoggerService</c> will use and attaches
        ///     their PushLog task to 
        /// </summary>
        public LoggerService()
        {
            DiscordHandler = new DiscordLogMessageHandler(this);
            DiscordHandler.PushLog += this.StandardLog;
        }
        private void PrintLog(IStandardLog message)
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

        /// <summary>
        ///     Task that if events are subscribed to, will print the contents of the log to the console.
        /// </summary>
        /// <param name='message'><c>StandardLog</c> to be printed to the console.</param>
        public Task StandardLog(StandardLog message)
        {
            PrintLog(message);
            return Task.CompletedTask;
        }

        public Task StandardLog(IStandardLog message)
        {
            StandardLog Message = new StandardLog(message.Severity, message.Source, message.Message, message.Exception);
            PrintLog(Message);
            return Task.CompletedTask;
        }
    }

}