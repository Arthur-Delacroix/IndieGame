using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SelectPanel : MonoBehaviour
{
    //单个设备信息按钮的预置，用于添加到DeviceList中，供用户选择连接哪一个设备
    [SerializeField] private GameObject prefab_DeviceItem;

    //存储所有显示在列表中的设备信息
    [SerializeField] private List<GameObject> insDeviceButton;

    //在加载时初始化
    private void OnEnable()
    {
        //当列表中没有任何设备信息时，从配置文件中读取
        if (insDeviceButton.Count == 0)
        {
            ReadConfigFile();
        }
    }

    //添加一个新的设备信息
    public void AddNewDeviceInfor()
    {

    }

    //读取 StreamingAssets 文件夹下的配置信息
    private void ReadConfigFile()
    {

    }

    //重置设备列表
    private void ResetDeviceList()
    {
    }
}