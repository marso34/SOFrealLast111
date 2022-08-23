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
        TrashFlag = true;
        TrashGravity = 0.005f;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            ShowText();
            GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
            Cam.transform.position = new Vector3(0, 0, Cam.transform.position.z);
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
            QM.GetComponent<QuestManager>().ObjMFlag = true;
            VEC = Instantiate(QM.GetComponent<QuestManager>().Vectorv, QM.GetComponent<QuestManager>().Player.transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (VEC.GetComponent<FlowingBigT>().BigT == null)
        {
            if (GameObject.FindWithTag("AiPlayer") != null)
                VEC.GetComponent<FlowingBigT>().setBigT(GameObject.FindWithTag("AiPlayer"));
                else if(GameObject.FindWithTag("Attacker") != null){
                    VEC.GetComponent<FlowingBigT>().setBigT(GameObject.FindWithTag("Attacker"));
                }
        }
        TrashOn();
        GoalCount = QM.GetComponent<QuestManager>().Player.GetComponent<PlayerScript>().killScore;
    }


    public void ShowText()
    {
        GameObject.FindGameObjectWithTag("ShowText").gameObject.GetComponent<ShowInLevel>().showText("���� ���� ������!");
        GameObject.FindGameObjectWithTag("QB").transform.GetChild(3).GetComponent<ShowQBText>().showText("���� ���� ������!");
    }
}

