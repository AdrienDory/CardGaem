using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Enumerations;
using CardGaem.Pawns.Ability.Implementations.StraightSentence;
using CardGaem.Abstractions;

namespace CardGaem.Pawns.Ability.Implementations.StraightSentence.Choices;

public static class ChoiceFactory
{
    public static Choice GetChoice(TargetChoice _enum, IBoardFromAbility board, Pawn pawn, IInputBoard ihm)
    {
        Choice choice = null;
        switch (_enum)
        {
            case TargetChoice.Choose1:

                break;
            case TargetChoice.All:
                choice = new ChoiceAll(board, pawn, ihm);
                break;
            case TargetChoice.RandomlyChoosed:

                break;
            case TargetChoice.RandomlyDistributed:

                break;
            case TargetChoice.ChooseEach:
                choice = new ChoiceChooseEach(board, pawn, ihm);
                break;
        }
        return choice ?? throw new Exception("wtf how did you do that, did u forget to implement a choice from the choicefactory ???");
    }
}
