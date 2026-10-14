using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards.Tiles.TileEffects;
using CardGaem.Pawns;

namespace CardGaem.Boards.Tiles;

public class Tile
{
    private Board board;
    protected Board Board => board;
    private Pawn? pawn;
    /// <summary>
    /// pls dont use the setter when moving pawns around the board
    /// </summary>
    public Pawn? Pawn
    {
        get => pawn;
        set => pawn = value;
    }
    private Dictionary<TEType, TileEffect> effects;
    public TEType[] Effects => effects.Keys.ToArray();
    public Tile(Board board, TEType initialType = TEType.Standard)
    {
        this.board = board;
        this.effects = new Dictionary<TEType, TileEffect>();
        this.AddEffect(initialType);
    }
    public void AddPawn(Pawn p)
    {
        foreach (TileEffect effect in effects.Values)
        {
            effect.AddPawn(p);
        }
    }
    public void RemovePawn(Pawn p)
    {
        foreach (TileEffect effect in effects.Values)
        {
            effect.RemovePawn(p);
        }
    }

    public void AddEffect(TEType type)
    {
        this.effects.Add(type, TileEffectFactory.GetTile(type, this.board, this));
    }
}
