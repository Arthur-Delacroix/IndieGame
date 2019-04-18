using System.Collections.Generic;

//JSON 各类操作的接口
public interface IJsonFunction
{
    //读取JSON配置文件
    string ReadData(string _dataPath);

    //将实体类信息写入 txt 配置文件
    void WriteJson(string _dataPath, object _model);

    //将JSON string转化为实体类
    List<object> JsonToModel(string _jsonStr, List<object> _list);
}