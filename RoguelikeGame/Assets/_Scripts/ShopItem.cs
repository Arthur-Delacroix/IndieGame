using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class ShopItem : MonoBehaviour
{
    //购买道具的文字提示
    [SerializeField] private GameObject buyMessage;

    //玩家是否进入了购买区域
    [SerializeField] private bool inBuyZone;

    //道具价格
    [SerializeField] private int prise;

    //3种道具类型，此判断应该使用继承
    [SerializeField] private bool isHealthRestore;
    [SerializeField] private bool isHealthUpgrade;
    [SerializeField] private bool isWeapon;

    //增加最大生命值上限的量
    [SerializeField] private int healthUpgradeAmount;

    //玩家进入了购买区域
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            buyMessage.SetActive(true);

            inBuyZone = true;
        }
    }

    //玩家走出购买区域
    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            buyMessage.SetActive(false);

            inBuyZone = false;
        }
    }

    private void Update()
    {
        //玩家在购买区域中触发
        if (inBuyZone)
        {
            //按下E键购买道具
            if (Input.GetKeyDown(KeyCode.E))
            {
                //判断金币是否足够
                if (LevelManager.ins.currentCoins >= prise)
                {
                    LevelManager.ins.SpendCoins(prise);

                    if (isHealthRestore)
                    {
                        PlayerHealthController.ins.HealPlayer(PlayerHealthController.ins.maxHealth);
                    }

                    if (isHealthUpgrade)
                    {
                        PlayerHealthController.ins.IncreaseMaxHealth(healthUpgradeAmount);
                    }

                    AudioManager.ins.playSFX(18);

                    Destroy(gameObject);
                }
                else
                {
                    AudioManager.ins.playSFX(19);
                }
            }
        }
    }
}