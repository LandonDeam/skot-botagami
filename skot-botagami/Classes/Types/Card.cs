using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Card
{
    private string suit;
    private int value;

    public Card(string suit, int value)
    {
        this.suit = suit;
        this.value = value;
    }

    public string getSuit()
    {
        return suit;
    }

    public int getValue()
    {
        return value;
    }

    private string getRank()
    {
        string temp = "ERROR: unknown";

        switch (value)
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
                temp = value.ToString();
                break;
        }

        return temp;
    }

    public override string ToString()
    {
        return $"{getRank()} of {suit}";
    }
}
