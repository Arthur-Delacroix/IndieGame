using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class UIController : MonoBehaviour
{
    public static UIController ins;

    //血条
    public Slider healthSlider;

    //显示生命值的UI文字
    public Text healthText;

    //游戏结束界面
    public GameObject deathScreen;

    private void Awake()
    {
        ins = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}