using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject[] BrokenPiece;


    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            if (PlayerController.ins.dashCounter > 0)
            {
                Destroy(gameObject);

                int randomPiece = Random.Range(0, BrokenPiece.Length);

                Instantiate(BrokenPiece[randomPiece], transform.position, transform.rotation);
            }
        }
    }
}