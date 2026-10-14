using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Enumerations;

namespace CardGaem.Abstractions;
/// <summary>
/// Interface with functions the user will interact with
/// </summary>
public interface IInputBoard
{
    void AskUserForPawnMovement(Targets[] options, Pawn p, int nbMovements);

}
