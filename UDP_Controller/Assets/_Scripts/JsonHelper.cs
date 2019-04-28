using System.IO;
using System.Text;
using UnityEngine;
using LitJson;
using System.Collections.Generic;

/// <summary>
/// 读取 / 写入 JSON
/// </summary>
public class JsonHelper : IJsonFunction
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

    /// <summary>
    /// 将JSON字符串转化为 ButtonInfo 实体类
    /// </summary>
    /// <param name="_jsonStr">JSON字符串</param>
    /// <param name="_list_ButtonInfo">转化的目标实体类</param>
    public void JsonToModel(string _jsonStr, ref List<ButtonInfo> _list_ButtonInfo)
    {
        _list_ButtonInfo = JsonMapper.ToObject<List<ButtonInfo>>(_jsonStr);
    }

    /// <summary>
    /// 将实体类信息转换成JSON信息
    /// </summary>
    /// <returns>JSON字符串</returns>
    /// <param name="_one">JSON字符串</param>
    public string OneTouchToJson(OneTouch _one)
    {
        string _str = JsonMapper.ToJson(_one);

        return _str;
    }

    //public string
}