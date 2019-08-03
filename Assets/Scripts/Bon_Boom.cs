using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bon_Boom : MonoBehaviour {

    //用于在执行enemy自己的die方法的时候有爆炸效果
    //public GameObject ExplosionPrefab;

    private void Awake()
    {
        Invoke("BonusDestroy", 15);
    }

    private void Boom()
    {   
        //用于查找tag为enemy的游戏物体，再发送Die信号
        //好像有点问题，为什么调用die的时候没有爆炸效果了
        //尝试引入和这个函数没半毛钱关系的爆炸预制体修正这个问题
        //草，真的有用，无法理解
        //绝了，注释掉以后依然有用，太神秘了
        //我可能感动了代码之神
        object[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject go in gameObjects)
        {
            go.SendMessage("Die");
            PlayerMannager.Instance.playerscore++;
        }
        BonusDestroy();
    }

    private void BonusDestroy()
    {
        Destroy(gameObject);
    }

}
