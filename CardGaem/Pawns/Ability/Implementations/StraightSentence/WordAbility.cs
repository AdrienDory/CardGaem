using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Pawns;

namespace CardGaem.Pawns.Ability.Implementations.StraightSentence;

public abstract class WordAbility
{
    private IBoardFromAbility board;
    protected IBoardFromAbility Board => board;
    private Pawn pawn;
    protected Pawn Pawn => pawn;
    public WordAbility(IBoardFromAbility board, Pawn pawn) 
    {
        this.board = board;
        this.pawn = pawn;
    }
    
}
