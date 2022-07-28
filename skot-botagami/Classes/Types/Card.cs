// <copyright file="Card.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class used for creating cards for use in a deck.
/// </summary>
public class Card
{
    private string suit;
    private int value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Card"/> class.
    /// </summary>
    /// <param name="suit">Suit of the card.</param>
    /// <param name="value">Value/Rank of the card.</param>
    public Card(string suit, int value)
    {
        this.suit = suit;
        this.value = value;
    }

    /// <summary>
    /// Gets the suit of the card.
    /// </summary>
    /// <returns>Suit of the card.</returns>
    public string GetSuit()
    {
        return this.suit;
    }

    /// <summary>
    /// Gets the stored value of the card.
    /// </summary>
    /// <returns>Stored value of the card.</returns>
    public int GetValue()
    {
        return this.value;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{this.GetRank()} of {this.suit}";
    }

    /// <summary>
    /// Gets the rank of the card.
    /// </summary>
    /// <returns>Rank of the card.</returns>
    private string GetRank()
    {
        string temp = "ERROR: unknown";

        switch (this.value)
        {
            case 1:
                temp = "Ace";
                break;
            case 11:
                temp = "Jack";
                break;
            case 12:
                temp = "Queen";
                break;
            case 13:
                temp = "King";
                break;
            case 14:
                temp = "Joker";
                break;
            default:
                temp = this.value.ToString();
                break;
        }

        return temp;
    }
}