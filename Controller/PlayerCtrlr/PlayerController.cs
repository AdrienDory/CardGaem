using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Cards;
using CardGaem.Players;

namespace Controller.PlayerCtrlr;
public class NotEnougthManaException : Exception
{
    public NotEnougthManaException(string msg) : base(msg) { }
}
public class PlayerController : PlayerObservable
{
    private Player player;

    public PlayerController(Player player)
    {
        this.player = player;
    }
    /// <summary>
    /// Ends the turn for this player
    /// </summary>
    /// <returns> the current player having their turn </returns>
    public Pnb EndTurn()
    {
        this.player.Deck.ReloadHand();
        this.player.MaxMana++;
        this.player.Mana = this.player.MaxMana;
        NotifyStatsUpdated(player.Nb);
        NotifyHandReloaded(player.Nb);
        return player.Nb.Other;
    }
    /// <summary>
    /// Removes a card from the deck of this player
    /// </summary>
    /// <param name="card">the card played</param>
    /// <param name="nb"></param>
    /// <exception cref="NotEnougthManaException">is thrown when trying to place a card with not enough mana</exception>
    public void UseCardFromHand(Card card)
    {
        if (card.ManaCost > this.player.Mana)
        {
            throw new NotEnougthManaException($"not enougth mana, the card costs {card.ManaCost} mana");
        }
        this.player.Mana -= card.ManaCost;
        NotifyStatsUpdated(this.player.Nb);
        this.player.Deck.UseCardFromHand(card);
    }
}
