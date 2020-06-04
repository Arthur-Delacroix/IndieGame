using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class EnemyController : MonoBehaviour
{
    //敌人的刚体组件
    [SerializeField] private Rigidbody2D theRB;

    //敌人的移动速度系数
    [SerializeField] private float moveSpeed;

    //敌人发现玩家的半径
    [SerializeField] private float rangeToChaseplayer;

    //敌人的移动方向
    private Vector3 moveDirection;

    [SerializeField] private Animator anim;

    //敌人生命值
    [SerializeField] private int health = 150;

    //死亡特效
    [SerializeField] private GameObject[] deathSplatters;

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.ins.transform.position) < rangeToChaseplayer)
        {
            //根据玩家和敌人之间的距离，计算出一个方向向量，这个向量就是敌人的移动方向
            moveDirection = PlayerController.ins.transform.position - transform.position;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        //将得到的方向归一化，便于乘以速度
        moveDirection.Normalize();

        //敌人移动
        theRB.velocity = moveDirection * moveSpeed;


        if (moveDirection != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    //被子弹击中后，子弹调用该方法，参数为子弹的伤害值
    public void DamageEnemy(int _damage)
    {
        //减去造成的伤害
        health -= _damage;

        //死亡时触发的一系列代码
        if (health <= 0)
        {
            Destroy(gameObject);

            //死亡后留下的痕迹，在多个效果中随机选择
            //Random.Range nim包含 max排除
            int _selectedSplatter = Random.Range(0, deathSplatters.Length);

            float _randomRot = Random.Range(0, 4);

            Instantiate(deathSplatters[_selectedSplatter], transform.position, Quaternion.Euler(0, 0, _randomRot * 90f));
        }
    }
}