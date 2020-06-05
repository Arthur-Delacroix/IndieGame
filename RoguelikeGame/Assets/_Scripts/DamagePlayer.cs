using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

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
            PlayerHealthController.ins.DamagePlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            PlayerHealthController.ins.DamagePlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            PlayerHealthController.ins.DamagePlayer();
        }
    }

    private void OnCollisionStay2D(Collision2D _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            PlayerHealthController.ins.DamagePlayer();
        }
    }
}