using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;
using Fungus;

public class SystemController
{
    private FungusManagerSystem m_FungusManager;
    private SceneChangeSystem m_sceneChangeSystem = null;
    private InventoryData m_inventoryData;
    //------------------------------------------------------------------------
    // Singleton模版
    private static SystemController _instance;
    public static SystemController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("產生SystemController");
                _instance = new SystemController();
            }
            return _instance;
        }
    }
    private SystemController()
    { }

    //=======
    // 基本方法
    //=======

    // 初始遊戲相關設定
    public void Initinal()
    {
        //遊戲系統
        m_FungusManager = new FungusManagerSystem(this);
        m_sceneChangeSystem = new SceneChangeSystem(this);
        // IsTalking = false;
        // m_inventoryData = GameObject.Find("InventoryData").GetComponent<InventoryData>();
    }

    // 釋放遊戲系統
    public void Release()
    {

    }

    // 更新
    public void Update()
    {
        // 玩家輸入
        InputProcess();

    }

    // 玩家輸入
    private void InputProcess()
    {
 
    }

    //=======
    // 讀帕拉提提
    //=======



    //=======
    // 改變甜點數量的方法
    //=======

    // 設定甜點數量
    public void SetDesert(Item.ItemName desertname, int num)
    {
        Item desert = new Item { itemName = desertname, quantity = num };
        m_inventoryData.RemoveItem(m_inventoryData.FindItem(desert));
        m_inventoryData.AddItem(desert);
    }

    // 增加甜點數量
    public void AddDesert(Item.ItemName desertname, int num)
    {
        Item desert = new Item { itemName = desertname, quantity = num };
        m_inventoryData.AddItem(desert);
    }

    // 取得甜點數量
    public int GetDesert(Item.ItemName desertname)
    {
        Item desert = new Item { itemName = desertname, quantity = 0 };
        Debug.Log("desertname" + desert.itemName);
        return m_inventoryData.FindItem(desert).quantity;
    }

    // 以字串取得甜點物件
    public Item.ItemName GetItemName(string desert)
    {
        return m_FungusManager.GetItemName(desert);
    }

    //=======
    // SceneChangeSystem方法
    //=======


    // 初始化地圖
    public void InitialMap()
    {
        m_sceneChangeSystem.InitialMap();
    }

    // 儲存小地圖的資料
    public void SetSmallMap(string smallMapName)
    {
        m_sceneChangeSystem.SetSmallMap(smallMapName);
    }

    // 轉場時儲存GameData, smallMap, PlayerPosition
    public void SaveMapData()
    {
        m_sceneChangeSystem.SaveMapData();
    }


    //=======
    // FungusManager方法
    //=======

    // 取得 Flowchart
    private Flowchart talkForchart;

    // 是否在對話的布林值
    public bool IsTalking
    {
        get {
            return m_FungusManager.IsTalking;
        }
        set { m_FungusManager.IsTalking = value; }
    }

    // 戰鬥結束變數
    public bool BattleEnd
    {
        get { return m_FungusManager.BattleEnd; }
        set { m_FungusManager.BattleEnd = value; }
    }

    // 戰鬥勝利變數
    public bool BattleWin
    {
        get { return m_FungusManager.BattleWin; }
        set { m_FungusManager.BattleWin = value; }
    }

    // 設定甜點變數
    public void SetFungusDessert(string dessertName)
    {
        m_FungusManager.SetFungusDessert(dessertName);
    }

    public void EndBattle(int winOrLose)
    {
        m_FungusManager.EndBattle(winOrLose);
    }

    // 結束戰鬥回到地圖
    public void EndBattlePrice()
    {
        m_FungusManager.EndBattlePrice();
    }

    /*

    // 提供ScreenUI方法
    public void SetSmallMap(string smallMapName)
    {
        //m_ScreenUI = gameObject.AddComponent<ScreenUI>();
        m_ScreenUI.SetSmallMap(smallMapName);
    }
    public bool GetIstalking()
    {
        return FungusManager.IsTalking;
    }
    */

    // 淡入
    /*
    public void FadeIn()
    {
        m_sceneChangeSystem.FadeIn();
    }

    // 淡出
    public void FadeOut()
    {
        Debug.Log("由SystemControll啟動FadeOut");
        m_sceneChangeSystem.FadeIn();
    }

    // 淡出+轉場
    public void FadeAndGo(string map)
    {
        m_sceneChangeSystem.FadeAndGo(map);

    }
    */

}
