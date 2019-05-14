using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class JsonCreater : MonoBehaviour
{
    private List<ButtonInfo> list_bi1 = new List<ButtonInfo>();//一级
    //private List<ButtonInfo> list_bi2 = new List<ButtonInfo>();//二级

    void Start()
    {
        ButtonInfo bi_1 = new ButtonInfo();
        bi_1.buttonName = "区位";
        bi_1.actionCode = "QuWei";

        ButtonInfo bi_2 = new ButtonInfo();
        bi_2.buttonName = "鸟瞰";
        bi_2.actionCode = "NiaoKan";

        ButtonInfo bi_3 = new ButtonInfo();
        bi_3.buttonName = "漫游";
        bi_3.actionCode = "ManYou";

        ButtonInfo bi_4 = new ButtonInfo();
        bi_4.buttonName = "选房";
        bi_4.actionCode = "XuanFang";

        ButtonInfo bi_5 = new ButtonInfo();
        bi_5.buttonName = "品牌";
        bi_5.actionCode = "PinPai";

        ButtonInfo bi_6 = new ButtonInfo();
        bi_6.buttonName = "视频";
        bi_6.actionCode = "ShiPin";

        list_bi1.Add(bi_1);
        list_bi1.Add(bi_2);
        list_bi1.Add(bi_3);
        list_bi1.Add(bi_4);
        list_bi1.Add(bi_5);
        list_bi1.Add(bi_6);

        string json1 = JsonMapper.ToJson(list_bi1);
        BaseClass bc1 = new BaseClass();
        bc1.classType = ReceiveMessageType.Msg_ButtonList.ToString();
        bc1.jsonData = json1;
        string bcjson1 = JsonMapper.ToJson(bc1);
        Debug.Log(bcjson1);
    }
}