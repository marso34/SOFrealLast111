using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLob : MonoBehaviour
{

    public GameObject Lobby;
    public GameObject StartPanel;
    //public GameObject GMC;
    public GameObject startAd;
    public GameObject Backg;

    public void OnClick()
    {
        Backg.SetActive(true);
        Lobby.SetActive(true);
        StartPanel.SetActive(false);
        Debug.Log("������");
        //Lobby.GetComponent<AddmobBanner>().StartAdInLob();
        //startAd.GetComponent<AddmobBanner>().StartAdInLob();


    }
}
