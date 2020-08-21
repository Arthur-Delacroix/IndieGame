using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnlockCage : MonoBehaviour
{
    private bool canUnlock;

    public GameObject message;

    public CharacterSelector[] characterSelects;

    public CharacterSelector playerUnlock;

    public SpriteRenderer cageSR;

    private void Start()
    {
        playerUnlock = characterSelects[Random.Range(0, characterSelects.Length)];

        cageSR.sprite = playerUnlock.playerToSpawn.bodySR.sprite;
    }

    private void Update()
    {
        if (canUnlock)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(playerUnlock, transform.position, transform.rotation);

                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            canUnlock = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            canUnlock = false;
            message.SetActive(false);
        }
    }
}