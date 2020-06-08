using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class BrokenPieces : MonoBehaviour
{
    //碎片的初始移动速度
    [SerializeField] private float moveSpeed;

    //碎片的移动方向
    private Vector3 moveDirection;

    //碎片的减速度
    [SerializeField] private float deceleration;

    //碎片在场景中存在的时间
    [SerializeField] private float lifeTime;

    [SerializeField] private SpriteRenderer theSR;

    [SerializeField] private float fadeSpeed;

    void Start()
    {
        //随机碎片的移动方向
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    void Update()
    {
        //赋予碎片初始移动速度
        transform.position += moveDirection * Time.deltaTime;

        //移动速度随着时间慢慢降低为0
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);

        //碎片的存在时间慢慢减少
        lifeTime -= Time.deltaTime;

        //当碎片存在时间为0时，alpha值从1到0，最后销毁碎片实例
        if (lifeTime < 0)
        {
            theSR.color = new Color(1, 1, 1, Mathf.MoveTowards(theSR.color.a, 0, fadeSpeed * Time.deltaTime));

            if (theSR.color.a == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}