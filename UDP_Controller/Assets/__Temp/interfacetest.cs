using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class interfacetest : MonoBehaviour
{
    void Start()
    {
        IJsonFunction jsonFun = new JsonHelper();

        string tempStr = jsonFun.ReadData("config.txt");

        //List<object> temp = new List<object>();

        //jsonFun.JsonToModel(tempStr,ref temp);

        //foreach (var item in temp)
        //{
        //    Debug.Log(item.ToString());
        //}

        //_Global.classType = jsonFun.JsonToModel(tempStr, _Global.classType);


    }
}