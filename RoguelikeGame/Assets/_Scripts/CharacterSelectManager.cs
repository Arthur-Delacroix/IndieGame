using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    public static CharacterSelectManager ins;

    public PlayerController activePlayer;

    public CharacterSelector activeCharacterSelect;

    private void Awake()
    {
        ins = this;
    }


}