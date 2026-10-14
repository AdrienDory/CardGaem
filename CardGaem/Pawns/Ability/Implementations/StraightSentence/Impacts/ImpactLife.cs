using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Implementations.StraightSentence;

namespace CardGaem.Pawns.Ability.Implementations.StraightSentence.Impacts;

public class ImpactLife : Impact
{
    public ImpactLife(IBoardFromAbility ctrlr, Pawn pawn) : base(ctrlr, pawn)
    {
    }

    public override void Run(Coordinates target, int amount)
    {
        this.Board[target]?.Hurt(amount, false);
    }
}
