using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    /*public List<GameObject> Cameras = new List<GameObject>();
    // Start is called before the first frame update
    public void ChangeCam(int num)
    {
        
        Cameras[num].SetActive(false);
        Cameras[num].SetActive(true);
    }*/
    public GameObject[] Cameras;
    // Start is called before the first frame update
    public void ChangeCam(string Map)
    {
        foreach (GameObject Camera in Cameras)
        {
            Camera.SetActive(false);
            if (Camera.name == Map)
            {
                Camera.SetActive(true);
            }
        }
            
        //Cameras[num -1].SetActive(true);
        //GameObject.FindGameObjectWithTag(Camera).SetActive(false);
        //GameObject.FindGameObjectWithTag(Camera).SetActive(true);
    }
}
