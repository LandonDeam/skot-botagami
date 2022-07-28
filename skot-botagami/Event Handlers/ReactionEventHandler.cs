// <copyright file="ReactionEventHandler.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

/// <summary>
/// Class used for handling reactions to messages.
/// </summary>
public class ReactionEventHandler
{
    /// <summary>
    /// Handling a reaction being added to a message.
    /// </summary>
    /// <param name="message">Message the reaction is on.</param>
    /// <param name="channel">Channel the reaction is in.</param>
    /// <param name="reaction">Reaction used.</param>
    /// <returns>Task.CompletedTask upon finishing.</returns>
    public static Task ReactionAdded(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
    {
        if (reaction == null || reaction.User.Value.IsBot)
        {
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}