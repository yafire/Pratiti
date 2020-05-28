using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

public class EndBattleManger : MonoBehaviour
{
    private PratitiBattleGrowth m_PratitiBattleGrowth;
    private BattleUseData m_battleUseData;

    public GameObject endBattlePanel;
    public GameObject losePanel;

    public bool isWin;

    public Text pratitiNameText;
    public Text hpText, atkText, defText, mAtkText, mDefText, agiText;
    public Text gpText, gpUseText;
    public int gpUse;
    public Slider gpSlider;

    private Pratiti pratiti;
    public GameObject pratitiImage;
    public GameObject itemSlotGrid;
    public GameObject itemSlot;
    private GameObject cloneSlot; 

    //勝利老虎機
    //public GameObject winRoller;
    public bool isRoll = true;
    public Button winRollerButton;
    private int rollRange = 6;
    private int rollResult = 1;
    private int timeCount;

    private void Awake() 
    {
        m_PratitiBattleGrowth = new PratitiBattleGrowth();
        m_battleUseData = GameObject.Find("GameData").GetComponent<BattleUseData>();
        pratiti = m_battleUseData.GetPlayerPratiti();
        winRollerButton.onClick.AddListener(delegate () { isRoll = !isRoll; });
    }
    void Start()
    {
        RefreshEndBattlePanel_Win();
    }

    public void RefreshEndBattlePanel_Win()
    {
        pratitiImage.GetComponent<Image>().sprite = pratiti.GetPratitiSprite();
        cloneSlot = Instantiate(itemSlot, itemSlotGrid.transform);
        cloneSlot.GetComponent<ItemSlot>().thisItem = m_battleUseData.GetItem;
        ShowPratitiStat();
    }

    public void ShowPratitiStat()
    {
        hpText.text = ((int)pratiti.hp).ToString();
        atkText.text = ((int)pratiti.atk).ToString();
        defText.text = ((int)pratiti.def).ToString();
        mAtkText.text = ((int)pratiti.mAtk).ToString();
        mDefText.text = ((int)pratiti.mDef).ToString();
        agiText.text = ((int)pratiti.agi).ToString();
        pratitiNameText.text = pratiti.pratitiName;
        gpText.text = pratiti.generalPoints.ToString();
    }

    void Update()
    {
        gpUse = (int)(333 * gpSlider.value);
        gpUseText.text = gpUse.ToString();


        /*if(pratiti.winTimer == 3)
        {
            winRollerButton.onClick.AddListener(delegate() { isRoll = !isRoll; });
            rollRange = 6;
        }*/

        //勝利老虎機
        /*if(isRoll)
        {
            rollResult = Random.Range(1, rollRange);
            Debug.Log(rollResult);
            winRollerButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = rollResult.ToString();
        }*/
    }

    private void FixedUpdate()
    {
        if((timeCount%100 == 0) & (isRoll))
        {
            rollResult = rollResult + 1;
            if (rollResult > rollRange)
                rollResult = 1;
            Debug.Log(rollResult);
            Debug.Log(timeCount);
            winRollerButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = rollResult.ToString();
        }
        timeCount++;
    }

    public void WinRoller()
    {

    }
}
