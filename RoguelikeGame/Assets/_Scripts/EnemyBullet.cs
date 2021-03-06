﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class EnemyBullet : MonoBehaviour
{
    //子弹速度
    [SerializeField] private float speed;

    //子弹的目标位置
    private Vector3 direction;

    void Start()
    {
        //确定玩家的位置
        direction = PlayerController.ins.transform.position - transform.position;

        direction.Normalize();
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            PlayerHealthController.ins.DamagePlayer();
        }

        Destroy(gameObject);

        AudioManager.ins.playSFX(4);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}