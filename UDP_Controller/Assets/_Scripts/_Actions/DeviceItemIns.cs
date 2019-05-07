using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using LitJson;

/// <summary>
/// 在设备下拉列表中的 单个设备信息块
/// </summary>

public class DeviceItemIns : MonoBehaviour
{
    [SerializeField] private Text deviceName;
    [SerializeField] private Text deviceIP;
    [SerializeField] private Text devicePort;

    //手指在该按钮上左右滑动的时候，滑动速度的极限值
    [SerializeField] private float threshold;

    //删除按钮的动画组件
    [SerializeField] private TweenScale deleteScale;

    //父级组件，用于调用删除方法
    [SerializeField] private SelectiontPanel panelComponent;

    //存储自己的信息，用于重新写入的时候使用
    private DeviceInfo _selfInfo;

    //实例化时，必须调用此方法
    public void Init(DeviceInfo _info, SelectiontPanel _script)
    {
        _selfInfo = _info;
        panelComponent = _script;

        deviceName.text = "设备名称：" + _info.deviceName;
        deviceIP.text = "设备IP：" + _info.deviceIP;
        devicePort.text = "设备端口：" + _info.devicePort;
    }

    //删除当前设备信息
    public void Delete()
    {
        panelComponent.DeleteDeviceInfo(gameObject);
    }

    //左右滑动，显示/隐藏 删除按钮
    public void DragEvent(BaseEventData data)
    {
        //将 BaseEventData类型强转为 PointerEventData类型，才可以取到delta属性
        PointerEventData _data = data as PointerEventData;

        //左滑显示 删除按钮
        if (_data.delta.x > threshold)
        {
            deleteScale.PlayReverse();
        }

        if (_data.delta.x < threshold * -1)
        {
            deleteScale.PlayForward();
        }
    }

    //获取设备的各类信息
    public DeviceInfo GetDeviceInfo()
    {
        return _selfInfo;
    }

    //启动UDP连接 并发送初始化信息
    public void StartUDP()
    {
        _Global.targetDeviceIP = _selfInfo.deviceIP;
        _Global.targetDevicePort = int.Parse(_selfInfo.devicePort);

        Debug.Log("<color=#ffff00>按钮点击，开始创建UDP连接</color>");
        Debug.Log("<color=#ffff00>IP: " + _Global.targetDeviceIP + "</color>");
        Debug.Log("<color=#ffff00>Port: " + _Global.targetDevicePort + "</color>");

        panelComponent.StartUDP();

        //发送初始化信息
        BaseClass bc = new BaseClass();
        bc.classType = MessageType.UDP_Init.ToString();
        bc.jsonData= MessageType.UDP_Init.ToString();

        string _msg = JsonMapper.ToJson(bc);

        panelComponent.SentUDPMessage(_msg);
    }
}