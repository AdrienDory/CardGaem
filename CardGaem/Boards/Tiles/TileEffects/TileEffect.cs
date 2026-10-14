using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Pawns;

namespace CardGaem.Boards.Tiles.TileEffects;
/// <summary>
/// Abstraction of the different effects a tile can have
/// </summary>
public abstract class TileEffect
{
    private Tile thisTile;
    private Board board;
    protected Tile ThisTile => this.thisTile;
    protected Board Board => this.board;
    public TileEffect(Board board, Tile tile)
    {
        this.thisTile = tile;
        this.board = board;
    }
    /// <summary>
    /// When a pawn walks on this tile
    /// </summary>
    /// <param name="p">the pawn that wants to be on this tile</param>
    public abstract void AddPawn(Pawn p);
    /// <summary>
    /// When a pawn leaves this tile
    /// </summary>
    /// <param name="p">the pawn that wants to leave this tile</param>
    public abstract void RemovePawn(Pawn p);
}
