using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bon_Life : MonoBehaviour {

    private void Awake()
    {
        Invoke("BonusDestroy", 15);
    }

    private void PlusLife()
    {
        PlayerMannager.Instance.healthvalue++;
        Destroy(gameObject);
    }

    private void BonusDestroy()
    {
        Destroy(gameObject);
    }

}
