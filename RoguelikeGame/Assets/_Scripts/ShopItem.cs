using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Gun[] potentialGuns;

    private Gun theGun;

    [SerializeField] private SpriteRenderer gunSprite;

    [SerializeField] private Text infoText;



    private void Start()
    {
        if (isWeapon)
        {
            int selectedGun = Random.Range(0, potentialGuns.Length);
            theGun = potentialGuns[selectedGun];

            gunSprite.sprite = theGun.gunShopSprite;
            infoText.text = theGun.weaponName + "\n - " + theGun.itemCost + " Gold - ";

            prise = theGun.itemCost;
        }
    }

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

                    if (isWeapon)
                    {
                        Gun gunClone = Instantiate(theGun);

                        gunClone.transform.parent = PlayerController.ins.gunArm;

                        gunClone.transform.localPosition = Vector3.zero;
                        gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        gunClone.transform.localScale = Vector3.one;

                        //gunClone.transform.position = PlayerController.ins.gunArm.position;

                        PlayerController.ins.availableGuns.Add(gunClone);

                        PlayerController.ins.currentGun = PlayerController.ins.availableGuns.Count - 1;

                        PlayerController.ins.SwitchGun();
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