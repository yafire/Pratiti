using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 用在戰鬥系統上，玩家與敵人的帕拉提提數值
public static class UData 
{
    // 戰鬥用資料
    public static BattleData playerData;
    public static BattleData enemyData;
    //public static BattleData enemyData = ScriptableObject.CreateInstance<BattleData>();
    //public static BattleData playerData = ScriptableObject.CreateInstance<BattleData>();

    // 地圖轉場用資料
    [Header("玩家所在地圖")]
    // 將所有Trigger顯示管理器中
    public static string Map;
    public static string smallMap;
    [Header("玩家位置")]
    public static Vector3 PlayerPos;

    // 是否在戰鬥
    // 戰鬥勝利變數
    public static bool BattleWin;
    // 戰鬥結束變數
    public static bool BattleEnd;

}
