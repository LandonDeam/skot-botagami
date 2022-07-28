using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Blackjack
{
    private List<Card> player;
    private List<Card> dealer;
    private Card dealerFirst;
    private Deck deck;

    public Blackjack()
    {
        player = new List<Card>();
        dealer = new List<Card>();
        deck = new Deck();
        deck.shuffle();
    }

    public void deal()
    {
        player.Add(deck.draw());
        dealerFirst = deck.draw();
        dealer.Add(dealerFirst);
        player.Add(deck.draw());
        dealer.Add(deck.draw());
    }

    public void playerHit()
    {
        player.Add(deck.draw());
    }

    public void dealerHit()
    {
        dealer.Add(deck.draw());
    }

    public void playerStand()
    {
        while (dealerHandValue() < 21 && (dealerHandValue() < playerHandValue() || (dealerHandValue == playerHandValue && dealerHandValue() < 12)))
        {
            dealerHit();
        }
    }

    private int getHandValue(List<Card> hand)
    {
        int value = 0;
        foreach (Card card in hand)
        {
            if (card.getValue() > 10) value += 10;
            else value += card.getValue();
        }
        if (value < 12 && hand.Any(x => x.getValue() == 1)) value += 10;
        return value;
    }

    private int playerHandValue()
    {
        return getHandValue(player);
    }

    private int dealerHandValue()
    {
        return getHandValue(dealer);
    }

    public string getDealerFirst()
    {
        return dealerFirst.ToString();
    }

    public string dealerHand()
    {
        string temp = $"Dealer ({dealerHandValue()}): ";
        foreach (Card card in dealer)
        {
            temp += card.ToString() + " ";
        }
        return temp;
    }

    public string playerHand()
    {
        string temp = $"Player ({playerHandValue}): ";
        foreach (Card card in player)
        {
            temp += card.ToString() + " ";
        }
        return temp;
    }
}
