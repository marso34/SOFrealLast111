using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSword : SelectObj    //Sword(0~5)Btn�� �� ��ũ��Ʈ
{
    public GameObject SwordNum;     //���� ��ȣ
    public GameObject LobbySword;       //�κ� ������ ����
    public GameObject GOSword;      //���ӿ��� �гο� ������ ����

    public void Start()
    {
        ObjNum = 6;     //���� �� ����
    }

    public override void CallLobby(int i)   //�� ���⸶�� �ο��� ��ȣ
    {
        base.CallLobby(i);      
        LobbySword.GetComponent<GOSword>().GetSwordNum(i);
        GOSword.GetComponent<GOSword>().GetSwordNum(i);
    }
}
