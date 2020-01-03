using UnityEngine;

//保存正六边形地图块的属性值

public static class HexMetrics
{
    //正六边形地图块的 外接圆半径
    public const float outerRadius = 10f;

    //正六边形地图块的 内切圆半径
    //根据勾股定理，内切圆半径是外接圆的 二分之一根号3倍
    public const float innerRadius = outerRadius * 0.866025404f;

    //中间的整六边形地图块顶点朝上
    //从正上方顶点开始,顺时针排列，记录每个顶点的位置
    public static Vector3[] corners =
    {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
    };
}