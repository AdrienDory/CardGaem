using CardGaem.Boards;
using CardGaem.Cards;
using CardGaem.Pawns;
using CardGaem.Players;
using System.Collections.Generic;
using System.Text;

namespace CardGaemTests;

public class PawnTests
{
    [Fact]
    public void AtkTest()
    {
        Pawn p1 = new Pawn("p1", [], 5 ,new Coordinates(0, 0), Pnb.P1);
        Pawn p2 = new Pawn("p2", [], 4 ,new Coordinates(0, 0), Pnb.P2);

        Assert.True(p1.Atk(p2));
    }

    [Fact]
    public void ComparerTest()
    {
        Pawn p1 = new Pawn("p1", [], 5, new Coordinates(0, 8), Pnb.P1);
        Pawn p2 = new Pawn("p2", [], 4, new Coordinates(0, 7), Pnb.P1);
        Pawn p3 = new Pawn("p3", [], 5, new Coordinates(0, 0), Pnb.P1);
        Pawn p4 = new Pawn("p4", [], 4, new Coordinates(0, 2), Pnb.P1);

        List<Pawn> pawns = [p1, p2, p3, p4];

        pawns.Sort(new PawnReachComparer());

        List<Pawn> expected = [p3, p4, p2, p1];

        for(int i = 0; i < pawns.Count; i++)
        {
            Assert.Equal(pawns[i].Coordinates.Y, expected[i].Coordinates.Y);
        }
    }
}
