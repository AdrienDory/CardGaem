using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Enumerations;
using CardGaem.Players;


namespace CardGaem.Pawns.Ability.Implementations.StraightSentence;

public static class FilterFactory
{
    public static Coordinates[] FilterIn(IBoardFromAbility board, Pawn summoner, Coordinates[] unfiltered, Filter[] filters)
    {
        Coordinates[] filtered = unfiltered.ToArray();
        foreach (Filter filter in filters)
        {
            filtered = FilterIn(board, summoner, filtered, filter);
        }
        return filtered;
    }

    public static Coordinates[] FilterIn(IBoardFromAbility board, Pawn summoner, Coordinates[] unfiltered, Filter filter = Filter.None)
    {
        Coordinates[] filtered = [];
        switch (filter)
        {
            case Filter.None:
                filtered = unfiltered;
                break;
            case Filter.Allies:
                filtered = FilterInPlayer(unfiltered, summoner.Player, board);
                break;
            case Filter.Enemies:
                filtered = FilterInPlayer(unfiltered, summoner.Player.Other, board);
                break;
            case Filter.HigherLifeThanPawn:
                filtered = FilterInWithOperation(unfiltered, (c) => board[c]?.Life > summoner.Life);
                break;
            case Filter.LowerLifeThanPawn:
                filtered = FilterInWithOperation(unfiltered, (c) => board[c]?.Life < summoner.Life);
                break;
        }
        return filtered;
    }

    private static Coordinates[] FilterInWithOperation(Coordinates[] unfiltered, Func<Coordinates, bool> operation)
    {
        List<Coordinates> filtered = [];
        foreach (Coordinates c in unfiltered)
        {
            if (operation.Invoke(c))
                filtered.Add(c);
        }
        return filtered.ToArray();
    }

    private static Coordinates[] FilterInPlayer(Coordinates[] unfiltered, Pnb player, IBoardFromAbility board)
    {
        List<Coordinates> filtered = [];
        foreach (Coordinates c in unfiltered)
        {
            if (board[c]?.Player == player)
                filtered.Add(c);
        }
        return filtered.ToArray();
    }
}
