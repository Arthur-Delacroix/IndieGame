using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class GunChest : MonoBehaviour
{
    //宝箱中的可掉落列表
    [SerializeField] private GunPickup[] potentialGuns;

    //自身sprite组件
    [SerializeField] private SpriteRenderer theSR;

    //宝箱打开的图片
    [SerializeField] private Sprite chestOpenSprite;

    //当玩家靠近箱子的时候，会显示的文字信息
    public GameObject notification;

    [SerializeField] private bool canOpen;
    [SerializeField] private bool isOpen;

    [SerializeField] private float scaleSpeed;

    //实例化武器的位置
    //实例化的时候可以做一个掉落动画
    [SerializeField] private GameObject spawnPoint;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            notification.SetActive(true);
        }

        canOpen = true;


    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            notification.SetActive(false);
        }

        canOpen = false;
    }

    private void Update()
    {
        if (canOpen && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                int gunSelect = Random.Range(0, potentialGuns.Length);

                Instantiate(potentialGuns[gunSelect], spawnPoint.transform.position, spawnPoint.transform.rotation);

                theSR.sprite = chestOpenSprite;

                isOpen = true;

                //注意这个开宝箱的动画，实现很简单，其实感觉手感和效果会很好
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
        }

        if (isOpen)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * scaleSpeed);
        }
    }
}