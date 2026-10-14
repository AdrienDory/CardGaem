using System;
using System.Collections.Generic;
using System.Text;
using CardGaem.Cards;

namespace ControllerTests;

public static class Cards
{
    public static List<Card> ToList => new List<Card>()
    {
        new Card("One", [], 0, 0, 4),
        new Card("Two", [], 8, 8, 8),
        new Card("Three", [], 4, 1, 4),
        new Card("four", [], 4, 1, 4)
    }.ToList();
}
