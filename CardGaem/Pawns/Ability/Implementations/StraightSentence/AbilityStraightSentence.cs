using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Boards;
using CardGaem.Abstractions;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability;
using CardGaem.Pawns.Ability.Enumerations;
using CardGaem.Pawns.Ability.Implementations.StraightSentence.Choices;
using CardGaem.Pawns.Ability.Implementations.StraightSentence.Impacts;

namespace CardGaem.Pawns.Ability.Implementations.StraightSentence;

public class AbilityStraightSentence : IAbility
{
    private IInputBoard ihm;
    private IBoardFromAbility board;
    private Pawn pawn;
    private PawnContext trigger;
    private Impact impact;
    private int impactAmount;
    private Targets impactAmountModifier;
    private Targets targets;
    private TargetChoice choice;
    private Filter[] filter;

    public AbilityStraightSentence(IInputBoard ihm, IBoardFromAbility board, Pawn pawn, PawnContext trigger, ImpactType impact, int impactAmount, Targets targets, TargetChoice choice, Filter[] filter, Targets impactAmountModifier = Targets.None)
    {
        this.board = board;
        this.pawn = pawn;
        this.trigger = trigger;
        this.impact = ImpactFactory.GetImpact(impact, board, pawn);
        this.impactAmount = impactAmount;
        this.targets = targets;
        this.choice = choice;
        this.filter = filter;
        this.impactAmountModifier = impactAmountModifier;
        this.ihm = ihm;
    }


    public void MayUseAbility(PawnContext c)
    {
        if (c == trigger)
        {
            int amount = this.impactAmount + board.PawnsLifePointsSum(TargetFactory.GetTargets(impactAmountModifier, pawn.Coordinates, board.Size, pawn.Player));
            this.impact.Amount = amount;
            Choice choice = ChoiceFactory.GetChoice(this.choice, board, pawn, ihm);
            choice.Run(this.impact, FilterFactory.FilterIn(board, pawn, TargetFactory.GetTargets(this.targets, pawn.Coordinates, board.Size, pawn.Player), filter));



        }
    }
}
