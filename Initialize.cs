using System;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Logger.Service;

namespace StockBot
{
    public class Initialize
    {
        private readonly CommandService _commands;
        private readonly CommandHandler _commandHandler;
        private readonly DiscordSocketClient _client;
        private readonly LoggerService _log;

        //Check if there are any existing CommandService or DiscordSocketClient,
        //otherwise, build new ones in constructor.
        public Initialize(CommandService commands = null, DiscordSocketClient client = null, CommandHandler commandHandler = null,
            LoggerService log = null)
        {
            _commands = commands ?? new CommandService();

            _commandHandler = commandHandler ?? new CommandHandler(client, commands);

            _client = client ?? new DiscordSocketClient();

            _log = log ?? new LoggerService();
        }

        public IServiceProvider BuildServiceProvider() => new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddSingleton(_log)

            //.AddSingleton<DatabaseService>()
            .AddSingleton(_commandHandler)
            .BuildServiceProvider();

    }
}