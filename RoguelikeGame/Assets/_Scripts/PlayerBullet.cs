using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    //子弹速度
    [SerializeField] private float speed = 7.5f;

    //子弹的刚体组件
    [SerializeField] private Rigidbody2D theRB;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //为刚体设置初始速度的正方向
        theRB.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}