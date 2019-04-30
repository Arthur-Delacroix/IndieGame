/// <summary>
/// 基础传输类
/// </summary>
public class BaseClass
{
    //传输的数据类型
    //此字段为判断data使用哪个类来接收/发送的重要依据
    public string classType;

    //接收到的实际数据
    //这个实际数据是一个JSON，先通过classType字段进行判断使用哪个类，再将这段JSON进行解析到对应类
    public string jsonData;
}