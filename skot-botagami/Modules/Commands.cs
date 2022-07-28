// <copyright file="Commands.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

namespace SkotBotagami.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Commands;

    /// <summary>
    /// List of commands used for the bot.
    /// </summary>
    public class Commands : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Command to ping the bot.
        /// </summary>
        /// <returns>Returns the task completed when finished.</returns>
        [Command("ping")]
        public async Task Ping()
        {
            await this.ReplyAsync($"Pong! " +
                $"{Math.Floor(DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds) - this.Context.Message.Timestamp.ToUnixTimeMilliseconds()}ms");
            return;
        }

        /// <summary>
        /// Command to play blackjack.
        /// </summary>
        /// <returns>Returns the task completed when finished.</returns>
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
                    IconUrl = this.Context.Message.Author.GetAvatarUrl(),
                });
            Emoji hit = new Emoji(":HIT:1002084815023124541");
            Emoji stand = new Emoji(":STAND:1002085670594035772");
            IMessage msg = await this.Context.Channel.SendMessageAsync(embed: embed.Build());
            await msg.AddReactionAsync(hit);
            await msg.AddReactionAsync(stand);

            return;
        }
    }
}
