using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace bavland_bot
{


    public class TestModule : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient _client;
        private ConfigJson _configJson;
        private readonly CommandService _service;
        private QuipsService _quips;

        public TestModule(CommandService service, ConfigJson configJson, QuipsService quips, DiscordSocketClient client)
        {
            _client = client;
            _service = service;
            _configJson = configJson;
            _quips = quips;
        }


        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo) => ReplyAsync(echo);

        [Command("update_avatar")]
        [Summary("EXPERIMENTAL - tells bot to upload the avatar image its config points to")]
        public async Task GetPrefixAsync()
        {
            await ReplyAsync("Attempting to change avatar...");
            await UpdateAvatarAsync();
            await ReplyAsync("Done.");
        }

        [Command("thanks")]
        [Summary("Be polite to the bot please :)")]
        public async Task ThankAsync()
        {
            await ReplyAsync($"{GetRandomString(_quips.Gratitude)}, {Context.User.Mention}!");
        }

        [Command("hello")]
        [Summary("Just saying hi")]
        public async Task GreetAsync()
        {
            await ReplyAsync( Context.User.Mention + " " + GetRandomString(_quips.Greetings) );
        }

        [Command("delete", RunMode = RunMode.Async)]
        [Summary("EXPERIMENTAL - Removes previous message that was a bot command")]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        public async Task RemoveAsync(int amount)
        {
            var messages = await Context.Channel.GetMessagesAsync(amount).FlattenAsync();
            await Context.Guild.GetTextChannel(Context.Channel.Id).DeleteMessagesAsync(messages);

            await ReplyAsync($"{amount} messages have been {GetRandomString(_quips.DeletedMessage)}!");
        }

        public async Task UpdateAvatarAsync()
        {
            Image avatar;
            string path = _configJson.Avatar;

            using (FileStream fileStream = File.OpenRead(path))
            {
                if (fileStream == null)
                {
                    Console.WriteLine($"Could not find avatar image at {path}");
                    await Task.CompletedTask;
                }

                avatar = new Image(fileStream);

                await _client.CurrentUser.ModifyAsync(properties => properties.Avatar = avatar, null);
            }
        }

        public string GetRandomString(string[] stringArray )
        {
            Random random = new Random();
            return stringArray[random.Next(0, stringArray.Length)];
        }
    }

}
