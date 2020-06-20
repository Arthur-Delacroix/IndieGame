using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class Room : MonoBehaviour
{
    //当玩家进入房间后，门关闭
    [SerializeField] private bool closeWhenEntered;

    //当前房间内所有的门的实例
    [SerializeField] private GameObject[] doors;

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

            Debug.Log("dsadas");
        }
    }
}