// <copyright file="Blackjack.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

/// <summary>
/// Class used for creating and utilizing a deck of cards to play blackjack.
/// </summary>
public class Blackjack
{
    private List<Card> player;
    private List<Card> dealer;
    private Card dealerFirst;
    private Deck deck;
    private SocketCommandContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="Blackjack"/> class.
    /// </summary>
    public Blackjack()
    {
        this.player = new List<Card>();
        this.dealer = new List<Card>();
        this.deck = new Deck("blackjack");
        this.deck.Shuffle();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Blackjack"/> class.
    /// </summary>
    /// <param name="context">Context of the game being played.</param>
    public Blackjack(SocketCommandContext context)
        : this()
    {
        this.context = context;
    }

    /// <summary>
    /// Plays the game of blackjack in the current context.
    /// </summary>
    /// <returns>Task after finishing.</returns>
    public async Task Play()
    {
        this.Deal();

        IMessage msg = await this.context.Channel.SendMessageAsync(
            string.Empty,
            false,
            embed: this.GetEmbed(false).Build(),
            components: GetButtons(false).Build());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Blackjack"/> class.
    /// </summary>
    public void Deal()
    {
        this.player.Add(this.deck.Draw());
        this.dealerFirst = this.deck.Draw();
        this.dealer.Add(this.dealerFirst);
        this.player.Add(this.deck.Draw());
        this.dealer.Add(this.deck.Draw());
    }

    /// <summary>
    /// Command to hit the player.
    /// </summary>
    public void PlayerHit()
    {
        this.player.Add(this.deck.Draw());
    }

    /// <summary>
    /// Command to hit the dealer.
    /// </summary>
    public void DealerHit()
    {
        this.dealer.Add(this.deck.Draw());
    }

    /// <summary>
    /// Command to let the player stand.
    /// </summary>
    public void PlayerStand()
    {
        while (this.DealerHandValue() < 21 &&
            (this.DealerHandValue() < this.PlayerHandValue() ||
            (this.DealerHandValue() == this.PlayerHandValue() && this.DealerHandValue() < 12)))
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
    /// Gets the component builder for the button selections.
    /// </summary>
    /// <param name="split">Whether to allow the use of the split button or not.</param>
    /// <returns>The component builder for the button selections.</returns>
    private static ComponentBuilder GetButtons(bool split)
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
                            Style = ButtonStyle.Success,
                            Label = "Hit",
                            CustomId = "blackjack-hit",
                            IsDisabled = false,
                        }.Build(),

                        // Split button
                        new ButtonBuilder
                        {
                            Style = ButtonStyle.Primary,
                            Label = "Split",
                            CustomId = "blackjack-split",
                            IsDisabled = split,
                        }.Build(),

                        // Stand button
                        new ButtonBuilder
                        {
                            Style = ButtonStyle.Danger,
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
    /// Gets the embed for showing the player their cards
    /// and that of the dealer's.
    /// </summary>
    /// <param name="showDealerHand">true to embed the dealer's whole hand,
    /// false to embed just the first card.</param>
    /// <returns>The embed for showing the player their cards
    /// and that of the dealer's.</returns>
    private EmbedBuilder GetEmbed(bool showDealerHand)
    {
        EmbedBuilder embed = new EmbedBuilder();
        embed.AddField(
            showDealerHand ? $"Dealder hand ({this.DealerHandValue()})" : $"Dealer face up",
            showDealerHand ? this.DealerHand() : this.GetDealerFirst())
        .AddField(
            $"Player hand ({this.PlayerHandValue()})",
            this.PlayerHand())
        .WithColor(Color.Blue)
            .WithAuthor(author: new EmbedAuthorBuilder
            {
                Name = $"{this.context.Message.Author.Username} - Blackjack",
                IconUrl = this.context.Message.Author.GetAvatarUrl(),
            })
        .WithCurrentTimestamp();

        return embed;
    }
}
