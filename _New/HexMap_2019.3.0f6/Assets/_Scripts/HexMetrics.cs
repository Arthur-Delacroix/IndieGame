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

    //海拔高度之间的跨度
    public const float elevationStep = 5f;

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

    //https://catlikecoding.com/unity/tutorials/hex-map/part-2/blend-regions/edge-bridge.png
    //获得V3 V4的位置
    public static Vector3 GetBridge(HexDirection direction)
    {
        //return (corners[(int)direction] + corners[(int)direction + 1]) * 0.5f * blendFactor;
        return (corners[(int)direction] + corners[(int)direction + 1]) * blendFactor;
    }



    //关于斜边梯度的部分

    //两个高度差1，并且相邻的HexCell，它们之间的过渡台阶数量
    public const int terracesPerSlope = 2;

    //根据台阶数量计算出实际的两个HexCell的 横向的连接区域的宽度
    public const int terraceSteps = terracesPerSlope * 2 + 1;

    //水平方向上，过渡台阶每一个部分的宽度(此为一个比例值，用来做lerp计算)
    //注意，过渡台阶的宽度分为两部分，斜边为1个单位宽度，平台边为一个单位宽度，这里球出来的是每个单位宽度占整个宽度的百分比
    public const float horizontalTerraceStepSize = 1f / terraceSteps;

    //垂直方向上，每一个台阶的高度，由于是2个过渡台阶加上自身的台阶，所以要+1
    public const float verticalTerraceStepSize = 1f / (terracesPerSlope + 1);

    //根据预设的台阶数量，计算出每个过渡台阶的宽度和高度
    public static Vector3 TerraceLerp(Vector3 a, Vector3 b, int step)
    {
        float h = step * HexMetrics.horizontalTerraceStepSize;
        a.x += (b.x - a.x) * h;
        a.z += (b.z - a.z) * h;
        float v = ((step + 1) / 2) * HexMetrics.verticalTerraceStepSize;
        a.y += (b.y - a.y) * v;
        return a;
    }

    //为过渡台阶着色
    public static Color TerraceLerp(Color a, Color b, int step)
    {
        float h = step * HexMetrics.horizontalTerraceStepSize;
        return Color.Lerp(a, b, h);
    }
}