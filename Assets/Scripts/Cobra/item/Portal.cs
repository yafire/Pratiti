using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour {
	public GameObject NowMap ;
    public GameObject ChangeMap;


    void OnTriggerEnter2D (Collider2D co) {
		if(co.tag != "Player") return ;
        NowMap.SetActive(false);
        ChangeMap.SetActive(true);
        UData.smallMap = ChangeMap.name;
    }

}
