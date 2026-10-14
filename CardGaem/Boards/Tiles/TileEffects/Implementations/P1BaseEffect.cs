using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Enumerations;
using CardGaem.Players;

namespace CardGaem.Boards.Tiles.TileEffects.Implementations;

public class P1BaseEffect : TileEffect
{
    public P1BaseEffect(Board board, Tile tile) : base(board, tile)
    {
    }

    public override void AddPawn(Pawn p)
    {
        Board.Players[Pnb.P1].Life -= p.Life;
        p.Update(PawnContext.Won);
    }
    /// <inheritdoc/>
    /// <summary>
    /// Dont do anything 
    /// </summary>
    /// <param name="p">the pawn that is leaving the tile</param>
    public override void RemovePawn(Pawn p)
    {
        //do nothing
    }
}
