using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoSetting : MonoBehaviour
{
    public GameObject SetPanel;
    public GameObject SetToLob;
    public void OnClick()      //���ӿ����гο��� ������ư ���� ��
    {

        SetPanel.SetActive(true);   //ȯ�漳�� �г� ���
        SetToLob.GetComponent<GoLobby>().IsGoGO(true);  
        //ȯ�漳������ �ڷΰ��� ��ư ������ �� ���ӿ��� �г��� �״�� ��������
    }
}
