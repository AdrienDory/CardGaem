using CardGaem.Boards;
using CardGaem.Pawns;
using CardGaem.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.BoardCtrlr;

public interface IBoardObserver
{
    /// <summary>
    /// Tells the view to move the pawn
    /// </summary>
    /// <param name="pawn"></param>
    /// <param name="coord"></param>
    void PawnMoved(Pawn pawn, Coordinates coord);
    /// <summary>
    /// tells the view to add the pawn
    /// </summary>
    /// <param name="pawn"></param>
    /// <param name="coord"></param>
    void PawnAdded(Pawn pawn, Coordinates coord);

    void PawnKilled(Pawn pawn, Coordinates coord);

    void PawnSurvived(Pawn pawn, Coordinates coord);
    /// <summary>
    /// Tells the view that the pawn won
    /// </summary>
    /// <param name="pawn"></param>
    /// <param name="coord"></param>
    void PawnWon(Pawn pawn, Coordinates coord);

    void UpdatePlayerReach(int reach, Pnb player);

}
