using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject[] BrokenPieces;

    //一个箱子破碎时最多产生多少个碎片
    [SerializeField] private int maxPieces;


    void Start()
    {

    }

    void Update()
    {

    }

    //当人物冲刺时碰到箱子会触发
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            //判断人物是否处在冲刺状态
            if (PlayerController.ins.dashCounter > 0)
            {
                Destroy(gameObject);

                //产生随机数量的碎片
                int piecesToDrop = Random.Range(1, maxPieces);

                //根据随机产生的碎片数量进行循环生成碎片
                for (int i = 0; i < piecesToDrop; i++)
                {
                    //随机从数组中获得碎片的外观
                    int randomPiece = Random.Range(0, BrokenPieces.Length);

                    Instantiate(BrokenPieces[randomPiece], transform.position, transform.rotation);
                }

            }
        }
    }
}