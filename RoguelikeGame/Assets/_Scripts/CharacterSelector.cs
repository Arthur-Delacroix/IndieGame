using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private bool canSelect;

    public GameObject message;

    public PlayerController playerToSpawn;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            canSelect = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            canSelect = false;
            message.SetActive(false);
        }
    }

    private void Update()
    {
        if (canSelect)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 playerPos = PlayerController.ins.transform.position;

                Destroy(PlayerController.ins.gameObject);

                PlayerController newPlayer = Instantiate(playerToSpawn, playerPos, playerToSpawn.transform.rotation);

                PlayerController.ins = newPlayer;

                gameObject.SetActive(false);

                CameraController.ins.target = newPlayer.transform;

                CharacterSelectManager.ins.activePlayer = newPlayer;
                CharacterSelectManager.ins.activeCharacterSelect.gameObject.SetActive(true);
                CharacterSelectManager.ins.activeCharacterSelect = this;
            }
        }
    }
}