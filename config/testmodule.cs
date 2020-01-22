using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace stockBot
{
    // Keep in mind your module **must** be public and inherit ModuleBase.
    // If it isn't, it will not be discovered by AddModulesAsync!
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
            => ReplyAsync(echo);

    }
}

[Group("dev")]
public class sampleModule : ModuleBase<SocketCommandContext>
{
    [Command("userinfo")]
    [Summary("Returns info about the current user, or the user parameter, if specified.")]
    [Alias("user", "whois")]
    public async Task UserInfoAsync(
        [Summary("The (optional) user to get the info from")]
        SocketUser user = null)
    {
        var userInfo = user ?? Context.Client.CurrentUser;
        await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}\n"
        + $"userID:{ userInfo.Id}\n"
        + $"avatarID:{ userInfo.AvatarId}\n"
        + $"status:{ userInfo.Status}"
        );
    }
}