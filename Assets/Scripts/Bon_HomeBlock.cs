using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bon_HomeBlock : MonoBehaviour {
    private void Awake()
    {
        Invoke("BonusDestroy", 10);
    }

    private void GetHomeBlock()
    {
        GameObject gameObject;
        gameObject = GameObject.Find("MapCreator");
        gameObject.SendMessage("CreateHomeBlock");
        BonusDestroy();
    }

    private void BonusDestroy()
    {
        Destroy(gameObject);
    }
}
