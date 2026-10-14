using CardGaem.Boards;

namespace CardGaemTests;

public class CoordinatesTests
{
    [Fact]
    public void IsInsideTest()
    {
        Coordinates c = new Coordinates(1, 2);
        Coordinates c2 = new Coordinates(0, 1);

        Assert.True(c2.IsInside(c));
        Assert.False(c.IsInside(c2));
    }

    [Fact]
    public void IsInsideTestBorders()
    {
        Coordinates c = new Coordinates(1, 2);
        Coordinates c2 = new Coordinates(2, 2);
        Coordinates c3 = new Coordinates(1, 3);

        Assert.False(c2.IsInside(c));
        Assert.False(c3.IsInside(c));
        Assert.True(c.IsInside(c2));
    }

    [Fact]
    public void IsPositive()
    {
        Coordinates c = new Coordinates(1, 2);
        Coordinates c2 = new Coordinates(0, -1);

        Assert.True(c.IsPositive());
        Assert.False(c2.IsPositive());
    }
}
