using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectiontPanel : MonoBehaviour
{
    //单个设备信息按钮的预置，用于添加到DeviceList中，供用户选择连接哪一个设备
    [SerializeField] private GameObject prefab_DeviceItem;

    //存储所有显示在列表中的设备信息
    [SerializeField] private List<GameObject> insDeviceButton;

    [SerializeField] private ContentSizeFitter fitter;
    [SerializeField] private GameObject deviceInfoParent;

    //在加载时初始化
    private void OnEnable()
    {
        //当列表中没有任何设备信息时，从配置文件中读取
        if (insDeviceButton.Count == 0)
        {
            ReadConfigFile();
        }

        //List<DeviceInfo> list1 = new List<DeviceInfo>();
        //DeviceInfo info1 = new DeviceInfo();
        //info1.deviceName = "一号VIP室 大屏 沙盘展示";
        //info1.deviceIP = "192.168.0.101";
        //info1.devicePort = "1234";

        //DeviceInfo info2 = new DeviceInfo();
        //info2.deviceName = "一号VIP室 大屏 漫游";
        //info2.deviceIP = "192.168.0.201";
        //info2.devicePort = "741852";

        //DeviceInfo info3 = new DeviceInfo();
        //info3.deviceName = "二号VIP室 大屏 选房";
        //info3.deviceIP = "192.168.0.103";
        //info3.devicePort = "1234";

        //DeviceInfo info4 = new DeviceInfo();
        //info4.deviceName = "大厅 大屏展示";
        //info4.deviceIP = "192.168.0.107";
        //info4.devicePort = "4267";

        //list1.Add(info1);
        //list1.Add(info2);
        //list1.Add(info3);
        //list1.Add(info4);

        //JsonHelper h = new JsonHelper();
        //h.WriteJson(list1);
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
}