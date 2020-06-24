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

    private void Awake()
    {
        ins = this;
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.ins.PlayLevelWin();

        PlayerController.ins.canMove = false;

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene(nextLevel);
    }
}