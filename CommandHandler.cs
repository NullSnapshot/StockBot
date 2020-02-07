using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using StockBot.Configuration;

namespace StockBot
{
    public class CommandHandler
    {
        public DiscordSocketClient Client { get; private set; }
        public CommandService Commands { get; private set; }
        public IServiceProvider Services { get; set; }

        public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider services)
        {
            Client = client;
            Commands = commands;
            Services = services;

            Initialize();
        }

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            Client = client;
            Commands = commands;

        }

        public void Initialize()
        {
            // Pass the service provider to the second parameter of
            // AddModulesAsync to inject dependencies to all modules 
            // that may require them.
            Commands.AddModulesAsync(
                assembly: Assembly.GetEntryAssembly(),
                services: Services);
            Client.MessageReceived += HandleCommandAsync;
        }

        public async Task HandleCommandAsync(SocketMessage arg)
        {
            // Bail out if it's a System Message.
            var msg = arg as SocketUserMessage;
            if (msg == null) return;

            // We don't want the bot to respond to itself or other bots.
            if (msg.Author.Id == Client.CurrentUser.Id || msg.Author.IsBot) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            //bot responds to command prefix or direct mention
            if (msg.HasCharPrefix(Config.CommandPrefix, ref argPos) || msg.HasMentionPrefix(Client.CurrentUser, ref argPos))
            {
                // Create a Command Context.
                var context = new SocketCommandContext(Client, msg);

                // ...
                // Pass the service provider to the ExecuteAsync method for
                // precondition checks.
                await Commands.ExecuteAsync(
                    context: context,
                    argPos: argPos,
                    services: Services);
                // ...

                // Uncomment the following lines if you want the bot
                // to send a message if it failed.
                // This does not catch errors from commands with 'RunMode.Async',
                // subscribe a handler for '_commands.CommandExecuted' to see those.
                //if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                //    await msg.Channel.SendMessageAsync(result.ErrorReason);


            }
        }
    }
}