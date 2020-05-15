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

            CreatFirstQuest();

            FindQuest(firstQuest).questStatus = Quest.QuestStatus.accepted;

            OKButton.GetComponent<Button>().interactable = false;
        }

        void CreatFirstQuest()
        {
            string aString = "Hi,\nthis is the discribetion of the 1st Quest";
            firstQuest = new Quest(Quest.QuestName.firstQuest , aString);

            QuestEvent a = new QuestEvent("event1" , "Info1" , null);
            a.questDemand = new QuestDemand(new Item { itemName = Item.ItemName.巧克力蛋糕 , quantity = 1 });
            itemCheck.Add(a.GetID(), a.questDemand.items);
            firstQuest.AddQuestEvent(a);

            QuestEvent b = new QuestEvent("event2", "Info2", null);
            b.questDemand = new QuestDemand(false);
            boolCheck.Add(b.GetID(), b.questDemand.flags);
            firstQuest.AddQuestEvent(b);

            QuestEvent c = new QuestEvent("event3", "Info3", null);
            c.questReward = new QuestReward(new Item { itemName = Item.ItemName.快樂草, quantity = 1 } , false);
            firstQuest.AddQuestEvent(c);

            firstQuest.questReward = c.questReward;

            firstQuest.QuestEventLinkerAndMarker();

            /*firstQuest.AddPath(a.GetID() , b.GetID());
            firstQuest.AddPath(b.GetID() , c.GetID());*/

            firstQuest.BFS(a);
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
                            foreach ( Item item in itemCheck[ FindQuest(firstQuest).GetCurrentEvent().GetID() ] ) 
                            {
                                inventoryData.RemoveItem(item);
                            }
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
                        });
                    }
                    break;

                case 3:
                    OKButton.GetComponent<Button>().interactable = true;
                    OKButton.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        foreach (Item item in FindQuest(firstQuest).questReward.items)
                        {
                            inventoryData.AddItem(item);
                        }

                        int n = 0;
                        do
                        {
                            FindQuest(firstQuest).questReward.flags[n] = true;
                            n++;
                        } while (n < FindQuest(firstQuest).questReward.flags.Count);

                        Debug.Log("third event done");
                        FindQuest(firstQuest).GetCurrentEvent().UpDateQuestEvent(QuestEvent.EventStatus.done);
                        questManager.GetComponent<QuestManager>().Initialize();
                        OKButton.GetComponent<Button>().onClick.RemoveAllListeners();
                        OKButton.GetComponent<Button>().interactable = false;
                        
                    });
                    break;

                default:
                    Debug.Log("default");
                    break;
            }

            Debug.Log("current" + FindQuest(firstQuest).GetCurrentEvent().eventName);
        }

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
            return null; ;
        }

        public Quest FindQuest(QuestEvent questEvent)   //透過事件搜索
        {
            foreach (Quest quest in questList)
            {
                if (quest.FindQuestEvent(questEvent) != null)
                    return quest;
            }
            Debug.Log("error");
            return null; ;
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

        public void Changebc()
        {
            boolCheck[FindQuest(firstQuest).questEvents[1].GetID()][0] = true;
            Debug.Log(boolCheck[FindQuest(firstQuest).questEvents[1].GetID()][0]);
        }
    }    
}
