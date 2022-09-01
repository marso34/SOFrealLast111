using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
public class QuestManager : MonoBehaviour
{

    public GameObject[] Stayges;//�������� ��ü �迭
    public int Score;

    public int Level_;// ����
    public GameObject Levelboard;// �κ��� ���� ����
    public GameObject Player;
    public GameObject Stayge; // ���� ����Ǵ� ��������
    public GameObject GM;// ���ӸŴ��� 
    public GameObject IntroPanelName;
    public GameObject[] IntroPanelPlan = new GameObject[3];// � ����Ʈ���� �˸� �ִ� 3��
    public GameObject QuestBoard_;// ����Ʈ ����
                                  //-----------------
    public GameObject KnifeEnemy;
    public GameObject BulletEnemy;
    public GameObject BossEnemy2;// 1���� ���� Ÿ�ھ�
    public GameObject BossEnemyCrab; // 2���� ���� ũ��
    public GameObject VictimObj;
    public GameObject WaveObj;
    public GameObject BigTrashObj; // ũ���� ������
    public GameObject BigTrashObj2; // ŷũ�� ������
    public GameObject TrashObj;
    public GameObject TrashObj2;

    public GameObject BubblesShiledObj;
    public int IngameLevel;
    //--------------------
    public bool LoseFlag;// �׾��ų� �ð��� �ٵ�����
    public bool ObjMFlag;
    public int ShapeNum;  // �������� ���� ��ȣ 0 = �ð���, 1 = ShapeA, 2 = ShapeB, 3 = ShapeC
    public int limitTime;// ���ѽð�
    public int MaxCount;//Ŭ���������� ����ϴ� ī��Ʈ ���� ������ Player?�� �������ְ� ���� ��ü����, ų����, ���ɰ���, 
    //-----------------------------------------�ʿ� ������ �� ������Ʈ�� �Ѱ����� ������.
    public int KnifeEnemyMaxCount = 0;
    public int BulletEnemyMaxCount = 0;
    public int WaveMaxCount = 0;

    public int BigTrashMaxCount = 0;

    public int TrashMaxCount = 0;
    public int Trash2MaxCount = 0;
    public int VictimMaxCount = 0;

    public int BossMaxCount = 0;

    //-------------------- ���� �ʿ� �� ������Ʈ�� ��ֳ� ī��Ʈ��
    public int KnifeEC = 0;
    public int BulletEC = 0;
    public int BossEC = 0;
    public int WaveOC = 0;
    public int BigTrashOC = 0;
    public int TrashOC = 0;
    public int Trash2OC = 0;
    public int VictimOC = 0;
    public int BubblesShiledOC = 0;
    //-----------------------------
    public int CurrentCount;// ShapeA�϶� ��� ���� ���� ���� ����
    public int[] Rank;// ShapeB�� �� ��� ���� ��ũ ���� 4�� ǥ��`
    public int OccupationTime;//ShapeC�� �� ��� ������ �󸶳� ã����.
    public bool Flag = true;// update���� �ѹ��� ������ ���� ��� ���� 
    public Image me;//�Ⱦ��� ����
    //�����ܵ��� ��Ʈ�� �гο��� ����. �� �̹���.
    //----------------------------------------------------
    public Sprite TimeIcon;
    public GameObject SpearImg;
    public Sprite FleshIcon;
    public Sprite[] BossIcon;
    public Sprite bubbleIcon;
    public Sprite TrushIcon;
    public Sprite killIcon;
    public Sprite FishIcon;
    public Sprite RankIcon;
    public Sprite FlagIcon;
    public Sprite BockBossIcon;
    public Sprite TakoBossIcon;
    public GameObject CanTrash;
    public GameObject PaperTrash;
    //------------------------------------------------------ y ????? ????
    public GameObject IntroPanel;

    public GameObject StageTag;
    public GameObject StaygeLevel;
    public Sprite UIM_Stage;
    public GameObject TutorialName;
    //public GameObject TutorialPlan;
    public bool StagyStagtFlag;

    public int TutorialLev;
    //public int TempTuLev;
    //public bool TuLev1 = false;
    //public bool EndTutorial; // Ʃ�丮�� ��������Ȯ��
    //public bool TutorialCheck;

    public bool CheckFlesh;
    public bool CheckTrash;
    public Transform Canvas;
    //public GameObject tutorial;
    public int ResetTouch;


    public float timer;
    public int waitingTime;
    public int TempFlesh;
    //public GameObject TutoBack;

    public bool A = true;
    public GameObject BokBoss;

    bool B = false;
    public GameObject Vectorv;

    public Transform IntroPenelT;
    public Transform PlayerT;

    public GameObject[] Stagys0;
    public GameObject[] Stagys1;
    public GameObject[] Stagys2;

    float Xc;
    float Yc;
    void Start()
    {
        Score = 0;

        GameLoad();//�ʱ� ������
        

        if (Level_ == 0)
            IngameLevel = 0;
        else 
            IngameLevel = 1; //n�������������� n-n ������������    

        LoseFlag = false;
        OccupationTime = 0;
        //TutorialLev = 0;
        waitingTime = 2;
        //TempTuLev = 0;
        Stayge = null;
        StagyStagtFlag = false;
        Flag = true;
        ObjMFlag = true;
        GM.GetComponent<GameManager_>().SuccesFlag = false;
        Levelboard.GetComponent<Text>().text = Level_.ToString();
    }
    void Update()
    {
        Test_Method();
        Levelboard.GetComponent<Text>().text = Level_.ToString();
        if (GM.GetComponent<GameManager_>().enterGame && GM.GetComponent<GameManager_>().EndFlag == false)
        {
            Player = GameObject.FindGameObjectWithTag("Player");

            if (Player != null)
            {
                FlagOnMethod();
                if (StagyStagtFlag)
                {
                    // Debug.Log("? ??" + KnifeEC + "???" + BulletEC);
                    QuestBoard_ = GameObject.FindGameObjectWithTag("QB");
                    if (Level_ < 1)
                    {
                        QuestBoard_.GetComponent<QB>().ShapeA.SetActive(false);
                    }
                    //if(Level_< 1) QuestBoard_
                    ShapeInit();
                    SucssesFlagOnOff();
                    EndGameCheck();
                    Objectmanager();
                    if (IngameLevel > 6) IngameLevel = 6;
                    if (IngameLevel < 0) IngameLevel = 0;
                }
            }
        }
       
    }
    public void FlagOnMethod()
    {//?? ???????? ??????? ????? ????
        if (Flag)
        {
            GM.GetComponent<GameManager_>().SuccesFlag = false;
            //GameObject.FindGameObjectWithTag("ShowText").gameObject.GetComponent<ShowInLevel>().showText("Level" + " " + IngameLevel.ToString());
            if (GameObject.FindGameObjectWithTag("Stage") != null)
            {
                Debug.Log(GameObject.FindGameObjectWithTag("Stage").GetComponent<Stage>().GoalCount + "yyyyy" + GameObject.FindGameObjectWithTag("Stage"));
                Destroy(GameObject.FindGameObjectWithTag("Stage"));
            }
            if (Level_ == 0)
            {
                Level_0_Action();
            }
            if (Level_ % 2 == 1)
            {
                Level_1_Action();
            }
            if (Level_ != 0 && Level_ % 2 == 0)
            {
                Level_2_Action();
            }
            //Debug.Log(IngameLevel + "????" + MaxCount + " " + KnifeEC);
            //if (Level_ == 8) Players[Random.Range(1, 7)].GetComponent<Player>().Flag_get = true;
            // Stayge = Instantiate(Stayges[Level_ - 1], Vector3.zero, Quaternion.Euler(0, 0, 0));

            if (QuestBoard_ != null)
                Debug.Log(QuestBoard_.tag);

            Flag = false;

        }
    }

    public void Test_Method()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            IngameLevel++;
            Flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            IngameLevel--;
            Flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            Level_++;
            Flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            Level_--;
            Flag = true;
        }

        if (Input.GetKeyDown(KeyCode.G)) Player.transform.localScale = new Vector3(Player.transform.localScale.x + 1f, Player.transform.localScale.y + 1f, 1f);
        if (Input.GetKeyDown(KeyCode.R)) Player.GetComponent<Player>().HP++;

        //if (Input.GetKeyDown(KeyCode.P)) Level_++;
        if (Input.GetKeyDown(KeyCode.O)) Level_--;
    }
    public void SucssesFlagOnOff()// ���� ���� �˻�
    {
        if (ShapeNum == 5) TimeOut_EndCheck();
        else if (ShapeNum == 1) ShapeA_EndCheck();
        //else if (ShapeNum == 2) ShapeB_EndCheck();
        //else if (ShapeNum == 10) tutorial.GetComponent<Tutorial>().Tutorial_EndCheck();
    }// ���� �����°� ü
    public void Level_0_Action()
    {
        if (IngameLevel < 5)
        {
            CurrentCount = 0;

            Stayge = Instantiate(Stagys0[IngameLevel - 1], Vector3.zero, Quaternion.Euler(0, 0, 0));
        }
    }
    public void Level_1_Action() // �� �������� �������� ����Ʈ �ʱ�ȭ 
    {
        if (IngameLevel < 5)
        {
            CurrentCount = 0;

            Stayge = Instantiate(Stagys1[IngameLevel], Vector3.zero, Quaternion.Euler(0, 0, 0));
        }
    }
    public void Level_2_Action()
    {
        if (IngameLevel < 6)
        {
            CurrentCount = 0;
            TrashMaxCount = 5;
            Trash2MaxCount = 5;
            Stayge = Instantiate(Stagys2[IngameLevel - 1], Vector3.zero, Quaternion.Euler(0, 0, 0));
        }
    }
    public void CurrentCountInit()//����Ʈ �Ϸ����� ����
    {
        CurrentCount = Stayge.GetComponent<Stage>().GoalCount;
        // Debug.Log(CurrentCount + "ī��Ʈ");
    }//ShapeA���� ���
    public void ResetPlayerStat()//�� �� �������� ���� �ʱ�ȭ �ž� �� �÷��̾�� �ʱ�ȭ
    {
        //Player.GetComponent<PlayerScript>().killScore = 0;
        Player.GetComponent<PlayerScript>().BosskillScore = 0;
        Player.GetComponent<PlayerScript>().BigTrashC = 0;
    }
    public void ObjectCleanerNextStage()//���� �� ���������� �� �� �ʼ������� �������Ұ͵鸸 ����
    {
        GameObject[] Items = GameObject.FindGameObjectsWithTag("Item");
        GameObject[] Attackers = GameObject.FindGameObjectsWithTag("Attacker");
        GameObject[] AiPlayers = GameObject.FindGameObjectsWithTag("AiPlayer");
        GameObject Kraken = GameObject.FindGameObjectWithTag("Kraken");
        GameObject KingCrab = GameObject.FindGameObjectWithTag("KingCrab");
        GameObject[] BigTrash = GameObject.FindGameObjectsWithTag("BigTrash");

        for (int i = 0; i < Attackers.Length; ++i)
        {
            Destroy(Attackers[i], 0f);
        }
        for (int i = 0; i < AiPlayers.Length; ++i)
        {
            Destroy(AiPlayers[i], 0f);
        }
    }
    public void Init_Stayge()// �� �������� �ʱ�ȭ ���ӽ��� ������ �ѹ��� ����
    {
        limitTime = 0;
        if (Level_ == 0)
        {
            GameObject.Find("IntroPanel").transform.Find("plan").gameObject.SetActive(true);
            GameObject.Find("IntroPanel").transform.Find("Stage1").gameObject.SetActive(true);
            GameObject.Find("IntroPanel").transform.Find("plan1").gameObject.SetActive(true);
            GameObject.Find("IntroPanel").transform.Find("Stagy Level").gameObject.SetActive(true);

            Color a;
            a.a = 1;
            a.b = 1;
            a.g = 1;
            a.r = 1;

            GameObject uim_Stage = GameObject.Find("GameManager/Canvas/IntroPanel");
            Color color = uim_Stage.GetComponent<Image>().color = a;
            IntroPanel.GetComponent<Image>().sprite = UIM_Stage;


            limitTime = 1;
            ShapeNum = 1;
            IntroPanelName.GetComponent<Text>().fontSize = 150;
            IntroPanelName.GetComponent<Text>().text = "Ʃ�丮��";
            IntroPanelPlan[1].SetActive(true);
            //IntroPanelPlan[1].GetComponent<Image>().sprite = BossIcon[Level_ - 1];
            IntroPanelPlan[1].transform.GetChild(0).GetComponent<Text>().text = "";
        }
        if (Level_ % 2 == 1)
        {
            IntroPanelName.GetComponent<Text>().fontSize = 200;
            GameObject.Find("IntroPanel").transform.Find("plan").gameObject.SetActive(true);
            GameObject.Find("IntroPanel").transform.Find("Stage1").gameObject.SetActive(true);
            GameObject.Find("IntroPanel").transform.Find("plan1").gameObject.SetActive(true);
            GameObject.Find("IntroPanel").transform.Find("Stagy Level").gameObject.SetActive(true);

            Color a;
            a.a = 1;
            a.b = 1;
            a.g = 1;
            a.r = 1;

            GameObject uim_Stage = GameObject.Find("GameManager/Canvas/IntroPanel");
            Color color = uim_Stage.GetComponent<Image>().color = a;
            IntroPanel.GetComponent<Image>().sprite = UIM_Stage;


            limitTime = 1;
            ShapeNum = 1;
            IntroPanelName.GetComponent<Text>().text = Level_.ToString();
            IntroPanelPlan[1].SetActive(true);
            // IntroPanelPlan[1].GetComponent<Image>().sprite = BossIcon[Level_ - 1];
            IntroPanelPlan[1].transform.GetChild(0).GetComponent<Text>().text = "������ óġ�ϼ���";
        }
        else if (Level_ != 0 && Level_ % 2 == 0)
        {

            GameObject.Find("IntroPanel").transform.Find("plan").gameObject.SetActive(true);
            GameObject.Find("IntroPanel").transform.Find("Stage1").gameObject.SetActive(true);
            GameObject.Find("IntroPanel").transform.Find("plan1").gameObject.SetActive(true);
            GameObject.Find("IntroPanel").transform.Find("Stagy Level").gameObject.SetActive(true);

            Color a;
            a.a = 1;
            a.b = 1;
            a.g = 1;
            a.r = 1;

            GameObject uim_Stage = GameObject.Find("GameManager/Canvas/IntroPanel");
            Color color = uim_Stage.GetComponent<Image>().color = a;
            IntroPanel.GetComponent<Image>().sprite = UIM_Stage;


            limitTime = 1;
            ShapeNum = 1;
            IntroPanelName.GetComponent<Text>().text = Level_.ToString();
            IntroPanelPlan[1].SetActive(true);
            IntroPanelPlan[1].GetComponent<Image>().sprite = FishIcon;
            IntroPanelPlan[1].transform.GetChild(0).GetComponent<Text>().text = "ŷũ���� ���";
        }


        CurrentCount = 0;
        OccupationTime = 0;
        GM.GetComponent<GameManager_>().GlobalTime = limitTime;
    }//�������� ����
    public void TimeOut_EndCheck()// �ð��ٵƳ� üũ ������ ������
    {
        if (GM.GetComponent<GameManager_>().GlobalTime <= 0)
        {

            Level_++;
            Flag = true;

        }
    }//timeout���� ��������
    public void Objectmanager()// �� �������� �ʱ�ȭ���� ������ ������Ʈ �ִ� ������ŭ �� ������Ʈ ��� ����, 
    {
        if (ObjMFlag)
        {
            // Debug.Log("�̰� ����ȴ�...");
            if (KnifeEnemyMaxCount > KnifeEC)
            {
                Invoke("CreateKnifeE", 4.5f);
                KnifeEC++;
            }// Į �� ����
            if (BulletEnemyMaxCount > BulletEC)
            {
                Invoke("CreateBulletE", 4.5f);// �Ѿ� �� ����
                BulletEC++;
            }
            if (WaveMaxCount > WaveOC) Invoke("CreateWaveO", 2.5f);// ��������� ����
            if (BigTrashMaxCount > BigTrashOC)
            {
                CreateBigTrashO();//ū������ ���� ķ�׼� �Ұ�.
                BigTrashOC++;
            }
            if (TrashMaxCount > TrashOC)
            {

                CreateTrashO();//ĵ ������ ����
                TrashOC++;
            }
            if (Trash2MaxCount > Trash2OC)
            {
                CreateTrash2O();
                Trash2OC++;
            }
            if (BossMaxCount > BossEC)
            {
                CreateBossE();
                BossEC++;
            }
        }
    }

    Vector3 ObjRandomPosition()// ������ġ
    {
        return new Vector3(Random.Range(-13, 13), Random.Range(-8, 8), 0f);
    }
    public int RandomSignDice()
    {
        return Random.Range(0, 2);


    }
    public bool CheckSignDice(float x_, float y_)
    {
        if (x_ == Xc && y_ == Yc) return true;
        else return false;
    }
    Vector3 EnemyRandomPosition() //������ ���� ��ȯ
    {
        if (Player != null)
        {
            float x = Player.transform.position.x;
            float y = Player.transform.position.y;
            if (x < 0) Xc = +1f;
            else Xc = -1f;
            if (y < 0) Yc = +1f;
            else Yc = -1f;
        }
        float x_ = 0f;
        float y_ = 0f;


        x_ = 23f;
        y_ = 11f;


        if (RandomSignDice() == 0) return new Vector3(x_ * Xc, Random.Range(-1 * y_, y_), 0);
        else return new Vector3(Random.Range(-1 * x_, x_), y_ * Yc, 0);

    }
    Vector3 SetPosition(float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }
    public Vector3 RandomSize()
    {
        int SizeDice = Random.Range(0, 8);
        float Size;
        Size = Random.Range(0.8f, 3.0f);
        return new Vector3(Size, Size, 1f);
    }
    //���� ������Ʈ�鳢�� ��ø�Ǽ� ��ȯ�Ǹ� �Ѵ� ���� �ϰ� �ٽü�ȯ�Ǳ�. �ٸ�������Ʈ�� �켱�������� ���� �����Ǳ�.
    public void CreateKnifeE()
    {// �� ũ�� ��������
        if (Player != null)
        {
            var Enemy = Instantiate(KnifeEnemy, EnemyRandomPosition(), Quaternion.Euler(0f, 0f, 0f));
            Enemy.transform.localScale = RandomSize();
            Enemy.GetComponent<Player>().StartFlag = true;
        }

    }
    public void CreateBulletE()
    {// ��ũ�� ��������
        var Enemy = Instantiate(BulletEnemy, EnemyRandomPosition(), Quaternion.Euler(0f, 0f, 0f));
        Enemy.transform.localScale = RandomSize();
        Enemy.name = "Attacker";
    }
    public void CreateBossE()
    {
        if (IngameLevel == 2 || IngameLevel == 3)
        {
            var Boss = Instantiate(BokBoss, EnemyRandomPosition(), Quaternion.Euler(0f, 0f, 0f)); //��ġ�������־�ߵ� �¿�� ��� ������.
            Boss.name = "Boss";
        }
        else if (IngameLevel == 5)
        {
            var Boss = Instantiate(BossEnemyCrab, SetPosition(0, -13.1f, 0f), Quaternion.Euler(0f, 0f, 0f)); // ŷũ�� ���� ���߿� ��ġ ����
            Boss.name = "Boss";
        }
        else if (IngameLevel == 4)
        {
            var Boss = Instantiate(BossEnemy2, SetPosition(0, -13.47f, 0f), Quaternion.Euler(0f, 0f, 0f)); // ũ���� ����
            Boss.name = "Boss";
        }
    }
    public void CreateVictimO()
    {// ��ġ �������־���ҵ�.
        var Obj = Instantiate(VictimObj, SetPosition(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));      //currentcount�� victim���� �÷��ش� ���� �� �̰� �����ص���         
        VictimOC++;
    }
    public void CreateWaveO()//�̱���
    {
        var Obj = Instantiate(WaveObj, ObjRandomPosition(), Quaternion.Euler(0f, 0f, 0f));
        WaveOC++;
    }
    public void CreateBigTrashO()
    {//��ġ ������ �־���ҵ�? �ٴ� �ʿ�
        if (Level_ % 2 == 1)
        {
            var Obj = Instantiate(BigTrashObj, SetPosition(0, -0.67f, 0f), Quaternion.Euler(0f, 0f, 0f)); // ũ���� ������
            Obj.name = "_Kraken";
        }
        else if (Level_ != 0 && Level_ % 2 == 0)
        {
            var Obj = Instantiate(BigTrashObj2, SetPosition(0, -13.3f, 0f), Quaternion.Euler(0f, 0f, 0f)); // ŷũ�� ������
            Obj.name = "TrashCrab";
        }


    }
    public void CreateTrashO()
    {
        var Obj = Instantiate(TrashObj, ObjRandomPosition(), Quaternion.Euler(0f, 0f, 0f));
        Obj.name = "Can";
    }
    public void CreateTrash2O()
    {
        var Obj = Instantiate(TrashObj2, ObjRandomPosition(), Quaternion.Euler(0f, 0f, 0f));
        Obj.name = "Paper";
    }
    public void CreateBubblesShiled()
    {
        var Obj = Instantiate(BubblesShiledObj, SetPosition(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
        BubblesShiledOC++;
    }
    public void CreateTrashRope()
    {

    }
    public void CamAnimation()//�̱���
    {
        //����ȭ�鸸���  UI�����, ķ�̵��ϱ�, �Ҹ����, ���� ĳ�����߱� Ư�� �� ���߱�
    }

    public void ResetMaxCounter()//��� ������Ʈ �ִ� ���� ���� 0���� �ʱ�ȭ
    {
        BossMaxCount = 0;
        WaveMaxCount = 0;
        TrashMaxCount = 0;
        Trash2MaxCount = 0;
        VictimMaxCount = 0;
        BigTrashMaxCount = 0;
        KnifeEnemyMaxCount = 0;
        BulletEnemyMaxCount = 0;
    }
    public void ResetCounter()//��� ������Ʈ ���� ��ȯ�� ���� 0���� �ʱ�ȭ
    {
        KnifeEC = 0;
        BulletEC = 0;
        BossEC = 0;
        VictimOC = 0;
        WaveOC = 0;
        BigTrashOC = 0;
        TrashOC = 0;
        Trash2OC = 0;

    }
    public void ShapeInit()// �� ����Ʈ�� ���� ����
    {
        if (QuestBoard_ != null)
        {
            if (ShapeNum == 1) ShapeA_Init();
            //else if (ShapeNum == 2) ShapeB_Init();
            //if (ShapeNum == 10) tutorial.GetComponent<Tutorial>().Tutorial_Init();
        }

    }// Shape�ʱ�ȭ
    public void ShapeA_Init()
    {
        if (Stayge != null)
        {
            CurrentCountInit();
            if (Level_ > 0)
            {
                Text T = QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(2).GetComponent<Text>();
                QuestBoard_.GetComponent<QB>().ShapeA.SetActive(true);
                QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(1).GetComponent<Image>().sprite = Stayge.GetComponent<Stage>().Icon;
                if (Level_ != 0 && Level_ % 2 == 0 && IngameLevel == 3)
                    T.text = "�� �� : " + Stayge.GetComponent<Stage23>().EnemyCount.ToString();
                else if (Level_ != 0 && Level_ % 2 == 0 && IngameLevel == 2) T.text = "HP : " + Stayge.GetComponent<Stage>().BossHP.ToString();
                else
                {
                    T.text = CurrentCount.ToString() + " / " + MaxCount.ToString();
                }
            }
            else
            {
                QuestBoard_.GetComponent<QB>().ShapeA.SetActive(false);
            }
        }
    }//???????? ????
    public void ShapeA_EndCheck()//shapeA�� ���� ����üũ(�����ٸ����°�)
    {
        if (CurrentCount >= MaxCount)
        {
            Debug.Log("��������");
            Debug.Log("�ƽ�" + MaxCount);

            if ((Level_ % 2 == 1 && IngameLevel == 5) || (Level_ != 0 && Level_ % 2 == 0 && IngameLevel == 6) || (Level_ == 0 && IngameLevel == 5))
            {
                GM.GetComponent<GameManager_>().SuccesFlag = true;
                ResetPlayerStat();
                ResetMaxCounter();
                ResetCounter();
                Flag = true;
            }
            else
            {
                Flag = true;
                IngameLevel++;
            }
        }

    }
    //??????
    public void EndGameCheck()// ���� ������ üũ
    {
        if (GM.GetComponent<GameManager_>().EndFlag == false)
        {

            if (LoseFlag || Player.GetComponent<PlayerScript>().StartFlag2 && Player.GetComponent<PlayerScript>().Life == false)
            {
                Player.GetComponent<Player>().MyBody.tag = "NotBody";
                GM.GetComponent<GameManager_>().EndFlag = true;
                GM.GetComponent<GameManager_>().LosePanel.SetActive(true);
                IngameLevel = 1;
                LoseFlag = false;
                Flag = true;
                StagyStagtFlag = false;

            }
            else if (GM.GetComponent<GameManager_>().SuccesFlag)
            {
                Player.GetComponent<Player>().MyBody.tag = "NotBody";
                Debug.Log("��������");
                Level_++;
                IngameLevel = 1;
                GM.GetComponent<GameManager_>().EndFlag = true;
                GM.GetComponent<GameManager_>().WinPanel.SetActive(true);
                Score = (Score + 1) * (1 + Player.GetComponent<PlayerScript>().killScore) / ((int)Player.GetComponent<PlayerScript>().globalTime + 1);
                GM.GetComponent<GameManager_>().WinPanel.transform.GetChild(4).GetComponent<Text>().text = "Score : " + Score.ToString();
                GM.GetComponent<GameManager_>().SuccesFlag = false;

                Flag = true;
                StagyStagtFlag = false;
                GameSave();
            }

        }
    }//??????????? ??

    public void GameSave()
    {
        PlayerPrefs.SetInt("Level", Level_);
        PlayerPrefs.Save();
    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            Level_ = 0;
            return;
        }
        else Level_ = PlayerPrefs.GetInt("Level");
    }
}