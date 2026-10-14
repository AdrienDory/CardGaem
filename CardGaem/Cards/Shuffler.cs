using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CardGaem.Cards;
/// <summary>
/// Randomizer
/// </summary>
public class Shuffler
{
    private int seed;
    private Random rand;
    public int Seed => seed;
    public Shuffler()
    {
        this.seed = new Random().Next();
        this.rand = new Random(seed);
    }
    public void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count - 1; ++i)
        {
            int r = rand.Next(i, list.Count);
            (list[r], list[i]) = (list[i], list[r]);
        }   
    }
}
