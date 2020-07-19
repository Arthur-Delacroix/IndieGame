using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class CoinPickup : MonoBehaviour
{
    //每个金币的价值
    [SerializeField] private int coinValue;

    //生成后等待一段时间才能被拾取
    [SerializeField] private float waitToBeCollected;

    private void Update()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        //玩家所持金币增加，销毁自身
        if (_other.tag == "Player" && waitToBeCollected <= 0)
        {
            LevelManager.ins.GetCoins(coinValue);

            Destroy(gameObject);

            AudioManager.ins.playSFX(5);
        }
    }
}