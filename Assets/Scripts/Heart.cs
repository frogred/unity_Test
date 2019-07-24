using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    private SpriteRenderer sr;

    public Sprite BrokenSprite;

    public GameObject ExplosionPrefab;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HeartDie()
    {
        sr.sprite = BrokenSprite;
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
    }
}
