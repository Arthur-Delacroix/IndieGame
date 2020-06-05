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

    //被击中后释放的粒子效果
    [SerializeField] private GameObject hitEffect;

    //当前敌人是否可以射击
    [SerializeField] private bool shouldShoot;

    //敌人的子弹预置
    [SerializeField] private GameObject bullet;

    //射击的目标位置
    [SerializeField] private Transform firePoint;

    //射速，攻击间隔
    [SerializeField] private float fireRate;

    //计数器，用来控制两次发射子弹的间隔
    private float fireCounter;

    //自身的身体Sprite，用来判断是否出现在屏幕内
    [SerializeField] private SpriteRenderer theBody;

    //敌人的射击范围，玩家进入该范围内才会进行射击
    [SerializeField] private float shotRange;

    private void Update()
    {
        //判断敌人是否出现在了屏幕中，出现在屏幕中才会执行相应的动作
        //判断玩家是否消失，玩家未消失才会执行
        if (theBody.isVisible && PlayerController.ins.gameObject.activeInHierarchy)
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


            //敌人是否可以射击
            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.ins.transform.position) <= shotRange)
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                }
            }
        }
        else
        {
            //当玩家消失后，自身的刚体运动速度为0
            theRB.velocity = Vector2.zero;
        }


        //敌人移动的动画
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

        Instantiate(hitEffect, transform.position, transform.rotation);

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