using CardGaem.Players;
using Controller.BoardCtrlr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.PlayerCtrlr;

public class PlayerObservable
{
    private List<IPlayerObserver> observers;
    public PlayerObservable()
    {
        this.observers = [];
    }

    public void AddObserver(IPlayerObserver obs)
    {
        this.observers.Add(obs);
    }
    public void RemoveObserver(IPlayerObserver obs)
    {
        this.observers.Remove(obs);
    }

    public void NotifyStatsUpdated(Pnb pNb)
    {
        foreach(IPlayerObserver obs in observers)
        {
            obs.StatsUpdated(pNb);
        }
    }

    public void NotifyHandReloaded(Pnb nb)
    {
        foreach (IPlayerObserver obs in observers)
        {
            obs.HandReloaded(nb);
        }
    }
}
