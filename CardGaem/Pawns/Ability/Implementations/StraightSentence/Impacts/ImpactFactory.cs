using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Enumerations;
using CardGaem.Pawns.Ability.Implementations.StraightSentence;

namespace CardGaem.Pawns.Ability.Implementations.StraightSentence.Impacts;

public static class ImpactFactory
{
    public static Impact GetImpact(ImpactType type, IBoardFromAbility ctrlr, Pawn pawn)
    {
        Impact impact = null;
        switch (type)
        {
            case ImpactType.Movement:

                break;
            case ImpactType.Life:

                break;
            case ImpactType.Mana:

                break;
        }
        return impact ?? throw new NotImplementedException("the impact is not yet set");
    }
}
