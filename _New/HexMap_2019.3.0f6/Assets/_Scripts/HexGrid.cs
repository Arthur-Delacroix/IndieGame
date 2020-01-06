﻿using UnityEngine;
using UnityEngine.UI;

//存放 六边形元素HexCell 的网格

public class HexGrid : MonoBehaviour
{
    //网格 横向个数
    public int width = 6;

    //网格 纵向个数
    public int height = 6;

    //单个六边形元素的脚本
    public HexCell cellPrefab;

    //调试用的坐标显示文本Prefab
    [SerializeField] private Text cellLabelPrefab;

    //显示坐标TEXT的Canvas组件
    [SerializeField] private Canvas gridCanvas;

    //保存每个HexCell的实例的数组
    [SerializeField] private HexCell[] cells;

    void Awake()
    {
        Debug.Log(0 / 2);
        Debug.Log(1 / 2);
        Debug.Log(2 / 2);
        Debug.Log(3 / 2);
        Debug.Log(4 / 2);


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
        Vector3 position;

        //由于Plane的默认为10X10，所以每次实例化下一个HexCell时，要偏移10
        //position.x = x * 10f;
        //position.y = 0f;
        //position.z = z * 10f;

        //整六边形和正方形不同，左右两个六边形中点相距2倍内切圆半径，上下两个正六边形中点相距1.5倍外接圆半径
        //position.x = x * (HexMetrics.innerRadius * 2f);

        //这里注意，六边形块上下不是正对齐的，而是错开的
        //上下两排 横向错开的距离，正好是内切圆半径，如下，Z表示第几排，z * HexMetrics.innerRadius 表示离初始正六边形越远，错开的距离远大
        //此计算结果会让整体地图呈现菱形
        //position.x = x * (HexMetrics.innerRadius * 2f) + z * HexMetrics.innerRadius;

        //为了让地图整体呈现正方形，则奇数行横向从0开始，而偶数行则整体向右移动内切圆半径个单位
        //注意，这里z * 0.5f - z / 2没没有抵消，因为z的数据类型为int，z除以2有余数是不会显示的，例如5/2结果为2
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);

        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        //将实例化的HexCell保存在数组的相应位置
        HexCell cell = Instantiate<HexCell>(cellPrefab);
        cells[i] = cell;

        //设置HexCell的父级和位置
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;

        //实例化显示HexCell坐标位置的text，并且设置其父级和位置没让text和HexCell位置相同
        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = x.ToString() + "\n" + z.ToString();
    }
}