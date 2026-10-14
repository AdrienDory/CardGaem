using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Abstractions;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Enumerations;

namespace ControllerTests;

internal class MockInputIhm : IInputBoard
{
    public void AskUserForPawnMovement(Targets[] options, Pawn p, int nbMovements)
    {
        //dont
    }
}
