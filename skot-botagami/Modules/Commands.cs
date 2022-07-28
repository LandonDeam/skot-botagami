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
            Blackjack game = new Blackjack();
            game.deal();
            Discord.EmbedBuilder msg = new Discord.EmbedBuilder
            {
                Title = "Blackjack",
                Description = $"Dealer first: {game.getDealerFirst()}\n{game.playerHand()}\nHit?"
            };
            msg.WithAuthor(author: new Discord.EmbedAuthorBuilder {Name=Context.Client.CurrentUser.Username, IconUrl= "https://media.discordapp.net/attachments/1000464257181286463/1002092083873587200/Cowboy_Bebop_Ed_Hacking_Logo_No_BG.png" });
            await Context.Channel.SendMessageAsync(embed: msg.Build());
        }
    }
}
