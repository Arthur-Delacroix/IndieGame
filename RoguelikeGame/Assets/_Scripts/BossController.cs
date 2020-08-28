using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController ins;

    public BossAction[] actions;

    private void Awake()
    {
        ins = this;
    }
}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;

    public bool shouldMove;
    public bool shouldChaseOlayer;
    public bool moveToPoint ;

    public float moveSpeed;

    public Transform targetPoint;

    public bool shouldShoot;
    public GameObject bossBullet;
    public float shootSpeed;
    public Transform[] shootPoints;
}