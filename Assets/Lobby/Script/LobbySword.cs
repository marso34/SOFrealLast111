using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySword : GOSword   //Lobby ���� �̹��� ����
{
    public override void IsLobby()
    {
        sword = Instantiate(charSword[0]);      //���� ���� �������� ������ �� ù��° ���� �̹��� ����
    }
}
