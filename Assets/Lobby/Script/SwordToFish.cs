using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordToFish : MonoBehaviour
{
    //Sword�������� Fish�������� �̵��ϴ� ��ư

    public GameObject Lobby;
    public GameObject Setting;
    public GameObject FishShop;
    public GameObject SwordShop;


    public void OnClick()
    {
        //FishShop�� ���
        Lobby.SetActive(false);
        Setting.SetActive(false);
        FishShop.SetActive(true);
        SwordShop.SetActive(false);
    }
}
