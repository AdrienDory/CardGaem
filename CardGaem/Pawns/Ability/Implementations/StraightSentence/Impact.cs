using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Pawns;

namespace CardGaem.Pawns.Ability.Implementations.StraightSentence;

public abstract class Impact : WordAbility
{
    private int amount;
    public int Amount
    {
        get => amount;
        set => amount = value;
    }
    public Impact(IBoardFromAbility ctrlr, Pawn pawn) : base(ctrlr, pawn) { this.amount = 1; }

    /// <summary>
    /// Runs the Impact's Operation
    /// </summary>
    /// <param name="target">the coordinates of the target</param>
    public abstract void Run(Coordinates target, int amount);
}
