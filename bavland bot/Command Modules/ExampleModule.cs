using bavland_bot;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Example.Modules
{
    [Name("Example")]
    public class ExampleModule : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient _client;
        private ConfigJson _configJson;
        private readonly CommandService _service;

        public ExampleModule(CommandService service, ConfigJson configJson, DiscordSocketClient client)
        {
            _client = client;
            _service = service;
            _configJson = configJson;
        }


        [Command("say"), Alias("s")]
        [Summary("Make the bot say something")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public Task Say([Remainder]string text)
            => ReplyAsync(text);
        

    }

    [Group("set"), Name("Example")]
    [RequireContext(ContextType.Guild)]
    public class Set : ModuleBase
    {
        [Command("nickname"), Priority(0)]
        [Summary("Change your nickname to the specified text")]
        [RequireUserPermission(GuildPermission.ChangeNickname)]
        public Task SetNickname([Remainder]string name)
            => SetNickname(Context.User as SocketGuildUser, name);

        [Command("nickname"), Priority(1)]
        [Summary("Change another user's nickname to the specified text")]
        [RequireUserPermission(GuildPermission.ManageNicknames)]
        public async Task SetNickname(SocketGuildUser user, [Remainder]string name)
        {
            await user.ModifyAsync(x => x.Nickname = name);
            await ReplyAsync($"{user.Mention} I changed your name to **{name}**");
        }
    }
}
