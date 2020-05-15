
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using InventorySystem;
using UnityEngine.SceneManagement;

// 進入戰鬥
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "EnterBattle", "轉場")]
public class EnterBattle : Command
{
    // 開始時執行 
    public override void OnEnter()
    {
        SystemController.Instance.SaveMapData();
        SceneManager.LoadScene("Battle");
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "進入戰鬥";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}


// 戰鬥結束
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "BackToMap", "戰鬥結束回到地圖")]
public class BackToMap : Command
{
    // 開始時執行 
    public override void OnEnter()
    {
        //SystemController.Instance.FadeAndGo(UData.Map);
        SceneManager.LoadScene(UData.Map);
        Debug.Log("先轉場");

        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "戰鬥結束回到地圖";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}


// 戰鬥結束
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "EndBattlePrice", "獎勵結算")]
public class EndBattlePrice : Command
{
    // 開始時執行 
    public override void OnEnter()
    {
        //SystemController.Instance.FadeAndGo(UData.Map);
        SystemController.Instance.EndBattlePrice();
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "判斷戰鬥勝利或是失敗，給予獎勵";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}


// 初始化地圖
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "InitialMap", "打開地圖")]
public class InitialMap : Command
{
    // 開始時執行 
    public override void OnEnter()
    {

        SystemController.Instance.InitialMap();
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "打開地圖";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}


// 初始化地圖
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "Protal", "同地圖轉移")]
public class Protal : Command
{
    public GameObject NowMap;
    public GameObject ChangeMap;
    // 開始時執行 
    public override void OnEnter()
    {
        NowMap.SetActive(false);
        ChangeMap.SetActive(true);
        UData.smallMap = ChangeMap.name;
        Continue();
    }


    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "同地圖轉移";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}



// SetSmallMap
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "SetSmallMap", "設立初始地圖")]
public class SetSmallMap : Command
{
    public string mapName;
    // 開始時執行 
    public override void OnEnter()
    {
        UData.smallMap = mapName;
        Continue();
    }


    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "同地圖轉移";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}

/*
//=======
// 甜點系統方法
//=======

//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "GetDessert", "轉場")]
public class GetDessert : Command
{
    public string dessertName;
    public int num;
    // 開始時執行 
    public override void OnEnter()
    {
        //string[] desserts = { "藍莓" , "巧克力", "堅果" , "巧克力蛋糕" , "快樂草" , "拴繩" , "牛奶" , "雞蛋", "黃油" };
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "進入戰鬥";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}

// 將甜點數與fungus連動
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "InitialDessert", "將甜點數與fungus連動")]
public class InitialDessert : Command
{
    private string[] desserts = { "巧克力", "牛奶", "雞蛋" };
    // private string[] desserts = {  "巧克力", "藍莓", "堅果", "牛奶", "雞蛋", "黃油" };
    // 開始時執行 
    public override void OnEnter()
    {
        foreach ( string dessert in desserts)
        {
            SystemController.Instance.SetFungusDessert(dessert);
        }
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "將甜點數與fungus連動";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}

// 設定甜點數量
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "SetDessertNum", "將甜點數與fungus連動")]
public class SetDessert : Command
{
    public string dessertName ;
    public int number;
    // private string[] desserts = {  "巧克力", "藍莓", "堅果", "牛奶", "雞蛋", "黃油" };
    // 開始時執行 
    public override void OnEnter()
    {
        SystemController.Instance.SetDesert(SystemController.Instance.GetItemName(dessertName), number);
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "將甜點數與fungus連動";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}

// 進入戰鬥
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "StartBattle", "轉場")]
public class StartBattle : Command
{
    // 開始時執行 
    public override void OnEnter()
    {
        SystemController.Instance.StartBattle = true;
        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "進入戰鬥";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}


// 回到地圖
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "EndBattle", "轉場")]
public class EndBattle : Command
{
    // 開始時執行 
    public override void OnEnter()
    {

        SystemController.Instance.StartBattle = false;

        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return "結束戰鬥";
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}


// 轉場
//A:[CommandInfo("選單位置", "名稱", "簡介")]
[CommandInfo("Pratiti", "FadeAndGo", "轉場")]
public class FadeAndGo : Command
{
    public string Map;
    // 開始時執行 
    public override void OnEnter()
    {
        SystemController.Instance.FadeAndGo(Map);

        Continue();
    }

    // B:給與Command匡的物件文字名稱
    public override string GetSummary()
    {
        if (gameObject != null)
        {
            return Map;
        }
        return "nothing";
    }

    // C:設定Inspecter右下角顏
    public override Color GetButtonColor()
    {
        return new Color32(111, 222, 0, 221);
    }
}


*/
