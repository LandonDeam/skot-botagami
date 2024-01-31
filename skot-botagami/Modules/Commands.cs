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
    using Discord.Interactions;

    /// <summary>
    /// List of commands used for the bot.
    /// </summary>
    public class Commands : InteractionModuleBase<SocketInteractionContext>
    {
        /// <summary>
        /// Command to ping the bot.
        /// </summary>
        /// <returns>Returns the task completed when finished.</returns>
        [SlashCommand("ping","Returns the ping from the time a message was sent to the time that the bot was able to see it.")]
        public async Task Ping()
        {
            await this.ReplyAsync($"Pong! " +
                $"{Math.Floor(DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds) - this.Context.Interaction.CreatedAt.ToUnixTimeMilliseconds()}ms");
            return;
        }

        /// <summary>
        /// Command to play blackjack.
        /// </summary>
        /// <returns>Returns the task completed when finished.</returns>
        [SlashCommand("blackjack","Creates a game of blackjack for the user to play.")]
        public async Task BlackJack()
        {
            Blackjack game = new Blackjack(this.Context);
            IUserMessage temp = await this.ReplyAsync("Blackjack loading...");
            await game.Play(temp);
            return;
        }
    }
}
