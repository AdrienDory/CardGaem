using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Pawns.Ability.Enumerations;
using CardGaem.Players;

namespace CardGaem.Pawns.Ability.Implementations.StraightSentence;
/// <summary>
/// Static target factory
/// </summary>
public static class TargetFactory
{
    /// <summary>
    /// Gets coordinates from a 'Targets' enumeration
    /// </summary>
    /// <param name="targets">which list of target it will be</param>
    /// <param name="pawnC">the position of the pawn from which the result will be relative of</param>
    /// <param name="boardSize">the size of the board, as to not returns coordinates outside of it </param>
    /// <param name="nb">the pawn's parent, to get where the pawn should be facing</param>
    /// <returns>all the coordinates of the tiles that are available, dependent on the value of 'targets' choosed</returns>
    public static Coordinates[] GetTargets(Targets targets, Coordinates pawnC, Coordinates boardSize, Pnb nb)
    {
        Coordinates[] listTarget = [];
        switch (targets)
        {
            // ---- Neighboring tiles ---- \\
            case Targets.CloseNeighbors:
                listTarget = GetCloseNeighbors(pawnC);
                break;
            case Targets.Corners:
                listTarget = GetCorners(pawnC);
                break;
            case Targets.FullNeighbors:
                listTarget = GetCorners(pawnC).Concat(GetCloseNeighbors(pawnC)).ToArray();
                break;

                // ---- Lines ----
            case Targets.InFront:
                listTarget = GetLine(pawnC, new Coordinates(0, nb.Direction), boardSize);
                break;
            case Targets.Behind:
                listTarget = GetLine(pawnC, new Coordinates(0, -nb.Direction), boardSize);
                break;
            case Targets.Left:
                listTarget = GetLine(pawnC, new Coordinates(-nb.Direction, 0), boardSize);
                break;
            case Targets.Right:
                listTarget = GetLine(pawnC, new Coordinates(nb.Direction, 0), boardSize);
                break;

                // ---- single tiles ---- \\
            case Targets.This:
                listTarget = [pawnC];
                break;
            case Targets.FrontTile:
                listTarget = [Shift(pawnC, 0, nb.Direction)];
                break;
            case Targets.TileBehind:
                listTarget = [Shift(pawnC, 0, -nb.Direction)];
                break;
            case Targets.LeftTile:
                listTarget = [Shift(pawnC, -nb.Direction, 0)];
                break;
            case Targets.RightTile:
                listTarget = [Shift(pawnC, nb.Direction, 0)];
                break;

                // ---- Absolute ---- \\
            case Targets.FullBoard:
                listTarget = GetAll(boardSize);
                break;
        }
        return RemoveOutsideOfBoardCoordinates(listTarget, boardSize);
    }

    /// <summary>
    /// Creates a line of the available tiles from the og to the max
    /// </summary>
    /// <param name="og">the relative point the line starts from</param>
    /// <param name="dir">the vector between a tile and another</param>
    /// <param name="max">the limit of the board</param>
    /// <returns>the array of all the possible tiles until the end of the board</returns>
    private static Coordinates[] GetLine(Coordinates og, Coordinates dir, Coordinates max)
    {
        List<Coordinates> line = new List<Coordinates>();
        Coordinates iterative = Coordinates.Copy(og);
        while (iterative.IsInside(max) && iterative.IsPositive())
        {
            line.Add(Coordinates.Copy(iterative + dir));
        }
        return line.ToArray();
    }

    private static Coordinates[] RemoveOutsideOfBoardCoordinates(Coordinates[] coords, Coordinates board)
    {
        List<Coordinates> inside = new List<Coordinates>();
        foreach (Coordinates c in coords)
        {
            if (c.IsInside(board) && c.IsPositive())
            {
                inside.Add(c);
            }
        }
        return inside.ToArray();
    }

    private static Coordinates Shift(Coordinates og, int x, int y)
    {
        return new Coordinates(og.X + x, og.Y + y);
    }

    private static Coordinates[] GetCloseNeighbors(Coordinates c)
    {
        return [
            Shift(c, -1,  0),
            Shift(c,  0, -1),
            Shift(c, +1,  0),
            Shift(c,  0, +1)
            ];
    }

    private static Coordinates[] GetCorners(Coordinates c)
    {
        return [
            Shift(c, -1, -1),
            Shift(c, +1, -1),
            Shift(c, -1, +1),
            Shift(c, +1, +1)
            ];
    }

    private static Coordinates[] GetAll(Coordinates size)
    {
        List<Coordinates> all = new List<Coordinates>();

        for (int x = 0; x <= size.X; x++)
        {
            for (int y = 0; y <= size.Y; y++)
            {
                all.Add(new Coordinates(x, y));
            }
        }

        return all.ToArray();
    }
}
