using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoLobby : MonoBehaviour
{
    //�гθ��� �ִ� �ڷ� ���� ��ư
    public GameObject Lobby;
    public GameObject Setting;
    public GameObject FishShop;
    public GameObject KnifeShop;
    public GameObject GOPanel;

    public bool IsGO; //GO�гξƴ� ��� inspector���� üũ
    public void Start()     
    {
        IsGO = true;
    }

    public void Back()
    {

        //�κ� ���
        if (IsGO)
        {
            FishShop.SetActive(false);
            Lobby.SetActive(true);
            Setting.SetActive(false);
            FishShop.SetActive(false);
            KnifeShop.SetActive(false);
            GOPanel.SetActive(false);
        }
        else if (!IsGO)
        {
            FishShop.SetActive(false);
            Lobby.SetActive(false);
            Setting.SetActive(false);
            FishShop.SetActive(false);
            KnifeShop.SetActive(false);
            GOPanel.SetActive(true);
        }
        IsGO = true;
    }


    public void IsGoGO(bool isgo)   //���ӿ��� �гο��� �ڷ� ������ �κ�� ������ �� ����
    {
        if (isgo)
            IsGO = false;
    }
}