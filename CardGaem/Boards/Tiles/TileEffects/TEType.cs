using System;
using System.Collections.Generic;
using System.Text;

namespace CardGaem.Boards.Tiles.TileEffects;

public enum TEType
{
    /// <summary>
    /// just a normal tile
    /// </summary>
    Standard,
    /// <summary>
    /// the base of player two
    /// </summary>
    p1Base,
    /// <summary>
    /// the base of player one
    /// </summary>
    p2Base,
    /// <summary>
    /// Hole that kills the pawn entering
    /// </summary>
    Hole
}
