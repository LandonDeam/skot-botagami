// <copyright file="Deck.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class used for creating and utilizing a deck of cards.
/// </summary>
public class Deck
{
    private List<Card> cards;

    /// <summary>
    /// Initializes a new instance of the <see cref="Deck"/> class.
    /// Creates a new, empty deck of cards.
    /// </summary>
    public Deck()
    {
        this.cards = new List<Card>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Deck"/> class.
    /// </summary>
    /// <param name="game">The game to build the deck specifically for.</param>
    public Deck(string game)
        : this()
    {
        switch (game)
        {
            case "blackjack":
                this.NoJokersSingle();
                break;
            default:
                this.NoJokersSingle();
                break;
        }
    }

    /// <summary>
    /// Sets up a singe 52 card deck with, specifically with
    /// no jokers.
    /// </summary>
    public void NoJokersSingle()
    {
        for (int i = 0; i < 13; i++)
        {
            this.cards.Add(new Card("Clubs", i + 1));
            this.cards.Add(new Card("Diamonds", i + 1));
            this.cards.Add(new Card("Hearts", i + 1));
            this.cards.Add(new Card("Spades", i + 1));
        }
    }

    /// <summary>
    /// Adds a card to the deck.
    /// </summary>
    /// <param name="card">Card to be added to the deck.</param>
    public void AddCard(Card card)
    {
        this.cards.Add(card);
    }

    /// <summary>
    /// Removes a card from the deck.
    /// </summary>
    /// <param name="card">Card to remove from the deck.</param>
    public void RemoveCard(Card card)
    {
        this.cards.Remove(card);
    }

    /// <summary>
    /// Shuffles the deck.
    /// </summary>
    public void Shuffle()
    {
        for (int i = 0; i < this.cards.Count; i++)
        {
            int randomIndex = Funcs.GetRandom().Next(i, this.cards.Count);
            Card temp = this.cards[i];
            this.cards[i] = this.cards[randomIndex];
            this.cards[randomIndex] = temp;
        }
    }

    /// <summary>
    /// Draws a card from the deck.
    /// </summary>
    /// <returns>Card that was drawn from the deck.</returns>
    public Card Draw()
    {
        Card card = this.cards[0];
        this.cards.RemoveAt(0);
        return card;
    }
}