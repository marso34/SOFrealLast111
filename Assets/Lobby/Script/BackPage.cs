using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPage : MonoBehaviour   //�κ��丮 �ι�° ���������� ù��° �������� ���� ��ư
{
    public GameObject backPageSword;
    public GameObject backPage;
    public GameObject prePage;

    public void OnClick()
    {
        backPageSword.SetActive(true);
        prePage.SetActive(false);
        backPage.SetActive(true);
    }
}
