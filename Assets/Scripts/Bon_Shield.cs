using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bon_Shield : MonoBehaviour {

    private void Awake()
    {
        Invoke("BonusDestroy", 15);
    }

    private void GetShield()
    {
        GameObject gameObject;
        gameObject = GameObject.FindGameObjectWithTag("Tank");
        gameObject.SendMessage("ShieldActive");
        BonusDestroy();
    }

    private void BonusDestroy()
    {
        Destroy(gameObject);
    }
}
