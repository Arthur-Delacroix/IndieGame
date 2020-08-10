using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class GunPickup : MonoBehaviour
{
    //枪械预置
    [SerializeField] private Gun theGun;

    //生成后等待一段时间才能被拾取
    [SerializeField] private float waitToBeCollected;

    private void Update()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player" && waitToBeCollected <= 0)
        {
            //检查玩家是否已经拥有了相同的武器
            bool hasGun = false;

            foreach (Gun _gunToCheck in PlayerController.ins.availableGuns)
            {
                if (theGun.weaponName == _gunToCheck.weaponName)
                {
                    hasGun = true;
                }
            }

            //如果没有当前拾取的枪械
            if (!hasGun)
            {
                Gun gunClone = Instantiate(theGun);

                gunClone.transform.parent = PlayerController.ins.gunArm;

                gunClone.transform.localPosition = Vector3.zero;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;

                //gunClone.transform.position = PlayerController.ins.gunArm.position;

                PlayerController.ins.availableGuns.Add(gunClone);

                PlayerController.ins.currentGun = PlayerController.ins.availableGuns.Count - 1;

                PlayerController.ins.SwitchGun();
            }

            Destroy(gameObject);

            AudioManager.ins.playSFX(7);
        }
    }
}