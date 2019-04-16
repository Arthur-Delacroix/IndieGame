using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public string name1;
    public int age1;
    public List<object> new2 = new List<object>();
    public List<object> new3 = new List<object>();


    private void Start()
    {

        New1 n1 = new New1();
        New2 n2 = new New2();
        n2.name2 = "222";

        n1.name1 = "111";
        n1.vls = n2;


        New3 n3 = new New3();
        n3.name3 = "333";


        string json = JsonMapper.ToJson(n1);

        Debug.Log(json);

    }

    //string json = JsonMapper.ToJson<New2>(nnn);
}

public class New1
{

    public string name1;
    public object vls;
}

public class New2
{
    public string name2;
}


public class New3
{
    public string name3;
}

//基础传输类
public class BaseClass
{
    //传输的数据类型
    //此字段为判断data使用哪个类来接收/传输的重要依据
    public string classType;

    //传输的数据，不同的数据接收的类也不同
    public object data;
}