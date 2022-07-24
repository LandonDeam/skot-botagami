using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace skot_botagami.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync($"Pong! " +
                $"{Math.Floor(DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds) - Context.Message.Timestamp.ToUnixTimeMilliseconds()}ms");
        }

        [Command("blackjack")]
        public async Task BlackJack()
        {
            
        }
    }
}
