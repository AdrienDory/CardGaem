using CardGaem.Boards;
using CardGaem.Cards;
using CardGaem.Pawns;
using CardGaem.Players;

namespace CardGaemTests;

public class BoardTests
{
    private List<Card> cards =
    [
        new Card("One", [], 6, 1, 4),
        new Card("Two", [], 2, 1, 8),
        new Card("Three", [], 4, 1, 4),
        new Card("four", [], 4, 1, 4),
        new Card("five", [], 4, 1, 4),
    ];

    private Player GetPlayer(Pnb p)
    {
        return new Player(new Deck(cards), 10, p, 5);
    }

    [Fact]
    public void AddPawn()
    {
        Player p1 = GetPlayer(Pnb.P1);
        Player p2 = GetPlayer(Pnb.P2);
        Board board = new Board(p1, p2);

        Pawn pawn1 = new Pawn("1", [], 4, new Coordinates(1, 1), Pnb.P1);
        Pawn pawn2 = new Pawn("2", [], 4, new Coordinates(2, 1), Pnb.P1);

        board.AddNewPawn(pawn1, pawn1.Coordinates);
        board.AddNewPawn(pawn2, pawn2.Coordinates);

        Assert.Equal(pawn1, board[pawn1.Coordinates]);
        Assert.Equal(pawn2, board[pawn2.Coordinates]);
    }

    [Fact]
    public void ThrowOutOfBoardException()
    {
        Player p1 = GetPlayer(Pnb.P1);
        Player p2 = GetPlayer(Pnb.P2);
        Board board = new Board(p1, p2);

        Pawn pawn1 = new Pawn("1", [], 4, new Coordinates(6, 7), Pnb.P1);

        Assert.Throws<OutOfBoardException>(() =>
        {
            board.AddNewPawn(pawn1, pawn1.Coordinates);
        });
    }

    [Fact]
    public void ThrowAlreadyAPawnHereException()
    {
        Player p1 = GetPlayer(Pnb.P1);
        Player p2 = GetPlayer(Pnb.P2);
        Board board = new Board(p1, p2);

        Pawn pawn1 = new Pawn("1", [], 4, new Coordinates(1,1), Pnb.P1);
        Pawn pawn2 = new Pawn("2", [], 4, new Coordinates(1,1), Pnb.P1);

        board.AddNewPawn(pawn1, pawn1.Coordinates);

        Assert.Throws<AlreadyAPawnHereException>(() =>
        {
            board.AddNewPawn(pawn2, pawn2.Coordinates);
        });
    }

    [Fact]
    public void AtkPawnTest()
    {
        Player p1 = GetPlayer(Pnb.P1);
        Player p2 = GetPlayer(Pnb.P2);
        Board board = new Board(p1, p2);

        Pawn pawn1 = new Pawn("1", [], 4, new Coordinates(1, 1), Pnb.P1);
        Pawn pawn2 = new Pawn("2", [], 4, new Coordinates(1, 2), Pnb.P1);

        board.AddNewPawn(pawn1, pawn1.Coordinates);
        board.AddNewPawn(pawn2, pawn2.Coordinates);

        board.MovePawn(pawn2, pawn2.Coordinates, new Coordinates(pawn2.Coordinates.X, pawn2.Coordinates.Y-1));

        Assert.Empty(board.AllPawns);
    }
    [Fact]
    public void AtkPawnTestSurvived()
    {
        Player p1 = GetPlayer(Pnb.P1);
        Player p2 = GetPlayer(Pnb.P2);
        Board board = new Board(p1, p2);

        Pawn pawn1 = new Pawn("1", [], 4, new Coordinates(1, 1), Pnb.P1);
        Pawn pawn2 = new Pawn("2", [], 3, new Coordinates(1, 2), Pnb.P1);

        board.AddNewPawn(pawn1, pawn1.Coordinates);
        board.AddNewPawn(pawn2, pawn2.Coordinates);

        board.MovePawn(pawn2, pawn2.Coordinates, new Coordinates(pawn2.Coordinates.X, pawn2.Coordinates.Y - 1));

        Assert.Equal(1, pawn1.Life);
    }
}
