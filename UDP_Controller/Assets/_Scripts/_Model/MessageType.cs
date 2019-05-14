/// <summary>
/// 向UE4端发送消息的类型
/// </summary>
enum SentMessageType
{
    UDP_Init,//UDP连接后初始化，向UE4端请求按钮信息
    UDP_Disconnect,//UDP连接断开
    Msg_ButtonClick,//按钮点击后向UE4发送信息
    Msg_OneTouchUp,//单指 向上
    Msg_OneTouchDown,//单指 向下
    Msg_TwoTouchBig,//双指放大
    Msg_TwoTouchSmall,//双指缩小
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