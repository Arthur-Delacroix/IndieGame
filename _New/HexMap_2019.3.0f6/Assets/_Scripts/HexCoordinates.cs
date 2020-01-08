using UnityEngine;
using System;

[Serializable]
public struct HexCoordinates
{
    public int X { get; private set; }

    public int Z { get; private set; }

    //注意，因为每个正六边形块是有6个方向的，所以在一个平面中会有3个坐标，即X Y Z
    //具体参考图片 https://catlikecoding.com/unity/tutorials/hex-map/part-1/hexagonal-coordinates/cube-diagram.png
    //其中，X+Y+Z的值恒等于0，所以在已知X Z的值，通过计算可以得出Y的值
    public int Y
    {
        get
        {
            return -X - Z;
        }
    }

    public HexCoordinates(int x, int z)
    {
        X = x;
        Z = z;
    }

    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {
        return new HexCoordinates(x - z / 2, z);
    }

    //重写了ToString，在一行中输出X Y Z的坐标
    public override string ToString()
    {
        return "(" + 
            "<color=red>" + X.ToString() + "</color>" + 
            "<color=green>" + Y.ToString() + "</color>" +
            "<color=blue>" + Z.ToString() + "</color>" + ")";
    }

    //X Y Z的坐标分别在单独的行输出
    public string ToStringOnSeparateLines()
    {
        return 
            "<color=red>" + X.ToString() + "</color>" + "\n" +
            "<color=green>" + Y.ToString() + "</color>" + "\n" +
            "<color=blue>" + Z.ToString() + "</color>";
    }


}