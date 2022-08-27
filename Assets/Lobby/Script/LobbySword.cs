using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySword : GOSword   //Lobby ���� �̹��� ����
{
    public override void IsLobby()
    {
        sword = Instantiate(charSword[0],transform.position,Quaternion.Euler(0,0,0));      //���� ���� �������� ������ �� ù��° ���� �̹��� ����
        sword.transform.parent = transform;
        sword.transform.position = Vector3.zero;
    }
}
