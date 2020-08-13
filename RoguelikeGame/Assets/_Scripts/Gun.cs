using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class Gun : MonoBehaviour
{
    //子弹预置
    [SerializeField] private GameObject bulletToFire;

    //子弹的射出点
    [SerializeField] private Transform firePoint;

    //当鼠标按下后，两个子弹的出现间隔
    //射速
    [SerializeField] private float timeBetweenShots;

    //这个变量用来触发连续射击
    private float shotCounter;

    //枪械名字，用于显示在ui文字上
    public string weaponName;
    //枪械等ui图片
    public Sprite weaponSprite;

    //枪械价格
    public int itemCost;
    //在商店中显示的图片
    public Sprite gunShopSprite;

    private void Update()
    {
        if (PlayerController.ins.canMove && !LevelManager.ins.isPaused)
        {
            if (shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {

                //人物开枪射击
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

                    shotCounter = timeBetweenShots;

                    AudioManager.ins.playSFX(12);
                }

                //连续射击
                //当鼠标一直按下的时候，shotCounter开始随时间变小，当小于等于0的时候就会触发连续射击
                //每次射出子弹后，重新初始化shotCounter的值
                //这样保证了玩家一直按下鼠标时候不会立刻触发连续射击，然后每次连续射击的最大射速和连点的最大射速相同
                //连续射击和连点射击的最大射速，都是timeBetweenShots控制
                //if (Input.GetMouseButton(0))
                //{
                //    shotCounter -= Time.deltaTime;

                //    if (shotCounter <= 0)
                //    {
                //        Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

                //        AudioManager.ins.playSFX(12);

                //        shotCounter = timeBetweenShots;
                //    }
                //}
            }
        }
    }
}