using CardGaem.Cards;
using CardGaem.Players;
using Controller.PlayerCtrlr;

namespace ControllerTests;

public class PlayerTests
{
    [Fact]
    public void PlayCard()
    {
        Player player = new Player(new Deck(Cards.ToList), 5, Pnb.P1, 5);

        PlayerController ctrlr = new PlayerController(player);

        int mana = player.Mana;

        ctrlr.UseCardFromHand(Cards.ToList[0]);

        Assert.Equal(player.Mana, mana - Cards.ToList[0].ManaCost);
        Assert.DoesNotContain<Card>(Cards.ToList[0], player.Deck.Hand);
    }

    [Fact]
    public void PlayCardNOTENOUGHMANA()
    {
        Player player = new Player(new Deck(Cards.ToList), 5, Pnb.P1, 5);

        PlayerController ctrlr = new PlayerController(player);

        Assert.Throws(new NotEnougthManaException("").GetType(), () => { ctrlr.UseCardFromHand(Cards.ToList[1]); });
    }

    [Fact]
    public void NextTurn()
    {
        int mana = 5;
        Player player = new Player(new Deck(Cards.ToList), 5, Pnb.P1, mana);

        PlayerController ctrlr = new PlayerController(player);

        Assert.Equal(Pnb.P2, ctrlr.EndTurn());
        Assert.Equal(mana + 1, player.Mana);
        Assert.Equal(player.Deck.HandSize, player.Deck.Hand.Count);
    }
}
