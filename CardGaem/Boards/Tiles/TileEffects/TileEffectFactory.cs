using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards.Tiles.TileEffects.Implementations;

namespace CardGaem.Boards.Tiles.TileEffects;

public static class TileEffectFactory
{
    public static TileEffect GetTile(TEType type, Board board, Tile tile)
    {
        TileEffect effect = null;
        switch (type)
        {
            case TEType.Standard:
                effect = new NormalEffect(board, tile);
                break;
            case TEType.p1Base:
                effect = new P1BaseEffect(board, tile);
                break;
            case TEType.p2Base:
                effect = new P2BaseEffect(board, tile);
                break;
            case TEType.Hole:
                break;
        }

        return effect ?? throw new NotImplementedException($"there is no implementation for {type}");
    }
}
