using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectiontPanel : MonoBehaviour
{
    //单个设备信息按钮的预置，用于添加到DeviceList中，供用户选择连接哪一个设备
    [SerializeField] private GameObject prefab_DeviceItem;

    //存储所有显示在列表中的设备信息
    [SerializeField] private List<GameObject> insDeviceButton;

    //实例化设备信息时，需要先将ContentSizeFitter设置为false
    [SerializeField] private ContentSizeFitter fitter;
    //设备信息实例元素的父级
    [SerializeField] private GameObject deviceInfoParent;

    //UDP组件 用于点击按钮后创建UDP连接
    [SerializeField] private UDP_Service udp;

    //总控制器 切换当前要显示的面板
    [SerializeField] private _Controller controlPanel;

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
    public void AddNewDeviceInfor(DeviceInfo _info)
    {
        //添加新设备信息之前，先关掉高度自适应
        fitter.enabled = false;

        //添加设备信息
        GameObject _ins = Instantiate(prefab_DeviceItem);//实例化预置
        _ins.transform.SetParent(deviceInfoParent.transform);//设备父级
        _ins.GetComponent<DeviceItemIns>().Init(_info, gameObject.GetComponent<SelectiontPanel>());//初始化数据
        insDeviceButton.Add(_ins);//添加到链表中

        //将链表中所有的信息写入配置文件
        List<DeviceInfo> _infoList = new List<DeviceInfo>();
        foreach (var item in insDeviceButton)
        {
            _infoList.Add(item.GetComponent<DeviceItemIns>().GetDeviceInfo());
        }
        JsonHelper jhple = new JsonHelper();

        jhple.WriteJson(_infoList);


        //打开自适应高度组件
        fitter.enabled = true;
    }

    //读取txt配置信息，并实例化
    private void ReadConfigFile()
    {
        fitter.enabled = false;

        JsonHelper jhple = new JsonHelper();

        foreach (DeviceInfo item in jhple.ReadData())
        {
            GameObject _ins = Instantiate(prefab_DeviceItem);//实例化预置
            _ins.transform.SetParent(deviceInfoParent.transform);//设备父级
            _ins.GetComponent<DeviceItemIns>().Init(item, gameObject.GetComponent<SelectiontPanel>());//初始化数据
            insDeviceButton.Add(_ins);//添加到链表中
        }

        fitter.enabled = true;
    }

    //删除设备信息
    public void DeleteDeviceInfo(GameObject _deviceInfo)
    {
        insDeviceButton.Remove(_deviceInfo);
        Destroy(_deviceInfo);

        //将链表中所有的信息写入配置文件
        List<DeviceInfo> _infoList = new List<DeviceInfo>();
        foreach (var item in insDeviceButton)
        {
            _infoList.Add(item.GetComponent<DeviceItemIns>().GetDeviceInfo());
        }
        JsonHelper jhple = new JsonHelper();

        jhple.WriteJson(_infoList);

        Debug.Log("<color=#00ffff>删除了设备信息，当前设备信息数量:" + insDeviceButton.Count + "</color>");
    }

    //按钮点击后 根据设备信息创建UDP连接
    public void StartUDP()
    {
        controlPanel.SwitchPanel();
        udp.StartUDP();
    }

    //通过UDP发出消息
    public void SentUDPMessage(string _msg)
    {
        udp.SentMsg(_msg);
    }
}