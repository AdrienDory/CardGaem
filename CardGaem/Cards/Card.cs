using CardGaem.Pawns.Ability;
using System.Data;

namespace CardGaem.Cards;

/// <summary>
/// a card
/// </summary>
public class Card
{
    private List<IAbility> abilities;
    public List<IAbility> Abilities => abilities;
    private string name;
    /// <summary>
    /// the name of the card
    /// </summary>
    public string Name => name;
    private int manaCost;
    /// <summary>
    /// the cost of summoning this card
    /// </summary>
    public int ManaCost => manaCost;
    private int initialMovement;
    /// <summary>
    /// how much the card will move when first placed on the board
    /// </summary>
    public int InitialMovement => initialMovement;
    private int life;
    /// <summary>
    /// the lifepoints it has
    /// </summary>
    public int Life => life;

    public Card(string name, List<IAbility> ability, int mana, int mov, int life)
    {
        this.name = name;
        this.abilities = ability;
        this.manaCost = mana;
        this.initialMovement = mov;
        this.life = life;
    }

    public override string ToString()
    {
        return $"{name} \n {life}♥ \t{manaCost}⍚ \t{initialMovement}🢂";
    }
}
