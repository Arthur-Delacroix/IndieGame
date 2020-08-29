﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class BossController : MonoBehaviour
{
    public static BossController ins;

    public BossAction[] actions;

    private int currentAction;
    private float actionCounter;

    private float shootCounter;

    public Rigidbody2D theRB;

    private Vector2 moveDirection;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        actionCounter = actions[currentAction].actionLength;
    }

    private void Update()
    {
        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;

            moveDirection = Vector2.zero;

            if (actions[currentAction].shouldMove)
            {
                if (actions[currentAction].shouldChaseOlayer)
                {
                    moveDirection = PlayerController.ins.transform.position - transform.position;
                    moveDirection.Normalize();
                }

                if (actions[currentAction].moveToPoint)
                {
                    moveDirection = actions[currentAction].targetPoint.position - transform.position;

                    moveDirection.Normalize();
                }
            }

            theRB.velocity = moveDirection * actions[currentAction].moveSpeed;

            //shoot
            if (actions[currentAction].shouldShoot)
            {
                shootCounter -= Time.deltaTime;

                if (shootCounter <= 0)
                {
                    shootCounter = actions[currentAction].shootSpeed;

                    foreach (Transform item in actions[currentAction].shootPoints)
                    {
                        Instantiate(actions[currentAction].bossBullet, item.position, item.rotation);
                    }
                }
            }

        }
        else
        {
            currentAction++;

            if (currentAction >= actions.Length)
            {
                currentAction = 0;
            }

            actionCounter = actions[currentAction].actionLength;
        }
    }
}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;

    public bool shouldMove;
    public bool shouldChaseOlayer;
    public bool moveToPoint;

    public float moveSpeed;

    public Transform targetPoint;

    public bool shouldShoot;
    public GameObject bossBullet;
    public float shootSpeed;
    public Transform[] shootPoints;
}