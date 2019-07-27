using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {

    public bool createPlayer;

    //引用
    public GameObject[] EnemyPrefabList;

    public GameObject PlayerPrefab;

	
	void Start () {
        Invoke("BornTank", 0.8f);
        Destroy(gameObject, 1.2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BornTank()
    {
        if (createPlayer)
        {
            Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(EnemyPrefabList[num], transform.position, Quaternion.identity);
        }
    }
}
