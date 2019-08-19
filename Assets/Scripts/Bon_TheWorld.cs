using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bon_TheWorld : MonoBehaviour {
    private void Awake()
    {
        Invoke("BonusDestroy", 10);
    }

    private void Dio()
    {
        object[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in gameObjects)
        {
            go.SendMessage("StopMoving");
        }
        BonusDestroy();
    }

    private void BonusDestroy()
    {
        Destroy(gameObject);
    }

}
