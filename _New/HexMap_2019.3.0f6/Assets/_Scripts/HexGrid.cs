using UnityEngine;

//存放 六边形元素HexCell 的网格

public class HexGrid : MonoBehaviour
{
    //网格 横向个数
    public int width = 6;

    //网格 纵向个数
    public int height = 6;

    //单个六边形元素的脚本
    public HexCell cellPrefab;

    //保存每个HexCell的实例的数组
    [SerializeField] private HexCell[] cells;

    void Awake()
    {
        //二维数组，保存HexGrid中每个HexCell的实例
        cells = new HexCell[height * width];

        //循环创建 HexCell
        //i为计数用，表示当前HexCell是第几个
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    //创建单个HexCell的方法
    //X数组中横向位置；Z数组中纵向位置；i该HexCell在数组是第几个
    void CreateCell(int x, int z, int i)
    {
        //由于Plane的默认为10X10，所以每次实例化下一个HexCell时，要偏移10单位
        Vector3 position;
        position.x = x * 10f;
        position.y = 0f;
        position.z = z * 10f;

        //将实例化的HexCell保存在数组的相应位置
        HexCell cell = Instantiate<HexCell>(cellPrefab);
        cells[i] = cell;

        //设置HexCell的父级和位置
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
    }
}