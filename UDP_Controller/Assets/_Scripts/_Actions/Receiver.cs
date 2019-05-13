using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 处理UDP 接收到的各类指令
/// </summary>

public class Receiver : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;//按钮预置
    [SerializeField] private GameObject buttonArea;//按钮的父级
    [SerializeField] private UDP_Service UDP;//UDP组件
    [SerializeField] private GameObject touchArea;//触摸区域

    [SerializeField] List<GameObject> btnIns = new List<GameObject>();//实例化的所有按钮预置

    public void OnReceiveMessage(string _str)
    {
        BaseClass bc = new BaseClass();

        //先将收到的字符串转化为基础实体类
        //通过判断BaseClass的classType，知道信息对应哪个实体类
        bc = JsonMapper.ToObject<BaseClass>(_str);

        switch (bc.classType)
        {
            case "Msg_ButtonList":
                ButtonList(bc.jsonData);
                break;
            case "Msg_ButtonTouch":
                ButtonTouch(bc.jsonData);
                break;
        }

        Debug.Log("接收到的信息为" + _str);
    }

    //接收到的是 Msg_ButtonList
    private void ButtonList(string _data)
    {
        touchArea.SetActive(false);

        //收到信息后先判断之前是否有按钮已经在显示
        if (btnIns.Count != 0)
        {
        }

        Debug.Log("接收到的信息为 Msg_ButtonList");

        //ButtonInfo bl = new ButtonInfo();
        List<ButtonInfo> list = new List<ButtonInfo>();

        list = JsonMapper.ToObject<List<ButtonInfo>>(_data);

        foreach (ButtonInfo item in list)
        {
            Debug.Log(item.buttonName);

            //实例化 按钮
            GameObject temp = Instantiate(buttonPrefab);
            temp.GetComponent<ButtonIns>().Init(item.buttonName, item.actionCode, UDP);

            //设置父级


            btnIns.Add(temp);
        }
    }

    //接收到的是 Msg_ButtonTouch
    private void ButtonTouch(string _data)
    {
        Debug.Log("接收到的信息为 Msg_ButtonTouch");

        touchArea.SetActive(true);
    } 
}