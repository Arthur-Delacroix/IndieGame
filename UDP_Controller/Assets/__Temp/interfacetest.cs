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



        //_Global.classType = jsonFun.JsonToModel(tempStr, _Global.classType);


    }
}