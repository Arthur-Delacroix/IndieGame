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

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
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
}