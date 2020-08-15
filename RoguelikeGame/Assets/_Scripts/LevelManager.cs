using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 649

public class LevelManager : MonoBehaviour
{
    public static LevelManager ins;

    //加载时要等待多少秒
    [SerializeField] private float waitToLoad = 4f;

    //要加在的关卡名称
    [SerializeField] private string nextLevel;

    //检测关卡是否处在暂停状态
    //[SerializeField] private bool isPaused;
    public bool isPaused;

    //玩家金币
    public int currentCoins;

    private void Awake()
    {
        ins = this;

        currentCoins = CharacterTracker.ins.currentCoins;
    }

    private void Start()
    {
        Time.timeScale = 1f;

        UIController.ins.coinText.text = currentCoins.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.ins.PlayLevelWin();

        PlayerController.ins.canMove = false;

        UIController.ins.StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);



        SceneManager.LoadScene(nextLevel);
    }

    //暂停/继续 游戏
    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UIController.ins.pauseMenu.SetActive(true);

            isPaused = true;

            Time.timeScale = 0f;
        }
        else
        {
            UIController.ins.pauseMenu.SetActive(false);

            isPaused = false;

            Time.timeScale = 1f;
        }
    }

    //获得金币
    public void GetCoins(int _amount)
    {
        currentCoins += _amount;

        UIController.ins.coinText.text = currentCoins.ToString();
    }

    //消费金币
    public void SpendCoins(int _amount)
    {
        currentCoins -= _amount;

        if (currentCoins < 0)
        {
            currentCoins = 0;
        }

        UIController.ins.coinText.text = currentCoins.ToString();
    }
}