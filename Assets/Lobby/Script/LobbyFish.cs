using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyFish : GOFish     //�κ� ����� ��Ų ����
{
    public GameObject GM;
    public override void IsLobby()
    {
        player = Instantiate(charFish[0]);      //���� �������� ù��° ����� ��Ų ����
       

    }
    
}
