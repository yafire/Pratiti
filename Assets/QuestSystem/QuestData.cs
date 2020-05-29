using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

namespace QuestSystem 
{
    [System.Serializable]
    public class QuestData : MonoBehaviour
    {
        private static QuestData instance;
        

        private static List<Quest> questList;

        public static Dictionary<string , List<bool>> boolCheck;
        public static Dictionary<string , List<Item>> itemCheck;

        public Quest firstQuest;
        public GameObject OKButton;
        private InventoryData inventoryData;

        [SerializeField]
        private GameObject questManager;

        void Awake()
        {
            inventoryData = GameObject.Find("GameData").GetComponent<InventoryData>();

            itemCheck = new Dictionary<string , List<Item>>();
            boolCheck = new Dictionary<string , List<bool>>();
            questList = new List<Quest>();

            CreatM1_puddin();
            OKButton.GetComponent<Button>().interactable = false;
        }


        #region 第一個任務

        void CreatM1_puddin()
        {
            string aString = "給雪拉布丁";
            firstQuest = new Quest(Quest.QuestName.firstQuest , aString);


            QuestEvent m1_1 = new QuestEvent("去村子找甜點屋老闆", "nani", null);
            m1_1.questDemand = new QuestDemand(false);
            boolCheck.Add(m1_1.GetID(), m1_1.questDemand.flags);
            firstQuest.AddQuestEvent(m1_1);

            QuestEvent m1_2 = new QuestEvent("蒐集3個砂糖、3個雞蛋", "" , null);
            m1_2.questDemand = new QuestDemand(
                new Item { itemName = Item.ItemName.糖 , quantity = 3 },
                new Item { itemName = Item.ItemName.雞蛋, quantity = 3 });
            itemCheck.Add(m1_2.GetID(), m1_2.questDemand.items);
            firstQuest.AddQuestEvent(m1_2);

            QuestEvent m1_3 = new QuestEvent("回去村子找甜點屋老闆", "", null);
            m1_3.questDemand = new QuestDemand(false);
            boolCheck.Add(m1_3.GetID(), m1_3.questDemand.flags);
            firstQuest.AddQuestEvent(m1_3);

            firstQuest.QuestEventLinkerAndMarker();

            /*firstQuest.AddPath(a.GetID() , b.GetID());
            firstQuest.AddPath(b.GetID() , c.GetID());*/

            firstQuest.BFS(m1_1);
            //firstQuest.PrintPath();

            questList.Add(firstQuest);
        }
        
        public void CheckFirstQuest()
        {
            switch (FindQuest(firstQuest).GetCurrentEvent().order)
            {
                case 1:
                    Debug.Log("first event");
                    if(ItemCheck(FindQuest(firstQuest).GetCurrentEvent().GetID()))
                    {
                        Debug.Log("itemCheck true");
                        OKButton.GetComponent<Button>().interactable = true;
                        OKButton.GetComponent<Button>().onClick.AddListener(delegate ()
                        {
                            Debug.Log(FindQuest(firstQuest));
                            Debug.Log(FindQuest(firstQuest).GetCurrentEvent());
                            FindQuest(firstQuest).GetCurrentEvent().UpDateQuestEvent(QuestEvent.EventStatus.done);
                            questManager.GetComponent<QuestManager>().Initialize();
                            OKButton.GetComponent<Button>().onClick.RemoveAllListeners();
                            OKButton.GetComponent<Button>().interactable = false;
                        });
                    }
                    else
                    {
                        Debug.Log("check no");
                    }
                    break;

                case 2:
                    foreach (bool bC in boolCheck[FindQuest(firstQuest).questEvents[1].GetID()])
                    {
                        if (!bC)
                        {
                            Debug.Log("not true");
                            break;
                        }

                        OKButton.GetComponent<Button>().interactable = true;
                        OKButton.GetComponent<Button>().onClick.AddListener(delegate ()
                        {
                            Debug.Log("Second event done");
                            FindQuest(firstQuest).GetCurrentEvent().UpDateQuestEvent(QuestEvent.EventStatus.done);
                            questManager.GetComponent<QuestManager>().Initialize();
                            OKButton.GetComponent<Button>().onClick.RemoveAllListeners();
                            OKButton.GetComponent<Button>().interactable = false;
                            // 第二個事件寫在這
                        });
                    }
                    break;


                default:
                    Debug.Log("default");
                    break;
            }

            Debug.Log("current" + FindQuest(firstQuest).GetCurrentEvent().eventName);
        }

        public void FindBoss()
        {
            boolCheck[FindQuest(firstQuest).questEvents[0].GetID()][0] = true;
            Debug.Log(boolCheck[FindQuest(firstQuest).questEvents[0].GetID()][0]);
        }

        public void FindBoss2()
        {
            boolCheck[FindQuest(firstQuest).questEvents[2].GetID()][0] = true;
            Debug.Log(boolCheck[FindQuest(firstQuest).questEvents[2].GetID()][0]);
        }

        public void GetFirstQuest()
        {
            firstQuest.questStatus = Quest.QuestStatus.accepted;
        }

        /*
        public void FirstMissionStory()
        {
            // SystemController.Instance.PlayBlock("S", "M1_2");
        }

    */
        #endregion


        #region 共同方法

        public void CheckAcceptedQuest()
        {
            if (GetAcceptedQuest() == null)
            {
                Debug.Log("no accepted quest");
                return;
            }

            Debug.Log(GetAcceptedQuest().questName.ToString());
            switch (GetAcceptedQuest().questName)
            {
                case Quest.QuestName.firstQuest:
                    CheckFirstQuest();
                    break;
                default:
                    Debug.Log("???");
                    break;
            }
        }

        public bool ItemCheck(string id)    
        {
            Debug.Log("itemCheck " + id);

            bool isEnough = true;
            foreach (Item iC in itemCheck[id])
            {
                foreach (Item item in inventoryData.GetInventory(iC))
                {
                    if (iC.itemName == item.itemName && iC.quantity == item.quantity && isEnough == true)
                        isEnough = true;
                    if (iC.itemName == item.itemName && iC.quantity != item.quantity)
                        isEnough = false;
                    Debug.Log("need"+iC.quantity);
                    Debug.Log("have"+item.quantity+item.itemName.ToString());
                }
                if (inventoryData.FindItem(iC) == null)
                    isEnough = false;
            }
            return isEnough;
        }

        public bool IsOK()
        {
            bool isOK = true;
            foreach (Item item in GetAcceptedQuest().GetCurrentEvent().questDemand.items)
            {
                if (inventoryData.FindItem(item) != null && inventoryData.FindItem(item).quantity >= item.quantity)
                    continue;
                isOK = false;    
            }

            foreach (bool flag in GetAcceptedQuest().GetCurrentEvent().questDemand.flags)
            {
                if (flag)
                    continue;
                isOK = false;
            }

            return isOK;
        }

        public Quest FindQuest (Quest q)    //透過名字搜索
        {
            foreach (Quest quest in questList)
            {
                if (quest.questName == q.questName)
                    return quest;
            }
            Debug.Log("error");
            return null; 
        }

        public Quest FindQuest(QuestEvent questEvent)   //透過事件搜索
        {
            foreach (Quest quest in questList)
            {
                if (quest.FindQuestEvent(questEvent) != null)
                    return quest;
            }
            Debug.Log("error");
            return null; 
        }

        public Quest GetAcceptedQuest()
        {
            foreach(Quest quest in questList)
            {
                if (quest.questStatus == Quest.QuestStatus.accepted)
                    return quest;
            }
            Debug.Log("no accepted");
            return null;
        } 



        public List<Quest> GetQuestList()
        {
            return questList;
        }

        public Quest GetCurrentQuest()
        {
            foreach (Quest quest in questList)
            {
                if (quest.questStatus == Quest.QuestStatus.accepted)
                    return quest;
            }
            return null;
        }

        #endregion
    }
}
