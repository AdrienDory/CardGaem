using System;
using System.Collections.Generic;
using System.Text;

namespace CardGaem.Pawns.Ability.Enumerations;

public enum Targets //faire les diagionales plus tard
{
    /// <summary>
    /// Litterally no one
    /// </summary>
    None,
    /// <summary>
    /// the tiles on the vertices of this tile
    /// </summary>
    CloseNeighbors,
    /// <summary>
    /// the tiles on the corners of this tile
    /// </summary>
    Corners,
    /// <summary>
    /// The tiles on the vertices and corners of this tile
    /// </summary>
    FullNeighbors,
    /// <summary>
    /// only this tile
    /// </summary>
    This,
    /// <summary>
    /// in front of this tile (depends on the player using this ability)
    /// </summary>
    InFront,
    /// <summary>
    /// the tiles behind this tile (from the player's perspective)
    /// </summary>
    Behind,
    /// <summary>
    /// the tiles at the left of this tile (facing the enemy)
    /// </summary>
    Left,
    /// <summary>
    /// the tiles at the right of this tile (looking opposite to this player's base)
    /// </summary>
    Right,
    /// <summary>
    /// The first tile in front of this tile (facing the enemy)
    /// </summary>
    FrontTile,
    /// <summary>
    /// The first tile behind this tile (facing the enemy)
    /// </summary>
    TileBehind,
    /// <summary>
    /// The first tile at the left of this tile (facing the enemy)
    /// </summary>
    LeftTile,
    /// <summary>
    /// The first tile at the right of this tile (facing the enemy)
    /// </summary>
    RightTile,
    /// <summary>
    /// All the tiles on the board
    /// </summary>
    FullBoard
}
