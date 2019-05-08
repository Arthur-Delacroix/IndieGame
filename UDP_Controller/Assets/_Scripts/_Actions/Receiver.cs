using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 处理UDP 接收到的各类指令
/// </summary>

public class Receiver : MonoBehaviour
{
    public void OnReceiveMessage(string _str)
    {
        BaseClass bc = new BaseClass();

        //先将收到的字符串转化为基础实体类
        //通过判断BaseClass的classType，知道信息对应哪个实体类
        bc = JsonMapper.ToObject<BaseClass>(_str);

        switch (bc.classType)
        {
            case "UDP_Init"
                break;
        }

        Debug.Log("接收到的信息为" + _str);
    }

    //接收到的是按钮位置信息
    private void ButtonPosition()
    {

    }
}