using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Fungus;

public class NPCTalk : MonoBehaviour
{
    // 取得 Flowchart
    public Flowchart talkForchart;
    // Block的名稱
    public string CallTalkBlock;
    internal bool inView; // 是否在視野內



    private void Awake()
    {
        inView = false;

    }

    private void Update()
    {
        // 按下Z鍵且可以開始對話啟動（inView = true）
        if (Input.GetKeyDown(KeyCode.Z) && inView)
        {
            // 當onCollosionEnter不是空白、不在對話中才能觸發
            if (!string.IsNullOrEmpty(CallTalkBlock) && !SystemController.Instance.IsTalking)
            {
                PlayBlock(CallTalkBlock);
            }
        }
    }

    // 執行Block
    void PlayBlock(string targetBlockName)
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



    private void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        // 當玩家靠夠近時，可以開始對話（inView = true;）
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Controller>().SpawnButton();
            
            inView = true;
        }
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D other)
    {
        // 當玩家靠太遠時，無法開始對話（inView = true;）
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Controller>().DestroyButton();
            inView = false;
        }
    }


}
