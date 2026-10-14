using System;
using System.Collections.Generic;
using System.Text;

namespace CardGaem.Pawns.Ability.Enumerations;
/// <summary>
/// Context that might trigger the ability 
/// </summary>
public enum PawnContext
{
    /// <summary>
    /// When the pawn is first created
    /// </summary>
    Created,
    /// <summary>
    /// Each new turn
    /// </summary>
    NewTurn,
    /// <summary>
    /// Just before moving this pawn
    /// </summary>
    WillMove,
    /// <summary>
    /// After moving the pawn
    /// </summary>
    Moved,
    /// <summary>
    /// Before attacking another pawn (not when attacking the enemy's base)
    /// </summary>
    Attacking,
    /// <summary>
    /// After attacking and surviving
    /// </summary>
    AttackedNSurvived,
    /// <summary>
    /// After being killed
    /// </summary>
    Killed,
    /// <summary>
    /// when the pawn enters the enemy's base
    /// </summary>
    Won
}
