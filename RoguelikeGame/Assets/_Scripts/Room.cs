using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class Room : MonoBehaviour
{
    //当玩家进入房间后，门关闭
    [SerializeField] private bool closeWhenEntered;

    //当房间内的人被全部消灭时，门会打开
    //[SerializeField] private bool openWhenEnemiesCleared;

    //当前房间内所有的门的实例
    [SerializeField] private GameObject[] doors;

    //房间内所有的敌人
    //[SerializeField] private List<GameObject> enemies = new List<GameObject>();

    //是否为玩家当前进入的房间
    public bool roomActive;

    private void Update()
    {
        //检测房间内是不是所有的敌人都被消灭了
        //if (enemies.Count > 0 && roomActive && openWhenEnemiesCleared)
        //{
        //    //isEnemiesCleared = false;
        //    for (int i = 0; i < enemies.Count; i++)
        //    {
        //        if (enemies[i] == null)
        //        {
        //            enemies.RemoveAt(i);
        //            i--;
        //        }
        //    }
            //if (enemies.Count == 0)
            //{
            //    foreach (GameObject door in doors)
            //    {
            //        door.SetActive(false);
            //        closeWhenEntered = false;
            //    }
            //}
        //}
    }

    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);

            closeWhenEntered = false;
        }
    }

    //当玩家进入该房间的时候，将该房间的位置信息传给camera
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            CameraController.ins.ChangeTarget(transform);

            if (closeWhenEntered)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }

            roomActive = true;
        }
    }

    //当玩家离开该房间时候触发
    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            roomActive = false;
        }
    }
}