using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTitleButton : MonoBehaviour, IPointerEnterHandler
{
    public GameObject Title;
    public GameObject start;
    public GameObject Setting;
    public GameObject Exit;
    // GameManager gameManager;
    // StoryManager storyManager;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.gameObject.name == "Start")
        {
            StartCoroutine(GameTitleStart());
        }
        else if (this.gameObject.name == "Setting")
        {
            StartCoroutine(GameTitleSetting());
        }
        else if (this.gameObject.name == "Exit")
        {
            StartCoroutine(GameTitleExit());
        }
    }
    IEnumerator GameTitleStart()
    {
        start.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        start.SetActive(false);
    }
    IEnumerator GameTitleSetting()
    {
        Setting.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        Setting.SetActive(false);
    }
    IEnumerator GameTitleExit()
    {
        Exit.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        Exit.SetActive(false);
    }
    public void GAMESTART()
    {
        // 初始化遊戲數值
        Debug.Log("之後會補充初始化設定");
        /*gameManager = FindObjectOfType<GameManager>();
        gameManager.gamedata.PlaxyerPos = new Vector3(-16, 27, 0);
        gameManager.gamedata.Map = "Map";
        gameManager.gamedata.smallMap = "Map1";

        // 初始化故事bool
        storyManager = FindObjectOfType<StoryManager>();
        storyManager.InitialStoryBoolTrue();
        */
        SceneManager.LoadScene("Map");
    }

}
