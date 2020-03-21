using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour
{
    public Color[] colors;

    public HexGrid hexGrid;

    private Color activeColor;

    //当前HexCell的海拔高度
    private int activeElevation;

    void Awake()
    {
        SelectColor(0);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput();
        }
    }

    //获取鼠标点击位置，并转换为点击到哪个HexCell
    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            //hexGrid.ColorCell(hit.point, activeColor);
            //hexGrid.GetCell(hit.point);
            EditCell(hexGrid.GetCell(hit.point));
        }
    }

    //设置HexCell的颜色（被UGUI调用）
    public void SelectColor(int index)
    {
        activeColor = colors[index];
    }

    //该方法负责所有HexCell的编辑操作，完成操作后调用刷新方法，确保正确显示操作结果
    void EditCell(HexCell cell)
    {
        cell.color = activeColor;
        cell.elevation = activeElevation;
        hexGrid.Refresh();
    }

    //设置HexCell的高度（被UGUI调用）
    public void SetElevation(float elevation)
    {
        activeElevation = (int)elevation;
    }
}