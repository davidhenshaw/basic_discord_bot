using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace bavland_bot
{

    public class Bot
    {
        private ServiceCollection _services;
        private CommandService _commands;
        private DiscordSocketClient _client;
        private ConfigJson _configJson;
        private QuipsService _quips;

        public async Task MainAsync()
        {

            _client = new DiscordSocketClient();
            _configJson = await Utility.ImportJson<ConfigJson>("config.json");
            _quips = await Utility.ImportJson<QuipsService>("quips.json");
            _commands = new CommandService();

            _services = new ServiceCollection();
            _services.AddSingleton(_client);
            _services.AddSingleton(_commands);
            _services.AddSingleton(_configJson);
            _services.AddSingleton(_quips);
            _services.AddSingleton<CommandHandler>();
            _services.AddSingleton<StartupService>();


            IServiceProvider provider = _services.BuildServiceProvider();

            SubscribeToClientEvents();

            await provider.GetRequiredService<CommandHandler>().InstallCommandsAsync();
            await provider.GetRequiredService<StartupService>().SignInToDiscord();

            // await forever
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private void SubscribeToClientEvents()
        {
            _client.Log += Log;
        }

    }
}
