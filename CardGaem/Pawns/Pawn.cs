using CardGaem.Abstractions;
using CardGaem.Boards;
using CardGaem.Cards;
using CardGaem.Pawns.Ability;
using CardGaem.Pawns.Ability.Enumerations;
using CardGaem.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGaem.Pawns;

public class Pawn : PawnObservable
{
    private static int AutoInc = 0;
    private static int NewId()
    {
        return ++AutoInc;
    }
    private int id;
    private Coordinates c;
    public Coordinates Coordinates
    {
        get => c;
        set => c = value;
    }
    private Pnb player;
    /// <summary>
    /// Which player it belongs
    /// </summary>
    public Pnb Player => player;

    private List<IAbility> abilities;
    public List<IAbility> Abilities => abilities;
    private string name;
    /// <summary>
    /// the name of the card
    /// </summary>
    public string Name => name;
    private int life;
    /// <summary>
    /// the lifepoints it has
    /// </summary>
    public int Life => life;

    public Pawn(string name, List<IAbility> ability, int life,Coordinates c, Pnb player)
    {
        this.id = NewId();
        this.player = player;
        this.name = name;
        this.abilities = ability;
        this.life = life;
        this.c = c;
    }

    public Pawn(Card card, Coordinates coord, Pnb player)
    {
        this.id = NewId();
        this.player = player;
        this.name = card.Name;
        this.abilities = card.Abilities;
        this.life = card.Life;
        this.c = coord;
    }

    /// <summary>
    /// Checks if the cards needs to do smth
    /// </summary>
    /// <param name="c">why it needs to be updated</param>
    public void Update(PawnContext c)
    {
        foreach (IAbility ability in this.abilities)
        {
            ability.MayUseAbility(c);
        }
        switch(c)
        {
            case PawnContext.Won:
                bool isNotEmpty = Observers.Count != 0;
                int i = 0;
                while (isNotEmpty)
                {
                    Observers[0].PawnWon(this, this.Coordinates);
                    Observers.Remove(Observers[0]);
                    isNotEmpty = Observers.Count != 0;
                }
                break;
            case PawnContext.Killed:
                bool jsNotEmpty = Observers.Count != 0;
                while (jsNotEmpty)
                {
                    Observers[0].PawnKilled(this, this.Coordinates);
                    Observers.Remove(Observers[0]);
                    jsNotEmpty = Observers.Count != 0;
                }
                break;
        }
    }
    /// <summary>
    /// Attacks the selected card : trades their lifepoints togethers
    /// </summary>
    /// <param name="p">the card that 'this' will fight against</param>
    /// <returns>true if 'this' lives afterwards, false otherwise</returns>
    public bool Atk(Pawn p)
    {
        this.Update(PawnContext.Attacking);
        //the enemy's life
        int enLife = p.Life;
        p.Hurt(this.life, false);
        return this.Hurt(enLife, true);
    }

    /// <summary>
    /// Hurts the pawn (they like being hurt)
    /// </summary>
    /// <param name="amount">the life stole</param>
    /// <param name="isAtk">true if the pawn is attacking</param>
    /// <returns>true if 'this' survives, false if 'this' dies</returns>
    public bool Hurt(int amount, bool isAtk)
    {
        this.life -= amount;
        bool survived = life > 0;
        if (survived)
        {
            if (isAtk)
            {
                this.Update(PawnContext.AttackedNSurvived);
            }
            else
            { //no updates when survived an attack but a notification
                this.NotifyPawnSurvivedAnAtk(this, this.Coordinates);
            }            
        }
        else
        {
            this.Update(PawnContext.Killed);
        }
        return survived;
    }

    public override bool Equals(object? obj)
    {
        return obj is Pawn other && this.id == other.id;
    }
}
