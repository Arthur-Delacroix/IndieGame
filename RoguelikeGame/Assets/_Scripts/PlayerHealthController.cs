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
        //初始化生命值
        currentHealth = maxHealth;

        //设置生命值的 最大/当前 值
        UIController.ins.healthSlider.maxValue = maxHealth;
        UIController.ins.healthSlider.value = currentHealth;

        //设置生命值显示文字
        UIController.ins.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    void Update()
    {
        //在无敌时间内触发以下代码
        if (invincCount > 0)
        {
            //无敌时间减少
            invincCount -= Time.deltaTime;

            //无敌时间结束后，将任务alpha值恢复正常
            if (invincCount <= 0)
            {
                PlayerController.ins.bodySR.color = new Color(1, 1, 1, 1);
            }

        }
    }

    //玩家受到伤害后触发
    public void DamagePlayer()
    {
        //受伤之前先判断是否处在无敌时间内

        //不在无敌时间内触发以下代码
        if (invincCount <= 0)
        {
            currentHealth--;

            AudioManager.ins.playSFX(11);

            //生命值为0时游戏结束
            if (currentHealth <= 0)
            {
                PlayerController.ins.gameObject.SetActive(false);

                UIController.ins.deathScreen.SetActive(true);

                AudioManager.ins.PlayGameOver();

                AudioManager.ins.playSFX(8);
            }

            //更新当前生命值显示 slider和文字
            UIController.ins.healthSlider.value = currentHealth;
            UIController.ins.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

            //激活无敌时间
            invincCount = damageInvincLength;

            //受伤后玩家身体变成半透明
            PlayerController.ins.bodySR.color = new Color(1, 1, 1, 0.5f);
        }
    }

    //_length 无敌时长
    public void MakeInvincible(float _length)
    {
        invincCount = _length;

        PlayerController.ins.bodySR.color = new Color(1, 1, 1, 0.5f);
    }

    //捡起医疗包时候触发这个方法
    public void HealPlayer(int _healAmount)
    {
        //增加一定量的生命值
        currentHealth += _healAmount;

        //判断是否达到最大生命值
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        //更新当前生命值显示 slider和文字
        UIController.ins.healthSlider.value = currentHealth;
        UIController.ins.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}