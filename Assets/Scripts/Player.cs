using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //属性值

    public float moveSpeed = 3;
    public float bulletSpeed = 0.4f;
    private float timeVal;
    private float shieldTimeVal = 3;
    private bool isDefend = true;
    private Vector3 bulletEulerAngles;
    public AudioClip BulletSound;
    private float upLevel = 0;

    //引用

    private SpriteRenderer sr;//控制2d物体渲染的时候需要这个组件

    public Sprite[] tankSprite;//储存放在sprite里的4张图

    public Sprite[] tankSpriteSR;//升级后的图

    public Sprite[] tankSpriteUR;//同上

    public GameObject BulletPrefab;//引用子弹预制体

    public GameObject ExplosionPrefab;//引用爆炸预制体

    public GameObject DefendEffectPrefab;

    public AudioSource MoveAudio;//控制移动音效播放

    public AudioClip BonusAudio;//控制获得奖励音效

    public AudioClip[] MoveAudioSource;// 存放音效素材

    //函数部分

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
            Shield();
        }
        //升级判断
        if (upLevel >= 2)
        {
            upLevel = 2;
        }
        if (upLevel == 1)
        {
            moveSpeed = 4;
            tankSprite = tankSpriteSR;
        }
        else if(upLevel == 2)
        {
            bulletSpeed = 0.3f;
            tankSprite = tankSpriteUR;
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
        if (timeVal > bulletSpeed)//攻击cd设置
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Bon_Life":
                collision.SendMessage("PlusLife");
                AudioSource.PlayClipAtPoint(BonusAudio,transform.position);
                break;
            case "Bon_Boom":
                collision.SendMessage("Boom");
                AudioSource.PlayClipAtPoint(BonusAudio, transform.position);
                break;
            case "Bon_Shield":
                collision.SendMessage("GetShield");
                AudioSource.PlayClipAtPoint(BonusAudio, transform.position);
                break;
            case "Bon_LevelUp":
                collision.SendMessage("GetLevelUp");
                AudioSource.PlayClipAtPoint(BonusAudio, transform.position);
                break;
            case "Bon_TheWorld":
                collision.SendMessage("Dio");
                AudioSource.PlayClipAtPoint(BonusAudio, transform.position);
                break;

            default:
                break;
        }
    }

    private void Shield()
    {
        DefendEffectPrefab.SetActive(true);//启用特效
        shieldTimeVal -= Time.deltaTime;
        if (shieldTimeVal < 0)
        {
            isDefend = false;
            DefendEffectPrefab.SetActive(false);//关闭特效
        }
    }

    private void ShieldActive()
    {
        isDefend = true;
        shieldTimeVal = 10;
    }

    private void LevelUp()
    {
        upLevel++;
    }
}