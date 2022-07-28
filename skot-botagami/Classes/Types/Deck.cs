using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Deck
{
    public List<Card> cards;
    public Deck()
    {
        cards = new List<Card>();
    }

    public Deck(string game)
    {
        switch(game)
        {
            case "blackjack":
                noJokersSingle();
                break;
            default:
                break;
        }
    }

    public void noJokersSingle()
    {
        for (int i = 1; i < 13; i++)
        {
            cards.Add(new Card("Clubs", i + 1));
            cards.Add(new Card("Diamonds", i + 1));
            cards.Add(new Card("Hearts", i + 1));
            cards.Add(new Card("Spades", i + 1));
        }
    }
    
    public void addCard(Card card)
    {
        cards.Add(card);
    }
    
    public void removeCard(Card card)
    {
        cards.Remove(card);
    }
    public void shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = Funcs.random.Next(i, cards.Count);
            Card temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }
    
    public Card draw()
    {
        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }
}