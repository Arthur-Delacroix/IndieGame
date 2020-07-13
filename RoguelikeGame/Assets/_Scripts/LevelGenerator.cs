using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 649

public class LevelGenerator : MonoBehaviour
{
    //创建房间的模板
    [SerializeField] private GameObject layoutRoom;

    //一个关卡内房间的总数
    [SerializeField] private int distanceToEnd;

    //关卡起始房间的颜色
    [SerializeField] private Color startColor;

    //关卡结束房间的颜色
    [SerializeField] private Color endColor;

    //创建房间的起始点
    [SerializeField] private Transform generatorPoint;

    //创建房间的方位
    public enum Direction { up, right, down, left };

    //生成下一个房间的方位
    [SerializeField] private Direction selectedDirection;

    //房间中心点横向之间的距离
    [SerializeField] private float xOffset;
    //房间中心点纵向之间的距离
    [SerializeField] private float yOffset;

    //生成的房间所在的layer，用来在OverlapCircle中进行过滤
    [SerializeField] private LayerMask whatIsRoom;

    //存储当前关卡的最后一个房间
    [SerializeField] private GameObject endRoom;

    //存储所有房间的链表
    [SerializeField] private List<GameObject> layoutRoomObjects = new List<GameObject>();

    //所有类型房间的prefab
    [SerializeField] private RoomPrefabs rooms;

    //记录已经生成的房间外框
    [SerializeField] private List<GameObject> generatedOutlines = new List<GameObject>();

    //起始房间的Prefab
    [SerializeField] private RoomCenter centerStart;
    //结束的房间
    [SerializeField] private RoomCenter centerEnd;
    //各种不同样式的中间房间
    [SerializeField] private RoomCenter[] potentialCenters;

    private void Start()
    {
        GameObject _startTmp;

        _startTmp = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
        _startTmp.GetComponent<SpriteRenderer>().color = startColor;


        //为下一个房间随机一个方位
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();

        //循环生成规定数量的房间
        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject _newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);

            layoutRoomObjects.Add(_newRoom);

            //判断当前生成的房间是不是最后一个，如果是的话，对该房间上色
            if (i + 1 == distanceToEnd)
            {
                _newRoom.GetComponent<SpriteRenderer>().color = endColor;

                //不将最后一个房间加入链表
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);

                endRoom = _newRoom;
            }

            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();

            //检测新生成的房间是否与已有的房间重叠
            while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, whatIsRoom))
            {
                MoveGenerationPoint();
            }
        }

        //创建初始房间的开口方向
        CreateRoomOutline(Vector3.zero);

        //创建除第一和最后一个房间以外，所有房间的开口方向
        foreach (GameObject _room in layoutRoomObjects)
        {
            CreateRoomOutline(_room.transform.position);
        }

        //创建最后一个房间的开口方向
        CreateRoomOutline(endRoom.transform.position);

        //生成房间的内容
        foreach (GameObject _outline in generatedOutlines)
        {
            if (generatedOutlines.IndexOf(_outline) == 0)
            {
                RoomCenter _tmp1 = Instantiate(centerStart, _outline.transform.position, _outline.transform.rotation);
                _tmp1.theRoom = _outline.GetComponent<Room>();
            }
            else if (generatedOutlines.IndexOf(_outline) == generatedOutlines.Count - 1)
            {
                RoomCenter _tmp2 = Instantiate(centerEnd, _outline.transform.position, _outline.transform.rotation);
                _tmp2.theRoom = _outline.GetComponent<Room>();
            }
            else
            {
                //从数组中随机算则一个房间内容
                int _centerSelect = Random.Range(0, potentialCenters.Length);

                //实例化该房间内容
                RoomCenter _tmp = Instantiate(potentialCenters[_centerSelect], _outline.transform.position, _outline.transform.rotation);

                //将房间内容与房间外框连接起来
                _tmp.theRoom = _outline.GetComponent<Room>();
            }
        }

        //销毁所有辅助定位用的layout
        foreach (GameObject _layout in layoutRoomObjects)
        {
            Destroy(_layout);
        }
        layoutRoomObjects.Clear();
        Destroy(_startTmp);
        Destroy(endRoom);


    }

    //按偏移量移动房间的生成器的位置
    public void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0f, yOffset * -1f, 0f);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(xOffset * -1f, 0f, 0f);
                break;
        }
    }

    public void CreateRoomOutline(Vector3 _roomPosition)
    {
        //上方是否有房间
        bool _roomUp = Physics2D.OverlapCircle(_roomPosition + new Vector3(0f, yOffset, 0f), 0.2f, whatIsRoom);

        //下方是否有房间
        bool _roomDown = Physics2D.OverlapCircle(_roomPosition + new Vector3(0f, yOffset * -1f, 0f), 0.2f, whatIsRoom);

        //左边是否有房间
        bool _roomLeft = Physics2D.OverlapCircle(_roomPosition + new Vector3(xOffset * -1f, 0f, 0f), 0.2f, whatIsRoom);

        //右边是否有房间
        bool _roomRight = Physics2D.OverlapCircle(_roomPosition + new Vector3(xOffset, 0f, 0f), 0.2f, whatIsRoom);

        //房间的开口数量
        int _directionCount = 0;

        //如果一个方向上为true，则证明该方向上有开口，开口数量就+1
        //先确定开口数量，再看开口位置
        if (_roomUp)
        {
            _directionCount++;
        }
        if (_roomDown)
        {
            _directionCount++;
        }
        if (_roomLeft)
        {
            _directionCount++;
        }
        if (_roomRight)
        {
            _directionCount++;
        }

        //分别判断每个房间有几个开口，开口方向
        switch (_directionCount)
        {
            case 0:
                Debug.LogError("no room exists!");
                break;

            case 1:
                if (_roomUp)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleUp, _roomPosition, transform.rotation));
                }
                if (_roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleDown, _roomPosition, transform.rotation));
                }
                if (_roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleLeft, _roomPosition, transform.rotation));
                }
                if (_roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleRight, _roomPosition, transform.rotation));
                }
                break;

            case 2:
                if (_roomUp && _roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpDown, _roomPosition, transform.rotation));
                }
                if (_roomLeft && _roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, _roomPosition, transform.rotation));
                }
                if (_roomUp && _roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpRight, _roomPosition, transform.rotation));
                }
                if (_roomRight && _roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleRightDown, _roomPosition, transform.rotation));
                }
                if (_roomDown && _roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleDownLeft, _roomPosition, transform.rotation));
                }
                if (_roomLeft && _roomUp)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftUp, _roomPosition, transform.rotation));
                }
                break;

            case 3:
                if (_roomUp && _roomRight && _roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpRightDown, _roomPosition, transform.rotation));
                }
                if (_roomRight && _roomDown && _roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleRightDownLeft, _roomPosition, transform.rotation));
                }
                if (_roomDown && _roomLeft && _roomUp)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleDownLeftUp, _roomPosition, transform.rotation));
                }
                if (_roomLeft && _roomUp && _roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftUpRight, _roomPosition, transform.rotation));
                }
                break;

            case 4:
                if (_roomLeft && _roomUp && _roomRight && _roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.fourway, _roomPosition, transform.rotation));
                }
                break;
        }
    }
}

//各种门朝向的房间
[System.Serializable]
public class RoomPrefabs
{
    public GameObject
        singleUp, singleDown, singleLeft, singleRight,
        doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp,
        tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
        fourway;
}