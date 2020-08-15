using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    public static CharacterTracker ins;

    public int currentHealth, maxHealth, currentCoins;

    private void Awake()
    {
        ins = this;
    }
}