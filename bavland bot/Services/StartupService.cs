using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bavland_bot
{
    class StartupService 
    {
        private ConfigJson _configJson;
        private DiscordSocketClient _client;

        public StartupService(DiscordSocketClient client, ConfigJson config)
        {
            _client = client;
            _configJson = config;
        }

        public async Task SignInToDiscord()
        {
            Console.WriteLine("Retrieving environment variable \"" + _configJson.Token + "\"...");
            string loginToken = Environment.GetEnvironmentVariable(_configJson.Token, EnvironmentVariableTarget.User);
            Console.WriteLine("Attempting to log into Discord...");
            await _client.LoginAsync(TokenType.Bot, loginToken);

            await _client.StartAsync();
        }

    }
}
