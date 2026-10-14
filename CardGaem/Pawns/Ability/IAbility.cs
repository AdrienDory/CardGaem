using CardGaem.Pawns.Ability.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGaem.Pawns.Ability;

public interface IAbility
{
    void MayUseAbility(PawnContext c);
}
