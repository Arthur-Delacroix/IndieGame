using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 单个按钮的实例
/// </summary>

public class ButtonIns : MonoBehaviour
{
    //按钮所显示的文字
    [SerializeField] private Text buttonText;

    //按钮按下，传给UE4的指令
    [SerializeField] private string directive;

    [SerializeField] private UDP_Service UDP;

    //按钮初始化 必须执行此方法
    public void Init(string _text, string _dir, UDP_Service _udp)
    {
        directive = _dir;

        buttonText.text = _text;

        UDP = _udp;
    }

    //按钮按下执行的方法
    public void ButtonClick()
    {


        UDP.SentMsg(directive);
    }
}