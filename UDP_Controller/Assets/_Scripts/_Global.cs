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

    //接收到的字符串
    //由于接收的字符串是在非主线程中，不能直接调用其他脚本，只能通过判断字符串是否为空来执行命令
    public static string transStr = "";
}