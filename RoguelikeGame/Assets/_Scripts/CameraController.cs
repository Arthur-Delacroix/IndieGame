using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649
public class CameraController : MonoBehaviour
{
    public static CameraController ins;

    //camera的移动速度
    [SerializeField] private float moveSpeed;

    //目标房间坐标
    [SerializeField] private Transform target;

    private void Awake()
    {
        ins = this;
    }

    private void Update()
    {
        //当目标房间坐标不为空时，camera强目标房间的位置移动
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }
    }

    //位置切换到目标房间
    public void ChangeTarget(Transform _newTarget)
    {
        target = _newTarget;
    }
}