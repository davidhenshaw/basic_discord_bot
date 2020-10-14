using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bavland_bot
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Bavland Bot";
            var bot = new Bot();
            bot.MainAsync().GetAwaiter().GetResult();
        }
    }

}
