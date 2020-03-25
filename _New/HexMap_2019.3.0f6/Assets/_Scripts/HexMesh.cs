using UnityEngine;
using System.Collections.Generic;

public class HexMesh : MonoBehaviour
{
    //存储生成的Mesh的容器
    [SerializeField] private Mesh hexMesh;

    //为了得到碰撞，这里要先获取自身的Mesh，通过Mesh生成Mesh Collider
    [SerializeField] private MeshCollider meshCollider = null;

    //存储正六边形的顶点的链表
    [SerializeField] private List<Vector3> vertices;

    //存储正六边形三角面片的链表
    [SerializeField] private List<int> triangles;

    //存储每个正六边形元素的颜色值
    [SerializeField] private List<Color> colors;

    private void Awake()
    {
        //初始化网格、顶点链表、三角面片链表、颜色值链表
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        colors = new List<Color>();
    }

    //绘制三角形
    public void Triangulate(HexCell[] cells)
    {
        //为确保Mseh与链表内无数据，先清空
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        colors.Clear();

        //根据数组的长度，实例化相应数量的正六边形
        for (int i = 0; i < cells.Length; i++)
        {
            Triangulate(cells[i]);
        }

        //绘制三角面片的代码
        //这里将3个顶点位置和其顺序，分别赋值给Mesh，Mesh组件会按照顶点位置和顺序绘制三角面片
        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();

        //赋值默认颜色，赋值过程由AddTriangleColor方法执行
        hexMesh.colors = colors.ToArray();

        //为保证视觉效果正确，需要重新根据顶点信息计算法线
        hexMesh.RecalculateNormals();

        //将生成好的Mesh传给Collider，进行碰撞体的生成
        meshCollider.sharedMesh = hexMesh;
    }

    //为每个顶点附加方位值
    void Triangulate(HexCell cell)
    {
        for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
            Triangulate(d, cell);
        }
    }

    //从HexMetrics的corners数组中，从六边形正上方开始，顺时针取出2个顶点信息，包括六边形中点，可以组成一个三角形
    /* private void Triangulate(HexDirection direction, HexCell cell)
     {
         Vector3 center = cell.transform.localPosition;

         //这里是经过系数计算之后得出的实际顶点位置，剩下的与其用来做颜色混合，这个区域填充单一颜色
         Vector3 v1 = center + HexMetrics.GetFirstSolidCorner(direction);
         Vector3 v2 = center + HexMetrics.GetSecondSolidCorner(direction);

         //混合区域外顶点位置信息
         //Vector3 v3 = center + HexMetrics.GetFirstCorner(direction);
         //Vector3 v4 = center + HexMetrics.GetSecondCorner(direction);

         //新的 颜色混合区域
         //这里是矩形的
         Vector3 bridge = HexMetrics.GetBridge(direction);
         Vector3 v3 = v1 + bridge;
         Vector3 v4 = v2 + bridge;

         //包括中点，每次取出3个顶点，依次存放到链表中
         for (int i = 0; i < 6; i++)
         {
             //AddTriangle(
             //    center,
             //    center + HexMetrics.GetFirstCorner(direction),
             //    center + HexMetrics.GetSecondCorner(direction)
             //);

             //新的创建三角面片的方法，因为新增了颜色混合区域，这里使用经过系数修正的外接圆半径，而非原始的外接圆半径
             //AddTriangle(
             //    center,
             //    center + HexMetrics.GetFirstSolidCorner(direction),
             //    center + HexMetrics.GetSecondSolidCorner(direction)
             //);

             //这里提前计算好了除重点外的另外两个顶点位置，直接使用
             AddTriangle(center, v1, v2);

             //将每个顶点的颜色信息赋值给mesh
             //AddTriangleColor(cell.color);

             //HexCell neighbor = cell.GetNeighbor(direction);

             //HexCell neighbor = cell.GetNeighbor(direction) ?? cell;
             ////AddTriangleColor(cell.color, neighbor.color, neighbor.color);
             //Color edgeColor = (cell.color + neighbor.color) * 0.5f;
             //AddTriangleColor(cell.color, edgeColor, edgeColor);

             //添加混合区域
             AddQuad(v1, v2, v3, v4);

             HexCell prevNeighbor = cell.GetNeighbor(direction.Previous()) ?? cell;
             HexCell neighbor = cell.GetNeighbor(direction) ?? cell;
             HexCell nextNeighbor = cell.GetNeighbor(direction.Next()) ?? cell;

             //添加三角形区域顶点颜色信息
             //AddTriangleColor(
             //    cell.color,
             //    (cell.color + prevNeighbor.color + neighbor.color) / 3f,
             //    (cell.color + neighbor.color + nextNeighbor.color) / 3f
             //);

             AddTriangleColor(cell.color);

             //TriangulateConnection(direction, cell, v1, v2);
             if (direction == HexDirection.NE)
             {
                 TriangulateConnection(direction, cell, v1, v2);
             }

             //混合区域顶点颜色赋值
             //AddQuadColor(
             //    cell.color,
             //    cell.color,
             //    (cell.color + prevNeighbor.color + neighbor.color) / 3f,
             //    (cell.color + neighbor.color + nextNeighbor.color) / 3f
             // );

             //颜色混合区域只混合相邻的cell的颜色
             //AddQuadColor(cell.color, (cell.color + neighbor.color) * 0.5f);

             //加入了 三个颜色混合区域
             Color bridgeColor = (cell.color + neighbor.color) * 0.5f;
             AddQuadColor(cell.color, bridgeColor);

             AddTriangle(v1, center + HexMetrics.GetFirstCorner(direction), v3);
             AddTriangleColor(
                 cell.color,
                 (cell.color + prevNeighbor.color + neighbor.color) / 3f,
                 bridgeColor
             );

             AddTriangle(v2, v4, center + HexMetrics.GetSecondCorner(direction));
             AddTriangleColor(
                 cell.color,
                 bridgeColor,
                 (cell.color + neighbor.color + nextNeighbor.color) / 3f
             );
         }
     }*/

    private void Triangulate(HexDirection direction, HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;
        Vector3 v1 = center + HexMetrics.GetFirstSolidCorner(direction);
        Vector3 v2 = center + HexMetrics.GetSecondSolidCorner(direction);

        AddTriangle(center, v1, v2);
        AddTriangleColor(cell.color);

        if (direction <= HexDirection.SE)
        {
            TriangulateConnection(direction, cell, v1, v2);
        }
    }

    //为每个顶点赋值颜色信息
    private void AddTriangleColor(Color c1, Color c2, Color c3)
    {
        colors.Add(c1);
        colors.Add(c2);
        colors.Add(c3);
    }

    //为三角形每个顶点赋值相同颜色
    private void AddTriangleColor(Color c)
    {
        colors.Add(c);
        colors.Add(c);
        colors.Add(c);
    }

    //分别将 顶点的位置 和 顶点的顺序，存放在相应的链表中
    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    //梯形的颜色混合区域的顶点信息
    void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        vertices.Add(v4);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 3);
    }

    //颜色混合区域 顶点颜色信息
    //void AddQuadColor(Color c1, Color c2, Color c3, Color c4)
    //{
    //    colors.Add(c1);
    //    colors.Add(c2);
    //    colors.Add(c3);
    //    colors.Add(c4);
    //}

    //该矩形区域只需要混合两个颜色，即相邻的两个cell之间的颜色
    private void AddQuadColor(Color c1, Color c2)
    {
        colors.Add(c1);
        colors.Add(c1);
        colors.Add(c2);
        colors.Add(c2);
    }

    //创建相邻HexCell之间的连接区域
    private void TriangulateConnection(HexDirection direction, HexCell cell, Vector3 v1, Vector3 v2)
    {
        HexCell neighbor = cell.GetNeighbor(direction);
        if (neighbor == null)
        {
            return;
        }

        Vector3 bridge = HexMetrics.GetBridge(direction);
        Vector3 v3 = v1 + bridge;
        Vector3 v4 = v2 + bridge;

        v3.y = v4.y = neighbor.Elevation * HexMetrics.elevationStep;

        AddQuad(v1, v2, v3, v4);
        AddQuadColor(cell.color, neighbor.color);

        HexCell nextNeighbor = cell.GetNeighbor(direction.Next());
        if (direction <= HexDirection.E && nextNeighbor != null)
        {
            Vector3 v5 = v2 + HexMetrics.GetBridge(direction.Next());
            v5.y = nextNeighbor.Elevation * HexMetrics.elevationStep;
            AddTriangle(v2, v4, v5);

            //AddTriangle(v2, v4, v2 + HexMetrics.GetBridge(direction.Next()));

            //AddTriangle(v2, v4, v2);
            AddTriangleColor(cell.color, neighbor.color, nextNeighbor.color);
        }
    }
}