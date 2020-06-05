using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class PlayerBullet : MonoBehaviour
{
    //子弹速度
    [SerializeField] private float speed = 7.5f;

    //子弹的刚体组件
    [SerializeField] private Rigidbody2D theRB;

    //子弹打击到墙面后的特效
    [SerializeField] private GameObject impactEffect;

    //子弹的伤害值
    [SerializeField] private int damageToGive;

    void Start()
    {

    }

    void Update()
    {
        //为刚体设置初始速度的正方向
        theRB.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);

        //Debug.Log(other.gameObject.name);

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}