using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public AudioClip BlockHit;

    public void BlockHitSound()
    {
        AudioSource.PlayClipAtPoint(BlockHit,transform.position);
    }


}
