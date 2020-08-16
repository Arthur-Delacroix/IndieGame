using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class PlayerController : MonoBehaviour
{
    public static PlayerController ins;

    //人物的普通移动速度
    [SerializeField] private float moveSpeed;

    //人物冲刺时候的速度
    [SerializeField] private float dashSpeed;
    //冲刺的持续时间
    [SerializeField] private float dashDuration;
    //冲刺技能冷却时间
    [SerializeField] private float dashCooldown;
    //冲刺中的无敌时间
    [SerializeField] private float dashInvincibility;
    //技能持续时间的计数器
    public float dashCounter;
    //技能冷却时间计数器
    private float dashCooldownCounter;

    //人物移动方向
    private Vector2 moveInput;

    //自身的刚体2D组件
    [SerializeField] private Rigidbody2D theRB;

    //拿枪的Sprite
    [SerializeField] public Transform gunArm;

    //主摄像机
    //[SerializeField] private Camera theCam;

    //动画控制器组件
    [SerializeField] private Animator anim;

    ////子弹预置
    //[SerializeField] private GameObject bulletToFire;

    ////子弹的射出点
    //[SerializeField] private Transform firePoint;

    ////当鼠标按下后，两个子弹的出现间隔
    ////射速
    //[SerializeField] private float timeBetweenShots;

    ////这个变量用来触发连续射击
    //private float shotCounter;

    //身体的sprite组件，用来显示中弹后无敌时间的效果
    public SpriteRenderer bodySR;

    //人物的实际速度
    private float activeMoveSpeed;

    public bool canMove = true;

    [SerializeField] public List<Gun> availableGuns = new List<Gun>();
    [SerializeField] public int currentGun;

    private void Awake()
    {
        ins = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        activeMoveSpeed = moveSpeed;

        UIController.ins.currentGunImage.sprite = availableGuns[currentGun].weaponSprite;
        UIController.ins.currentGunText.text = availableGuns[currentGun].weaponName;
    }

    void Update()
    {
        if (canMove && !LevelManager.ins.isPaused)
        {
            //将移动方向的X Y分别绑定到输入轴向上
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            //将输入的值进行归一化，使其在各个方向的输入数值的绝对值始终相等（即为一个圆形）
            //举例：如果是向正上方移动，输入为1，乘以速度系数2.5后，速度为2.5，正右方向移动输入为1，乘以速度系数2.5后，速度为2.5
            //但是当向右斜上方移动时候，向量长度则为根号2，是大于1的，乘以速度系数之后，是2.5*根号2，所以人物在非竖直水平移动的时候，，速度会快
            //归一化是位了让人物在各个方向的移动上速度都是相同的
            moveInput.Normalize();

            //注意，这里X Y乘以了Time.deltaTime，目的是：
            //如果只是单纯的数值控制移动，在不同性能的设备上，由于帧数的差别，可能造成不同的效果
            //这里乘以Time.deltaTime，为了达到在不同性能设备上，移动距离相等的效果
            // gameObject.transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

            //为人物刚体增加位移
            theRB.velocity = moveInput * activeMoveSpeed;

            //将鼠标的位置转换到游戏屏幕内的坐标
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = CameraController.ins.mainCamera.WorldToScreenPoint(transform.localPosition);

            //判断鼠标是在人物的左面还是右面
            //人物始终要看向鼠标所在的那一侧
            if (mousePos.x < screenPoint.x)
            {
                //通过改变X的scale来控制人物的朝向，1为右侧，-1为左侧
                transform.localScale = new Vector3(-1, 1, 1);
                gunArm.localScale = new Vector3(-1, -1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
                gunArm.localScale = Vector3.one;
            }


            //这里用鼠标在屏幕中的位置 减去 人物在屏幕中的位置，求两者X Y的距离，其实是求出了一个向量
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            //通过取正弦值，先得出弧度值，然后通过乘以角度系数，得出该向量的角度值
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            //旋转拿枪的Sprite
            gunArm.rotation = Quaternion.Euler(0, 0, angle);

            ////人物开枪射击
            //if (Input.GetMouseButtonDown(0))
            //{
            //    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

            //    shotCounter = timeBetweenShots;

            //    AudioManager.ins.playSFX(12);
            //}

            ////连续射击
            ////当鼠标一直按下的时候，shotCounter开始随时间变小，当小于等于0的时候就会触发连续射击
            ////每次射出子弹后，重新初始化shotCounter的值
            ////这样保证了玩家一直按下鼠标时候不会立刻触发连续射击，然后每次连续射击的最大射速和连点的最大射速相同
            ////连续射击和连点射击的最大射速，都是timeBetweenShots控制
            //if (Input.GetMouseButton(0))
            //{
            //    shotCounter -= Time.deltaTime;

            //    if (shotCounter <= 0)
            //    {
            //        Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

            //        AudioManager.ins.playSFX(12);

            //        shotCounter = timeBetweenShots;
            //    }
            //}

            //切换武器
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (availableGuns.Count > 0)
                {
                    currentGun++;

                    if (currentGun >= availableGuns.Count)
                    {
                        currentGun = 0;
                    }

                    SwitchGun();
                }
            }

            //按下按键触发无敌
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //技能在冷却完成状态 & 技能不再施放中
                if (dashCooldownCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashDuration;

                    anim.SetTrigger("dash");

                    PlayerHealthController.ins.MakeInvincible(dashInvincibility);

                    AudioManager.ins.playSFX(8);
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;

                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCooldownCounter = dashCooldown;
                }
            }

            if (dashCooldownCounter > 0)
            {
                dashCooldownCounter -= Time.deltaTime;
            }

            //判断人物是否在移动，并播放相应的动画
            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

        }
        else//当玩家不能移动时触发
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }

    public void SwitchGun()
    {
        foreach (Gun _theGun in availableGuns)
        {
            _theGun.gameObject.SetActive(false);
        }

        availableGuns[currentGun].gameObject.SetActive(true);

        UIController.ins.currentGunImage.sprite = availableGuns[currentGun].weaponSprite;
        UIController.ins.currentGunText.text = availableGuns[currentGun].weaponName;
    }
}