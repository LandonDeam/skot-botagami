// <copyright file="LoggingService.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

/// <summary>
/// Used for logging messages to the console.
/// </summary>
public class LoggingService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingService"/> class.
    /// </summary>
    /// <param name="client">Client to be used for logging.</param>
    /// <param name="command">Command service to use to call the logging service.</param>
    public LoggingService(DiscordSocketClient client, CommandService command)
    {
        client.Log += this.LogAsync;
        command.Log += this.LogAsync;
    }

    /// <summary>
    /// Logs the given message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <returns>Task.Completed upon finishing logging the given message.</returns>
    private Task LogAsync(LogMessage message)
    {
        if (message.Exception is CommandException cmdException)
        {
            Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases[0]}"
                + $" failed to execute in {cmdException.Context.Channel}.");
            Console.WriteLine(cmdException);
        }
        else
        {
            Console.WriteLine($"[General/{message.Severity}] {message}");
        }

        return Task.CompletedTask;
    }
}