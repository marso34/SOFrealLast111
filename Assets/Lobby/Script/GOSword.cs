using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOSword : MonoBehaviour    //���ӿ��� �гο��� �����Ǵ� ����
{
    public GameObject[] charSword;  //���� ������ ����
    public GameObject sword;    //������� ���� clone

    public int SwordNum;    //SelectSword���� �޾ƿ� ���� ��ȣ
    public void Start() //���� ����� 
    {
        IsLobby();  //�κ� �гο��� �����Ǵµ� ���⼭ �Ⱦ��Ŷ� �Լ� ���ֳ�
        sword.transform.SetParent(transform.parent.transform.GetChild(1));      //LobbySword�ؿ� �ڽ����� clone����� ��
        //���� ���̴� ��ġ
        sword.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        sword.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        sword.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 160);

    }
    public void GetSwordNum(int num)    //���� �������� ���� ������ �� ����
    {

        Destroy(sword);     //������ ������ ���� clone ����
        SwordNum = num;     //������ ���� ��ȣ
        sword = Instantiate(charSword[SwordNum]);       //�޾ƿ� ���� ��ȣ�� clone ����
        sword.transform.SetParent(transform.parent.transform.GetChild(1));

        sword.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        sword.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        sword.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 160);

    }


    public virtual void IsLobby()
    {
        sword = Instantiate(charSword[0]);
    }
}
