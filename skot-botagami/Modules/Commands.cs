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
            await this.ReplyAsync($"Pong! " +
                $"{Math.Floor(DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds) - this.Context.Message.Timestamp.ToUnixTimeMilliseconds()}ms");
        }

        [Command("blackjack")]
        public async Task BlackJack()
        {
            Blackjack game = new Blackjack();
            game.Deal();
            EmbedBuilder embed = new EmbedBuilder();
            embed.AddField($"Dealer face up", game.GetDealerFirst())
                .AddField($"Player hand ({game.PlayerHandValue()})", game.PlayerHand())
                .WithColor(Color.Blue)
                .WithAuthor(author: new EmbedAuthorBuilder
                {
                    Name = $"{this.Context.Message.Author.Username} - Blackjack",
                    IconUrl = this.Context.Message.Author.GetAvatarUrl()
                });
            Emoji hit = new Emoji(":HIT:1002084815023124541");
            Emoji stand = new Emoji(":STAND:1002085670594035772");
            IMessage msg = await this.Context.Channel.SendMessageAsync(embed: embed.Build());
            await msg.AddReactionAsync(hit);
            await msg.AddReactionAsync(stand);

        }
    }
}
