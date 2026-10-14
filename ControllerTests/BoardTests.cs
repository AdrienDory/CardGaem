using CardGaem.Boards;
using CardGaem.Cards;
using CardGaem.Players;
using Controller.BoardCtrlr;

namespace ControllerTests;

public class BoardTests
{

    private Player GetPlayer(Pnb p)
    {
        return new Player(new Deck(Cards.ToList), 10, p, 5);
    }

    private Board GetBoard()
    {
        return new Board(GetPlayer(Pnb.P1), GetPlayer(Pnb.P2));
    }

    [Fact]
    public void AddNewPawn()
    {
        Board board = GetBoard();
        BoardController ctrlr = new BoardController(board, new MockInputIhm());

        ctrlr.AddNewPawn(Cards.ToList[0], new Coordinates(1, 1), Pnb.P1);

        Assert.Equal(Cards.ToList[0].Life, board[new Coordinates(1, 1)].Life);
    }

    [Fact]
    public void BoardNextTurnP1()
    {
        Board board = GetBoard();
        BoardController ctrlr = new BoardController(board, null);

        ctrlr.AddNewPawn(Cards.ToList[0], new Coordinates(1, 1), Pnb.P1);
        ctrlr.AddNewPawn(Cards.ToList[0], new Coordinates(2, 1), Pnb.P1);

        ctrlr.StartNextTurn(Pnb.P1);
        Assert.Equal(Cards.ToList[0].Life, board[new Coordinates(1, 2)].Life);
        Assert.Equal(Cards.ToList[0].Life, board[new Coordinates(2, 2)].Life);
    }

    [Fact]
    public void BoardNextTurnP2()
    {
        Board board = GetBoard();
        BoardController ctrlr = new BoardController(board, null);

        ctrlr.AddNewPawn(Cards.ToList[0], new Coordinates(1, 5), Pnb.P2);
        ctrlr.AddNewPawn(Cards.ToList[0], new Coordinates(2, 5), Pnb.P2);

        ctrlr.StartNextTurn(Pnb.P1);
        ctrlr.StartNextTurn(Pnb.P1.Other);
        Assert.Equal(Cards.ToList[0].Life, board[new Coordinates(1, 4)].Life);
        Assert.Equal(Cards.ToList[0].Life, board[new Coordinates(2, 4)].Life);
    }

    [Fact]
    public void BoardNbTurns()
    {
        Board board = GetBoard();
        BoardController ctrlr = new BoardController(board, null);

        for (int i = 1; i <= 150; i++)
        {
            Assert.Equal(i, ctrlr.NbTurns);

            ctrlr.StartNextTurn(Pnb.P1);
            ctrlr.StartNextTurn(Pnb.P2);
        }
    }
}
