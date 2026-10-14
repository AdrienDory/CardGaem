using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CardGaem.Players;

public enum Pnb
{
    P1,
    P2
}
public static class PlayersNbExtension
{
    extension (Pnb nb)
    {
        /// <summary>
        /// Returns the other player (p1 => p2 & p2 => p1)
        /// </summary>
        public Pnb Other => nb == Pnb.P1
            ? Pnb.P2
            : Pnb.P1;

        /// <summary>
        /// Returns the direction towards the enemy base
        /// </summary>
        public int Direction => nb == Pnb.P1
            ?  1
            : -1;

    }
}
