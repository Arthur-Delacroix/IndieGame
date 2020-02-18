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
    private void Triangulate(HexDirection direction, HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;

        //包括中点，每次取出3个顶点，依次存放到链表中
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + HexMetrics.GetFirstCorner(direction),
                center + HexMetrics.GetSecondCorner(direction)
            );

            //将每个顶点的颜色信息赋值给mesh
            //AddTriangleColor(cell.color);

            //HexCell neighbor = cell.GetNeighbor(direction);

            HexCell neighbor = cell.GetNeighbor(direction) ?? cell;
            AddTriangleColor(cell.color, neighbor.color, neighbor.color);
        }
    }

    //为每个顶点赋值颜色信息
    private void AddTriangleColor(Color c1, Color c2, Color c3)
    {
        colors.Add(c1);
        colors.Add(c2);
        colors.Add(c3);
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
}