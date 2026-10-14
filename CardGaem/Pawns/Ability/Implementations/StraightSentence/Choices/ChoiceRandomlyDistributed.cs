using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Implementations.StraightSentence;
using CardGaem.Abstractions;


namespace CardGaem.Pawns.Ability.Implementations.StraightSentence.Choices;

public class ChoiceRandomlyDistributed : Choice
{
    public ChoiceRandomlyDistributed(IBoardFromAbility board, Pawn pawn, IInputBoard ihm) : base(board, pawn, ihm)
    {
    }

    public override void Run(Impact impact, Coordinates[] targets)
    {
        while (impact.Amount > 0)
        {
            impact.Run(targets[Random.Shared.Next(0, targets.Length + 1)], 1);
            impact.Amount -= 1;
        }
    }
}
