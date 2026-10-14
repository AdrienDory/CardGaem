using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Pawns;

namespace CardGaem.Boards.Tiles.TileEffects.Implementations;

internal class NormalEffect : TileEffect
{
    public NormalEffect(Board board, Tile tile) : base(board, tile) { }

    public override void AddPawn(Pawn p)
    {
        this.ThisTile.Pawn = p;
    }

    public override void RemovePawn(Pawn p)
    {
        this.ThisTile.Pawn = null;
    }
}
