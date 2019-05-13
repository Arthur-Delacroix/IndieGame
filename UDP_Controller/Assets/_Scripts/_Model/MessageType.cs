/// <summary>
/// 向UE4端发送消息的类型
/// </summary>
enum SentMessageType
{
    UDP_Init,//UDP连接后初始化，向UE4端请求按钮信息
    UDP_Disconnect,//UDP连接断开
    Msg_ButtonClick,//按钮点击后向UE4发送信息
    Msg_OneTouch,//单指滑动的位置信息
    Msg_TwoTouch,//双指放大/缩小的位置信息
    Msg_Complete//收到的指令完成后发送
}

/// <summary>
/// 从UE4端接收到的数据的类型 
/// </summary>
enum ReceiveMessageType
{
    Msg_ButtonList,//按钮信息 只含有按钮
    Msg_ButtonTouch//按钮加触摸区域
}