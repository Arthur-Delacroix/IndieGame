using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;

        //为下一个房间随机一个方位
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();
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