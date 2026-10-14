using System.Windows;
using System.Windows.Controls;
using CardGaem.Abstractions;
using CardGaem.Boards;
using CardGaem.Cards;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Enumerations;
using CardGaem.Pawns.Ability.Implementations.StraightSentence;
using CardGaem.Players;
using Controller.BoardCtrlr;
using Controller.PlayerCtrlr;
using Microsoft.VisualBasic.FileIO;

namespace View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IBoardObserver, IPlayerObserver, IInputBoard
{
    private Dictionary<Pnb, Player> players;
    private Dictionary<Pnb, PlayerController> playersCtrlr;
    private Dictionary<Pnb, Grid> viewHands;
    private Dictionary<Pnb, Grid> reaches;
    private Dictionary<Pnb, Label> Labels;
    private Dictionary<Pnb, BaseAtkButton> baseAtks;
    private List<TileButton> possibleTiles;
    private Dictionary<Pawn, TileLabel> pawnToLabel;
    private Pnb currentTurn;

    private CardButton? selectedCard;

    private Board boardModel;
    private BoardController BoardCtrlr;
    private List<Card> allCards5;

    public MainWindow()
    {
        this.pawnToLabel = [];

        this.allCards5 = [
                new Card("Wall of dudes",[],6,0,7),
                new Card("Duo",[],2,1,2),
                new Card("Four wet rats",[],4,1,4),
                new Card("Four oily birds",[],5,2,4),
                new Card("three dolls that wanna eat the creator of this game",[],3,1,3),
                new Card("one scared puppy",[],1,1,1)
            ];

        this.players = new Dictionary<Pnb, Player>()
        {
            { Pnb.P1, new Player(new Deck(allCards5.ToList()),10, Pnb.P1, 2) },
            { Pnb.P2, new Player(new Deck(allCards5.ToList()),10, Pnb.P2, 3) }
        };
        this.boardModel = new Board(players[Pnb.P1], players[Pnb.P2]);

        this.playersCtrlr = new Dictionary<Pnb, PlayerController>()
        {
            { Pnb.P1, new PlayerController(players[Pnb.P1]) },
            { Pnb.P2, new PlayerController(players[Pnb.P2]) }
        };
        this.BoardCtrlr = new BoardController(this.boardModel, this);

        this.BoardCtrlr.AddObserver(this);

        InitializeComponent();

        this.Labels = new Dictionary<Pnb, Label>()
        {
            { Pnb.P1, p1Label },
            { Pnb.P2, p2Label }
        };

        this.baseAtks = new Dictionary<Pnb, BaseAtkButton>()
        {
            { Pnb.P1, new BaseAtkButton(BoardCtrlr,Pnb.P1) },
            { Pnb.P2, new BaseAtkButton(BoardCtrlr,Pnb.P2) },
        };

        this.viewHands = new Dictionary<Pnb, Grid>()
        {
            { Pnb.P1, this.p1Cards },
            { Pnb.P2, this.p2Cards },
        };

        this.reaches = new Dictionary<Pnb, Grid>()
        {
            {Pnb.P1, this.p1ReachView },
            {Pnb.P2, this.p2ReachView }
        };

        // init both hands
        foreach (Pnb pNb in playersCtrlr.Keys)
        {
            playersCtrlr[pNb].AddObserver(this);

            viewHands[pNb].RowDefinitions.Add(new RowDefinition() { });
            UpdatePlayerStats(pNb);
            PrintHand(pNb);

            //init reach
            this.reaches[pNb].Background = ColorHelper.ColorFromPlayer(pNb, 125);
            this.UpdatePlayerReach(this.players[pNb].Reach, pNb);
        }
        this.possibleTiles = [];

        currentTurn = Pnb.P1;
        this.selectedCard = null;
    }


    private void PrintHand(Pnb pNb)
    {
        viewHands[pNb].Children.Clear();
        viewHands[pNb].Children.Add(Labels[pNb]);
        for (int i = 0; i < players[pNb].Deck.Hand.Count; i++)
        {
            viewHands[pNb].RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        
            CardButton cBtn = new CardButton(players[pNb].Deck.Hand[i], i + 1, pNb);
            cBtn.Click += CardIsClicked;

            viewHands[pNb].Children.Add(cBtn);
        }
    }

    private void CardIsClicked(object sender, RoutedEventArgs e)
    {
        selectedCard?.UnSelected();
       
        if (sender is CardButton card)
        {
            if (card.Nb == currentTurn)
            {
                if (card == selectedCard)
                {
                    card.UnSelected();
                    this.selectedCard = null;
                    HidePossibleTiles();
                }
                else
                {
                    card.Selected();
                    this.selectedCard = card;
                    ShowPossibleCardPlacementTiles(card.Nb);
                }
            }
        }
    }
    private void HidePossibleTiles()
    {
        foreach (TileButton tile in possibleTiles)
        {
            this.gridBoard.Children.Remove(tile);
        }
        this.possibleTiles.Clear();
    }
    private void ShowPossibleCardPlacementTiles(Pnb nb)
    {

        for (int i = 0; i < 4; i++)
        {
            if (nb == Pnb.P1)
            {
                for (int j = 1; j < players[nb].Reach + 1; j++)
                {
                    AddNewPossibleCardPlacementTile(i, j);
                }
            }
            else
            {
                for (int j = 5; j > 5 - players[nb].Reach; j-- )
                {
                    AddNewPossibleCardPlacementTile(i, j);
                }
            }
        }
    }

    private void AddNewPossibleCardPlacementTile(int i, int j)
    {
        if (boardModel[new Coordinates(i, j)] == null)
        {
            TileButton tile = new TileButton(new Coordinates(i, j));
            gridBoard.Children.Add(tile);

            Grid.SetRow(tile, i);
            Grid.SetColumn(tile, j - 1);

            possibleTiles.Add(tile);

            tile.Click += AddNewPawnOnTile;
        }
    }

    private void AddNewPawnOnTile(object sender, RoutedEventArgs e)
    {
        try
        {
            if (sender is TileButton tile)
            {
                this.playersCtrlr[selectedCard!.Nb].UseCardFromHand(selectedCard.Model);
                this.BoardCtrlr.AddNewPawn(selectedCard.Model, tile.C, selectedCard.Nb);
                PrintHand(selectedCard.Nb);
            }
        }
        catch (NotEnougthManaException _e)
        {
            MessageBox.Show(_e.Message, "Erreur");
        }
    }

    public void PawnMoved(Pawn pawn, Coordinates coord)
    {
        if (coord.Y != 0)
        {
            Grid.SetColumn(pawnToLabel[pawn], coord.Y - 1);
        }
        else
        {
            gridBoard.Children.Remove(pawnToLabel[pawn]);
        }
        Grid.SetRow(pawnToLabel[pawn], coord.X);
        pawnToLabel[pawn].C = coord;
        pawnToLabel[pawn].UpdateContent();
        this.UpdatePlayerReach(players[pawn.Player].Reach, pawn.Player);
    }

    public void PawnAdded(Pawn pawn, Coordinates coord)
    {
        TileLabel newTile = new TileLabel(coord, pawn);
        newTile.Pawn = pawn;
        this.pawnToLabel.Add(pawn, newTile);
        gridBoard.Children.Add(newTile);

        Grid.SetColumn(pawnToLabel[pawn], coord.Y - 1);
        Grid.SetRow(pawnToLabel[pawn], coord.X);

        pawnToLabel[pawn].UpdateContent();
        HidePossibleTiles();
    }

    public void PawnKilled(Pawn pawn, Coordinates coord)
    {
        gridBoard.Children.Remove(pawnToLabel[pawn]);
    }

    public void PawnWon(Pawn pawn, Coordinates coord)
    {
        gridBoard.Children.Remove(pawnToLabel[pawn]);
        this.StatsUpdated(pawn.Player.Other);
    }

    public void PawnSurvived(Pawn pawn, Coordinates coord)
    {
        pawnToLabel[pawn].UpdateContent();
    }

    public void UpdatePlayerReach(int reach, Pnb player)
    {
        if (player != Pnb.P1)
        {
            Grid.SetColumn(this.reaches[player], 5 - reach);
        }
        Grid.SetColumnSpan(this.reaches[player], reach); 
    }

    public void UpdatePlayerStats(Pnb nb)
    {
        this.Labels[nb].Content = $"{nb} : {players[nb].Life}♥ {players[nb].Mana}⍚ ";
        
    }

    public void StatsUpdated(Pnb pNb)
    {
        UpdatePlayerStats(pNb);
    }

    private void OnNextTurn(object sender, RoutedEventArgs e)
    {
        this.HidePossibleTiles();
        this.currentTurn = this.playersCtrlr[currentTurn].EndTurn();
        this.BoardCtrlr.StartNextTurn(currentTurn);
        this.UpdatePlayerReach(players[currentTurn].Reach, currentTurn);
        foreach (TileLabel item in gridBoard.Children)
        {
            item.UpdateContent();
        }
    }

    public void HandReloaded(Pnb nb)
    {
        PrintHand(nb);
    }

    private void ShowBaseAtk(Pnb nb)
    {
        this.Grid.Children.Add(this.baseAtks[nb]);
    }

    private void HideBaseAtk()
    {
        foreach (BaseAtkButton btn in baseAtks.Values)
        {
            this.Grid.Children.Remove(btn);
            btn.Click -= FromButtonAtkBase;
        }
    }

    public void AskUserForPawnMovement(Targets[] options, Pawn p, int nbRepetitions)
    {
        HidePossibleTiles();

        List<Coordinates> tAsC = [];
        foreach (Targets item in options)
        {
            tAsC.AddRange(TargetFactory.GetTargets(item, p.Coordinates, boardModel.Size, p.Player));
        }

        foreach (Coordinates option in tAsC)
        {
            if (option.Y < 6 && option.Y > 0)
            {
                TileLabel? lp = null;
                if (boardModel[option] is not null)
                {
                    lp = this.pawnToLabel[boardModel[option]];
                }
                TileButton tile = new TileButton(option, lp);
                gridBoard.Children.Add(tile);

                Grid.SetRow(tile, option.X);
                Grid.SetColumn(tile, option.Y - 1);

                possibleTiles.Add(tile);

                tile.Click += (obj, e) =>
                {
                    this.BoardCtrlr.MovePawn(p, tile.C);
                    HidePossibleTiles();
                    HideBaseAtk();

                    if (nbRepetitions > 1)
                    {
                        AskUserForPawnMovement(options, p, nbRepetitions - 1);
                    }
                };
            }
            // if they can hit a base 
            else
            {
                if (option.Y == 6)
                {
                    ShowBaseAtk(Pnb.P2);
                }
                else if (option.Y == 0)
                {
                    ShowBaseAtk(Pnb.P1);
                }

                foreach (BaseAtkButton btn in baseAtks.Values)
                {
                    btn.Pawn = p;
                    btn.Click += FromButtonAtkBase;
                }
            }
        }
    }
    
    private void FromButtonAtkBase(object obj, RoutedEventArgs e)
    {
        if (obj is BaseAtkButton btn)
        {
            btn.AtkBase();
            HidePossibleTiles();
            HideBaseAtk();
        }
    }


}
