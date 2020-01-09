using UnityEngine;
using System;

[Serializable]
public struct HexCoordinates
{
    //public int X { get; private set; }

    //public int Z { get; private set; }

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

    //这里要将X Y Z 的数值序列化显示在inspector面板上
    //显示在indpector的样式
    [SerializeField]
    private int x, z;

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Z
    {
        get
        {
            return z;
        }
    }

    //public HexCoordinates(int x, int z)
    //{
    //    X = x;
    //    Z = z;
    //}

    //重写了构造方法
    public HexCoordinates(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    //这里修正了锯齿状的X坐标问题，通过x - z / 2将坐标对齐，具体效果参考上方图片链接
    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {
        return new HexCoordinates(x - z / 2, z);
    }

    //重写了ToString，在一行中输出X Y Z的坐标
    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
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