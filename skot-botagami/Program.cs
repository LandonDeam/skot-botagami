using System;
using System.Threading.Tasks;
using DSharpPlus;

namespace skot_botagami
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var client = new DiscordClient(new DiscordConfiguration()
            {
                Token = "temp",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All
            });

            await client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
