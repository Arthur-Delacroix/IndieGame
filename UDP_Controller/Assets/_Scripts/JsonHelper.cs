using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using LitJson;

/// <summary>
/// 读取 / 写入 JSON配置文件
/// </summary>
public class JsonHelper : MonoBehaviour
{
    //读取StreamingAssets下的 txt 文件
    public string ReadData(string _dataPath)
    {
        StreamReader sr = new StreamReader(Application.streamingAssetsPath + _dataPath, Encoding.Default);
        string s = sr.ReadToEnd();

        //释放文件流
        sr.Close();
        sr.Dispose();
        sr = null;
        return s;
    }

    //public 

    //读取JSON
    //temp_ = JsonMapper.ToObject<List<Item>>(ReadData("/Data.txt"));
    //将已经读取到的JSON存入数据库中
    //for (int i = 0; i <= 150; i++)
    //{
    //    sql.InsertValues("ItemInfo", new string[] { temp_[i].item_ID.ToString(), temp_[i].item_Name, temp_[i].category.ToString(), temp_[i].item_Icon, temp_[i].item_Detail, "0" });
    //}

    //写入JSON
}