using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class HealthPickup : MonoBehaviour
{
    //回复多少生命值
    [SerializeField] private int healAmount;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        //恢复玩家生命值，销毁自身
        if (_other.tag == "Player")
        {
            PlayerHealthController.ins.HealPlayer(healAmount);

            Destroy(gameObject);
        }
    }
}