using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        if (gameObject.transform.position.y < 536)
        {
            UIMove();
        }
    }

    private void UIMove()
    {
        int speed = 200;
        gameObject.transform.Translate(Vector3.up * speed * Time.fixedDeltaTime, Space.World);
    }
}
