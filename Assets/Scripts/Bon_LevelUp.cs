using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bon_LevelUp : MonoBehaviour {

    private void Awake()
    {
        Invoke("BonusDestroy", 10);
    }

    private void GetLevelUp()
    {
        GameObject gameObject;
        gameObject = GameObject.FindGameObjectWithTag("Tank");
        gameObject.SendMessage("LevelUp");
        BonusDestroy();
    }

    private void BonusDestroy()
    {
        Destroy(gameObject);
    }
}
