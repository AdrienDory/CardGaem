using CardGaem;
using CardGaem.Cards;
using System.Diagnostics.CodeAnalysis;

namespace CardGaemTests;

public class DeckTests
{
    private List<Card> allCards3 = [
                new Card("One",[],6,1,4),
                new Card("Two",[],2,1,8),
                new Card("Three",[],4,1,4),
            ];
    private List<Card> allCards5 = [
                new Card("One",[],6,1,4),
                new Card("Two",[],2,1,8),
                new Card("Three",[],4,1,4),
                new Card("four",[],4,1,4),
                new Card("five",[],4,1,4),
            ];
    private Deck GetDeck(List<Card> cards)
    {
        return new Deck(cards);
    }

    [Fact]
    public void UseTests()
    {
        Deck deck = this.GetDeck(allCards3.ToList());
        deck.UseCardFromHand(allCards3[0]);
        Assert.Null(deck.Hand.Find(c => c.Name == allCards3[0].Name));
    }

    [Fact]
    public void ReloadTest()
    {
        //five card deck
        Deck deck = this.GetDeck(allCards5.ToList());
        //many cards gets a copy of the hand
        List<Card> manyCards = deck.Hand.ToList();
        //we use the { One } card
        deck.UseCardFromHand(allCards5[0]);
        //we reload the hand from the deck
        deck.ReloadHand();
        //we add to manycards the last card from the deck
        //which will never be { One }
        manyCards.Add(deck.Hand.Last());
        //we check if we dont have any duplicates
        Assert.Distinct<Card>(manyCards);
        //for each card of the many card
        //we should have the deck
        foreach (Card c in manyCards)
        {
            Assert.Contains<Card>(c, allCards5);
        }
    }
}
