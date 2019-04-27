using System.Collections.Generic;

//基础传输类
public class BaseClass
{
    //传输的数据类型
    //此字段为判断data使用哪个类来接收/发送的重要依据
    public string classType;

    //接收到的 按钮信息 数据
    public List<ButtonInfo> receive_ButtonData;

    //发送的 单指手指位置信息
    public List<OneTouch> send_TouchPos;
}