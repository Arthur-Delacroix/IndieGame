using System.IO;
using System.Text;
using UnityEngine;
using LitJson;

/// <summary>
/// 读取 / 写入 JSON
/// </summary>
public class JsonHelper
{
    /// <summary>
    /// 读取StreamingAssets下的 txt 配置文件
    /// </summary>
    public string ReadData(string _dataPath)
    {
        StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/" + _dataPath, Encoding.Default);
        string s = sr.ReadToEnd();

        //释放文件流
        sr.Close();
        sr.Dispose();
        sr = null;
        return s;
    }

    /// <summary>
    /// 将实体类信息写入 txt 配置文件
    /// </summary>
    public void WriteJson(string _dataPath, object _model)
    {
        string tempJson = JsonMapper.ToJson(_model);

        FileStream fs = new FileStream(Application.streamingAssetsPath + "/" + _dataPath, FileMode.Create);
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