using System.Collections.Generic;
using UnityEngine;

public class _Global : MonoBehaviour
{
    //存储所有设备的名称、IP地址、端口号
    public static List<DeviceModel> deviceInfo = new List<DeviceModel>();

    //判断传入的JSON具体对应哪个实体类
    public static List<string> classType = new List<string>();
}