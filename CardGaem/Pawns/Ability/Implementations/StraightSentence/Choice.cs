using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Pawns;
using CardGaem.Abstractions;

namespace CardGaem.Pawns.Ability.Implementations.StraightSentence;

public abstract class Choice : WordAbility
{
    private IInputBoard ihm;
    protected IInputBoard Ihm
    {
        get => ihm;
    }
    public Choice(IBoardFromAbility board, Pawn pawn, IInputBoard ihm) : base(board, pawn)
    {
        this.ihm = ihm;
    }

    /// <summary>
    /// Runs the Ability
    /// </summary>
    /// <param name="impact">the impact of the ability</param>
    /// <param name="targets">the targets that will/might get impacted</param>
    public abstract void Run(Impact impact, Coordinates[] targets);

}
