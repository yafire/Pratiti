using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;
using TMPro;

namespace TrainSystem{
	public class TrainManager : MonoBehaviour
	{
		//精靈列表    public List<Pratiti> pratitiData; 
		private List<Pratiti> pratitiData;
		public GameObject pratitiSlotGrid;
		public GameObject pratitiSlot;
		public Button[] toPage;//toPage[0]=>statPage;toPage[1]=>infoPage;

		//在trainmenu上顯示能力值
		public Text pratitiNameText,sexText,characteristicText;
		public TextMeshProUGUI hpText,atkText,defText,mAtkText,mDefText,agiText;

		public Image pressureMaskImage;
		public Image nowWatch;    //頭貼顯示處
		private Pratiti pratiti;	//當前選中的Pratiti

		float trainUp = 1.0f;	//訓練後提升的數值
		int maxPressure=15;	//最大壓力值

		private Button[] setTrainUp; //設定培養所提升的數值大小;現版本不會用到

        private void Awake()
        {
            pratitiData = GameObject.Find("GameData").GetComponent<PratitiData>().pratitiData;
        }

        public void Initialize()
		{
            this.gameObject.transform.parent.gameObject.SetActive(true);
            Debug.Log("Train" + pratitiData );
            RefreshPrattiScrollPanel();

			pratitiSlotGrid.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.Invoke();
		}

        public void Escape()
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }


        public void SelectPratiti(int i)
		{
			Debug.Log(i);
			pratiti = pratitiData[i];
		}

		void Update()
		{
            if (this.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                Escape();
            }	
		}
		public void RefreshPrattiScrollPanel()
		{
			for (int i = 0; i<10; i++)
			{
					if (i < pratitiData.Count)
					{
						pratitiSlotGrid.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = pratitiData[i].PratitiSprite();

						//顯示帕拉緹緹的名字與性別
						if (pratitiData[i].sex)		
							pratitiSlotGrid.transform.GetChild(i).transform.GetChild(1).gameObject.GetComponent<Text>().text = pratitiData[i].pratitiBrand.pratitiName + "♂";
						else
							pratitiSlotGrid.transform.GetChild(i).transform.GetChild(1).gameObject.GetComponent<Text>().text = pratitiData[i].pratitiBrand.pratitiName + "♀";			
					}
					else
					{
						pratitiSlotGrid.transform.GetChild(i).GetComponent<Button>().interactable = false;
						pratitiSlotGrid.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
						pratitiSlotGrid.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
					}
			}
		}

			//改變界面上顯示的帕拉提提資訊
		public void RefreshStatPanel()
		{
			toPage[0].onClick.Invoke();
			Debug.Log(pratiti);
			hpText.text= ((int)pratiti.hp).ToString();
			atkText.text= ((int)pratiti.atk).ToString();
			defText.text= ((int)pratiti.def).ToString();
			mAtkText.text= ((int)pratiti.mAtk).ToString();
			mDefText.text= ((int)pratiti.mDef).ToString();
			agiText.text= ((int)pratiti.agi).ToString();
			pratitiNameText.text= pratiti.pratitiName;
			sexText.text = (pratiti.sex) ? "♂" : "♀" ;
			pressureMaskImage.rectTransform.sizeDelta = new Vector2(240.0f* pratiti.pressure/maxPressure,50.0f);//改變Mask物件的寬度達成pressure槽減少的效果
			characteristicText.text= pratiti.characteristic.ToString();
			nowWatch.GetComponent<Image>().sprite= pratiti.PratitiSprite();
		}

		public void RefreshInfoPanel(Pratiti sD)
		{
			
		}
		
		//設定增加能力值的大小，現版本不會用到，故先註解起來
		// public void SetTrainUp(int setTrainUp)
		// {
		// 	trainUp=setTrainUp;	//按下按鈕傳入trainUp值；
		// }

		public void StatUp(int stat){
			if ((pratiti.pressure+trainUp)<=maxPressure)
			{
				switch (stat)	//根據傳入值提升某項能力
				{
					case 1:
						pratiti.hp+=trainUp;
						break;
					case 2:
						pratiti.atk+=trainUp;
						break;	
					case 3:
						pratiti.def+=trainUp;
						break;	
					case 4:
						pratiti.mAtk+=trainUp;
						break;	
					case 5:
						pratiti.mDef+=trainUp;
						break;		
					case 6:
						pratiti.agi+=trainUp;
						break;	
				}
				pratiti.pressure=pratiti.pressure + (int)trainUp;
				RefreshStatPanel();
				PratitiData.SavePratitiData(pratitiData); 
			}
			else
			{
				Debug.Log("too much pressure");
			}
			toPage[0].GetComponent<Button>().interactable = true;
			toPage[1].GetComponent<Button>().interactable = true;
		}
	}
}