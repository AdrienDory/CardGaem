using CardGaem.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGaem.Players;

public class Player
{
    private Pnb nb;
    /// <summary>
    /// Either player1 or player2
    /// </summary>
    public Pnb Nb => nb;

    private int life;
    /// <summary>
    /// Lifepoints of the player
    /// </summary>
    public int Life
    {
        get => life;
        set => life = value;
    }
    private int reach;
    /// <summary>
    /// How far can the player put cards
    /// </summary>
    public int Reach
    {
        get => reach;
        set => reach = value;
    }
    private int mana;
    public int Mana
    {
        get => mana;
        set => mana = value;
    }
    private int maxMana;
    public int MaxMana
    {
        get => maxMana;
        set => maxMana = value;
    }
    private Deck deck;
    /// <summary>
    /// All cards of the player
    /// </summary>
    public Deck Deck => deck; //Might be changed with fuckery later


    public Player(Deck deck, int life, Pnb nbplayer, int startingMana)
    {
        this.deck = deck;
        this.life = life;
        this.nb = nbplayer;
        this.reach = 1;
        this.maxMana = startingMana;
        this.mana = maxMana;
    }

}
