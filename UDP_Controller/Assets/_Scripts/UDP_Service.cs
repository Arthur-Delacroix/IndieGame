using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.NetworkInformation;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// UDP基础的接收、发送方法
/// </summary>
public class UDP_Service : MonoBehaviour
{
    //在主界面上显示当前UDP的IP地址和端口号，供UE4程序连接
    [SerializeField] private Text UDPInfo;

    //接收消息后进行各种处理的模块
    [SerializeField] private Receiver rce;

    //此属性需要配置
    public int portListen;//本程序所监听的端口号

    private UdpClient client;
    private Thread receiveThread;
    private IPEndPoint remoteEndPoint;
    //private IPAddress ipAddressSend;
    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);

    private void Start()
    {
        //显示本机UDP信息，供UE4连接用
        UDPInfo.text = "本机IP地址为：" + GetIpAddress() + "\n 本机端口号为：" + portListen.ToString();
    }

    //开启UDP连接
    public void StartUDP()
    {
        //配置目标设备的IP和端口号，用于发送数据
        IPAddress ip;
        if (IPAddress.TryParse(_Global.targetDeviceIP, out ip))
        {
            remoteEndPoint = new IPEndPoint(ip, _Global.targetDevicePort);
        }
        else
        {
            remoteEndPoint = new IPEndPoint(IPAddress.Broadcast, _Global.targetDevicePort);
        }

        //启动一个在后台运行的线程，监听的端口是 portListen
        client = new UdpClient(portListen);
        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();

        Debug.Log("<color=#add8e6>UDP已经启动</color>");
        Debug.Log("<color=#add8e6>目标设备IP：" + _Global.targetDeviceIP + "</color>");
        Debug.Log("<color=#add8e6>目标设备端口：" + _Global.targetDevicePort + "</color>");
        Debug.Log("<color=#add8e6>自身设备监听端口：" + portListen + "</color>");
    }

    //接收来自UE4的消息
    public void ReceiveData()
    {
        while (true)
        {
            //接收到的数据类型为byte数组
            byte[] data = client.Receive(ref anyIP);
            //将byte数组转换为字符串，供JSON解析
            string str = Encoding.Default.GetString(data);

            //rce.OnReceiveMessage(str);
            //rce.rec1(str);
            _Global.transStr = str;

            //此处后期会增加消息反馈机制
            //当收到UDP消息之后，会直接向UE4发送反馈，之后再进行命令处理

            //Debug.Log("<color=#00ff00>接收到的信息为</color>" + "<color=#ffa500>" + str + "</color>");
        }
    }

    //检查端口号是否被占用
    public bool PortInUse(int port)
    {
        bool inUse = false;
        IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
        IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
        foreach (IPEndPoint endPoint in ipEndPoints)
        {
            if (endPoint.Port == port)
            {
                inUse = true;
                break;
            }
        }
        return inUse;
    }

    //获取本机IP地址
    //本机IPv4地址在IP地址列表中，AddressFamily名称为InterNetwork
    private string GetIpAddress()
    {
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault<IPAddress>(a => a.AddressFamily.ToString().Equals("InterNetwork")).ToString();
    }

    //关闭 UDP 连接
    public void CloseUPD()
    {
        if (receiveThread != null)
        {
            receiveThread.Abort();
            receiveThread = null;
        }
        if (client != null)
        {
            client.Close();
        }

        Debug.Log("<color=#00ff00>UDP已关闭</color>");
    }

    //发送数据
    public void SentMsg(string _json)
    {
        //将JSON数据转换为byte
        byte[] byteArray = Encoding.Default.GetBytes(_json);

        //发送至目标设备
        client.Send(byteArray, byteArray.Length, remoteEndPoint);

        Debug.Log("<color=#800080>信息已发送</color>");
    }

    private void OnDisable()
    {
        CloseUPD();
    }

    private void OnDestroy()
    {
        CloseUPD();
    }
}

/*
public class UDP_Client : MonoBehaviour
{
    public int portListen;//3001
    public string ipSend;//192.168.0.101
    public int portSend;//2001

    //接收后经过转换的数据
    public static float received = 0;
    private float lastY = 0;

    private UdpClient client;
    private Thread receiveThread;
    private IPEndPoint remoteEndPoint;
    private IPAddress ipAddressSend;
    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);

    //要同步移动的底图
    [SerializeField] private RectTransform BG_Trans;
    //转换系数 即实际位置*系数=程序中底图位置
    private const float XiShu = 1.52866f;
    public Config config;

    //新
    //屏幕是否在移动
    //此项目左为4900，右为0
    //[SerializeField] private string isMoving;

    //屏幕要移动到的目标物理位置
    //[SerializeField] private float targetPosition;
    //end新
    public void Awake()
    {
        Screen.SetResolution(1080, 1920, true);
        config = new Config();
        StartCoroutine(SetSmoothing());
        //Check if the ip address entered is valid. If not, sendMessage will broadcast to all ip addresses 
        IPAddress ip;
        if (IPAddress.TryParse(ipSend, out ip))
        {
            remoteEndPoint = new IPEndPoint(ip, portSend);
        }
        else
        {
            remoteEndPoint = new IPEndPoint(IPAddress.Broadcast, portSend);
        }

        //Initialize client and thread for receiving
        client = new UdpClient(portListen);

        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private IEnumerator SetSmoothing()
    {
        WWW w = new WWW(Application.streamingAssetsPath + "/config.txt");
        yield return w;

        config = JsonUtility.FromJson<Config>(w.text);
        Debug.Log(config.Smoothing);
        //Debug.Log(config.MoveWay);
    }

    void Update()
    {
        //Check if a message has been recibed
        //if (received != "")
        //{
        Debug.Log(received);
        //这里把接收到的 实际位置信息 转换成 unity中的位置信息
        float temp = received * XiShu;

        //设置背景图位置
        BG_Trans.localPosition = Vector3.Lerp(BG_Trans.localPosition, new Vector3(temp, 0, 0), Time.deltaTime * config.Smoothing) * -1;

        //received = "";

        //}

        //新
        //if (received != "")
        //{
        //    //这里把接收到的 实际位置信息 转换成 unity中的位置信息
        //    float received_float = float.Parse(received);

        //    float temp = received_float * XiShu;

        //    //设置背景图位置
        //    //BG_Trans.localPosition = new Vector3(temp, 0, 0);
        //    BG_Trans.localPosition = Vector3.Lerp(BG_Trans.localPosition, new Vector3(temp, 0, 0), Time.deltaTime * 8.5f);

        //    //判断屏幕的物理位置是否到达目标位置
        //    //向左移动
        //    if (isMoving == "L" && received_float >= targetPosition)
        //    {
        //        UDP_Stop();
        //    }

        //    //向左移动
        //    if (isMoving == "R" && received_float <= targetPosition)
        //    {
        //        UDP_Stop();
        //    }

        //    received = "";
        //}
        //end新
    }

    //接收消息并进行转换
    public void ReceiveData()
    {
        while (true)
        {
            // Bytes received         
            byte[] data = client.Receive(ref anyIP);
            byte[] newData = new byte[4];
            newData[0] = data[3];
            newData[1] = data[2];
            newData[2] = data[1];
            newData[3] = data[0];
            float y = BitConverter.ToSingle(newData, 0);
            if (lastY == y) continue;
            lastY = y;
            // Bytes into text
            received = y;
        }
    }

    //Exit UDP client
    public void OnDisable()
    {
        if (receiveThread != null)
        {
            receiveThread.Abort();
            receiveThread = null;
        }
        client.Close();
        Debug.Log("UDPClient: exit");
    }

    ////////////////////////////////////////////////
    public void SendValueTo(int target)
    {
        if (_Controller.prefabPanel != null)
        {
            Destroy(_Controller.prefabPanel);
        }

        byte[] pos = new byte[] { 0xFF, 0xFF, 0xFF, 0x01 };

        switch (target)
        {
            case 1:
                pos = new byte[] { 0xFF, 0xFF, 0xFF, 0x01 };
                break;
            case 2:
                pos = new byte[] { 0xFF, 0xFF, 0xFF, 0x02 };
                break;
            case 3:
                pos = new byte[] { 0xFF, 0xFF, 0xFF, 0x03 };
                break;
            case 4:
                pos = new byte[] { 0xFF, 0xFF, 0xFF, 0x04 };
                break;
            case 5:
                pos = new byte[] { 0xFF, 0xFF, 0xFF, 0x05 };
                break;
            case 6:
                pos = new byte[] { 0xFF, 0xFF, 0xFF, 0x06 };
                break;
            case 7:
                pos = new byte[] { 0xFF, 0xFF, 0xFF, 0x07 };
                break;
        }

        client.Send(pos, pos.Length, remoteEndPoint);
        Debug.Log("to" + target);
    }

    public void SendValue_Play()
    {
        if (_Controller.prefabPanel != null)
        {
            Destroy(_Controller.prefabPanel);
        }

        byte[] pos = new byte[] { 0xFF, 0xFF, 0xFF, 0x08 };

        client.Send(pos, pos.Length, remoteEndPoint);
        Debug.Log("play");
    }

    public void SendValue_Stop()
    {
        byte[] pos = new byte[] { 0xFF, 0xFF, 0xFF, 0x09 };

        client.Send(pos, pos.Length, remoteEndPoint);
        Debug.Log("stop");
    }

    //新
    ////////////////////////////////////////////////
    //向左移动 0x01左
    //参数为移动到的目标物理位置，一旦超过，发出停止命令
    //public void UDP_MoveLeft(float target)
    //{
    //    isMoving = "L";
    //    targetPosition = target;

    //    byte[] pos = new byte[] { 0x01 };

    //    client.Send(pos, pos.Length, remoteEndPoint);
    //}

    ////向右移动 0x02右
    //public void UDP_MoveRight(int target)
    //{
    //    isMoving = "R";
    //    targetPosition = target;

    //    byte[] pos = new byte[] { 0x02 };

    //    client.Send(pos, pos.Length, remoteEndPoint);
    //}

    ////停止移动 0x03停
    //public void UDP_Stop()
    //{
    //    byte[] pos = new byte[] { 0x03 };

    //    client.Send(pos, pos.Length, remoteEndPoint);
    //}
    //end新
}
*/
