using UnityEngine;

//保存正六边形地图块的属性值

public static class HexMetrics
{
    //正六边形地图块的 外接圆半径
    public const float outerRadius = 10f;

    //正六边形地图块的 内切圆半径
    //根据勾股定理，内切圆半径是外接圆的 二分之根号3倍
    public const float innerRadius = outerRadius * 0.866025404f;

    //颜色混合区域
    //其中地图块实体颜色为外半径的0.75，颜色混合区域为外半径的0.25
    public const float solidFactor = 0.75f;
    public const float blendFactor = 1f - solidFactor;

    //中间的整六边形地图块顶点朝上
    //从正上方顶点开始,顺时针排列，记录每个顶点的位置
    private static Vector3[] corners =
    {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),

        //注意，这里是新增一个，因为第一个顶点和最后一个顶点位置是重合的
        //如果没有这个顶点数据，会造成HexMesh.Triangulate(HexCell cell)循环越界
        //因为每次从最上方顶点开始，顺时针依次取出两个顶点，加上中点，绘制三角形，最后一个三角形的顶点与第一个三角形顶点重合
        new Vector3(0f, 0f, outerRadius)
    };

    //获取地图块的顶点方法
    public static Vector3 GetFirstCorner(HexDirection direction)
    {
        return corners[(int)direction];
    }
    public static Vector3 GetSecondCorner(HexDirection direction)
    {
        return corners[(int)direction + 1];
    }

    //获取地图块实际顶点的位置，因为有颜色混合区域存在，所以要乘以颜色混合区域的系数
    public static Vector3 GetFirstSolidCorner(HexDirection direction)
    {
        return corners[(int)direction] * solidFactor;
    }

    public static Vector3 GetSecondSolidCorner(HexDirection direction)
    {
        return corners[(int)direction + 1] * solidFactor;
    }

    public static Vector3 GetBridge(HexDirection direction)
    {
        return (corners[(int)direction] + corners[(int)direction + 1]) *
            0.5f * blendFactor;
    }
}