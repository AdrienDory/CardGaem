using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;

namespace CardGaem.Pawns;

public class PawnObservable
{
    private List<IPawnObserver> observers;
    protected List<IPawnObserver> Observers => observers;

    public PawnObservable()
    {
        this.observers = [];
    }

    public void AddObserver(IPawnObserver obs)
    {
        this.observers.Add(obs);
    }
    public void RemoveObserver(IPawnObserver obs)
    {
        this.observers.Remove(obs);
    }

    public void NotifyPawnSurvivedAnAtk(Pawn p, Coordinates coords)
    {
        foreach (IPawnObserver obs in observers)
        {
            obs.PawnSurvived(p, coords);
        }
    }


}
