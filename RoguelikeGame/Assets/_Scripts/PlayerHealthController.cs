using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController ins;

    //当前人物生命值
    [SerializeField] private int currentHealth;

    //人物最大生命值
    [SerializeField] private int maxHealth;

    //中弹后的无敌时间
    [SerializeField] private float damageInvincLength = 1f;
    //无敌时间计数器
    [SerializeField] private float invincCount;

    private void Awake()
    {
        ins = this;
    }

    void Start()
    {
        currentHealth = maxHealth;

        UIController.ins.healthSlider.maxValue = maxHealth;
        UIController.ins.healthSlider.value = currentHealth;

        UIController.ins.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    void Update()
    {
        if (invincCount > 0)
        {
            invincCount -= Time.deltaTime;

            if (invincCount <= 0)
            {
                PlayerController.ins.bodySR.color = new Color(1, 1, 1, 1);
            }

        }
    }

    public void DamagePlayer()
    {
        //受伤之前先判断是否处在无敌时间内
        if (invincCount <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                PlayerController.ins.gameObject.SetActive(false);

                UIController.ins.deathScreen.SetActive(true);
            }

            UIController.ins.healthSlider.value = currentHealth;
            UIController.ins.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

            invincCount = damageInvincLength;

            PlayerController.ins.bodySR.color = new Color(1, 1, 1, 0.5f);
        }
    }
}