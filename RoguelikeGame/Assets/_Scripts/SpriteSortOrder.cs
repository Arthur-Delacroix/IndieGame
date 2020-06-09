using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class SpriteSortOrder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer theSR;

    private void Start()
    {
        //Mathf.RoundToInt会将一个float值四舍五入为整数
        //这里要对箱子进行前后排序，如果只是乘以-1，那么Y为0.6的箱子和Y为0.8的箱子，四舍五入后都是1
        //所以这里为了让箱子的前后顺序更加精确，这里将Y值乘以-100
        theSR.sortingOrder = Mathf.RoundToInt(transform.position.y * -100f);
    }
}