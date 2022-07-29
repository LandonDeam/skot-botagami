﻿// <copyright file="ButtonInteractionHandler.cs" company="Landon Deam">
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
/// Handler for interactions with buttons.
/// </summary>
public class ButtonInteractionHandler
{
    /// <summary>
    /// Handles buttons being executed.
    /// </summary>
    /// <param name="component">Component button that was executed.</param>
    /// <returns>Task.CompletedTask upon finishing.</returns>
    public static async Task OnButtonExecution(SocketMessageComponent component)
    {
        await component.DeferAsync();

        try
        {
            switch (component.Data.CustomId)
            {
                case "blackjack-hit":
                    Blackjack.GetGame(component.Message).PlayerHit();
                    break;
                case "blackjack-split":

                    break;
                case "blackjack-stand":
                    Blackjack.GetGame(component.Message).PlayerStand();
                    break;
                default:
                    await component.RespondAsync("An error occurred: could not find custom ID of the button pressed");
                    break;
            }
        }
        catch (Exception e)
        {
            await component.RespondAsync("An unknown error occurred");
            Console.WriteLine(e.ToString());
        }

        return;
    }
}