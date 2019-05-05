using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Controller : MonoBehaviour
{
    //选择设备界面
    [SerializeField] private GameObject selectiontPanel;

    //操作界面
    [SerializeField] private GameObject controlPanel;

    //在 选择设备界面 与 操作界面 之间切换
    //每个面板的初始化方法在各自的脚本中，这里只做控制，不做具体初始化等工作
    public void Switchpanel()
    {
        selectiontPanel.SetActive(!selectiontPanel.activeSelf);
        controlPanel.SetActive(!controlPanel.activeSelf);
    }


}