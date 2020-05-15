
using UnityEngine;
using Fungus;
using System;
using InventorySystem;
using System.Collections.Generic;

public class FungusManagerSystem : IGameSystem
{
    public static Flowchart FungusData;
    public static Flowchart FungusBattle;
    public static Flowchart FungusMap;

    public FungusManagerSystem(SystemController SControl) : base(SControl)
	{
        //
        Debug.Log("產生FungusManagerSystem" );
        GetFungusData();
	}

	public void GetFungusData()
	{
        FungusData = GameObject.Find("FungusData").GetComponent<Flowchart>();
        //Debug.Log("FungusManagerSystem產生FungusBattle:" + FungusBattle);
        if (FungusData == null)
        {
            Debug.Log("FungusManagerSystem找不到FungusData" );
        }
	}


    public void GetFungusBattle()
    {
        FungusBattle = GameObject.Find("FungusBattle").GetComponent<Flowchart>();
        if (FungusBattle == null)
        {
            Debug.Log("FungusManagerSystem找不到FungusBattle");
        }
    }

    public void GetFungusMap()
    {
        FungusMap = GameObject.Find("FungusMap").GetComponent<Flowchart>();
        if (FungusMap == null)
        {
            Debug.Log("FungusManagerSystem找不到FungusMap");
        }
    }


    //=======
    // 常用變數
    //=======

    // 對話時暫停遊戲的變數
    public bool IsTalking
	{
		get {return FungusData.GetBooleanVariable("IsTalking");}
		set { FungusData.SetBooleanVariable("IsTalking", value); }
	}

    // 戰鬥結束變數
    public bool BattleEnd
    {
        get { return UData.BattleEnd; }
        set { UData.BattleEnd = value; }
    }

    // 戰鬥勝利變數
    public bool BattleWin
    {
        get { return UData.BattleWin; }
        set { UData.BattleWin = value; }
    }


    // 結束戰鬥
    // 戰鬥勝利數入1。敗北輸入0
    public void EndBattle(int winOrLose)
    {
        GetFungusBattle();
        talkForchart = FungusBattle;
        if(FungusBattle == null)
        {
            Debug.Log("找不到 FungusBattle");
            return;
        }
        BattleEnd = true;
        BattleWin = (winOrLose == 1) ? true : false;
        if (BattleWin)
        {
            PlayBlock("WinBattle");
        }
        else
        {
            PlayBlock("LoseBattle");
        }
        
    }

    // 結束戰鬥回到地圖
    public void EndBattlePrice()
    {
        GetFungusMap();
        talkForchart = FungusMap;
        if (FungusMap == null)
        {
            Debug.Log("找不到 FungusMap");
            return;
        }
        if (BattleEnd)
        {
            if (BattleWin)
            {
                PlayBlock("WinBattle");
            }
            else
            {
                PlayBlock("LoseBattle");
            }
            BattleEnd = false;
        }
    }


    // 執行Block
    public void PlayBlock(string targetBlockName)
    {
        // 尋找Block
        Block targetBlock = talkForchart.FindBlock(targetBlockName);
        // 當targetBlock有物件時執行Block
        if (targetBlock != null)
        {
            talkForchart.ExecuteBlock(targetBlock);
        }
        else
        {
            Debug.LogError("找不到在" + talkForchart.name + "裡的" + targetBlockName + "Block");
        }

    }


    //==========
    // 取得甜點數量
    //==========
    // 取得 Flowchart
    private Flowchart talkForchart;
    
    // 取的甜點數量，供Fungus使用
    public void SetFungusDessert(string dessertName)
	{
		// 要輸進去的甜點
		talkForchart = GameObject.Find("FungusData").GetComponent<Flowchart>();
		talkForchart.SetIntegerVariable(dessertName, SystemController.Instance.GetDesert(GetItemName(dessertName)));
		Debug.Log(dessertName + talkForchart.GetIntegerVariable(dessertName));
	}
    
    // 以字串取得甜點物件
    public Item.ItemName GetItemName(string desert)
    {
        Debug.Log("很廢的取得甜點的方法，之後修");
        switch (desert)
        {
            case "藍莓":
                return Item.ItemName.藍莓;
            case "巧克力":
                return Item.ItemName.巧克力;
            case "堅果":
                return Item.ItemName.堅果;
            case "巧克力蛋糕":
                return Item.ItemName.巧克力蛋糕;
            case "快樂草":
                return Item.ItemName.快樂草;
            case "拴繩":
                return Item.ItemName.拴繩;
            case "牛奶":
                return Item.ItemName.牛奶;
            case "雞蛋":
                return Item.ItemName.雞蛋;
            case "黃油":
                return Item.ItemName.黃油;
            default:
                Debug.Log("食材名字輸入錯誤了");
                return Item.ItemName.藍莓;
        }
    }
}
