//基础传输类
public class BaseClass
{
    //传输的数据类型
    //此字段为判断data使用哪个类来接收/发送的重要依据
    public string classType;

    //传输的数据，不同的数据接收的类也不同
    public object data;
}