using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class RoomCenter : MonoBehaviour
{
    //当房间内的人被全部消灭时，门会打开
    [SerializeField] private bool openWhenEnemiesCleared;

    //房间内所有的敌人
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    public Room theRoom;

    private void Start()
    {
        if (openWhenEnemiesCleared)
        {
            theRoom.closeWhenEntered = true;
        }
    }

    private void Update()
    {
        //检测房间内是不是所有的敌人都被消灭了
        if (enemies.Count > 0 && theRoom.roomActive && openWhenEnemiesCleared)
        {
            //isEnemiesCleared = false;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                theRoom.OpenDoors();
            }
        }
    }
}