using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    public GameObject CanTrash;
    public GameObject PaperTrash;
    //------------------------------------------------------ y ????? ????
    public GameObject IntroPanel;

    public GameObject StageTag;
    public GameObject StaygeLevel;
    public Sprite UIM_Stage;
    public GameObject TutorialName;
    public GameObject TutorialPlan;


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
    public GameObject Slider1; //�����̴� �̹���2
    public GameObject BusterBtn;
    public GameObject SkillBtn;
    public GameObject Stop;
    bool B = false;
    public GameObject Vectorv;
    public GameObject KillBoard;
    public GameObject TimeBoard;
    public GameObject SkillBtn2; //��ų ��ư �ȿ� ä������ �̹���
    public int tempkill;
    public bool temp = false;
    public GameObject itembtn;
    void Start()
    {
        Level_ = 1;//�ʱ� ������

        IngameLevel = 1; //n�������������� n-n ������������    
        LoseFlag = false;
        OccupationTime = 0;
        TutorialLev = 0;
        waitingTime = 2;
        TempTuLev = 0;

    }

    // Update is called once per frFLame
    void Update()
    {
        if (GM.GetComponent<GameManager_>().enterGame && GM.GetComponent<GameManager_>().EndFlag == false)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Test_Method();
            FlagOnMethod();
            if (Player != null)
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
            if (Level_ == 1)
            {
                Level_1_Action();
            }

            Debug.Log(IngameLevel + "????" + MaxCount + " " + KnifeEC);
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
    }// ���� �����°� üũ


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
            
            //GameObject.FindWithTag("Slider").SetActive(true);

            TutorialName.SetActive(true);
            TutorialLev = 1;
            
     
            tutorial.transform.SetParent(Canvas);
            tutorial.transform.SetSiblingIndex(0);
            tutorial.transform.GetChild(0).gameObject.SetActive(false);
        }




        else if (Level_ == 1)
        {

            IntroPanel.transform.GetChild(0).gameObject.SetActive(true);
            IntroPanel.transform.GetChild(1).gameObject.SetActive(true);
            IntroPanel.transform.GetChild(2).gameObject.SetActive(true);
            IntroPanel.transform.GetChild(3).gameObject.SetActive(true);


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
        float realX = x + 8* Xc;
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
            var Boss = Instantiate(BossEnemy2, SetPosition(0, -14.9f, 0f), Quaternion.Euler(0f, 0f, 0f));
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
        // QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(0).GetComponent<Image>().sprite = Stayge.GetComponent<SpriteRenderer>().sprite;
        QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(0).gameObject.SetActive(false);
        //QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(1).GetComponent<Text>().text = CurrentCount.ToString() + " / " + MaxCount.ToString();
        QuestBoard_.GetComponent<QB>().ShapeA.transform.GetChild(1).gameObject.SetActive(false);

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


    public void bornguide() { //�÷��̾� ���߰� ���̵� ����⸦ �÷��̾� �ڽ����� ��
        GM.GetComponent<GameManager_>().Player_p.GetComponent<PlayerScript>().StopMove();
        Guide.transform.SetParent(GM.GetComponent<GameManager_>().Player_p.transform);
    } 
    public void Tutorial_Init() //�÷��̾� ĵ������ �ִ°� �־��
    {
/*
        JoyStick = GM.GetComponent<GameManager_>().Player_p.transform.GetChild(3).gameObject.transform.GetChild(4).gameObject;
        KillBoard = GM.GetComponent<GameManager_>().Player_p.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject;
        TimeBoard = GM.GetComponent<GameManager_>().Player_p.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject;
        BusterBtn = GM.GetComponent<GameManager_>().Player_p.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        SkillBtn = GM.GetComponent<GameManager_>().Player_p.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;
        SkillBtn2 = GM.GetComponent<GameManager_>().Player_p.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject;
        Stop = GM.GetComponent<GameManager_>().Player_p.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject;
        itembtn = GM.GetComponent<GameManager_>().Player_p.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject;
*/


        if (TutorialLev == 1) //�̵�
        {
                if(A)   //�����Ҷ� Ʃ�丮�� ������������ �ѹ� ������ �ӽ÷� �ص�
                {
                    tutorial.transform.GetChild(0).gameObject.SetActive(true);
                    TutoBack.SetActive(true);
                    A = false;
                }

            TutorialName.SetActive(false); //Ʃ�丮�� ���� ��Ȱ
            TutorialPlan.GetComponent<Text>().text = "���̽�ƽ�� �̿��ؼ� �����ϼ���";
            
            if (GM.GetComponent<GameManager_>().Player_p.GetComponent<PlayerScript>().Timer33 > 1) //2���̻� �����̸� playerscript���� üũ��
            {
                TutorialCheck = true; //�����ϸ� üũ��
                tutorial.SetActive(true);   //���� Ʃ�丮�� �������� ��
                tutorial.transform.GetChild(0).gameObject.SetActive(true); //���� Ʃ�丮�� �̸� �ǳ�

                bornguide(); //���̵� ����� Ȱ��ȭ
                tutorial.transform.GetChild(0).GetComponent<Tutorial>().Touch =0;
                TutorialPlan.GetComponent<Text>().text = "�ν��͸� ���� ����غ�����"; //���� ����
                Guide.GetComponent<GuidePet>().OffCanvas(); //�÷��̾� ĵ���� ��ų��ư ���� ���� �Ⱥ��̰�
                TutorialLev++;

            }


        }


        else if (TutorialLev == 2) //�ν��� Ʃ�丮��
        {

            if (GM.GetComponent<GameManager_>().Player_p.GetComponent<PlayerScript>().BusterFlag
                && GM.GetComponent<GameManager_>().Player_p.GetComponent<PlayerScript>().cutGauge < 70) //�ν��� 90���Ϸ� ��� �ϸ� ����
            {

                timer += Time.deltaTime;
                if (timer > waitingTime-2) //�����ϰ� ���̵� �����ߴٰ� ���
                {
                    GM.GetComponent<GameManager_>().Player_p.GetComponent<PlayerScript>().StopMove();   //�÷��̾� ����
                    tutorial.SetActive(true);
                    tutorial.transform.GetChild(0).gameObject.SetActive(true);


                    Guide.GetComponent<GuidePet>().BornGuide();
                    tutorial.transform.GetChild(0).GetComponent<Tutorial>().Touch = 0;
                    timer = 0;
                    TutorialPlan.GetComponent<Text>().text = "��ų��ư�� ���� ��ų�� ����غ�����";
                    Guide.GetComponent<GuidePet>().OffCanvas();
                    TutorialLev++;

                }
            }
        }

        else if (TutorialLev == 3)
        {


            GM.GetComponent<GameManager_>().Player_p.GetComponent<PlayerScript>().FishNumber = 2; //���ŷ� ����
            if (GM.GetComponent<GameManager_>().Player_p.GetComponent<PlayerScript>().skillcheck) //playerscript�� PlaySkill()�Լ� ������ ���⵵ ����
            {
                timer += Time.deltaTime;

                if (timer > waitingTime) //��ų ���� 3�ʵڿ� ����
                {

                    GM.GetComponent<GameManager_>().Player_p.GetComponent<PlayerScript>().StopMove();
                    timer = 0;
                    tutorial.SetActive(true);
                    tutorial.transform.GetChild(0).gameObject.SetActive(true);


                    Guide.GetComponent<GuidePet>().OffCanvas();
                    EndTutorial = true;
                    TutorialLev++;
                    Destroy(tutorial);

                }
            }
        }

    }


}



