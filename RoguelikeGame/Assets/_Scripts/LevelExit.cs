using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 649
public class LevelExit : MonoBehaviour
{
    [SerializeField] private string levelToLoad;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            CharacterTracker.ins.currentCoins = LevelManager.ins.currentCoins;
            CharacterTracker.ins.maxHealth = PlayerHealthController.ins.maxHealth;
            CharacterTracker.ins.currentHealth = PlayerHealthController.ins.currentHealth;

            StartCoroutine(LevelManager.ins.LevelEnd());
        }
    }
}