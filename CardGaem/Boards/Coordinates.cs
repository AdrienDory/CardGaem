using System.Numerics;

namespace CardGaem.Boards;
public class Coordinates : IAdditionOperators<Coordinates, Coordinates, Coordinates>
{
    private int x;
    private int y;
    /// <summary>
    /// Horizontal value
    /// </summary>
    public int X
    {
        get => x;
        set => x = value;
    }
    /// <summary>
    /// Vertical value
    /// </summary>
    public int Y
    {
        get => y;
        set => y = value;
    }

    public Coordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    /// <summary>
    /// Checks if the current coords are lower than the given coords
    /// </summary>
    /// <param name="c">the given coords</param>
    /// <returns>true if inside, false if outside</returns>
    public bool IsInside(Coordinates c)
    {
        return this.X <= c.X && this.Y <= c.Y;
    }
    /// <summary>
    /// Checks if x or y is positive
    /// </summary>
    /// <returns>true if both positive, false otherwise</returns>
    public bool IsPositive()
    {
        return this.X >= 0 && this.Y >= 0;
    }

    /// <summary>
    /// The vector addition formula
    /// </summary>
    /// <param name="left">the left coordinate</param>
    /// <param name="right">the right coordinates</param>
    /// <returns>1.x + 2.x, 1.y + 2.y</returns>
    public static Coordinates operator +(Coordinates left, Coordinates right)
    {
        return new Coordinates(left.x + right.x, left.y + right.y);
    }

    public static Coordinates Copy(Coordinates c)
    {
        return new Coordinates(c.x, c.y);
    }

    /// <inheritdoc/>
    /// <returns>$"[x:{x}   y:{y}]"</returns>
    public override string ToString()
    {
        return $"[x:{this.x}\ty:{this.y}]";
    }


}

