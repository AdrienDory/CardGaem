using System.ComponentModel;
using CardGaem.Boards;
using CardGaem.Cards;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Enumerations;
using CardGaem.Players;
using CardGaem.Abstractions;
using CardGaem.Pawns.Ability.Implementations.StraightSentence;

namespace Controller.BoardCtrlr;

public class BoardController : BoardObservable
{
    private int nbTurns;
    public int NbTurns => nbTurns;
    private Board board;
    private IInputBoard ihm;
    public IBoardFromAbility Board => board;

    public BoardController(Board board, IInputBoard ihm)
    {
        this.board = board;
        this.ihm = ihm;
        this.nbTurns = 1;
    }
    /// <summary>
    /// Adds a new pawn to the board
    /// </summary>
    /// <param name="card">the card played, summoning the pawn</param>
    /// <param name="c">the coordinates from the board</param>
    /// <param name="p">the player responsible for the birth of this new pawn</param>
    public Pawn AddNewPawn(Card card, Coordinates c, Pnb p)
    {
        Pawn pawn = new Pawn(card, c, p);
        pawn.AddObserver(this);

        this.board.AddNewPawn(pawn, c);
        this.NotifyPawnAdded(pawn, c);

        int direction = p.Direction;

        if (card.InitialMovement > 0)
        {
            //demander à la vue où aller
            Targets[] ways = [
                    Targets.FrontTile,
                    Targets.LeftTile,
                    Targets.RightTile
                ];
            
            this.ihm.AskUserForPawnMovement(ways, pawn, card.InitialMovement);
        }
        return pawn;
    }
    /// <summary>
    /// Move the pawn then notifies its observers
    /// </summary>
    /// <param name="p">the pawn y'all wanna move</param>
    /// <param name="newTile">the target coordinates where the pawn wanna go</param>
    public void MovePawn(Pawn p, Coordinates newTile)
    {
        this.board.MovePawn(p, p.Coordinates, newTile);
        this.NotifyPawnMoved(p, newTile);
    }

    /// <summary>
    /// Starts the next turn
    /// Calls all card to do their 'next turn thing' and then move them
    /// </summary>
    /// <param name="currentPlayer">the player </param>
    public void StartNextTurn(Pnb currentPlayer)
    {
        this.NotifyUpdatePlayerReach(board.UpdateReach(currentPlayer), currentPlayer);
        foreach (Pawn pawn in board.AllPawns) 
        {
            if (pawn.Player == currentPlayer)
            {
                int direction = currentPlayer.Direction;
                this.NotifyUpdatePlayerReach(board.UpdateReach(currentPlayer), currentPlayer);

                pawn.Update(PawnContext.NewTurn);

                Coordinates oldC = Coordinates.Copy(pawn.Coordinates);

                Coordinates newC = new Coordinates(pawn.Coordinates.X, pawn.Coordinates.Y + (1 * direction));

                this.board.MovePawn(pawn, oldC, newC);
                this.NotifyPawnMoved(pawn, newC);
            }
        }
        if (currentPlayer == Pnb.P1)
        {
            nbTurns++;
        }
    }
}
