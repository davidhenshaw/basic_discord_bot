using Discord.WebSocket;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace bavland_bot
{
    public class Utility
    {
        public static async Task<T> ImportJson<T>(string path)
        {
            var jsonEncoded = string.Empty;

            using (FileStream fileStream = File.OpenRead(path))
            {
                using (var streamReader = new StreamReader(fileStream, new UTF8Encoding(false)))
                {
                    jsonEncoded = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                }
            }

            var config = new DiscordSocketConfig
            {

            };


            return JsonConvert.DeserializeObject<T>(jsonEncoded);
        }
    }
}
