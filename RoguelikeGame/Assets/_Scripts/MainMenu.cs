using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 649

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string levelToLoad;

    public CharacterSelector[] characterToDelete;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DeleteSave()
    {
        foreach (CharacterSelector _item in characterToDelete)
        {
            PlayerPrefs.SetInt(_item.playerToSpawn.name, 0);
        }
    }
}