using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 添加新设备信息 面板
/// </summary>

public class AddDeviceInfo : MonoBehaviour
{
    //用户输入的设备信息
    [SerializeField] private InputField deviceName;
    [SerializeField] private InputField deviceIP;
    [SerializeField] private InputField devicePort;

    //调用上层脚本中的方法才可以执行添加操作，自身不能直接执行添加操作
    [SerializeField] private SelectiontPanel panelScript;

    public void AddNewDeviceInfo()
    {
        DeviceInfo _info = new DeviceInfo();

        _info.deviceName = deviceName.text;
        _info.deviceIP = deviceIP.text;
        _info.devicePort = devicePort.text;

        panelScript.AddNewDeviceInfor(_info);
    }
}