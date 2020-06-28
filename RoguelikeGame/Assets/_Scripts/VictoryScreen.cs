using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 649

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private float waitForAnyKey = 2f;

    [SerializeField] private GameObject anyKeyText;

    [SerializeField] private string mainMenuScene;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (waitForAnyKey > 0)
        {
            waitForAnyKey -= Time.deltaTime;

            if (waitForAnyKey <= 0)
            {
                anyKeyText.SetActive(true);
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(mainMenuScene);
            }
        }
    }
}