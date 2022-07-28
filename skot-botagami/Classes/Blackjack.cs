// <copyright file="Blackjack.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class used for creating and utilizing a deck of cards to play blackjack.
/// </summary>
public class Blackjack
{
    private List<Card> player;
    private List<Card> dealer;
    private Card dealerFirst;
    private Deck deck;

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
}
