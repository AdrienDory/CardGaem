using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Implementations.StraightSentence;
using CardGaem.Abstractions;


namespace CardGaem.Pawns.Ability.Implementations.StraightSentence.Choices;

public class ChoiceChooseEach : Choice
{
    public ChoiceChooseEach(IBoardFromAbility board, Pawn pawn, IInputBoard ihm) : base(board, pawn, ihm)
    {
    }

    public override void Run(Impact impact, Coordinates[] targets)
    {
        //Ask for each impact the coordinates
    }
}
