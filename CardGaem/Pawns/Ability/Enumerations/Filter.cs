namespace CardGaem.Pawns.Ability.Enumerations;
public enum Filter
{
    /// <summary>
    /// does not filter out anything
    /// </summary>
    None,
    /// <summary>
    /// Filters in the coordinates of the pawns allied to the pawn summoning the ability
    /// </summary>
    Allies,
    /// <summary>
    /// Filters in the coordinates of the pawns enemy to the pawn summoning the ability
    /// </summary>
    Enemies,
    /// <summary>
    /// Filters in the coordinates of the pawns who has higher HP compared to the pawn summoning the ability
    /// </summary>
    HigherLifeThanPawn,
    /// <summary>
    /// Filters in the coordinates of the pawns who has lower HP compared to the pawn summoning the ability
    /// </summary>
    LowerLifeThanPawn 
}
