using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//���� �Լ�

// �÷��̾��ȯ �� �ʱ�ȭ

public class FakePanel : MonoBehaviour
{   
    // �ΰ����г��̶� ���� �������� �ΰ��� �г��� ���������.
    
    public GameObject[] PlayersProfil = new GameObject[8];   
    GameObject [] LodingCycles = new GameObject[8];
    
    private void Start()
    {
       
    }
    public void SetProfil(int index, GameObject Player)//������ ��ҵ� ����
    {       
        
        Destroy(LodingCycles[index]);
        // ������ �г��ӿ� �÷��̾� �̸� �ֱ�.            
    }      

    
}

