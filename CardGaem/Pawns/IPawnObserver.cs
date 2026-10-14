using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;

namespace CardGaem.Pawns;

public interface IPawnObserver
{
    /// <summary>
    /// Notifies that the pawn was killed
    /// </summary>
    /// <param name="pawn">the killed pawn</param>
    /// <param name="Coords">the coordinates of the death</param>
    void PawnKilled(Pawn pawn, Coordinates Coords);
    void PawnWon(Pawn pawn, Coordinates Coords);
    void PawnSurvived(Pawn pawn, Coordinates Coords);
}
