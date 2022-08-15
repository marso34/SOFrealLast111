using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage11 : Stage
{
    
    void Start()
    {
        GoalCount = 0;
        flag = true;
        QM = GameObject.FindGameObjectWithTag("QM");
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            Debug.Log("�������� ����!");
            QM.GetComponent<QuestManager>().ResetPlayerStat();
            QM.GetComponent<QuestManager>().ShapeNum = 1;
            //TutorialName.SetActive(false);
            //GM.GetComponent<GameManager_>().ObjectCleaner();
            QM.GetComponent<QuestManager>().ResetMaxCounter();// ��� �ƽ�ī���� 0���� �ʱ�ȭ
            QM.GetComponent<QuestManager>().ResetCounter();// ��� ���� �����ϴ� ������Ʈ ī��Ʈ�Ѱ� 0���� �� �ʱ�ȭ
            QM.GetComponent<QuestManager>().KnifeEnemyMaxCount = 2;// Į�� ������� �ִ� 4������ȯ
            QM.GetComponent<QuestManager>().MaxCount = 4;// ų ���忡 ǥ�õ� ų���ھ� 2�޼��� Ŭ����
            QM.GetComponent<QuestManager>().Player.transform.localPosition = Vector3.zero;
            flag = false;
            QM.GetComponent<QuestManager>().StagyStagtFlag = true;
             QM.GetComponent<QuestManager>().ObjMFlag = false;
        }
        GoalCount = QM.GetComponent<QuestManager>().Player.GetComponent<PlayerScript>().killScore;
    }
}
