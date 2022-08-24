using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagy21 : Stage
{
    // Start is called before the first frame update

    public GameObject Potal;// ��Ż�� ��Ƽ� n�������� Ŭ����
    public int ClearLevel;
    public GameObject[] Wall;
    public GameObject ResponePoint;

    void Start()
    {
        ClearLevel = 1;
        GoalCount = 0;
        flag = true;
        QM = GameObject.FindGameObjectWithTag("QM");
        TrashFlag = true;
        Potal = GameObject.FindGameObjectWithTag("Potal");
        TrashGravity = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        TrashOn();
        if (flag)
        {
            ShowText();
            GameObject Cam = GameObject.FindGameObjectWithTag("MainCamera");
            Cam.transform.position = new Vector3(0, 0, Cam.transform.position.z);
            setWalls();
            QMInit();
            flag = false;
            Destroy(GameObject.FindGameObjectWithTag("V"));
            VEC = Instantiate(QM.GetComponent<QuestManager>().Vectorv, QM.GetComponent<QuestManager>().Player.transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (VEC.GetComponent<FlowingBigT>().BigT == null)
        {
            if (GameObject.FindWithTag("Potal") != null)
                VEC.GetComponent<FlowingBigT>().setBigT(GameObject.FindWithTag("AiPlayer"));
        }
        GoalCount = Potal.GetComponent<Potal>().Goal;
    }
    void QMInit()
    {
        QM.GetComponent<QuestManager>().ResetPlayerStat();
        //TutorialName.SetActive(false);
        //GM.GetComponent<GameManager_>().ObjectCleaner();
        QM.GetComponent<QuestManager>().ShapeNum = 1;
        QM.GetComponent<QuestManager>().ResetCounter();
        //QM.GetComponent<QuestManager>().ObjectCleanerNextStage();
        QM.GetComponent<QuestManager>().ResetMaxCounter();
        QM.GetComponent<QuestManager>().MaxCount = 1;
        QM.GetComponent<QuestManager>().StagyStagtFlag = true;
        QM.GetComponent<QuestManager>().ObjMFlag = false;
        QM.GetComponent<QuestManager>().Player.transform.position = ResponePoint.transform.position;
    }
    void setWalls()
    {
        Wall[0] = GameObject.FindGameObjectWithTag("1");
        Wall[1] = GameObject.FindGameObjectWithTag("2");
        Wall[2] = GameObject.FindGameObjectWithTag("3");

    }

    public void upWall()
    {
        Destroy(Wall[ClearLevel - 1]);
        ClearLevel++;
    }
    public void ShowText()
    {
        GameObject.FindGameObjectWithTag("ShowText").gameObject.GetComponent<ShowInLevel>().showText("����� ��Ż�� ����!");
        GameObject.FindGameObjectWithTag("QB").transform.GetChild(3).GetComponent<ShowQBText>().showText("����� ��Ż�� ����!");
    }

}

