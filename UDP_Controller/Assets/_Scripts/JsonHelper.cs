using System.IO;
using System.Text;
using UnityEngine;
using LitJson;

/// <summary>
/// 读取 / 写入 JSON
/// </summary>
public class JsonHelper
{
    //读取StreamingAssets下的 txt 配置文件
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
    /// 将JSON字符串转化为实体类
    /// </summary>
    public object JsonToModel(string _jsonStr, object _jsonModel)
    {
        _jsonModel = JsonMapper.ToObject<object>(_jsonStr);

        return _jsonModel;
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
    }
}