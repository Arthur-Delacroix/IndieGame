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

    [SerializeField] private RoomPrefabs rooms;

    private void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;

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