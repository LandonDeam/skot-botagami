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
            await this.Context.Message.DeleteAsync();
            Blackjack game = new Blackjack(this);
            IUserMessage temp = await this.ReplyAsync(
                string.Empty,
                false,
                embed: game.GetEmbed(false).Build(),
                components: Blackjack.GetButtons(false).Build());
            await game.Play(temp);
            return;
        }
    }
}
