﻿// <copyright file="Blackjack.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.API;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;

/// <summary>
/// Class used for creating and utilizing a deck of cards to play blackjack.
/// </summary>
public class Blackjack
{
    private static List<Blackjack> games = new List<Blackjack>();
    private List<Card> player;
    private List<Card> dealer;
    private Card dealerFirst;
    private Deck deck;
    private SocketInteractionContext context;
    private IUserMessage gameWindow;
    private bool playerControls;
    private Timer timer;
    private ulong playerId;

    /// <summary>
    /// Initializes a new instance of the <see cref="Blackjack"/> class.
    /// </summary>
    public Blackjack()
    {
        this.playerControls = true;
        this.player = new List<Card>();
        this.dealer = new List<Card>();
        this.deck = new Deck("blackjack");
        this.deck.Shuffle();
        games.Add(this);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Blackjack"/> class.
    /// </summary>
    /// <param name="context">Context of the game being played.</param>
    public Blackjack(SocketInteractionContext context)
        : this()
    {
        this.context = context;
        this.playerId = context.User.Id;
    }

    /// <summary>
    /// Gets the blackjack game corresponding to the given message.
    /// </summary>
    /// <param name="original">The original message to start the game.</param>
    /// <returns>The blackjack object corresponding to the message.</returns>
    public static Blackjack GetGame(SocketUserMessage original)
    {
        return games.Find(x => x.gameWindow.Id == original.Id);
    }

    /// <summary>
    /// Gets the component builder for the button selections.
    /// </summary>
    /// <param name="split">Whether to allow the use of the split button or not.</param>
    /// <returns>The component builder for the button selections.</returns>
    public static ComponentBuilder GetButtons(bool split)
    {
        ComponentBuilder builder = new ComponentBuilder
        {
            ActionRows = new List<ActionRowBuilder>
            {
                new ActionRowBuilder
                {
                    Components = new List<IMessageComponent>
                    {
                        // Hit button
                        new ButtonBuilder
                        {
                            Style = ButtonStyle.Primary,
                            Label = "Hit",
                            CustomId = "blackjack-hit",
                            IsDisabled = false,
                        }.Build(),

                        // Split button
//                        new ButtonBuilder
//                        {
//                            Style = ButtonStyle.Primary,
//                            Label = "Split",
//                            CustomId = "blackjack-split",
//                            IsDisabled = split,
//                        }.Build(),

                        // Stand button
                        new ButtonBuilder
                        {
                            Style = ButtonStyle.Primary,
                            Label = "Stand",
                            CustomId = "blackjack-stand",
                            IsDisabled = false,
                        }.Build(),
                    },
                },
            },
        };

        return builder;
    }

    /// <summary>
    /// Gets the owner of the blackjack game's user ID.
    /// </summary>
    /// <returns>User ID of the owner of the blackjack game.</returns>
    public ulong GetOwnerId()
    {
        return this.playerId;
    }

    /// <summary>
    /// Resets the game to play again.
    /// </summary>
    public async void PlayAgain()
    {
        this.playerControls = true;
        this.player = new List<Card>();
        this.dealer = new List<Card>();
        this.deck = new Deck("blackjack");
        this.deck.Shuffle();
        await this.Deal();
    }

    /// <summary>
    /// Gets the embed for showing the player their cards
    /// and that of the dealer's.
    /// </summary>
    /// <param name="showDealerHand">true to embed the dealer's whole hand,
    /// false to embed just the first card.</param>
    /// <returns>The embed for showing the player their cards
    /// and that of the dealer's.</returns>
    public EmbedBuilder GetEmbed(bool showDealerHand)
    {
        EmbedBuilder embed = new EmbedBuilder();
        embed.AddField(
            showDealerHand ? $"Dealer hand (**{this.DealerHandValue()}**)" : $"Dealer face up",
            showDealerHand ? this.DealerHand() : this.GetDealerFirst())
        .AddField(
            $"Player hand (**{this.PlayerHandValue()}**)",
            this.PlayerHand())
        .WithColor(Color.Blue)
            .WithAuthor(author: new EmbedAuthorBuilder
            {
                Name = $"{this.context.User.Username} - Blackjack",
                IconUrl = this.context.User.GetAvatarUrl(),
            })
        .WithCurrentTimestamp();

        return embed;
    }

    /// <summary>
    /// Plays the game of blackjack in the current context.
    /// </summary>
    /// <param name="message">Original message replying to a command.</param>
    /// <returns>Task after finishing.</returns>
    public async Task Play(IUserMessage message)
    {
        this.gameWindow = message;
        await this.Deal();
        this.TimerStart();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Blackjack"/> class.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task Deal()
    {
        this.player.Add(this.deck.Draw());
        this.dealerFirst = this.deck.Draw();
        this.dealer.Add(this.dealerFirst);
        this.player.Add(this.deck.Draw());
        this.dealer.Add(this.deck.Draw());
        await this.UpdateGameWindow(false);

        if (this.PlayerHandValue() == 21)
        {
            this.PlayerStand();
        }
    }

    /// <summary>
    /// Command to hit the player.
    /// </summary>
    public async void PlayerHit()
    {
        // Checks if the player has control or not
        if (!this.playerControls)
        {
            return;
        }

        this.TimerReset();

        this.player.Add(this.deck.Draw());

        await this.UpdateGameWindow(false);

        if (this.PlayerHandValue() > 21)
        {
            this.EndGame(0);
        }
        else if (this.PlayerHandValue() == 21)
        {
            this.PlayerStand();
        }

        return;
    }

    /// <summary>
    /// Command to hit the dealer.
    /// </summary>
    public async void DealerHit()
    {
        await this.UpdateGameWindow(true);

        this.dealer.Add(this.deck.Draw());

        await Task.Delay(TimeSpan.FromSeconds(1));

        await this.UpdateGameWindow(true);

        if (this.DealerHandValue() > 21)
        {
            this.EndGame(2);
        }
        else if (this.CheckForWin() == 0)
        {
            this.EndGame(0);
        }
        else if (
            (this.CheckForWin() == 1 && this.DealerHandValue() <= 17) ||
            (this.CheckForWin() == 2))
        {
            this.DealerHit();
        }
        else
        {
            this.EndGame(this.CheckForWin());
        }
    }

    /// <summary>
    /// Command to let the player stand.
    /// </summary>
    public void PlayerStand()
    {
        // Checks if the player has control or not
        if (!this.playerControls)
        {
            return;
        }

        this.TimerReset();

        this.playerControls = false;

        if (this.CheckForWin() < 2)
        {
            this.EndGame(this.CheckForWin());
        }
        else
        {
            this.DealerHit();
        }
    }

    /// <summary>
    /// Gets the value of the player's hand.
    /// </summary>
    /// <returns>Blackjack value of the player's hand.</returns>
    public int PlayerHandValue()
    {
        return GetHandValue(this.player);
    }

    /// <summary>
    /// Gets the value of the dealer's hand.
    /// </summary>
    /// <returns>Blackjack value of the dealer's hand.</returns>
    public int DealerHandValue()
    {
        return GetHandValue(this.dealer);
    }

    /// <summary>
    /// Gets the first card of the dealer's hand.
    /// </summary>
    /// <returns>First card of the dealer's hand.</returns>
    public string GetDealerFirst()
    {
        return this.dealerFirst.ToString();
    }

    /// <summary>
    /// Gets the dealer's hand.
    /// </summary>
    /// <returns>The dealer's hand.</returns>
    public string DealerHand()
    {
        string temp = string.Empty;
        foreach (Card card in this.dealer)
        {
            temp += card.ToString() + ", ";
        }

        return temp.Substring(0, temp.Length - 2);
    }

    /// <summary>
    /// Gets the player's hand.
    /// </summary>
    /// <returns>The player's hand.</returns>
    public string PlayerHand()
    {
        string temp = string.Empty;
        foreach (Card card in this.player)
        {
            temp += card.ToString() + ", ";
        }

        return temp.Substring(0, temp.Length - 2);
    }

    /// <summary>
    /// Gets the value of a given hand.
    /// </summary>
    /// <param name="hand">Hand to evaluate.</param>
    /// <returns>Blackjack value of the given hand.</returns>
    private static int GetHandValue(List<Card> hand)
    {
        int value = 0;
        foreach (Card card in hand)
        {
            if (card.GetValue() > 10)
            {
                value += 10;
            }
            else
            {
                value += card.GetValue();
            }
        }

        if (value < 12 && hand.Any(x => x.GetValue() == 1))
        {
            value += 10;
        }

        return value;
    }

    /// <summary>
    /// Deletes the message associated with this object after no interaction
    /// for 60 seconds.
    /// </summary>
    /// <param name="source">Source of the call.</param>
    /// <param name="e">Any event args associated with the elapsed event.</param>
    private void DeleteMessage(object source, ElapsedEventArgs e)
    {
        if (this.playerControls)
        {
            this.PlayerStand();
            this.TimerReset();
            return;
        }

        try
        {
            this.gameWindow.DeleteAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("Error deleting message");
        }

        games.Remove(this);
    }

    /// <summary>
    /// Starts the timer.
    /// </summary>
    private void TimerStart()
    {
        this.timer = new Timer(60000);
        this.timer.Elapsed += new ElapsedEventHandler(this.DeleteMessage);
        this.timer.Start();
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    private void TimerReset()
    {
        if (this.timer is null)
        {
            this.TimerStart();
        }

        this.timer.Stop();
        this.timer.Start();
    }

    /// <summary>
    /// Updates the game window to reflect the current state of the game.
    /// </summary>
    /// <returns>Nothing.</returns>
    /// <param name="showDealerHand">true to show the dealer's hand, false to only show the first card.</param>
    private async Task UpdateGameWindow(bool showDealerHand)
    {
        await this.gameWindow.ModifyAsync(x =>
        {
            x.Content = string.Empty;
            x.Embed = this.GetEmbed(showDealerHand).Build();
            x.Components = GetButtons(false).Build();
        });
        return;
    }

    private async void EndGame(int winStatus)
    {
        // Sets appropriate win status
        string loseTieWin;
        switch (winStatus)
        {
            case 0:
                loseTieWin = "lost";
                break;
            case 1:
                loseTieWin = "push";
                break;
            default:
                loseTieWin = "won";
                break;
        }

        this.playerControls = false;

        await this.gameWindow.ModifyAsync(x =>
        {
            x.Embed = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    IconUrl = this.context.User.GetAvatarUrl(),
                    Name = $"{this.context.User.Username} - Blackjack",
                },
                Title = $"{this.context.User.Username} {loseTieWin}!",
                Fields = new List<EmbedFieldBuilder>
                {
                    new EmbedFieldBuilder
                    {
                        Name = $"Dealer hand (**{this.DealerHandValue()}**)",
                        Value = this.DealerHand(),
                    },
                    new EmbedFieldBuilder
                    {
                        Name = $"Player Hand (**{this.PlayerHandValue()}**)",
                        Value = this.PlayerHand(),
                    },
                },
                Color = (winStatus < 2) ? (winStatus == 0) ? Color.Red : Color.LightGrey : Color.Green,
            }.WithCurrentTimestamp().Build();
            x.Components = new ComponentBuilder
            {
                ActionRows = new List<ActionRowBuilder>
                {
                    new ActionRowBuilder
                    {
                        Components = new List<IMessageComponent>
                        {
                            new ButtonBuilder
                            {
                                Style = ButtonStyle.Primary,
                                CustomId = "blackjack-playagain",
                                Label = "Play again",
                                IsDisabled = false,
                            }.Build(),
                        },
                    },
                },
            }.Build();
        });
    }

    /// <summary>
    /// Checks if the player has won, tied, or lost.
    /// </summary>
    /// <returns>0 for player lose, 1 for tie, 2 for player win.</returns>
    private int CheckForWin()
    {
        if (this.PlayerHandValue() > 21)
        {
            return 0;
        }
        else if (this.DealerHandValue() > 21)
        {
            return 2;
        }
        else if (this.PlayerHandValue() > this.DealerHandValue())
        {
            return 2;
        }
        else if (this.PlayerHandValue() < this.DealerHandValue())
        {
            return 0;
        }

        return 1;
    }
}
