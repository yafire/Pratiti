using UnityEngine;

[CreateAssetMenu(fileName = "BattleData", menuName = "BattleData")]
public class BattleData : ScriptableObject
{

    [Header("帕拉緹緹資料")]
    // 將所有Trigger顯示管理器中
    [Header("帕拉緹緹類型")]
    public string type = "";

    [Header("帕拉緹緹數值")]
    public float atk = 500;
    public float mAtk = 500;
    public float def = 500;
    public float mDef = 500;
    public float agi = 500;
    public float hp = 500;

}