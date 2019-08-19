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
            int num = Random.Range(0, 8);
            if(num==0 || num == 2 || num == 1)
            {
                Instantiate(EnemyPrefabList[0], transform.position, Quaternion.identity);
            }
            else if(num == 3 || num == 5 || num == 4)
            {
                Instantiate(EnemyPrefabList[1], transform.position, Quaternion.identity);
            }
            else if(num == 6 || num == 7)
            {
                Instantiate(EnemyPrefabList[2], transform.position, Quaternion.identity);
            }
        }
    }
}
