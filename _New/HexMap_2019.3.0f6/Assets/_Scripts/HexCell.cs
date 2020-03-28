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
    private int elevation;

    //为了让显示HexCell的坐标跟随HexCell，必须要存储其坐标
    public RectTransform uiRect;

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

    public int Elevation
    {
        get
        {
            return elevation;
        }
        set
        {
            elevation = value;
            Vector3 position = transform.localPosition;
            position.y = value * HexMetrics.elevationStep;
            transform.localPosition = position;

            Vector3 uiPosition = uiRect.localPosition;
            uiPosition.z = elevation * -HexMetrics.elevationStep;
            uiRect.localPosition = uiPosition;
        }
    }

    //获取当前HexCell指定方位上，矩形连接区域的类型
    public HexEdgeType GetEdgeType(HexDirection direction)
    {
        return HexMetrics.GetEdgeType(
            elevation, neighbors[(int)direction].elevation
        );
    }
}