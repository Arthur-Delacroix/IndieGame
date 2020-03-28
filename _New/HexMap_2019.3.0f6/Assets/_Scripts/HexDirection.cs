//描述每个正六边形HexCell的方位
public enum HexDirection
{
    NE,
    E,
    SE,
    SW,
    W,
    NW
}

//面数三种不同的高差类型
public enum HexEdgeType
{
    Flat,
    Slope,
    Cliff
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

    public static HexDirection Previous(this HexDirection direction)
    {
        return direction == HexDirection.NE ? HexDirection.NW : (direction - 1);
    }

    public static HexDirection Next(this HexDirection direction)
    {
        return direction == HexDirection.NW ? HexDirection.NE : (direction + 1);
    }
}