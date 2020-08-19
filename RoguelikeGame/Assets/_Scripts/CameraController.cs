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
    public Transform target;

    //游戏视图 和 大地图视图切换用的camera
    public Camera mainCamera;
    [SerializeField] private Camera bigMapCamera;
    //是否显示大地图
    [SerializeField] private bool isBigMapActive;

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

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isBigMapActive)
            {
                ActivateBigMap();
            }
            else
            {
                DeactivateBigMap();
            }
        }
    }

    //位置切换到目标房间
    public void ChangeTarget(Transform _newTarget)
    {
        target = _newTarget;
    }

    public void ActivateBigMap()
    {
        if (!LevelManager.ins.isPaused)
        {
            isBigMapActive = true;

            bigMapCamera.enabled = true;
            mainCamera.enabled = false;

            PlayerController.ins.canMove = false;
            Time.timeScale = 0f;

            UIController.ins.mapDisplay.SetActive(false);
            UIController.ins.bigMapText.SetActive(true);
        }
    }

    public void DeactivateBigMap()
    {
        if (!LevelManager.ins.isPaused)
        {
            isBigMapActive = false;

            bigMapCamera.enabled = false;
            mainCamera.enabled = true;

            PlayerController.ins.canMove = true;
            Time.timeScale = 1f;

            UIController.ins.mapDisplay.SetActive(true);
            UIController.ins.bigMapText.SetActive(false);
        }
    }
}