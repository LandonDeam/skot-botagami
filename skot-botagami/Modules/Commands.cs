using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

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
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("Blackjack")
                .AddField($"Dealer face up", game.getDealerFirst())
                .AddField($"Player hand ({game.playerHandValue()})", game.playerHand())
                .WithAuthor(author: new EmbedAuthorBuilder
                {
                    Name = Context.Client.CurrentUser.Username,
                    IconUrl = "https://media.discordapp.net/attachments/1000464257181286463/1002092083873587200/Cowboy_Bebop_Ed_Hacking_Logo_No_BG.png"
                });
            Emoji hit = new Emoji(":HIT:1002084815023124541");
            Emoji stand = new Emoji(":STAND:1002085670594035772");
            IMessage msg = await Context.Channel.SendMessageAsync(embed: embed.Build());
            await msg.AddReactionAsync(hit);
            await msg.AddReactionAsync(stand);
        }
    }
}
