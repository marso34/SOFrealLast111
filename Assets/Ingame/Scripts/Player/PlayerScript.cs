using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//flesh�κ��� ���������� ũ��Ű����Լ��� ����ɼ������� ���ǹٶ�.//  
public class PlayerScript : Player
{
    
    public JoystickValue value;
    public GameObject Knife;//MFish�ʱ�ȭ�Ҷ�����
    public GameObject Body;//MBody�ʱ�ȭ �Ҷ� ���� 
                           //LOBBYPLAYER???? ??????
    public int[] ShapeKillCount;
    //public int KnifeEnemyKillCount;
    //public int BulletEnemyKillCount;
    public int MaxBodyShape = 5;
    public Slider BusterBar;
    public GameObject Kombo;
    double timer;// �ν��� ������ �������� ���� ������ ���� �ð� Time.deltaTime �� ���� ����
    double timer1;

    float waitingTime2;
    float waitingTime;// �ν��� ������ �������� ���� ���ǹ� ���� �ð�, <- timer�� waitingTime�̵Ǹ� Ư�� �Լ� ����
    double timer_;// �ν��� ������ �������� ���� ������ ���� �ð� Time.deltaTime �� ���� ����
    float waitingTime_;// �ν��� ������ �������� ���� ���ǹ� ���� �ð�, <- timer�� waitingTime�̵Ǹ� Ư�� �Լ� ����

    private float maxGauge = 100f;//�ִ� �ν��� ������ ��
    public float cutGauge = 100f;// ���� �����ϴ� �������� 

    GameObject[] AiPlayers_;

    public Text KillBoard;
    public Text TIMEBoard;

    public GameObject SkillBtn;
    public GameObject ItemBtn;
    public GameObject NotEndGame;
    public GameObject JoyStick;
    public float globalTime = 1;

    public bool StartFlag2;
    bool firstFlag;

    //����ų ���ú���
    float CountTime;
    public int CountKill;
    float CountWaitTime;
    bool KCFlag;
    bool RaiseFlag;

    public GameObject TwoKillSound;
    public GameObject ThreeKillSound;
    public GameObject FourKillSound;

    public GameObject FiveKillSound;



    public Camera maincam_;
    public int TrushCount = 0;
    public GameObject HPCTR;
    public GameObject[] HeartArr;
    public GameObject[] BlackHeartArr;
    public int BosskillScore;
    public int BigTrashC;
    float ReduceTime = 0;
    float ReduceWTime = 0.3f;
    int MaxHP = 5;

    
    public bool killcheck = false; //y �� ���̱� Ʃ�丮��
    public void Start()
    {
        TuLev1 = false; //y �̵� Ʃ�丮�� 
        transform.position = Vector3.zero;
        HP = 5;
        BigTrashC = 0;
        BosskillScore = 0;
        Flag_get = false;
        Life = true;// ������ ��
        S = Skin.transform.GetComponent<SpriteRenderer>();
        ShapeKillCount = new int[MaxBodyShape];
        for (int i = 0; i < MaxBodyShape; ++i) ShapeKillCount[i] = 0;
        StartFlag2 = false;
        killScore = 0;
        MovementSpeed = 2.3f + transform.localScale.y / 2f;//3.8
        BusterSpeed = 4.6f + transform.localScale.y / 2;// �ν��� �ӵ� //10  
        RotationSpeed = 1500f;

        Speed = MovementSpeed;// ���ǵ� ������ �⺻���ǵ�� �ٽ� �ʱ�ȭ             
        skin_ = Skin.GetComponent<Skin>();// ��Ų������Ʈ ����

        timer = 0;
        timer_ = 0;
        waitingTime = 0.11f;
        waitingTime_ = 0.1f;

       // GameWaitInit();

        timer1 = 0;

        waitingTime2 = 0.05f;


        C.a = 1f;
        C.b = 1f;
        C.r = 1f;
        C.g = 1f;

        CountTime = 0f;
        CountKill = 0;

        CountWaitTime = 4f;
        RaiseFlag = false;
        KCFlag = false;
          GameWaitInit();
       
        StartCoroutine("Start_");
    }



    private void Update()
    {



        reset_();// �����ϸ� ����
        if (Life == false) NotInit();
        // *************************** ?????? ******* ????????? ????????��??? ???**********
        if (StartFlag == true && !StartFlag2) // �÷��̾�n���̸� ���ӽ��� 
        {
            GameStartInit();
            StartFlag2 = true;
            Init_();

        }
        if (StartFlag2 == true)//��ŸƮ
        {

            if (firstFlag)
            {
                
                

                firstFlag = false;

            }

            dir = value.joyTouch;

            if (Life)
            {

        
                HPManager();
                if (KCFlag)
                {
                    if (RaiseFlag)
                    {
                        CountKill++;
                        RaiseFlag = false;
                        KomboKillSounds();
                    }
                    CountTime += Time.deltaTime;
                    if (CountTime > CountWaitTime)
                    {
                        CountTime = 0;
                        KCFlag = false;

                        CountKill = 0;
                    }
                }
                PlayerMove();//������ �� State�ʱ�ȭ  
                
                if (cutGauge > 0 && isMove)
                    GetPlayer_BusterInput();
                else if (cutGauge <= 0 || !isMove)
                {
                    Destroy(GameObject.FindWithTag("BS"));
                    reSpeed();
                    BusterFlag = false;
                }
                GetPlayer_tp();
                Handlebar();

                if (Speed == MovementSpeed)
                    RecuveryBusterGage();




            }
            else if (!Life)
            {


                cutGauge = 100;
                Destroy(GameObject.FindWithTag("BS"));
            }
            maincam_.GetComponent<Tracking_player>().BustValue(BusterFlag);
            ChangeKnife();
            ChangeBody();
            TestSkill(); // J
            TestItem(); // J

            if (!BusterFlag && skin_.GetComponent<SpriteRenderer>().color == Color.white && !SkillFlag) TempReSpped();
            AnimState(dir);
            CheckWall();
            CheckMaxSize();
            ShowBoard();
            mobileBuster();
            endchek();
            Check_Flag();
            //ReduceSize();

            if (HP <= 0) DieLife();

        }
        
    }
    public void HPManager()
    {
        for (int i = 0; i < MaxHP; ++i)
        {
            if (i < MaxHP - HP)
            {
                HeartArr[i].SetActive(false);
                BlackHeartArr[i].SetActive(true);
            }
            else
            {
                BlackHeartArr[i].SetActive(false);
                HeartArr[i].SetActive(true);
            }
        }
    }


    public void endchek()
    {
        if (GM.GetComponent<GameManager_>().EndFlag)
        {
            NotInit();
            NotEndGame.SetActive(false);
            value.joyTouch = Vector3.zero;
            JoyStick.SetActive(false);
            StartFlag2 = false;
            MyKnife.SetActive(false);
        }
    }
    public void mobileBuster()
    {
        if (BusterFlag == true)
        {
            chSpeed();
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                cutGauge -= 2f;
                timer = 0;
            }
        }
        else if (BusterFlag == false) reSpeed();
    }
    void ReduceSize()
    {
        ReduceTime += Time.deltaTime;
        if (ReduceTime > ReduceWTime)
        {
            if (transform.localScale.x < 0)
                transform.localScale = new Vector3(transform.localScale.x + 0.003f, transform.lossyScale.y - 0.003f, 1);
            else transform.localScale = new Vector3(transform.localScale.x - 0.003f, transform.lossyScale.y - 0.003f, 1);
            SizeDownKnife();
            ReduceTime = 0;
        }
        if (transform.localScale.y < 1) transform.localScale = (transform.localScale / transform.localScale.y);
    }
    public void SizeDownKnife()
    {
        SeparationKnife();
        if (MyKnife.transform.localScale.x < 0) MyKnife.transform.localScale = new Vector3(MyKnife.transform.localScale.x + 0.003f, MyKnife.transform.localScale.y - 0.03f, 1);
        else MyKnife.transform.localScale = new Vector3(MyKnife.transform.localScale.x - 0.003f, MyKnife.transform.localScale.y - 0.003f, 1);
        CombinationKnife();
        CheckMaxKnife();
    }//Įũ�� Ű���
    void ChangeKnife()//�ӽ� �׽�Ʈ��
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetRandomKnife();
        }
    }
    void ChangeBody()//�ӽ� �׽�Ʈ��
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetRandomBody();
        }
    }

    void TestSkill()//�ӽ� �׽�Ʈ�� J
    {
        if (Input.GetKeyDown(KeyCode.A))
            PlaySkill();
            // SkillBtn.GetComponent<SkillBtn>().UseSkill(); 

    }

    void TestItem()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            ItemBtn.GetComponent<ItemBtn>().ChangeImage(1);
        if (Input.GetKeyDown(KeyCode.X))
            ItemBtn.GetComponent<ItemBtn>().ChangeImage(2);
        if (Input.GetKeyDown(KeyCode.C))
            ItemBtn.GetComponent<ItemBtn>().ChangeImage(3);
        if (Input.GetKeyDown(KeyCode.S))
            ItemBtn.GetComponent<ItemBtn>().UseItem();
    }

    private void GetPlayer_BusterInput()// �ν��� ����
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bubbleSound = Instantiate(BubbleSound, transform.position, Quaternion.Euler(0, 0, 0));
            bubbleSound.transform.parent = transform;
            chSpeed();
            BusterFlag = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            chSpeed();

            BusterFlag = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Destroy(GameObject.FindWithTag("BS"));
            reSpeed();
            BusterFlag = false;
        }
    }
    public void TempReSpped()
    {
        MovementSpeed = TempMovementSp + transform.localScale.y / 2; // J
        BusterSpeed = TempBusterSp + transform.localScale.y / 2; // J
        RotationSpeed = TempRotateSp + transform.localScale.y / 2; // J
    }
    public void RecuveryBusterGage()//�ν���ȸ��
    {
        timer_ += Time.deltaTime;
        if (timer_ > waitingTime_ && cutGauge < maxGauge)
        {
            if (!isMove)
                cutGauge += 1f;
            else if (isMove)
                cutGauge += 0.9f;
            timer_ = 0;
            transform.localScale = new Vector3(transform.localScale.x * 0.9992f, transform.localScale.y * 0.9992f, 1);
        }
    }

    public void Handlebar(float aa)//Ÿ��ũ��Ʈ������ ����ϱ� ������ �������̵���. �ν��� ������ �ٿ��� �÷Ǵ� ����
    {
        cutGauge += aa;

        if (cutGauge > 100.0) cutGauge = maxGauge;

        if (cutGauge <= 0)
        {
            cutGauge = 0;
        }
        BusterBar.value = Mathf.Lerp(BusterBar.value, (float)cutGauge / (float)maxGauge, Time.deltaTime * 10);//Mathf.Lerp �˻�! �ν��Ͱ����� �����ڵ�.
    }

    public void Handlebar() //�ν��� ������ �ٿ��� �÷Ǵ� ����
    {
        BusterBar.value = Mathf.Lerp(BusterBar.value, (float)cutGauge / (float)maxGauge, Time.deltaTime * 10);
    }

    public void ShowBoard()//ų�� �ð����̱�
    {
        KillBoard.text = killScore.ToString();
        TIMEBoard.text = ((int)globalTime).ToString();
    }
    public void EraserPlayer()//�÷��̾� ����� �����Ⱦ���
    {
        Destroy(gameObject);
    }
   
   
    public override void KillScoreUp()
    {
        Debug.Log(transform.tag + "�׿���");
        Kombo.GetComponent<KomboKillImage>().Init_Img(CountKill);
        ++killScore;
        killcheck = true;
        //SizeUpKnife();
        CountTime = 0;
        KCFlag = true;
        RaiseFlag = true;
        Handlebar(10f);
    }//ų�ϸ� ����Ǵ��Լ�

    public void KomboKillSounds()
    {

        var Sound1 = Instantiate(KillSound, transform.localPosition, Quaternion.Euler(0f, 0f, 0f));
        Sound1.transform.parent = transform.parent;
        Sound1.transform.localPosition = Vector3.zero;
        if (Life)
        {
            if (CountKill == 2)
            {
                var Sound2 = Instantiate(TwoKillSound, transform.localPosition, Quaternion.Euler(0f, 0f, 0f));

            }
            else if (CountKill == 3)
            {
                var Sound3 = Instantiate(ThreeKillSound, transform.localPosition, Quaternion.Euler(0f, 0f, 0f));

            }
            else if (CountKill == 4)
            {
                var Sound4 = Instantiate(FourKillSound, transform.localPosition, Quaternion.Euler(0f, 0f, 0f));

            }
            else if (CountKill == 5)

            {
                var Sound5 = Instantiate(FiveKillSound, transform.localPosition, Quaternion.Euler(0f, 0f, 0f));

            }
            else if (CountKill == 6)

            {
                var Sound5 = Instantiate(FiveKillSound, transform.localPosition, Quaternion.Euler(0f, 0f, 0f));
                CountKill = 0;
            }


        }
        if (!Life)
        {
            //var a = 1;
        }

    }//�ϴ� �̱���� ����

    public void EatItem(int i)
    {
        ItemBtn.GetComponent<ItemBtn>().ChangeImage(i);
    }

    public void StopMove()  //�̵�Ʃ�丮�󿡼� ���
    {
        value.joyTouch = Vector3.zero;
        transform.rotation = Quaternion.Euler(0f, 0f,  0f);
        transform.localScale = new Vector3(1, 1, 1);
    }
}

