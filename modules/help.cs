using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace stockBot
{
    public class HelpModule: ModuleBase<SocketCommandContext>
    {
        public CommandService commandService { get; set; }


        [Command("help")]
        [Summary("Provides summaries of all commands available to use.")]
        public async Task sayHelpAsync([Remainder] [Summary("The default help prompt")] SocketUser user = null)
        {
            SearchResult test = commandService.Search("help");
            IEnumerable<CommandInfo> commands = commandService.Commands;
            EmbedBuilder embedBuilder = new EmbedBuilder();
            foreach(CommandInfo command in commands)
            {
                string embedFieldText = command.Summary ?? "No description available";
                embedBuilder.AddField(command.Name, embedFieldText);
            }
            await ReplyAsync("", false, embedBuilder.Build());
        }
    }
}
