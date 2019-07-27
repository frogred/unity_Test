using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //属性值

    public float moveSpeed = 3;

    private float timeVal;
    private float shieldTimeVal = 3;
    private bool isDefend = true;
    private Vector3 bulletEulerAngles;
    public AudioClip BulletSound;


    //引用

    private SpriteRenderer sr;//控制2d物体渲染的时候需要这个组件

    public Sprite[] tankSprite;//储存放在sprite里的4张图

    public GameObject BulletPrefab;//引用子弹预制体

    public GameObject ExplosionPrefab;//引用爆炸预制体

    public GameObject DefendEffectPrefab;

    public AudioSource MoveAudio;//控制音效播放

    public AudioClip[] MoveAudioSource;// 存放音效素材

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();//上右下左，切换图片
    }

    void Start () {
		
	}
	
	void Update () {
        //是否处于无敌状态
        if (isDefend)
        {
            DefendEffectPrefab.SetActive(true);//启用特效
            shieldTimeVal -= Time.deltaTime;
            if(shieldTimeVal < 0)
            {
                isDefend = false;
                DefendEffectPrefab.SetActive(false);//关闭特效
            }
        }
        
    }

    private void FixedUpdate()//帧数固定
    {   
        //如果失败了，玩家就不能再动
        if(PlayerMannager.Instance.isDefeat)
        {
            return;
        }
        Move();//坦克移动
        //Attack();//坦克攻击
        if (timeVal > 0.4f)//攻击cd设置
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }

    private void Move()//坦克，子弹的移动方法
    {
        float h = Input.GetAxisRaw("Horizontal");

        float v = Input.GetAxisRaw("Vertical");

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

        if (v != 0 || h != 0)
        {
            MoveAudio.clip = MoveAudioSource[0];
            if (!MoveAudio.isPlaying)
            {
                MoveAudio.Play();
            }
        }
        else
        {
            MoveAudio.clip = MoveAudioSource[1];
            if (!MoveAudio.isPlaying)
            {
                MoveAudio.Play();
            }
        }

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   //子弹产生的角度：当前坦克的角度加上子弹应该旋转的角度
            //因为没给坦克角度
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
            AudioSource.PlayClipAtPoint(BulletSound, transform.position);
            timeVal = 0;
        }
    }

    private void Die()
    {
        if (isDefend)
        {
            return;
        }
        //产生爆炸特效
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        PlayerMannager.Instance.isDead = true;
    }
}