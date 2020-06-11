using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class AudioManager : MonoBehaviour
{
    public static AudioManager ins;

    //各个状态的音乐播放组件
    [SerializeField] private AudioSource levelMusic;
    [SerializeField] private AudioSource gameOverMusic;
    [SerializeField] private AudioSource winMusic;

    private void Awake()
    {
        ins = this;
    }

    public void PlayGameOver()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }

    public void PlayLevelWin()
    {
        levelMusic.Stop();
        winMusic.Play();
    }
}