using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    //用于渲染（？）
    private SpriteRenderer sr;
    //用于改变渲染
    public Sprite BrokenSprite;
    //用于存放爆炸特效
    public GameObject ExplosionPrefab;
    //引入音频组件
    public AudioClip DieAudio;

	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}

	void Update () {
		
	}

    public void HeartDie()
    {
        sr.sprite = BrokenSprite;
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        PlayerMannager.Instance.isDefeat = true;
        Invoke("ReturnTotheMenu", 4);
        //在死亡的时候播放音效
        //在当前位置播放
        AudioSource.PlayClipAtPoint(DieAudio, transform.position);

    }
}
