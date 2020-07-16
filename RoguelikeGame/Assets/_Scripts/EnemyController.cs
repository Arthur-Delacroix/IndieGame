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

    [SerializeField] private bool shouldChasePlayer;
    //敌人发现玩家的半径
    [SerializeField] private float rangeToChasePlayer;

    //敌人的移动方向
    private Vector3 moveDirection;

    //Coward人物控制
    [SerializeField] private bool shouldRunaway;
    [SerializeField] private float runawayRange;

    //Blob
    [SerializeField] private bool shouldWander;
    [SerializeField] private float wanderLength;
    [SerializeField] private float pauselength;
    [SerializeField] private float wanderCounter;
    [SerializeField] private float pauseCounter;
    [SerializeField] private Vector3 wanderDirection;

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

    private void Start()
    {
        if (shouldWander)
        {
            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
    }

    private void Update()
    {
        //判断敌人是否出现在了屏幕中，出现在屏幕中才会执行相应的动作
        //判断玩家是否消失，玩家未消失才会执行
        if (theBody.isVisible && PlayerController.ins.gameObject.activeInHierarchy)
        {
            moveDirection = Vector3.zero;

            if (Vector3.Distance(transform.position, PlayerController.ins.transform.position) < rangeToChasePlayer && shouldChasePlayer)
            {
                //根据玩家和敌人之间的距离，计算出一个方向向量，这个向量就是敌人的移动方向
                moveDirection = PlayerController.ins.transform.position - transform.position;
            }
            else
            {
                //wander
                if (shouldWander)
                {
                    if (wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;

                        //move the enemy
                        moveDirection = wanderDirection;

                        if (wanderCounter <= 0)
                        {
                            pauseCounter = Random.Range(pauselength * 0.5f, pauselength * 1.5f);
                        }
                    }

                    if (pauseCounter > 0)
                    {
                        pauseCounter -= Time.deltaTime;

                        if (pauseCounter <= 0)
                        {
                            wanderCounter = wanderCounter = Random.Range(wanderLength * 0.5f, wanderLength * 1.5f);

                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                        }
                    }
                }
                //wander end
            }

            //Coward 玩家离敌人太远，敌人会逃走
            if (shouldRunaway && Vector3.Distance(transform.position, PlayerController.ins.transform.position) < runawayRange)
            {
                moveDirection = -(PlayerController.ins.transform.position - transform.position);
            }

            //else
            //{
            //    moveDirection = Vector3.zero;
            //}

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

                    AudioManager.ins.playSFX(13);
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

        AudioManager.ins.playSFX(2);

        Instantiate(hitEffect, transform.position, transform.rotation);

        //死亡时触发的一系列代码
        if (health <= 0)
        {
            Destroy(gameObject);

            AudioManager.ins.playSFX(1);

            //死亡后留下的痕迹，在多个效果中随机选择
            //Random.Range nim包含 max排除
            int _selectedSplatter = Random.Range(0, deathSplatters.Length);

            float _randomRot = Random.Range(0, 4);

            Instantiate(deathSplatters[_selectedSplatter], transform.position, Quaternion.Euler(0, 0, _randomRot * 90f));
        }
    }
}