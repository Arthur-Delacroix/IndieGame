using System.IO;
using System.Text;
using UnityEngine;
using LitJson;
using System.Collections.Generic;

/// <summary>
/// 读取 / 写入 JSON
/// </summary>
public class JsonHelper
{
    /// <summary>
    /// 读取StreamingAssets下的配置文件，并将读取到的数据转换为设备信息(DeviceInfo)类
    /// </summary>
    public List<DeviceInfo> ReadData()
    {
        StreamReader sr = new StreamReader(Application.streamingAssetsPath + _Global.configFilePath, Encoding.Default);
        string s = sr.ReadToEnd();

        //释放文件流
        sr.Close();
        sr.Dispose();
        sr = null;

        Debug.Log("<color=#00ff00>文件读取完成</color>");

        List<DeviceInfo> _infoList = new List<DeviceInfo>();
        _infoList = JsonMapper.ToObject<List<DeviceInfo>>(s);

        return _infoList;
    }

    /// <summary>
    /// 将实体类信息写入 txt 配置文件
    /// </summary>
    public void WriteJson(List<DeviceInfo> _info)
    {
        string tempJson = JsonMapper.ToJson(_info);

        FileStream fs = new FileStream(Application.streamingAssetsPath + _Global.configFilePath, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        //开始写入
        sw.Write(tempJson);
        //清空缓冲区
        sw.Flush();
        //关闭流
        sw.Close();
        fs.Close();

        Debug.Log("<color=#00ff00>JSON写入完成</color>");
    }
}