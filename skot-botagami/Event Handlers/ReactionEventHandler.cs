using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

public class ReactionEventHandler
{
    public static Task ReactionAdded(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
    {
        Console.WriteLine($"{message.Id} was reacted to with {reaction.Emote.Name} by {reaction.User.Value}");
        return Task.CompletedTask;
    }
}