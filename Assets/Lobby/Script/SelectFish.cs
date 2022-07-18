using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFish : SelectObj
{
    public GameObject FishNum;
    public GameObject LobbyFish;
    public GameObject GOFish;

    public void Start()
    {
        ObjNum = 5;     //���� ����� ��Ų ��ü ����
    }

    public override void CallLobby(int i)
    {
        base.CallLobby(i);
        LobbyFish.GetComponent<GOFish>().GetFishNum(i);     //�κ� ����� ��Ų ����
        GOFish.GetComponent<GOFish>().GetFishNum(i);        //���ӿ��� �г� ����� ��Ų ����
    }


}
