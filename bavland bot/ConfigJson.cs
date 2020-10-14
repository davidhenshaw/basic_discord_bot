using Newtonsoft.Json;

namespace bavland_bot
{
    public class ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public char Prefix { get; private set; }
        [JsonProperty("avatar")]
        public string Avatar { get; private set; }
    }
}
