public enum HexDirection
{
    NE,
    E,
    SE,
    SW,
    W,
    NW
}

public static class HexDirectionExtensions
{
    public static HexDirection Opposite(this HexDirection direction)
    {
        //return (int)direction < 3 ? (direction + 3) : (direction - 3);

        if ((int)direction < 3)
        {
            return (direction + 3);
        }
        else
        {
            return (direction - 3);
        }
    }
}