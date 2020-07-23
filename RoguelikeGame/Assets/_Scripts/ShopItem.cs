using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class ShopItem : MonoBehaviour
{
    [SerializeField] private GameObject buyMessage;

    [SerializeField] private bool inBuyZone;

    [SerializeField] private int prise;

    [SerializeField] private bool isHealthRestore;
    [SerializeField] private bool isHealthUpgrade;
    [SerializeField] private bool isWeapon;


    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            buyMessage.SetActive(true);

            inBuyZone = true;
        }
    }

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
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (LevelManager.ins.currentCoins >= prise)
                {
                    LevelManager.ins.SpendCoins(prise);

                    if (isHealthRestore)
                    {
                        PlayerHealthController.ins.HealPlayer(PlayerHealthController.ins.maxHealth);
                    }
                }
            }
        }
    }
}