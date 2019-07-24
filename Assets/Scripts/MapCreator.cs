using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {
    //0.老家 1.墙 2.障碍 3.出生效果 4.河流 5.草 6.空气墙
    public GameObject[] Items;//存放地图中的物件的数组

    private void Awake()
    {
        //实例化老家
        CreateItem(Items[0], new Vector3(0, -8, 0), Quaternion.identity);
        //实例化老家边上的墙
        CreateItem(Items[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(Items[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(Items[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
    }
    //自己封装一个创造物体的方法
    private void CreateItem(GameObject createGameObject,Vector3 createPosition,Quaternion createRotation)
    {
        GameObject itemGO = Instantiate(createGameObject, createPosition, createRotation);
        //setparent，使生成的物体不会散落出来，归属于当前的游戏物体（parent）。
        itemGO.transform.SetParent(gameObject.transform);
    }


}
