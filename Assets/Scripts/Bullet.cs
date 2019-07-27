using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float moveSpeed = 10;

    public bool isPlayerBullet = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
	}

    private void OnTriggerEnter2D(Collider2D collision)//collider类，当触发器碰到一个刚体的时候
    {
        switch (collision.tag)//
        {
            case "Tank":
                if (!isPlayerBullet)//非玩家子弹
                {
                    collision.SendMessage("Die");//?
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                if(isPlayerBullet == false)
                {
                    collision.SendMessage("HeartDie");
                    Destroy(gameObject);
                }
                break;
            case "Enemy":
                if (isPlayerBullet)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                    PlayerMannager.Instance.playerscore++;
                }
                break;
            case "Wall":
                Destroy(collision.gameObject);//销毁墙
                Destroy(gameObject);//销毁子弹自身
                break;
            case "Block":
                if (isPlayerBullet)
                {
                    collision.SendMessage("BlockHitSound");
                }
                Destroy(gameObject);//销毁子弹
                break;
            default:
                break;
        }
    }
}
