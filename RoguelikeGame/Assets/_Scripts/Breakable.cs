using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class Breakable : MonoBehaviour
{
    //每个箱子包含的碎片prefab
    [SerializeField] private GameObject[] BrokenPieces;

    //一个箱子破碎时最多产生多少个碎片
    [SerializeField] private int maxPieces;

    //箱子被击碎时是否会掉落道具
    [SerializeField] private bool shouldDropItem;

    //箱子会掉落的道具prefab
    [SerializeField] private GameObject[] itemToDrop;

    //掉落物品的几率
    [SerializeField] private float itemDropPercent;





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

                //触发掉落物品
                if (shouldDropItem)
                {
                    //首先随机产生一个0到100的数值
                    float dropChance = Random.Range(0f, 100f);

                    //判断该数值是否在掉落几率内
                    if (dropChance < itemDropPercent)
                    {
                        //随机选择一个掉落物品
                        int randomItem = Random.Range(0, itemToDrop.Length);

                        //生成掉落物品
                        Instantiate(itemToDrop[randomItem], transform.position, transform.rotation);
                    }
                }

            }
        }
    }
}