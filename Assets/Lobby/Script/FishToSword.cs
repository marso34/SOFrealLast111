using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishToSword : MonoBehaviour
{
    //FishShop���� SwordShop���� �̵��ϴ� ��ư

    public GameObject Lobby;
    public GameObject Setting;
    public GameObject FishShop;
    public GameObject SwordShop;
    public GameObject SwordShop_0;
    public GameObject SwordShop_1;

    public void OnClick()
    {
        //SwordShop�� ���
        Lobby.SetActive(false);
        Setting.SetActive(false);
        FishShop.SetActive(false);
        SwordShop.SetActive(true);
        SwordShop_0.SetActive(true);
        SwordShop_1.SetActive(false);
    }
}
