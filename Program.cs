using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Logger;
using Logger.Adapter;
using Logger.Service;

using StockBot.Configuration;

namespace StockBot
{
    public class Program
    {
        //program entry point
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly Initialize _initializer;
        private readonly LoggerService _log;

        private readonly IServiceProvider _services;

        private Program()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                // How much logging do you want to see?
                LogLevel = Discord.LogSeverity.Info,

                // If you or another service needs to do anything with messages
                // (eg. checking Reactions, checking the content of edited/deleted messages),
                // you must set the MessageCacheSize. You may adjust the number as needed.
                //MessageCacheSize = 50,
            });

            _commands = new CommandService(new CommandServiceConfig
            {
                LogLevel = Discord.LogSeverity.Info,
                CaseSensitiveCommands = false,
            });

            _log = new LoggerService();

            //subscribe discord logs to our log service.
            _client.Log += _log.DiscordHandler.Log;
            _commands.Log += _log.DiscordHandler.Log;

            Config.Log += _log.StandardLog;

            Config.Initialize();

            _initializer = new Initialize(_commands, _client, _log);

            _services = _initializer.BuildServiceProvider();

        }

        private async Task MainAsync()
        {
            //logic for commands in different method


            // Login and connect.
            await _client.LoginAsync(TokenType.Bot, Config.BotToken);
            await _client.StartAsync();

            // Wait infinitely so the bot actually stays connected.
            await Task.Delay(Timeout.Infinite);
        }
    }
}