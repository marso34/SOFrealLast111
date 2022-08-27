using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GOFish : MonoBehaviour
{
    public GameObject[] charFish;   //����� ������ ����
    public GameObject player;   //������� ����� ��ü clone

    public int FishNum;     //SelectFish���� �޾ƿ� ������ ����� ��ȣ

    public void Start()     //���� �����
    {
        IsLobby();
        //player = Instantiate(charFish[0]);      //�⺻ ���� ����
        player.transform.SetParent(transform.parent.transform.GetChild(2));     //LobbyFish�ؿ� �ڽ����� clone����

        //����� ��ü ��ġ
        player.GetComponent<RectTransform>().anchoredPosition = new Vector3(-160, 0, 0);
        player.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f,0.5f);
        player.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        player.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        player.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 320);
    }
    public void GetFishNum(int num)     //����� �������� ����� ������ ������
    {
        Destroy(player);        //������ ������ ����� ����
        FishNum = num;      //������ ����� ��ȣ
        player = Instantiate(charFish[FishNum]);    //�޾ƿ� ����� ��ȣ clone ����
        player.transform.SetParent(transform.parent.transform.GetChild(2));


        player.GetComponent<RectTransform>().anchoredPosition = new Vector3(-160, 0, 0);
        player.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        player.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 320);

    }

     public virtual void IsLobby()     //���ӿ��� �гο��� ù��° ĳ���� ���Ķߴ� �� ����
    {
        player = Instantiate(charFish[0]);
    }

}
