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

    private void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
    }

}