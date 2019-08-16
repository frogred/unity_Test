using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMannager : MonoBehaviour {

    //引用
    //其实引用只是给个框架，在unity里面会需要把born放进去
    public GameObject Born;
    public Text PlayerScoreText;
    public Text PlayerLifeText;
    public GameObject isDefeatUI;
    public GameObject[] Bonus;
    //定义玩家属性值
    public int healthvalue = 3;
    public int playerscore = 0;
    public bool isDead = false;
    public bool isDefeat = false;
    public bool isBonus;
    //单例，外界调用

    private static PlayerMannager instance;
    //提供给全局，且用static
    public static PlayerMannager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    
    void Start () {
		
	}
	
	// 在update中监听玩家的状态
    // 7/30更新，在update中监听奖励生成的状态
	void Update () {

        if (isBonus)
        {
            InitBonus();
            isBonus = false;
            //生成奖励
        }
        if (isDead)
        {
            Recover();
        }
        PlayerScoreText.text = playerscore.ToString();
        PlayerLifeText.text = healthvalue.ToString();
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
        }
	}
    //写生成奖励的方法
    private void InitBonus()
    {
        Vector3 bonusPosition = new Vector3(Random.Range(-9f, 10f), Random.Range(-7f, 8f), 0);
        Instantiate(Bonus[4], bonusPosition, Quaternion.identity);
    }

    //写复活方法
    private void Recover()
    {
        if(healthvalue <= 0)
        {
            //调用游戏失败
            isDefeat = true;
            Invoke("ReturnTotheMenu", 4);
        }
        else
        {   
            healthvalue--;
            if(healthvalue == 0)
            {
                isDefeat = true;
                return;
            }
            //用go来存放一下object，然后调用object里面的born
            GameObject go = Instantiate(Born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;//调用方法，getcomponent，在<>中取需要的脚本
            isDead = false;
        }
    }

    private void ReturnTotheMenu()
    {
        SceneManager.LoadScene(0);
    }
}
