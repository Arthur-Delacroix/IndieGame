using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //人物的移动速度
    [SerializeField] private float moveSpeed;

    //人物移动方向
    private Vector2 moveInput;

    //自身的刚体2D组件
    [SerializeField] private Rigidbody2D theRB;

    //拿枪的Sprite
    [SerializeField] private Transform gunArm;

    void Start()
    {

    }

    void Update()
    {
        //将移动方向的X Y分别绑定到输入轴向上
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //z互译，这里X Y乘以了Time.deltaTime，目的是：
        //如果只是单纯的数值控制移动，在不同性能的设备上，由于帧数的差别，可能造成不同的效果
        //这里乘以Time.deltaTime，为了达到在不同性能设备上，移动距离相等的效果
        // gameObject.transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

        //为人物刚体增加位移
        theRB.velocity = moveInput * moveSpeed;

        //将鼠标的位置转换到游戏屏幕内的坐标
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        //旋转拿枪的Sprite
        //这里用鼠标在屏幕中的位置 减去 人物在屏幕中的位置，求两者X Y的距离，其实是求出了一个向量
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

        //通过取正弦值，先得出弧度值，然后通过乘以角度系数，得出该向量的角度值
        float angle = Mathf.Atan2(offset.y, offset.x)*Mathf.Rad2Deg;

        gunArm.localRotation = Quaternion.Euler(0, 0, angle);
    }
}