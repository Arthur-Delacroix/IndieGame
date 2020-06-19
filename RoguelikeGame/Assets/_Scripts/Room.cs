using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //当玩家进入该房间的时候，将该房间的位置信息传给camera
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            CameraController.ins.ChangeTarget(transform);
        }
    }
}