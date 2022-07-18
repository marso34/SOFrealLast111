using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObj : MonoBehaviour
{
    public GameObject OffBtn;     //�����ϱ� ��ư
    public int ObjNum;  //���⳪ ����� ��ü ����
    public int PreObj; //���� ���⳪ ����� ��ȣ
    public GameObject[] ObjNumArr;   //������ ��ư �߰� �� �� ���� �Լ�
    public int i;

    public void OnClick()   //������ �� ��ư �������̶�� �߰� �ϱ�
    {
        for (i = 0; i < ObjNum; i++)
        {
            if (i == PreObj)
            {
                for (int j = 0; j < ObjNum; j++)
                    ObjNumArr[j].SetActive(false);  //���� �ȵȰ� �����ϱ� ��ư

                OnSelect();
                transform.GetChild(0).transform.gameObject.SetActive(true);     //�����Ѱ� ���� �� ��ư
                CallLobby(i);   //��ȣ Ȯ���ϰ� �ϱ�
            }
        }
    }

    public void OnSelect()  
    {
        OffBtn.SetActive(false);
    }

    public void NotSelect()
    {
        OffBtn.SetActive(true);
    }


    public virtual void CallLobby(int i)
    {
        Debug.Log(i);
    }
}
