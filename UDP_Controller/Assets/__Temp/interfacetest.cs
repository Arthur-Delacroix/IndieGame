using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class interfacetest : MonoBehaviour
{
    string _Json1 = "{\"info\":\"123456\",\"objs\":[{\"_x1\":11,\"_y1\":11,\"_x2\":12,\"_y2\":12},{\"_x1\":21,\"_y1\":21,\"_x2\":22,\"_y2\":22},{\"_x1\":31,\"_y1\":31,\"_x2\":32,\"_y2\":32}]}";

    string _Json2 = "{\"info\":\"qazwsx\",\"objs\":[{\"_x\":1,\"_y\":1},{\"_x\":2,\"_y\":2},{\"_x\":1,\"_y\":1}]}";

    void Start()
    {
        List<object> o1 = new List<object>();

        //c2 cc1 = new c2();
        //cc1._x = 1;
        //cc1._y = 1;

        //c2 cc2 = new c2();
        //cc2._x = 2;
        //cc2._y = 2;

        //c2 cc3 = new c2();
        //cc3._x = 1;
        //cc3._y = 1;

        /*
        c3 c3_1 = new c3();
        c3 c3_2 = new c3();
        c3 c3_3 = new c3();

        c3_1._x1 = 11;
        c3_1._y1 = 11;
        c3_1._x2 = 12;
        c3_1._y2 = 12;

        c3_2._x1 = 21;
        c3_2._y1 = 21;
        c3_2._x2 = 22;
        c3_2._y2 = 22;

        c3_3._x1 = 31;
        c3_3._y1 = 31;
        c3_3._x2 = 32;
        c3_3._y2 = 32;

        o1.Add(c3_1);
        o1.Add(c3_2);
        o1.Add(c3_3);

        c1 c1 = new c1();
        c1.info = "123456";
        c1.objs = o1;

        string _json = JsonMapper.ToJson(c1);

        Debug.Log(_json);
        */

        c1 c1 = new c1();
        c1 = JsonMapper.ToObject<c1>(_Json1);

        foreach (var item in c1.objs)
        {
            Debug.Log(item.ToString());
        }
    }
}

public class c1
{
    public string info;

    public List<object> objs = new List<object>();
}

public class c2
{
    public int _x;
    public int _y;
}

public class c3
{
    public int _x1;
    public int _y1;

    public int _x2;
    public int _y2;
}

/*
{
	"info": "123456",
	"objs": [{
		"_x": 1,
		"_y": 1
	}, {
		"_x": 2,
		"_y": 2
	}, {
		"_x": 1,
		"_y": 1
	}]
}

{
	"info": "123456",
	"objs": [{
		"_x1": 11,
		"_y1": 11,
		"_x2": 12,
		"_y2": 12
	}, {
		"_x1": 21,
		"_y1": 21,
		"_x2": 22,
		"_y2": 22
	}, {
		"_x1": 31,
		"_y1": 31,
		"_x2": 32,
		"_y2": 32
	}]
}
*/
