using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Fungus;

public class ProtalChangeScene : MonoBehaviour
{
    // 取得 Flowchart
    private Flowchart FungusMap;
    // Block的名稱
    public string CallTalkBlock;
    internal bool inView; // 是否在視野內

    private void Awake()
    {
        inView = false;
        FungusMap = GameObject.Find("FungusMap").GetComponent<Flowchart>();
        //Debug.Log("FungusManagerSystem產生FungusBattle:" + FungusBattle);
        if (FungusMap == null)
        {
            Debug.Log("FungusManagerSystem找不到FungusMap");
        }
    }


    // 執行Block
    void PlayBlock(string targetBlockName)
    {
        // 尋找Block
        Block targetBlock = FungusMap.FindBlock(targetBlockName);
        // 當targetBlock有物件時執行Block
        if (targetBlock != null)
        {
            FungusMap.ExecuteBlock(targetBlock);
        }
        else
        {
            Debug.LogError("找不到在" + FungusMap.name + "裡的" + targetBlockName + "Block");
        }

    }

    // 靠夠近，進行傳送
    private void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        // 當玩家靠夠近時，可以開始對話（inView = true;）
        if (other.gameObject.CompareTag("Player"))
        {
            // 當onCollosionEnter不是空白、不在對話中才能觸發
            if (!string.IsNullOrEmpty(CallTalkBlock) && !SystemController.Instance.IsTalking)
            {
                PlayBlock(CallTalkBlock);
            }
        }
    }



}
