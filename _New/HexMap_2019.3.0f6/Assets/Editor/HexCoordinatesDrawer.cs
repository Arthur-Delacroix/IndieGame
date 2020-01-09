using UnityEngine;
using UnityEditor;

//这里是重新绘制了 每个正六边形上所挂载脚本显示自身坐标数值 的样式

//通过这行代码链接到了HexCoordinates脚本
[CustomPropertyDrawer(typeof(HexCoordinates))]
public class HexCoordinatesDrawer : PropertyDrawer
{
    //通过OnGUI方法重新绘制参数的显示样式外观
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //读取出相应参数
        HexCoordinates coordinates = new HexCoordinates(
            property.FindPropertyRelative("x").intValue,
            property.FindPropertyRelative("z").intValue);

        //这里使用PrefixLabel重新匹配了参数的外观样式，可以与其他的参数对齐显示
        position = EditorGUI.PrefixLabel(position, label);

        //输出显示的内容
        GUI.Label(position, coordinates.ToString());
    }
}