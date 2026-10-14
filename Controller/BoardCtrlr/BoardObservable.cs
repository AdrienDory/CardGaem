using CardGaem.Boards;
using CardGaem.Pawns;
using CardGaem.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace Controller.BoardCtrlr;

public abstract class BoardObservable : IPawnObserver
{
    private List<IBoardObserver> observers;

    protected List<IBoardObserver> Observers
    {
        get => observers;
        set => observers = value;
    }

    protected BoardObservable()
    {
        this.observers = [];
    }

    public void AddObserver(IBoardObserver obs)
    {
        this.observers.Add(obs);
    }

    public void RemoveObserver(IBoardObserver obs)
    {
        this.observers.Remove(obs);
    }

    protected void NotifyPawnMoved(Pawn pawn, Coordinates newCoords)
    {
        foreach (IBoardObserver obs in observers)
        {
            obs.PawnMoved(pawn, newCoords);
        }
    }

    protected void NotifyPawnAdded(Pawn pawn, Coordinates coords)
    {
        foreach (IBoardObserver obs in observers)
        {
            obs.PawnAdded(pawn, coords);
        }
    }

    public void PawnKilled(Pawn pawn, Coordinates coords)
    {
        foreach (IBoardObserver obs in observers)
        {
            obs.PawnKilled(pawn, coords);
        }
    }

    public void PawnWon(Pawn pawn, Coordinates coords)
    {
        foreach (IBoardObserver obs in observers)
        {
            obs.PawnWon(pawn, coords);
        }
    }
    public void PawnSurvived(Pawn pawn, Coordinates coords)
    {
        foreach (IBoardObserver obs in observers)
        {
            obs.PawnSurvived(pawn, coords);
        }
    }
    protected void NotifyUpdatePlayerReach(int reach, Pnb p)
    {
        foreach (IBoardObserver obs in observers)
        {
            obs.UpdatePlayerReach(reach, p);
        }
    }


}
