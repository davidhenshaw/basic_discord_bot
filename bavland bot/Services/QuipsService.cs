using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bavland_bot
{
    public class QuipsService
    {
        [JsonProperty("Gratitude")]
        public string[] Gratitude { get; private set; }

        [JsonProperty("Greetings")]
        public string[] Greetings { get; private set; }

        [JsonProperty("DeletedMessage")]
        public string[] DeletedMessage { get; private set; }
    }
}
