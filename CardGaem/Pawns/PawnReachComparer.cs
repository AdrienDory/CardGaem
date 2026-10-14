using System;
using System.Collections.Generic;
using System.Text;

namespace CardGaem.Pawns;

public class PawnReachComparer : IComparer<Pawn>
{
    public int Compare(Pawn p1, Pawn p2)
    {
        return p1.Coordinates.Y - p2.Coordinates.Y;
    }
}
