using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {
    //0.老家 1.墙 2.障碍 3.出生效果 4.河流 5.草 6.空气墙
    //存放地图中的物件的数组
    //在unity中导入
    public GameObject[] Items;
    //存放将会生成的奖励物品
    //已经有东西的列表
    public List<Vector3> itemPositionList = new List<Vector3>();



    private void Awake()
    { 
        FirstInit();
        InitPlandEnemy();
        InitMap();
    }

    private void InitPlandEnemy()
    {
        //实例化玩家
        GameObject pl = Instantiate(Items[3], new Vector3(-2, -8, 0), Quaternion.identity);
        //先生成一个pl的born，再get到born组件，设置其中的creatplayer为true
        //getcomponent?
        pl.GetComponent<Born>().createPlayer = true;

        //在游戏一开始实例化三个敌人
        CreateItem(Items[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(Items[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(Items[3], new Vector3(10, 8, 0), Quaternion.identity);
        //invoke,三个参数，第一个是需要调用的方法，第二个是第一次调用的延时，第三个是之后每一次调用的时间间隔
        InvokeRepeating("CreateEnemy", 4, 5);
    }

    private void FirstInit()
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
        //实例化空气墙
        for (int x = -11; x < 12; x++)
        {
            CreateItem(Items[6], new Vector3(x, -9, 0), Quaternion.identity);
        }
        for (int x = -11; x < 12; x++)
        {
            CreateItem(Items[6], new Vector3(x, 9, 0), Quaternion.identity);
        }
        for (int y = -8; y < 9; y++)
        {
            CreateItem(Items[6], new Vector3(-11, y, 0), Quaternion.identity);
        }
        for (int y = -8; y < 9; y++)
        {
            CreateItem(Items[6], new Vector3(11, y, 0), Quaternion.identity);
        }
    }

    private void InitMap()
    {
        //实例化地图
        for (int i = 0; i < 40; i++)
        {
            CreateItem(Items[1], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(Items[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(Items[4], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(Items[5], CreateRandomPosition(), Quaternion.identity);
        }
    }

    //自己封装一个创造物体的方法
    private void CreateItem(GameObject createGameObject,Vector3 createPosition,Quaternion createRotation)
    {
        GameObject itemGO = Instantiate(createGameObject, createPosition, createRotation);
        //setparent，使生成的物体不会散落出来，归属于当前的游戏物体（parent）。
        itemGO.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }

    private Vector3 CreateRandomPosition()
    {
        //不生成边上一圈，让坦克上下存在通路
        while (true)
        {
            Vector3 creatPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(creatPosition))//如果该位置还没用过
            {
                return creatPosition;
            }
            
        }
    }

    private bool HasThePosition(Vector3 createPos)
    {
        for(int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPos == itemPositionList[i])
            {
                return true;//遍历完位置列表，发现已经有该位置存在
            }
        }
        return false;
    }

    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 enemyPos = new Vector3();
        if (num == 0)
        {
            enemyPos = new Vector3(-10, 8, 0);
        }
        else if(num == 1)
        {
            enemyPos = new Vector3(0, 8, 0);
        }
        else if(num == 2)
        {
            enemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(Items[3], enemyPos, Quaternion.identity);
    }
}
