using CardGaem.Boards.Tiles;
using CardGaem.Boards.Tiles.TileEffects;
using CardGaem.Cards;
using CardGaem.Pawns;
using CardGaem.Pawns.Ability.Enumerations;
using CardGaem.Players;

namespace CardGaem.Boards;

public class OutOfBoardException : Exception
{
    public OutOfBoardException(string msg) : base(msg) { }
}
public class AlreadyAPawnHereException : Exception
{
    public AlreadyAPawnHereException(string msg) : base(msg) { }
}
/// <summary>
/// Where it wars
/// </summary>
public class Board : IPawnObserver, IBoardFromAbility
{
    private Dictionary<Pnb, Player> players;
    public Dictionary<Pnb, Player> Players => players;
    private Tile[][] tiles;
    /// <summary>
    /// nested array that holds the tiles of the board
    /// </summary>
    public Tile[][] Tiles => tiles;
    /// <summary>
    /// nested array that represents the board
    /// with a pawn or nothing (null) on it
    /// </summary>
    public Pawn?[][] PawnsOnBoard
    {
        get
        {
            Pawn?[][] pawns = new Pawn?[size.X + 1][];
            for (int i = 0; i < pawns.Length; i++)
            {
                pawns[i] = new Pawn?[size.Y + 1];
            }
            for (int x = 0; x < pawns.Length; x++)
            {
                for (int y = 0; y < pawns[x].Length; y++)
                {
                    pawns[x][y] = tiles[x][y].Pawn;
                }
            }
            return pawns;
        }
    }
    /// <summary>
    /// Gets a pawn
    /// </summary>
    /// <param name="c">the coordinate of the pawn</param>
    /// <returns>the pawn walking on this tile, is null if there is not pawn there</returns>
    public Pawn? this[Coordinates c] => tiles[c.X][c.Y].Pawn;
    /// <summary>
    /// A multitude of pawns from a multitude of coordinates
    /// </summary>
    /// <param name="coords">each coordinates one must check</param>
    /// <returns>every pawn walking on the desired coordinates</returns>
    public Pawn[] this[Coordinates[] coords]
    {
        get
        {
            List<Pawn> pawns = [];

            foreach (Coordinates c in coords)
            {
                Pawn? p = this[c];
                if (p is not null)
                {
                    pawns.Add(p);
                }
            }
            return pawns.ToArray();
        }
    }
    /// <summary>
    /// All the pawns present on the board
    /// </summary>
    public Pawn[] AllPawns
    {
        get
        {
            List<Pawn> allPawns = [];
            allPawns.AddRange(PawnsOnBoard.SelectMany(array => array).OfType<Pawn>());
            return allPawns.ToArray();
        }
    }
    private Coordinates size;
    /// <summary>
    /// Size of board in tiles
    /// </summary>
    public Coordinates Size => size;
    private int MaxReach => size.Y - 2;

    public Board(Player player1, Player player2)
    {
        this.players = new Dictionary<Pnb, Player>()
        {
            { Pnb.P1, player1 },
            { Pnb.P2, player2 }
        };
        size = new Coordinates(3, 6); // im takin account of base enemy boom boom :D
        tiles = new Tile[size.X+1][];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new Tile[size.Y + 1];
            for (int j = 0; j < tiles[i].Length; j++)
            {
                TEType type = TEType.Standard;
                if (j == 0)
                {
                    type = TEType.p1Base;
                }
                else if (j == Size.Y)
                {
                    type = TEType.p2Base;
                }
                tiles[i][j] = new Tile(this, type);
            }
        }
    }

    /// <summary>
    /// Adds a new pawn on the board
    /// </summary>
    /// <param name="p">the pawn to add</param>
    /// <param name="c">the coordinates of the pawn</param>
    /// <exception cref="AlreadyAPawnHereException">thrown if the tile already has a pawn inside</exception>
    public void AddNewPawn(Pawn p, Coordinates c)
    {
        if (!c.IsInside(this.size) || !c.IsPositive())
        {
            throw new OutOfBoardException($"the pawn is out of the board");
        }
        if (tiles[c.X][c.Y].Pawn != null)
        {
            throw new AlreadyAPawnHereException($"there is already the pawn {tiles[c.X][c.Y].Pawn?.Name} in this tile");
        }
        this.tiles[c.X][c.Y].AddPawn(p);
        p.AddObserver(this);
        p.Update(PawnContext.Created);
    }
    /// <summary>
    /// Updates the reach of a player after their enemy turn
    /// </summary>
    /// <param name="currentP">the player that needs their reach reseted</param>
    public int UpdateReach(Pnb currentP)
    {
        List<Pawn> pawns = [];
        foreach (Pawn pawn in AllPawns)
        {
            if (pawn.Player == currentP)
            {
                pawns.Add(pawn);
            }
        }
        players[currentP].Reach = currentP == Pnb.P1                                                 // if statement
            ? Math.Min(MaxReach, pawns.Max(new PawnReachComparer())?.Coordinates.Y ?? 1)            //statement true
            : Math.Min(MaxReach, size.Y - pawns.Min(new PawnReachComparer())?.Coordinates.Y ?? 1); //statement false
        return players[currentP].Reach;
    }

    /// <summary>
    /// Moves the card from one point to another,
    /// and fights whatever's in the same tile as them
    /// </summary>
    /// <param name="p">the card that wants to move</param>
    /// <param name="oldC">old coordinates</param>
    /// <param name="newC">new coordinates</param>
    /// <exception cref="OutOfBoardException">thrown if the new coordinates are out of bound</exception>
    public void MovePawn(Pawn p, Coordinates oldC, Coordinates newC)
    {
        if (!newC.IsInside(this.size) || !newC.IsPositive())
            throw new OutOfBoardException("Card moved out of Board's range");
        tiles[oldC.X][oldC.Y].RemovePawn(p);
        p.Update(PawnContext.WillMove);

        // does the pawn survive ?
        bool survived = true;

        //checks if there is an encounter
        if (PawnsOnBoard[newC.X][newC.Y] != null)
        {
            Pawn p2 = PawnsOnBoard[newC.X][newC.Y];
            survived = p.Atk(p2);
        }

        //fills the tile with the correct encounter
        if (survived)
        {
            tiles[newC.X][newC.Y].AddPawn(p);
            p.Coordinates = new Coordinates(newC.X, newC.Y);
            p.Update(PawnContext.Moved);
            // the new reach shall not exceed the max reach
            UpdateReach(p.Player);
        }
        
    }
    /// <inheritdoc/>
    public int PawnsLifePointsSum(Coordinates[] coordinates)
    {
        int sum = 0;
        foreach (Coordinates c in coordinates)
        {
            sum += this[c]?.Life ?? 0;
        }
        return sum;
    }

    /// <inheritdoc/>
    public void PawnKilled(Pawn pawn, Coordinates coords)
    {
        DestroyPawn(pawn, coords);
    }

    /// <inheritdoc/>
    public void PawnWon(Pawn pawn, Coordinates coords)
    {
        DestroyPawn(pawn, coords);
    }

    public void PawnSurvived(Pawn pawn, Coordinates Coords)
    {
        //mhm nice i dont give a fuck
    }

    private void DestroyPawn(Pawn p, Coordinates c)
    {
        tiles[c.X][c.Y].RemovePawn(p);
    }


}
