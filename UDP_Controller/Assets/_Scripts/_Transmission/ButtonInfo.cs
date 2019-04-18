//存储单个按钮的信息
public class ButtonInfo
{
    //在Unity上显示的按钮文字
    public string buttonName;

    //按钮所触发的动作名称
    //该值由UE4传到Unity，当按钮在Unity中被点击后，会将这个值传回UE4，具体触发的功能由UE4来决定
    //Unity传给UE4的时候，只传输这段代码
    public string actionCode;
}