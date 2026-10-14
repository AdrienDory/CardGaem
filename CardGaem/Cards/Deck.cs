using System;
using System.Collections.Generic;
using System.Text;

namespace CardGaem.Cards;

public class Deck
{
    private List<Card> allCards;
    private List<Card> deck;
    private Shuffler shuffler;
    private int handSize;
    /// <summary>
    /// the maximum number of cards in the hand
    /// is the minimum value between four and the number of cards in the deck
    /// </summary>
    public int HandSize
    {
        get => handSize;
        set => handSize = value;
    }
    private List<Card> hand;
    /// <summary>
    /// The hand to play
    /// </summary>
    public List<Card> Hand => this.hand;
    public Deck(List<Card> allCard)
    {
        if (allCard.Count == 0) 
            throw new Exception("the Deck is empty, its not a deck");
        this.shuffler = new Shuffler();
        this.allCards = allCard;
        this.deck = allCard;
        //size of hand is equal by default to four,
        //or the number of cards in the deck
        //if the deck is lower than four
        this.handSize = Math.Min(4, allCard.Count);
        this.hand = [];
        this.AllCardsShuffle();
        this.ReloadHand();
    }
    /// <summary>
    /// Removes the card from the current hand
    /// </summary>
    /// <param name="c"></param>
    public void UseCardFromHand(Card c)
    {
        this.hand.Remove(c);
        this.deck.Add(c);
    }
    /// <summary>
    /// Reload the hand from cards from the deck
    /// </summary>
    public void ReloadHand()
    {
        for (int i = this.hand.Count; i < handSize; i++)
        {
            this.hand.Add(this.deck.First());
            this.deck.Remove(this.deck.First());
        }
    }
    private void AllCardsShuffle()
    {
        this.shuffler.Shuffle<Card>(this.allCards);
    }
}
