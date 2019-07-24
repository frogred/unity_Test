using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //属性值

    public float moveSpeed = 3;
    private float shieldTimeVal = 3;
    //private bool isDefend = true;
    private Vector3 bulletEulerAngles;

    private float v;
    private float h;

    //引用

    private SpriteRenderer sr;//控制2d物体渲染的时候需要这个组件
    public Sprite[] tankSprite;//储存放在sprite里的4张图
    public GameObject BulletPrefab;//引用子弹预制体
    public GameObject ExplosionPrefab;//引用爆炸预制体
    //public GameObject DefendEffectPrefab;

    //计时器

    private float timeVal;
    private float timeValChangeDirection = 6;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();//上右下左，切换图片
    }


    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //是否处于无敌状态
        //if (isDefend)
        //{
        //    DefendEffectPrefab.SetActive(true);//启用特效
        //    shieldTimeVal -= Time.deltaTime;
        //    if (shieldTimeVal < 0)
        //    {
        //        isDefend = false;
        //        DefendEffectPrefab.SetActive(false);//关闭特效
        //    }
        //}
        int num = Random.Range(1, 5);
        if (timeVal > num)//攻击的时间间隔
        {
            Attack();
            timeVal = 0;
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    private void FixedUpdate()//帧数固定
    {
        Move();//坦克移动
        //Attack();//坦克攻击
    }

    private void Move()//坦克，子弹的移动方法
    {
        if(timeValChangeDirection > 4)
        {
            int num = Random.Range(0, 7);
            if (num > 4)
            {
                v = -1;
                h = 0;
            }
            else if(num == 0)
            {
                v = 1;
                h = 0;
            }
            else if(num==1 || num == 2)
            {
                v = 0;
                h = 1;
            }
            else if (num == 3 || num == 4)
            {
                v = 0;
                h = -1;
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }

        if (h != 0)
        {
            v = 0;
        }
        if (v != 0)
        {
            h = 0;
        }
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, +90);
        }
        else if (h > 0)//向右
        {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }

        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

    }

    private void Attack()
    {
        //子弹产生的角度：当前坦克的角度加上子弹应该旋转的角度？
        Instantiate(BulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        timeVal = 0;
    }

    private void Die()
    {
        
        //产生爆炸特效
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
