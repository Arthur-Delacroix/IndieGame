using UnityEngine;

public class _Global : MonoBehaviour
{
    //StreamingAssets目录下config.txt文件的位置
    //如果config.txt之前有文件夹，格式为 /FileName/config.txt
    public static string configFilePath = "/config.txt";

    //存储当前目标设备的IP地址
    public static string targetDeviceIP;

    //当前目标设备的端口号
    public static int targetDevicePort;
}