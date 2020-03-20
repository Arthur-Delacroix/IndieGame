using UnityEngine;

public class HexCell : MonoBehaviour
{
    //进行坐标转换与输出的脚本
    public HexCoordinates coordinates;

    //存储自身颜色值
    public Color color;

    //存储相邻的六个地图块
    [SerializeField] private HexCell[] neighbors = null;

    //HexCell的海拔高度
    public int elevation;

    //海拔高度之间的跨度
    public const float elevationStep = 5f;

    //根据方位 获取其相邻的地图块
    public HexCell GetNeighbor(HexDirection direction)
    {
        return neighbors[(int)direction];
    }

    //根据方位 设置其相邻的地图块
    //数组neighbors在Unity的inspector中预警初始化为长度6
    //在这里每个正六边形相邻的地图块都为6个，不用担心索引越界的问题，所以不用验证
    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }
}