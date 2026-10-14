using CardGaem.Pawns;

namespace CardGaem.Boards;

public interface IBoardFromAbility
{
    /// <summary>
    /// all the pawns on the board
    /// </summary>
    /// <param name="c">the coordinates of the desired card</param>
    /// <returns>the pawn on the coords or null if there is not a pawn here</returns>
    Pawn? this[Coordinates c] { get; }

    /// <summary>
    /// all the pawns on the board
    /// </summary>
    Pawn[] AllPawns { get; }

    /// <summary>
    /// The size of the board
    /// </summary>
    Coordinates Size { get; }

    /// <summary>
    /// Gets the sum of all lifepoints of the pawns at the selected coordinates
    /// </summary>
    /// <param name="coordinates">the positions of all the targeted pawns</param>
    /// <returns>the sum of all targeted pawn's lifepoints</returns>
    int PawnsLifePointsSum(Coordinates[] coordinates);
}
