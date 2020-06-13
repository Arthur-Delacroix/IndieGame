using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class HealthPickup : MonoBehaviour
{
    //回复多少生命值
    [SerializeField] private int healAmount;

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
        //恢复玩家生命值，销毁自身
        if (_other.tag == "Player" && waitToBeCollected <= 0)
        {
            PlayerHealthController.ins.HealPlayer(healAmount);

            Destroy(gameObject);

            AudioManager.ins.playSFX(7);
        }
    }
}