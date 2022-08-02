using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
public class QuestManager : MonoBehaviour
{

    public GameObject[] Stayges;//�������� ��ü �迭


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
    public GameObject BossEnemy1;//�߰����� ����
    public GameObject BossEnemy2;// 1���� ���� Ÿ�ھ�
    public GameObject VictimObj;
    public GameObject WaveObj;
    public GameObject BigTrashObj;
    public GameObject TrashObj;
    public GameObject TrashObj2;

    public GameObject BubblesShiledObj;
    public int IngameLevel;
    //--------------------
    public bool LoseFlag;// �׾��ų� �ð��� �ٵ�����
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
    public int[] Rank;// ShapeB�� �� ��� ���� ��ũ ���� 4�� ǥ��
    public int OccupationTime;//ShapeC�� �� ��� ������ �󸶳� ã����.
    public bool Flag = true;// update���� �ѹ��� ������ ���� ��� ���� 
    public Image me;//�Ⱦ��� ����
    //�����ܵ��� ��Ʈ�� �гο��� ����. �� �̹���.
    //----------------------------------------------------
    public Sprite TimeIcon;
    public GameObject SpearImg;
    public Sprite FleshIcon;
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
    public GameObject TutorialPlan;
    public bool StagyStagtFlag;

    public int TutorialLev;
    public int TempTuLev;
    //public bool TuLev1 = false;
    public bool EndTutorial; // Ʃ�丮�� ��������Ȯ��
    public bool TutorialCheck;

    public bool CheckFlesh;
    public bool CheckTrash;
    public Transform Canvas;
    public GameObject tutorial;
    public int ResetTouch;


    public float timer;
    public int waitingTime;
    public int TempFlesh;
    public GameObject TutoBack;
    public GameObject Guide;
    public bool A = true;
    public GameObject BokBoss;
    public GameObject JoyStick;
    public GameObject Slider; //�����̴� �̹���1

    public GameObject BusterBtn;
    public GameObject SkillBtn;
    public GameObject Stop;
    public GameObject QuestBoard;
    bool B = false;
    public GameObject Vectorv;
    public GameObject KillBoard;
    public GameObject TimeBoard;

    public int tempkill;
    public bool temp = false;
    public GameObject itembtn;
    public Transform IntroPenelT;
    public Transform PlayerT;
    public int TutorialLevel;
    public GameObject body;
    
    public bool TIsMove = false;
    void Start()
    {
        Level_ = 0;//�ʱ� ������
        IngameLevel = 1; //n�������������� n-n ������������    
        TutorialLevel = 0;
        LoseFlag = false;
        OccupationTime = 0;
        TutorialLev = 0;
        waitingTime = 2;
        TempTuLev = 0;
        StagyStagtFlag = false;
    }
    void Update()
    {
        if (GM.GetComponent<GameManager_>().enterGame && GM.GetComponent<GameManager_>().EndFlag == false)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Test_Method();
            FlagOnMethod();
            if (Player != null && StagyStagtFlag)
            {
                // Debug.Log("? ??" + KnifeEC + "???" + BulletEC);
                QuestBoard_ = GameObject.FindGameObjectWithTag("QB");

                ShapeInit();
                SucssesFlagOnOff();
                EndGameCheck();
                Objectmanager();
                if (IngameLevel > 8) IngameLevel = 5;
                if (IngameLevel < 0) IngameLevel = 0;
            }
        }

    }
    public void FlagOnMethod()
    {//?? ???????? ??????? ????? ????
        if (Flag)
        {
            if (Level_ == 0)
            {
                Level_0_Action();
            }

            if (Level_ == 1)
            {
                Level_1_Action();
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
            IngameLevel = 3;
            Flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            IngameLevel = 4;
            Flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            IngameLevel = 6;
            Flag = true;
        }

        if (Input.GetKeyDown(KeyCode.G)) Player.transform.localScale = new Vector3(Player.transform.localScale.x + 1f, Player.transform.localScale.y + 1f, 1f);
        if (Input.GetKeyDown(KeyCode.R)) Player.GetComponent<Player>().HP++;
        Levelboard.GetComponent<Text>().text = Level_.ToString();
        //if (Input.GetKeyDown(KeyCode.P)) Level_++;
        if (Input.GetKeyDown(KeyCode.O)) Level_--;
    }
    public void SucssesFlagOnOff()// ���� ���� �˻�
    {
        if (ShapeNum == 5) TimeOut_EndCheck();
        else if (ShapeNum == 1) ShapeA_EndCheck();
        //else if (ShapeNum == 2) ShapeB_EndCheck();
        else if (ShapeNum == 3) ShapeC_EndCheck();
        else if (ShapeNum == 10) Tutorial_EndCheck();
    }// ���� �����°� ü
    public void Level_0_Action()
    {
        if (Level_ == 0)
        {
            /*
            ResetPlayerStat();

            if(TutorialLev < 3)
            {
                KnifeEnemyMaxCount = 0;
                BulletEnemyMaxCount = 0;
                MaxCount = 1;
            }

            else if(TutorialLev >= 3)
            {
                TrashMaxCount = 5;
                Trash2MaxCount = 5;

            }*/
        }
    }
    public void Level_1_Action() // �� �������� �������� ����Ʈ �ʱ�ȭ 
    {
        if (Level_ == 1)
        {
            ResetPlayerStat();
            if (IngameLevel == 1)
            {
                Destroy(GameObject.FindGameObjectWithTag("tt"));
                //TutorialName.SetActive(false);
                //GM.GetComponent<GameManager_>().ObjectCleaner();
                ResetMaxCounter();// ��� �ƽ�ī���� 0���� �ʱ�ȭ
                ResetCounter();// ��� ���� �����ϴ� ������Ʈ ī��Ʈ�Ѱ� 0���� �� �ʱ�ȭ
                KnifeEnemyMaxCount = 4;// Į�� ������� �ִ� 4������ȯ
                MaxCount = 2;// ų ���忡 ǥ�õ� ų���ھ� 2�޼��� Ŭ����
                Player.transform.localPosition = Vector3.zero;
            }
            else if (IngameLevel == 2)
            {
                ResetMaxCounter();
                KnifeEnemyMaxCount = 5;
                BulletEnemyMaxCount = 1;// �Ѿ˽�� �� 1���� ��ȯ
                MaxCount = 4;//ų ���忡 ǥ�õ� ų���ھ� 4�޼��� Ŭ����
            }
            else if (IngameLevel == 3)
            {
                ResetMaxCounter();
                KnifeEnemyMaxCount = 5;
                BulletEnemyMaxCount = 2;
                MaxCount = 6;// ų ���忡 ǥ�õ� ų���ھ� 6�޼��� Ŭ����
            }
            else if (IngameLevel == 4)
            {
                ObjectCleanerNextStage();
                ResetMaxCounter();
                //ResetCounter();
                BossMaxCount = 1;
                MaxCount = 1; // ���� �Ѹ� ������ Ŭ����
            }
            else if (IngameLevel == 5)
            {
                ResetMaxCounter();
                ResetCounter();
                KnifeEnemyMaxCount = 5;
                BulletEnemyMaxCount = 3;
                MaxCount = 9;// ų ���忡 ǥ�õ� ų���ھ� 9�޼��� Ŭ����
            }
            else if (IngameLevel == 6)
            {
                Instantiate(Vectorv, Player.transform.position, Quaternion.Euler(0, 0, 0));
                ResetCounter();
                ObjectCleanerNextStage();
                //?��?????? ???? ??? ?��?????????? ??????????? ???? ?????. ??? ?????? ??????????? ?? ??????? ????
                ResetMaxCounter();
                KnifeEnemyMaxCount = 0;
                BulletEnemyMaxCount = 0;
                BigTrashMaxCount = 1;
                MaxCount = 1;// ū������ 1�� �μ��� Ŭ����
            }
            else if (IngameLevel == 7)
            {
                Destroy(GameObject.FindGameObjectWithTag("V"));
                ResetCounter();
                ObjectCleanerNextStage();
                ResetMaxCounter();
                KnifeEnemyMaxCount = 2;
                BulletEC = 2;
                BossMaxCount = 1;
                MaxCount = 1;
                // ���������� Ŭ����
            }
            TrashMaxCount = 5;
            Trash2MaxCount = 5;
            CurrentCount = 0;
        }
    }
    public void Level_2_Action()
    {
        if (Level_ == 2)
        {
            if (IngameLevel == 1)
            {
            }
            if (IngameLevel == 2)
            {
            }
            if (IngameLevel == 3)
            {
            }
            if (IngameLevel == 4)
            {

            }
            if (IngameLevel == 5)
            {

            }
        }
    }
    public void CurrentCountInit()//����Ʈ �Ϸ����� ����
    {
        if (IngameLevel == 1)
        {
            CurrentCount = Player.GetComponent<PlayerScript>().killScore;
        }
        else if (IngameLevel == 2)
        {
            CurrentCount = Player.GetComponent<PlayerScript>().killScore;// ������ ���� CurrentCount�� ä���ִ°��� �޶������Ѵ�.
        }
        else if (IngameLevel == 3)
        {
            CurrentCount = Player.GetComponent<PlayerScript>().killScore;
        }
        else if (IngameLevel == 4)
        {
            CurrentCount = Player.GetComponent<PlayerScript>().BosskillScore;//Maxcount�� Ư�� ����� ��ȣ
        }
        else if (IngameLevel == 5)
        {
            CurrentCount = Player.GetComponent<PlayerScript>().killScore;//Maxcount�� Ư�� ����� ��ȣ
        }
        else if (IngameLevel == 6)
        {
            CurrentCount = Player.GetComponent<PlayerScript>().BigTrashC;
        }

        else if (IngameLevel == 7)
        {
            CurrentCount = Player.GetComponent<PlayerScript>().BosskillScore;
        }
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
            ShapeNum = 10;
            IntroPanel.SetActive(true);


            TutorialLev = 1;

            GameObject.FindWithTag("plan").SetActive(false);
            GameObject.FindWithTag("Stage1").SetActive(false);
            GameObject.FindWithTag("plan1").SetActive(false);
            GameObject.FindWithTag("Stagy Level").SetActive(false);

            tutorial = Instantiate(tutorial);
            TutorialName = Instantiate(TutorialName);
            TutoBack = Instantiate(TutoBack);

            
            tutorial.transform.SetParent(Canvas);
            TutoBack.transform.SetParent(GM.transform);
            tutorial.transform.SetSiblingIndex(0);
            tutorial.SetActive(false);
            TutoBack.SetActive(false);
            TutorialPlan.SetActive(false);
            A = true;
            
            TutorialName.transform.SetParent(IntroPenelT);
            TutorialName.transform.localPosition = new Vector3(0, 0, 0);      
            //GameObject.FindWithTag("IntroPanel").SetActive(false);


        }




        else if (Level_ == 1)
        {

            GameObject.Find("IntroPanel").transform.Find("plan").gameObject.SetActive(true);
            GameObject.Find("IntroPanel").transform.Find("Stage").gameObject.SetActive(true);
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
            IntroPanelName.GetComponent<Text>().text = "1";
            IntroPanelPlan[1].SetActive(true);
            IntroPanelPlan[1].GetComponent<Image>().sprite = FishIcon;
            IntroPanelPlan[1].transform.GetChild(0).GetComponent<Text>().text = "ũ������ ����";



        }
        else if (Level_ == 2)
        {
            ShapeNum = 1;
            //IntroPanelName.GetComponent<Text>().text = "2";
            //IntroPanelPlan[1].SetActive(true);
            //IntroPanelPlan[1].GetComponent<Image>().sprite = FleshIcon;
            //IntroPanelPlan[1].transform.GetChild(0).GetComponent<Text>().text = "10���� ��⸦ ��������";
            limitTime = 1;
            MaxCount = 25;
            KnifeEnemyMaxCount = 3;
            BulletEnemyMaxCount = 2;
        }
        else if (Level_ == 3)
        {
            ShapeNum = 1;
            IntroPanelName.GetComponent<Text>().text = "3";
            IntroPanelPlan[1].SetActive(true);
            IntroPanelPlan[1].GetComponent<Image>().sprite = killIcon;
            IntroPanelPlan[1].transform.GetChild(0).GetComponent<Text>().text = "10ų �ϼ���";
            limitTime = 90;
            MaxCount = 10;
        }
        else if (Level_ == 4)
        {
            ShapeNum = 1;
            IntroPanelName.GetComponent<Text>().text = "4";
            IntroPanelPlan[1].SetActive(true);
            IntroPanelPlan[1].GetComponent<Image>().sprite = FishIcon;
            IntroPanelPlan[1].transform.GetChild(0).GetComponent<Text>().text = "����4���� �� ��������";
            limitTime = 90;
            MaxCount = 4;
        }
        else if (Level_ == 5)
        {
            ShapeNum = 2;
            IntroPanelName.GetComponent<Text>().text = "5";
            IntroPanelPlan[1].SetActive(true);
            IntroPanelPlan[1].GetComponent<Image>().sprite = RankIcon;
            IntroPanelPlan[1].transform.GetChild(0).GetComponent<Text>().text = "1���� �����ϼ���";
            limitTime = 40;
            MaxCount = 1;
        }
        else if (Level_ == 6)
        {
            ShapeNum = 3;
            IntroPanelName.GetComponent<Text>().text = "6";
            IntroPanelPlan[1].SetActive(true);
            IntroPanelPlan[1].GetComponent<Image>().sprite = bubbleIcon;
            IntroPanelPlan[1].transform.GetChild(0).GetComponent<Text>().text = "������� �����ϼ���";
            limitTime = 40;
            MaxCount = 10;
        }
        else if (Level_ == 7)
        {
            ShapeNum = 1;
            IntroPanelName.GetComponent<Text>().text = "7";
            IntroPanelPlan[1].SetActive(true);
            IntroPanelPlan[1].GetComponent<Image>().sprite = TrushIcon;
            IntroPanelPlan[1].transform.GetChild(0).GetComponent<Text>().text = "�����⸦ �����ϼ���";
            limitTime = 60;
            MaxCount = 25;
        }
        else if (Level_ == 8)
        {
            ShapeNum = 3;
            IntroPanelName.GetComponent<Text>().text = "8";
            IntroPanelPlan[1].SetActive(true);
            IntroPanelPlan[1].GetComponent<Image>().sprite = FlagIcon;
            IntroPanelPlan[1].transform.GetChild(0).GetComponent<Text>().text = "����� Ż���ϼ���";
            limitTime = 60;
            MaxCount = 10;
        }
        CurrentCount = 0;
        OccupationTime = 0;
        GM.GetComponent<GameManager_>().GlobalTime = limitTime;
    }//�������� ����
    public void TimeOut_EndCheck()// �ð��ٵƳ� üũ ������ ������
    {
        if (GM.GetComponent<GameManager_>().GlobalTime <= 0)
        {
            //GM.GetComponent<GameManager_>().SuccesFlag = true;
            Level_++;
            Flag = true;

        }
    }//timeout���� ��������
    public void Objectmanager()// �� �������� �ʱ�ȭ���� ������ ������Ʈ �ִ� ������ŭ �� ������Ʈ ��� ����, 
    {
        // Debug.Log("�̰� ����ȴ�...");
        if (KnifeEnemyMaxCount > KnifeEC)
        {
            Invoke("CreateKnifeE", 2.5f);
            KnifeEC++;
        }// Į �� ����
        if (BulletEnemyMaxCount > BulletEC)
        {
            Invoke("CreateBulletE", 2.5f);// �Ѿ� �� ����
            BulletEC++;
        }
        if (WaveMaxCount > WaveOC) Invoke("CreateWaveO", 2.5f);// ��������� ����
        if (BigTrashMaxCount > BigTrashOC) CreateBigTrashO();//ū������ ���� ķ�׼� �Ұ�.
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
        if (BossMaxCount > BossEC) CreateBossE();//���� ���� ķ�׼��Ұ�.�� �׼� �Ұ�.
    }
    Vector3 ObjRandomPosition()// ������ġ
    {
        return new Vector3(Random.Range(-13, 13), Random.Range(-8, 8), 0f);
    }
    Vector3 EnemyRandomPosition() //������ ���� ��ȯ
    {
        float x = Player.transform.position.x;
        float y = Player.transform.position.y;
        float Xc;
        float Yc;
        if (x < 0) Xc = 1;
        else Xc = -1;
        if (y < 0) Yc = 1;
        else Yc = 1;
        float realX = x + 8 * Xc;
        float realY = y + 5 * Xc;
        return new Vector3(Random.Range(realX, realX + Xc * 3), Random.Range(realY, realY + Yc * 2), 0f);
    }
    Vector3 SetPosition(float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }
    public Vector3 RandomSize()
    {
        int SizeDice = Random.Range(0, 8);
        float Size;
        float bigSize = Player.transform.localScale.y + 1.2f;
        float littleBigSize = Player.transform.localScale.y + 0.5f;
        if (SizeDice == 0)
            Size = Random.Range(Player.transform.localScale.y, bigSize);
        else if (SizeDice == 1 || SizeDice == 2) Size = Random.Range(Player.transform.localScale.y, littleBigSize);
        else Size = Player.transform.localScale.y;
        if (Size > 3) Size = 3;
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
        if (IngameLevel == 4)
        {
            var Boss = Instantiate(BokBoss, SetPosition(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f)); //��ġ�������־�ߵ� �¿�� ��� ������.
            Boss.name = "Boss";


        }
        else if (IngameLevel == 7)
        {
            var Boss = Instantiate(BossEnemy2, SetPosition(0, -15.55f, 0f), Quaternion.Euler(0f, 0f, 0f));
            Boss.name = "Boss";
        }
        BossEC++;
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
        var Obj = Instantiate(BigTrashObj, SetPosition(0, 0, 0f), Quaternion.Euler(0f, 0f, 0f));
        BigTrashOC++;
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
    public void CreateTrashRope(){
        
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
            else if (ShapeNum == 3) ShapeC_Init();
            else if (ShapeNum == 10) Tutorial_Init();
        }


    }// Shape�ʱ�ȭ
    public void ShapeA_Init()
    {
        CurrentCountInit();
        QuestBoard_.GetComponent<QB>().ShapeA.SetActive(true);
        if (Level_ == 1)
        {
            if (IngameLevel < 4){
                QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(0).GetComponent<Image>().sprite = killIcon;
            }
            else if (IngameLevel ==4){
                QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(0).GetComponent<Image>().sprite = BockBossIcon;
            }
            else if (IngameLevel == 5){
                QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(0).GetComponent<Image>().sprite = killIcon;
            }
            else if (IngameLevel == 6){
                QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(0).GetComponent<Image>().sprite = TrushIcon;
            }
            else if(IngameLevel == 7){
                QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(0).GetComponent<Image>().sprite = TakoBossIcon;
            }
            QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(1).GetComponent<Text>().text = CurrentCount.ToString() + " / " + MaxCount.ToString();
        }
        if(Level_ == 2){
            if(IngameLevel == 1){
                //�������� ����, ��� ������ �μ��� ���.
            }
            if(IngameLevel == 2){
                // �Ʊ� ����� ���潺  
            }
            if(IngameLevel == 3){
                // ���ο� ����� 10����, ����Ŀ 3���� ���
            }
            if(IngameLevel == 4){
                // �Ŵ뾲���� �� �ı�
            }
            if(IngameLevel == 5){
                // 
            }
            
        }

    }//�������� �ʱ�ȭ
    /*
    public void ShapeB_Init()
    {
        Rank = new int[Players.Length];
        for (int i = 0; i < Players.Length; ++i)
            Rank[i] = Players[i].GetComponent<Player>().killScore;


        QuestBoard_.GetComponent<QB>().ShapeB.SetActive(true);
        System.Array.Sort(Rank);
        System.Array.Reverse(Rank);
        bool flag__ = true;
        for (int i = 0; i < 4; ++i)
        {

            QuestBoard_.GetComponent<QB>().ShapeB.GetComponent<ShapeB>().Ranks[i].GetComponent<Text>().text = Rank[i].ToString();
            if (Rank[i] == .GetComponent<Player>().killScore && flag__)
            {
                QuestBoard_.GetComponent<QB>().ShapeB.GetComponent<ShapeB>().Ranks[i].GetComponent<Text>().text = "Me";
                flag__ = false;
            }
        }
      //QuestBoard_.GetComponent<QB>().ShapeB.GetComponent<ShapeB>().first;
    }//��ŷ�����ʱ�ȭ*/
    public void ShapeC_Init()
    {
        QuestBoard_.GetComponent<QB>().ShapeC.SetActive(true);
        QuestBoard_.GetComponent<QB>().ShapeC.transform.GetChild(0).GetComponent<Image>().fillAmount = (float)OccupationTime / 10f;
    }//�������ʱ�ȭ
    public void ShapeA_EndCheck()//shapeA�� ���� ����üũ(�����ٸ����°�)
    {
        if (CurrentCount >= MaxCount)
        {
            if (IngameLevel == 7)
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
        /*
        else if (GM.GetComponent<GameManager_>().GlobalTime <= 0)
        {
            GM.GetComponent<GameManager_>().SuccesFlag = false;
            LoseFlag = true;
    }*/
    }
    /* public void ShapeB_EndCheck()
     {
         if (GM.GetComponent<GameManager_>().GlobalTime <= 0)
         {
             if (Rank[0] == .GetComponent<Player>().killScore)
             {
                // GM.GetComponent<GameManager_>().SuccesFlag = true;
                 Level_++;
                 Flag = true;
             }
             else 
             {
                 GM.GetComponent<GameManager_>().SuccesFlag = false;
                 LoseFlag = true;
             }
         }


     }   //��ŷ����� ��������
     */
    public void ShapeC_EndCheck()
    {
        if (OccupationTime >= MaxCount)
        {
            //GM.GetComponent<GameManager_>().SuccesFlag = true;
            Level_++;
            Flag = true;
        }
        /*
        else if (GM.GetComponent<GameManager_>().GlobalTime <= 0)
        {
            GM.GetComponent<GameManager_>().SuccesFlag = false;
            LoseFlag = true;

        }
*/
    }//������
    public void EndGameCheck()// ���� ������ üũ
    {
        if (GM.GetComponent<GameManager_>().EndFlag == false)
        {
            if (LoseFlag || Player.GetComponent<PlayerScript>().StartFlag2 && Player.GetComponent<PlayerScript>().Life == false)
            {

                GM.GetComponent<GameManager_>().EndFlag = true;
                GM.GetComponent<GameManager_>().LosePanel.SetActive(true);
                IngameLevel = 1;
                LoseFlag = false;
                Flag = true;
            }
            else if (GM.GetComponent<GameManager_>().SuccesFlag)
            {
                // Level_++;
                IngameLevel = 1;
                GM.GetComponent<GameManager_>().EndFlag = true;
                GM.GetComponent<GameManager_>().WinPanel.SetActive(true);
                GM.GetComponent<GameManager_>().SuccesFlag = false;
                Level_ = 1;
                Flag = true;
            }
        }
    }//���ӳ������� üũ
    public void Tutorial_EndCheck()
    {
        if (EndTutorial)
        {
            Level_++;
            GM.GetComponent<GameManager_>().SuccesFlag = true;


        }

    }



    public void bornguide() 
    { //�÷��̾� ���߰� ���̵� ����⸦ �÷��̾� �ڽ����� ��

        GameObject.Find("Canvas").transform.Find("Tutorial(Clone)").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("Tutorial(Clone)").transform.Find("TuText").gameObject.SetActive(true);

    } 


    public void NextTutorial()
    {
        Player.GetComponent<PlayerScript>().StopMove();
                    
        tutorial.SetActive(true);
        TutorialPlan.SetActive(false);

        Guide = GameObject.Find("Player(Clone)").transform.Find("GuidePet(Clone)").gameObject;
        Guide.GetComponent<GuidePet>().BornGuide();
        tutorial.GetComponent<Tutorial>().Touch = 0;
                    
        TutoBack.SetActive(true);
                    
                    
        TutorialLev++;
    }
    
    public void Tutorial_Init() //�÷��̾� ĵ������ �ִ°� �־��
    {

                
        TutorialPlan = GameObject.Find("Canvas").transform.Find("Tutorial(Clone)").transform.Find("TuText").gameObject;
        tutorial = GameObject.Find("Canvas").transform.Find("Tutorial(Clone)").gameObject;
        //TutorialPlan = GameObject.Find("Canvas").transform.Find("Tutorial(Clone)").transform.Find("TuText").gameObject;
        TutoBack = GameObject.Find("GameManager").transform.Find("TutoBack(Clone)").gameObject;
        //TutoBack.gameObject(SortingLayer(1));
        if (TutorialLev == 1) //�̵�
        {
                
                
                if(A)   //�����Ҷ� Ʃ�丮�� ������������ �ѹ� ������ �ӽ÷� �ص�
                {
                    //tutorial.SetActive(true);

                    //TutorialPlan.SetActive(true);
                    TutoBack.SetActive(true);
                    
                    tutorial.SetActive(true);
                    Vector3 direction = Player.transform.localRotation * new Vector3(0,0,-90);
                    A = false;
                }
            

            
            TutorialPlan.GetComponent<Text>().text = "�̵��ϸ鼭 �ν��͸� ����غ�����";
            
            if (Player.GetComponent<PlayerScript>().BusterFlag
                && Player.GetComponent<PlayerScript>().cutGauge < 70 && Player.transform.localRotation.z != 0) 
            {

                timer += Time.deltaTime;
                if (timer > waitingTime-2) //�����ϰ� ���̵� �����ߴٰ� ���
                {
                    
                    timer = 0;
                    Player.GetComponent<PlayerScript>().BusterFlag = false;
                    TutorialPlan.GetComponent<Text>().text = "��ų�� ����غ�����";
                    NextTutorial();

                }
            }


        }


        else if (TutorialLev == 2) //�ν��� Ʃ�丮��
        {

            Player.GetComponent<PlayerScript>().FishNumber = 2;
            if (Player.GetComponent<PlayerScript>().skillcheck) //playerscript�� PlaySkill()�Լ� ������ ���⵵ ����
            {
                timer += Time.deltaTime;

                if (timer > waitingTime) //��ų ���� 3�ʵڿ� ����
                {

                    
                    timer = 0;
                    TutorialPlan.GetComponent<Text>().text = "�����⸦ ġ��� ���� �������� �԰�"+"\n"+"������ ��ư�� ���� ����ϼ���";
                    tutorial.GetComponent<Tutorial>().OnVideo1 = true;
                    tutorial.GetComponent<Tutorial>().StopClick = true;
                    NextTutorial();
                    itembtn = GameObject.Find("Player(Clone)").transform.Find("Canvas").transform.Find("NotEndGame").transform.Find("ItemBtn").gameObject;

                    Trash2MaxCount = 5;


                }
            }
        }

        else if (TutorialLev == 3)
        {
            //���⼭ �ΰ��� ���� �ٲٱ�

            if (itembtn.GetComponent<ItemBtn>().TutorialItem) //playerscript�� PlaySkill()�Լ� ������ ���⵵ ����
            {
                timer += Time.deltaTime;

                if (timer > waitingTime) //��ų ���� 3�ʵڿ� ����
                {

                    
                    timer = 0;
                    TutorialPlan.GetComponent<Text>().text = "�����ϴ� ����⸦ ������ ���"+"\n"+"���� ��ü�� ��������";
                    tutorial.GetComponent<Tutorial>().OnVideo2 = true;
                    tutorial.GetComponent<Tutorial>().StopClick = true;
                    tutorial.GetComponent<Tutorial>().BornAtt = false;
                    NextTutorial();

                    

                }
            }
        }

        else if (TutorialLev ==4) 
        {
            //Cursor.visible = false;
            body = GameObject.Find("Player(Clone)").transform.Find("body").gameObject;

            if(tutorial.GetComponent<Tutorial>().TouchMo == true && tutorial.GetComponent<Tutorial>().BornAtt)
            {
                BulletEnemyMaxCount = 1;
            }

            
            if(body.GetComponent<BodyInteraction>().TutorialFlesh)
            {
                timer += Time.deltaTime;

                if (timer > waitingTime) //��ų ���� 3�ʵڿ� ����
                {

                    timer = 0;
                    
                    Player.GetComponent<PlayerScript>().StopMove();
                    Destroy(tutorial);
                    Destroy(TutoBack);
                    Destroy(TutorialName);
                    EndTutorial = true;
                    
                }
            }
        }

    }


}