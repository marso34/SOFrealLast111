using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPage : MonoBehaviour   //�κ��丮���� ���� ������ �Ѿ�� ��ư
{

    public GameObject prePageSword;     //���� ������
    public GameObject nextPage;     //���� ������

    public void OnClick()       
    {
        prePageSword.SetActive(false);      //���� ������ ����
        nextPage.SetActive(true);       //���� ������ ����

    }

}
