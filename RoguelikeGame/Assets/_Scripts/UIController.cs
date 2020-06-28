using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    //屏幕淡入淡出遮挡层
    [SerializeField] private Image fadeScreen;
    //淡入淡出速度
    [SerializeField] private float fadeSpeed;

    //
    [SerializeField] private bool fadeToBlack;
    //
    [SerializeField] private bool fadeOutBlack;

    //[SerializeField] private GameObject pauseMenu;

    public GameObject pauseMenu;

    private void Awake()
    {
        ins = this;
    }

    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    void Update()
    {
        //屏幕遮挡层逐渐消失
        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
                //fadeScreen.gameObject.SetActive(false);
            }
        }

        //屏幕遮挡层逐渐消失
        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeOutBlack = false;
        fadeToBlack = true;
    }

    public void LaodScene(string _sceneName)
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(_sceneName);
    }

    public void ResumeGame()
    {
        //pauseMenu.SetActive(false);

        LevelManager.ins.PauseUnpause();
    }
}