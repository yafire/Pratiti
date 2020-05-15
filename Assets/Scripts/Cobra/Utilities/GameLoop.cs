using UnityEngine;
using System;
using System.Collections;

// 遊戲主迴圈
public class GameLoop : MonoBehaviour
{
    // 場景狀態
    // SceneStateController m_SceneStateController = new SceneStateController();

    // 跨場景物件
    static GameLoop instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(this != instance)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        SystemController.Instance.Initinal();
        Debug.Log("初始化地圖在GameLoop");
        UData.Map = "Map";
        UData.smallMap = "NoviceVillageStreet1";
        // 設定起始的場景
        // m_SceneStateController.SetState(new MapState(m_SceneStateController), "");
    }

}
