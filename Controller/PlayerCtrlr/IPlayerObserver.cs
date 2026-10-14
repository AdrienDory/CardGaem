using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Players;

namespace Controller.PlayerCtrlr;

public interface IPlayerObserver
{
    /// <summary>
    /// The stats have been updated
    /// </summary>
    /// <param name="pNb">the player whose stats have been updated</param>
    void StatsUpdated(Pnb pNb);
    /// <summary>
    /// Asks the observer to update the hand
    /// </summary>
    /// <param name="nb">the player who reloaded their hand from their deck</param>
    void HandReloaded(Pnb nb);
}
