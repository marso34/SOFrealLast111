using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Player : MonoBehaviour
{
    public int Score;
    public bool UseItem_;
    public float FRZTimer;
    public float FRZWatime;
    public GameObject DamageText;
    bool Flag_ = false;
    public Rigidbody2D RB;
    public int AnimFlame = 10; // ?��?�� ?��?���? ?��?�� ?��?��?��
    public int DieAnimFlame = 10; // 죽음 ?��?�� ?��?��리시?�� ?��?��?��
    public GameObject Barriar;
    public ParticleSystem Skill; // J
    public GameObject Skill2; // J
    public ParticleSystem BubbleP; // J
    public bool isMove = false; //???직임?��?��
    public enum State { Idle, Move, Die, };//?��?��?�� 집합
    public State state;// ?��?��?��?��
    public int PlayerCount = 8;
    public bool BSErrorFlag;
    public float Speed;//�??��?�� ?��?��?���? ?��?�� �??��
    public float RotationSpeed;//?��?��?��?��
    public float MovementSpeed;//기본 ?��?��?�� ?��?��
    public float BusterSpeed;
    public bool BusterFlag;
    public bool ErrorFlag; // ����� �÷��� ���� ����

    public float TempMovementSp; // J
    public float TempBusterSp; // J
    public float TempRotateSp; // J
    public bool StateMoveFlag_;
    public bool StateRotateFlag_;

    public bool XFlag;
    public bool YFlag;
    float MaxSize = 2f;
    public int fleshCount = 0;
    public SpriteRenderer MFish;// ?�� 물고�? ?��?�� ?��?�� 객체?��?�� 초기?��
    public SpriteRenderer MKnife;//?�� �? ?��?��// ?��?��?��?�� 초기?��.
    public GameObject MyKnife;
    public GameObject MyBody;
    //LOBBYPLAYER?��?�� 초기?��?��
    public int FishNumber;//0~N GameManager ?��?�� ?��?�� ?�� ?�� 초기?�� ?���?
    public int KnifeNumber;//0~N GameManager ?��?�� ?��?�� ?�� ?�� 초기?�� ?���?


    public GameObject Skin;// ?��?�� ?���? ?��브젝?��
    public Skin skin_;//?��?��
    public GameObject Flesh;//?���? 참조
    public float chsize = 0.05f;


    public GameObject Bubble;// 버블 객체  

    public bool StartFlag;// 게임 ?��?��?�� ?��리는 ?��?���?.

    public bool Life = true;//?��?�� ?��?���? ?��?��?��?�� ?��&죽음 ?��?��?��기위?���??�� ?��?��?��?�� life?�� ?��?��?�� 못바꾸기?���?. ?��?�� ?��?��?��계때�? ?��?��기코?��?��?��? 

    public int killScore;//?��?��코어

    public bool endFlag;//게임 ?��?��?���??��

    public bool SkillFlag; // 

    public GameObject GM;// 게임매니?��
    public Color C;//캐릭?�� ?��명도�?경할?��?���??�� (죽을?��)
    public Sprite[] KnifeAnims;//칼애?��
    public Sprite[] BodyAnims;//몸애?��
    public SpriteRenderer S;//몸투명도 바�???�� ?��?���??��

    public GameObject BubbleSound;
    public Vector3 dir;//???직�
                       // Start is called before the first frame update
    public GameObject Flag_Image;
    public bool Flag_get;
    public int HP = 5;
    public bool hitFlag;
    public bool flagerror = true;
    public GameObject KillSound;
    public GameObject PlayerHitSound;
    public GameObject QM;

    //--------//Ʃ�丮�󿡼� ���
    public bool skillcheck = false;
    //public bool TutorialLev4 = false;

    public int Timer33 = 0;
    public double Timer22 = 0;
    public float MoveTime = 3f;
    public bool TuLev1 = false;
    public bool SlowFlag;
    public Vector2 VWall;
    public bool SharkFlag;

    public bool FRZFlag;

    public void GameStartInit()// 게임?��?��?�� ?��번실?��
    {
        Init_();
        StartFlag = false;
        InitTemp();
        //DefaultMoveSpeed();
        DefaultRotateSpeed();
        XFlag = false;
        YFlag = false;
        VWall = Vector2.zero;
    }
    public void StopTime_()
    {
        Time.timeScale = 0.01f;
    }
    public void StartTime_()
    {
        Time.timeScale = 1;
        //transform.parent.GetComponent<Player>().HP--;
    }
    public void GameWaitInit()//?��?��?��?��?��?��?�� 기다릴때
    {
        MFish = Skin.GetComponent<SpriteRenderer>();//?��?��?�� SpriteRenderer 참조
        MKnife = MyKnife.GetComponent<SpriteRenderer>();//칼의 SpriteRenderer
        BSErrorFlag = true;

    }//�������? �÷��̾� �� ���̱������� ���� �����̰� �ʱ�ȭ    

    public IEnumerator Start_()//?��반적?�� ?��????�� (코루?��) 반복문임.)
    {
        while (true) yield return StartCoroutine(state.ToString());//코루?�� ?��?�� 매프?��?��마다. 코루?�� ?��?��?�� �??��?��?�� ?��?��보기
    }
    public void Init_()//초기?��
    {
        MyKnife.tag = "Knife";
        MyBody.tag = "Body";
    }

    public void InitTemp()
    {
        RotationSpeed = 1200f;
        TempMovementSp = 2.3f; //J
        TempBusterSp = 4.6f;     // J
        TempRotateSp = 1200f;   // J
    }

    public void FRZOn()
    {
        FRZTimer = 0;
        FRZFlag = true;
    }

    public void FRZOff()
    {
        FRZFlag = false;
        C = Color.white;
        S.color = C;
    }

    // public void DefaultMoveSpeed()
    // {
    //     if (StateMoveFlag_ == false)
    //     {
    //         if (GameObject.FindGameObjectWithTag("BS") != null) ;
    //         Destroy(GameObject.FindGameObjectWithTag("BS"));
    //         // if (transform.tag == "Player")
    //         //     Debug.Log("���� �ӵ�");
    //         
    //         StateMoveFlag_ = false;
    //         DefaultRotateSpeed();
    //         BSErrorFlag = true;
    //     }
    //     // 
    // }

    public void DefaultRotateSpeed()
    {
        if (StateRotateFlag_ == false)
        {
            RotationSpeed = 1200f;
            // Debug.Log("���� ȸ��");
        }
    }

    public void StopMoveSpeed()
    {
        if (StateMoveFlag_ == false)
        {
            Speed = 0f;
            MovementSpeed = 0f;
            BusterSpeed = 0f;
            StateMoveFlag_ = true;
            Debug.Log("����");
            Invoke("InitState", 2.5f);
            Invoke("DefaultRotateSpeed", 2.5f);
        }
    }


    public void StopRotateSpeed()
    {
        if (StateRotateFlag_ == false)
        {
            RotationSpeed = 0.0001f;
            StateRotateFlag_ = true;
        }
    }


    public void FastSpeed(float index)// 물고�? ?��?��?��?�� �??��?��?�� ?��기화.
    {
        if (!SharkFlag && transform.tag == "Player")
        {
            var b = Instantiate(BubbleSound, transform.position, Quaternion.Euler(0, 0, 0));
            b.tag = "BS";
        }
        BusterFlag = true;
    }

    public void OffFastSpeed()
    {
        if (GameObject.FindGameObjectWithTag("BS"))
            Destroy(GameObject.FindGameObjectWithTag("BS"));
        BusterFlag = false;
    }
    public void Sharkmove()
    {
        var b = Instantiate(BubbleSound, transform.position, Quaternion.Euler(0, 0, 0));
        b.tag = "BS";
        SharkFlag = true;
        Invoke("SharkOff", 3f);
    }
    public void SharkOff()
    {

        if (GameObject.FindGameObjectWithTag("BS"))
            Destroy(GameObject.FindGameObjectWithTag("BS"));
        SharkFlag = false;
    }
    public void SlowMoveSpeed(float index)
    {
        // if (StateMoveFlag_ == false)
        // {
        //     MovementSpeed = index;
        //     Speed = index;
        //     Debug.Log(Speed + "slowMove");
        //     BusterSpeed = index * 2;
        //     StateMoveFlag_ = true;
        //     ErrorFlag = false;
        //     SetColor(Color.green);
        //     Invoke("InitState", 2.5f);

        // }
        SetColor(Color.green);
        SlowFlag = true;
        Invoke("OffSlow", 2f);
    }
    public void OffSlow()
    {
        SlowFlag = false;
        InitState();
    }
    // public void SlowRotateSpeed(float index)
    // {
    //     // if (StateRotateFlag_ == false)
    //     // {
    //     //     RotationSpeed = index * 300;
    //     //     Debug.Log(RotationSpeed + "slowRotate");
    //     //     StateRotateFlag_ = true;
    //     // }

    // }

    public void InitState() //J 초기?��
    {
        if (Life)
        {
            C = Color.white;
            // StateMoveFlag_ = false;
            // StateRotateFlag_ = false;
            //    DefaultMoveSpeed();
            //  DefaultRotateSpeed();

        }
    }
    public void NotInit()
    {
        MyKnife.tag = "NotKnife";
        MyBody.tag = "NotBody";

    }//not?���? 초기?��
    public void InitBody__()
    {
        if (Life)
        {
            MyBody.tag = "Body";
            MyKnife.tag = "Knife";
            state = State.Move;
            hitFlag = false;
        }
    }
    public void LifeOff()
    {
        Life = false;
    }
    public virtual void DieLife()// 죽었?��?��,?��?��기본?��?���?,?���? 죽음?���?, ?��?��?�� 죽음?���?, 컬러리셋,?��체생?��,2초뒤�??��
    {

    }
    public void Glitter()
    {
        translucence();
        Invoke("ResetColor", 0.2f);
        Invoke("translucence", 0.3f);
        Invoke("ResetColor", 0.4f);
        Invoke("translucence", 0.5f);

    }
    public void WhiteFlesh()
    {
        OnOutLine(14);
        Invoke("OffOutLine", 0.07f);
    }
    public void Check_Flag()
    {
        if (!Flag_get) Flag_Image.GetComponent<Image>().color = Color.clear;
        else if (Flag_get) Flag_Image.GetComponent<Image>().color = Color.white;
    }
    public void CreateFlesh()//?��체생?��
    {
        for (int i = 0; i < 3 + transform.localScale.y; ++i)
        {
            var flesh_ = Instantiate(Flesh, transform.position + RandomFleshPosition(), Quaternion.Euler(0, 0, 0));
        }
    }//?���? 만들�?

    public Vector3 RandomFleshPosition() //?��?��?�� ?��체위치백?�� 반환
    {
        return new Vector3(Random.Range(-1.2f, 1.2f), Random.Range(-1.2f, 1.2f), 0);
    }

    public Vector3 RandomPosition(bool AiFlag) //?��?��?�� ?��치백?�� 반환
    {
        Vector3 relate = Vector3.zero;
        if (AiFlag)
        {
            relate = GameObject.FindWithTag("Player").transform.position;
        }
        return new Vector3(Random.Range(-20, 20), Random.Range(-10, 10), 0);
    }
    public bool jugeAi()
    {
        if (transform.tag == "AiPlayer")
            return true;
        else return false;
    }
    // public void Respone()//�??��과정
    // {
    //     if (transform.tag == "AiPlayer")
    //     {
    //         ResetColor();
    //         Vector3 postion_ = RandomPosition(jugeAi());
    //         transform.Translate(postion_, Space.World);//?��?��?��?��치에 ?��?��    
    //         Life = true;
    //         SetRandomBody();
    //         SetRandomKnife();
    //         //sizeInit();
    //     }
    // }

    public void SetRandomBody()//?��로운 바디 ?��?��?��?��?���?
    {
        if (transform.tag == "AiPlayer")
        {
            int R = Random.Range(5, 6);// 몸스?���??��5

            if (QM.GetComponent<QuestManager>().Level_ == 2 && QM.GetComponent<QuestManager>().IngameLevel == 3)
                FishNumber = 9;
            else
                FishNumber = 5;
        }

    }

    public void SetRandomKnife()// ?��로운 �? ?��?��?��?��?���?
    {
        int R = Random.Range(1, 6);//칼스?���??��6
        KnifeNumber = R;
    }

    public void InitKnife()// 물고�? ?��?��그�?�기반으�? ?��?��?�� 초기?��
    {
        KnifeAnims = new Sprite[10];
        if (KnifeNumber == 0) KnifeAnims = skin_.BasicKnife;
        else if (KnifeNumber == 1) KnifeAnims = skin_.SpearKnife;
        else if (KnifeNumber == 2) KnifeAnims = skin_.PanKnife_R;
        else if (KnifeNumber == 3) KnifeAnims = skin_.Rager_R;
        else if (KnifeNumber == 4) KnifeAnims = skin_.XKnife;
        else if (KnifeNumber == 5) KnifeAnims = skin_.CandyKnife;
    }

    public void InitBody()//�? ?��?���? 기반?���? ?��?��?�� 초기?��
    {
        BodyAnims = new Sprite[10];
        if (FishNumber == 0) BodyAnims = skin_.FirstTailAnims;
        else if (FishNumber == 1) BodyAnims = skin_.SharkTailAnims;
        else if (FishNumber == 2) BodyAnims = skin_.BlowfishTailAnims;
        else if (FishNumber == 3) BodyAnims = skin_.OctopusTailAnims;
        else if (FishNumber == 4) BodyAnims = skin_.WaileTailAnims_R;
        else if (FishNumber == 5) BodyAnims = skin_.BornAnims_E;
        else if (FishNumber == 6) BodyAnims = skin_.Gabock_E;
        else if (FishNumber == 7) BodyAnims = skin_.InkOctAnims_E;
        else if (FishNumber == 8) BodyAnims = skin_.Granpa_V;
        else if (FishNumber == 9) BodyAnims = skin_.PupleAnims_E;
    }
    public void InitDieBody()
    {
        if (FishNumber == 0) MFish.sprite = skin_.DieAnims[0];
        else if (FishNumber == 1) MFish.sprite = skin_.DieAnims[1];
        else if (FishNumber == 2) MFish.sprite = skin_.DieAnims[2];
        else if (FishNumber == 3) MFish.sprite = skin_.DieAnims[3];
        else if (FishNumber == 4) MFish.sprite = skin_.DieAnims[4];
        else if (FishNumber == 5) MFish.sprite = skin_.DieAnims[5];
        else if (FishNumber == 6) MFish.sprite = skin_.DieAnims[6];
        else if (FishNumber == 9) MFish.sprite = skin_.DieAnims[6];
    }
    public void ResetColor()//?��명도 0?���? �? ?��명하�??���?
    {
        C.a = 1f;
        S.color = C;
        MKnife.color = C;
    }
    public void SetColor(Color c)
    {
        S.color = c;
        MKnife.color = C;
        C = c;
    }
    public void translucence() // J 반투명하�?
    {
        C.a = 0.5f;
        S.color = C;
        MKnife.color = C;
    }
    public void sizeInit()//기본?��?��즈로 바꾸�?
    {
        Sizech(transform.localScale / transform.localScale.y);
    }

    public void KnifeInit()//기본?��?��즈로 바꾸�?
    {
        MyKnife.transform.parent = null;//최�???���? �??�� ?��?��?��.,
        if (MyKnife.transform.localScale.x < 0) MyKnife.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else MyKnife.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        MyKnife.transform.parent = transform;
    }

    public void AnimState(Vector3 dir)//�? ?��?��메이?�� ?��?��, 칼애?��매이?�� ?��?��, ?��?�� ?��?��?��?��?��?��?�� ?���? 못구?��
    {
        if (Life)
        {
            if (isMove)
            {

                if (!hitFlag)
                    state = State.Move;
                float x_ = transform.localScale.x;// x_?�� ?��?��?��?��?��브젝?�� scale.x �? ?��?��. scale.x�? ?��?��?��?�� ?��?��?��?��?�� 좌우반전?���? ?��?��?��?��. ?��를이?��?��?�� ?��쪽으�? 많이 ?��?��?�� ?��집어�? 모양?�� ?��?��?���? ?��.
                if (x_ < 0)
                    x_ *= -1;
                if (transform.rotation.normalized.w * transform.rotation.normalized.z < 0)
                {


                    transform.localScale = new Vector3(x_, transform.localScale.y, 1);
                }
                else if (transform.rotation.normalized.w * transform.rotation.normalized.z > 0)
                {


                    transform.localScale = new Vector3(x_ * -1, transform.localScale.y, 1);
                }
            }
            else if (!isMove)
            {
                if (!hitFlag)
                    state = State.Idle;
            }
        }
        else if (!Life)
        {
            state = State.Die;

        }
    }// 조건?�� ?��?�� ?��?��메이?�� ?��?�� ?��?���?.
    IEnumerator Idle()//멈춤?��?��0.2초마?�� shoAnim?��?�� ?��?��
    {
        ResetColor();
        ShowBodyAnim(0);
        ShowKnifeAnim(0);
        yield return new WaitForSeconds(0.2f);
    }
    IEnumerator Move()//???직임?��?��매이?��?��?��
    {
        ResetColor();
        for (int i = 0; i < AnimFlame; ++i)
        {
            if (!Life || state != State.Move || Speed == 0) break;
            ShowBodyAnim(i);
            ShowKnifeAnim(i);
            for (int j = 0; j < 2; ++j)
            {
                if (Speed == 0) break;
                // CreateBubbles();//버블?��?��

                yield return new WaitForSeconds(0.2f / Speed);

            }
        }
    }
    IEnumerator Die() //죽음 ?��?��
    {
        for (int i = 0; i < 50; ++i)
        {

            if (i == 49 && !Life && transform.tag != "Player")
            {
                if (gameObject.name == "Boss") transform.GetComponent<AttackerScript>().Win();
                Destroy(gameObject);
            }
            //if (Life) break;
            ShowDieAnim(i);

            yield return new WaitForSeconds(0.01f);
        }
    }
    public void ShowDieAnim(int index)//죽었?��?�� ?��?��매이?�� ?��?��?��?��
    {
        InitDieBody();

        if (S.color.a > 0 && !Life)
        {
            for (int i = 0; i < 50; i++)
            {
                if (i == index)
                {
                    C.a -= 0.02f;
                    S.color = C;
                    MKnife.color = C;
                }
            }
        }
    }

    public void ShowBodyAnim(int index)//?��?��?��?��?�� ?��?��매이?�� ?��?��?��?��
    {
        InitBody();
        for (int i = 0; i < AnimFlame; i++)
        {
            if (!Life || state == State.Die) break;
            if (i == index)
                MFish.sprite = BodyAnims[i];
        }
    }
    public void ShowKnifeAnim(int index)//?��?��?��?��?�� ?��?��매이?�� ?��?��?��?��
    {
        InitKnife();
        for (int i = 0; i < AnimFlame; i++)
        {
            if (i == index)
                MKnife.sprite = KnifeAnims[i];
        }

    }
    public void rota()
    {
        if (FRZFlag) RotationSpeed = 0f;
        else if (SlowFlag) RotationSpeed = 300f;
        else RotationSpeed = 1200f;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, RB.velocity.normalized);//?��?��방향?�� 맞게 ?��면을 보도�? ?��?���? 받아?���?.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);//?��?��?��?��?��브젝?��?���? 받아?�� ?��?���? ?��?��
        float x_ = transform.localScale.x;
    }
    public void PlayerMove()
    {
        Speed = TempMovementSp + transform.localScale.y / 2;
        MovementSpeed = TempMovementSp + transform.localScale.y / 2;
        BusterSpeed = TempBusterSp + transform.localScale.y / 2;
        isMove = true; //dir != Vector3.zero;

        if ((transform.tag == "InkOct"||transform.tag == "AiPlayer" ||(transform.tag =="Player" && MyBody.GetComponent<HitFillBody>().SlowFlag_ == false)) && isMove && Life)
        {
            if (isMove)
            {
                Timer22 += Time.deltaTime;
                if (Timer22 > MoveTime)
                {
                    Timer22 = 0;
                    Timer33++;
                }
            }

            if (SharkFlag)
            {
                Speed = BusterSpeed * 3f;
                if (BusterFlag) Speed = BusterSpeed * 4f;
            }
            else if (SlowFlag) Speed = 1f;
            else if (BusterFlag) Speed = BusterSpeed;
            else Speed = MovementSpeed;


            {
                RB.velocity = dir * Speed * Time.deltaTime * 60f;
                //if (transform.tag == "InkOct") Debug.Log("Ÿ���̵�");
                rota();
            }
        }

    }

    public void GetPlayer_tp()// ?���? 구현
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Translate(transform.up * 10f, Space.World);
        }
    }//?��미없?��코드
    public void Sizech(Vector3 v_)
    {
        transform.localScale = v_;
        CheckMaxSize();
        CheckMaxKnife();
    }// ?��?���? ?��?���? ?��?��?��?��???,그아?�� 모든 ?��브젝?��?���? ?��?���?
    public void SizeUpKnife()
    {
        SeparationKnife();
        if (MyKnife.transform.localScale.x < 0) MyKnife.transform.localScale = new Vector3(MyKnife.transform.localScale.x - 0.0005f, MyKnife.transform.localScale.y + 0.005f, 1);
        else MyKnife.transform.localScale = new Vector3(MyKnife.transform.localScale.x + 0.005f, MyKnife.transform.localScale.y + 0.005f, 1);
        CombinationKnife();
        CheckMaxKnife();
    }//칼크�? ?��?���?
    public void SeparationKnife()
    {
        MyKnife.tag = "NotKnife";
        MyKnife.transform.parent = null;//최�???���? �??�� ?��?��?��.,
    }//�? 분리 몸에????�� ?��????��?��기�???��?�� ?��????��?�� 칼의 ?��기�?�알기위?��. ?��????��기�?? 3?��?�� 커�??�? ?��?��?��.
    public void CombinationKnife()
    {
        MyKnife.transform.parent = transform;
        MyKnife.tag = "Knife";
        MyKnife.transform.localPosition = new Vector3(0f, 0.35f, 0f);
        MyKnife.transform.localRotation = Quaternion.Euler(0, 0, 90f);
    }// 칼을 ?��?��?��?���? ?��?�� ?���?
    public void CheckMaxSize()
    {
        if (transform.localScale.y > MaxSize)
        {
            float a = MaxSize, b = MaxSize;

            if (transform.localScale.x < 0) a *= -1;

            Vector3 V__ = new Vector3(a, b, 1);

            Sizech(V__);
        }
    }//최�???��기넘?��?�� 체크
    public void CheckMaxKnife()//칼크�? ?��?�� 체크
    {
        SeparationKnife();
        if (MyKnife.transform.localScale.y > MaxSize)
        {
            if (MyKnife.transform.localScale.x < 0) MyKnife.transform.localScale = new Vector3(-1f * MaxSize, MaxSize, 1f);
            else MyKnife.transform.localScale = new Vector3(MaxSize, MaxSize, 1f);
        }
        else if (MyKnife.transform.localScale.y < 1)
        {
            if (MyKnife.transform.localScale.x < 0) MyKnife.transform.localScale = new Vector3(-1f * 1, 1, 1f);
            else MyKnife.transform.localScale = new Vector3(1, 1, 1f);
        }
        CombinationKnife();
    }
    public void CreateBubbles()//?��?��?���? 만들�? 구현
    {

        int count = 0;
        if (Speed == MovementSpeed) count = 1;
        else if (Speed == BusterSpeed) count = 4;
        for (int i = 0; i < count; ++i)
        {
            float randemX = 0;
            float randemY = 0;
            if (Speed == MovementSpeed)
                randemX = Random.Range(-0.3f, 0.3f); //0.3~-0.3
            else if (Speed == BusterSpeed)
                randemX = Random.Range(-0.4f, 0.4f);

            if (Speed == MovementSpeed)
                randemY = Random.Range(-0.2f, -0.4f); //-0.2 ~-0.4
            else if (Speed == BusterSpeed)
                randemY = Random.Range(-0.2f, -0.6f);

            var V_ = new Vector3(transform.position.x, transform.position.y, 1);
            var bubble_ = Instantiate(Bubble, V_, transform.rotation);
            V_ = new Vector3(transform.position.x + randemX, transform.position.y + randemY, 1);
            bubble_.transform.parent = transform;
            bubble_.transform.localPosition = new Vector3(randemX, randemY, 1);
            bubble_.transform.parent = transform.parent;
            float randemsize = Random.Range(0.2f, 1f);

            bubble_.transform.localScale = transform.localScale * randemsize;
            float bubbleSpeed;
            if (Speed == MovementSpeed) bubbleSpeed = 0.003f;
            else
            {
                bubble_.transform.localScale = new Vector3(bubble_.transform.localScale.x, bubble_.transform.localScale.y, bubble_.transform.localScale.z);
                bubbleSpeed = 0.03f;
            }
            bubble_.GetComponent<bubble>().Speed = bubbleSpeed;
            bubble_.GetComponent<bubble>().dir = dir * -1;
        }


    } //버블만들�?
    public virtual void KillScoreUp()
    {

        //SizeUpKnife();


    }//?��?���? ?��?��?��?��?��?��
    public void CheckWall(GameObject other, bool T)//HitP�浹 �� ����
    {
    }//�ʹ�????? ����??����?????????
    public void CreatBarriar()//?��?��?��?�� 방어�? �?�?�? ?��?��?���?. 방어막만?��?�� ?��?��.
    {
        var a = Instantiate(Barriar, transform.position, Quaternion.Euler(0, 0, 0));
        a.transform.parent = transform;
        a.transform.localPosition = Vector3.zero;
        a.transform.localScale = new Vector3(1f, 1f, 1f);
        MyBody.tag = "NotBody";
    }

    public void reset_()
    {
        if (GM.GetComponent<GameManager_>().resetFlag)
        {
            if (GameObject.FindGameObjectWithTag("Bubble") != null)
                Destroy(GameObject.FindGameObjectWithTag("Bubble"));
            Destroy(gameObject);
        }
    }

    public void CreateSkill() // J ?��?�� 만드?�� ?��?��
    {
        var a = Instantiate(Skill, transform.position, Quaternion.Euler(0, 0, 0));
        a.transform.parent = transform;
        a.transform.localPosition = Vector3.zero;
        a.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void CreateSkill(string Name) // J ?��?�� 만드?�� ?��?��
    {
        var a = Instantiate(Skill, transform.position, Quaternion.Euler(0, 0, 0));
        a.transform.parent = transform;
        a.transform.localPosition = Vector3.zero;
        a.transform.localScale = new Vector3(1f, 1f, 1f);
        a.name = Name;
    }
    public void CreateSkill2() // J ?��?�� 만드?�� ?��?��
    {
        var a = Instantiate(Skill2, transform.position, Quaternion.Euler(0, 0, 0));
        a.transform.parent = transform;
        a.transform.localPosition = Vector3.zero;
        a.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void CreateSkill2(string Name) // J ?��?�� 만드?�� ?��?��
    {
        var a = Instantiate(Skill2, transform.position, Quaternion.Euler(0, 0, 0));
        a.transform.parent = transform;
        a.transform.localPosition = Vector3.zero;
        a.transform.localScale = new Vector3(1f, 1f, 1f);
        a.name = Name;
    }
    public void PlaySkill() //J
    {
        skillcheck = true;

        if (FishNumber == 1 && !StateMoveFlag_) // ?????
        {
            SkillFlag = true;
            CreateSkill();
            OnOutLine(1);
            Sharkmove();
            //StateMoveFlag_ = true;
            MyBody.tag = "Shiled";
            Invoke("InitState", 3f);
            Invoke("Init_", 3f);
            Invoke("OffSkillFlag", 3f);
            Invoke("OffOutLine", 3f);
        }
        else if (FishNumber == 2)  // ????
        {
            CreateSkill();
            for (int i = 0; i < 13 + (int)transform.localScale.y * 10; i++)
                CreateSkill2();
        }
        else if (FishNumber == 3)  //????
            CreateSkill2();
        else if (FishNumber == 4)   // ???? ???
        {
            CreateSkill();
            SkillFlag = true;
            Invoke("OffSkillFlag", 3f);
        }
        else if (FishNumber == 9) // ???????
        {
            CreateSkill2();
        }
    }
    public void PlaySkill(string Name) //J
    {
        // Flag_ = !Flag_;
        // if (FishNumber == 0)
        // {
        //     if (Flag_ == true)
        //     {
        //         StopMoveSpeed();
        //     }
        //     else if (Flag_ == false)
        //     {
        //        // DefaultMoveSpeed();
        //     }
        // }
        if (FishNumber == 1 && !StateMoveFlag_) // ?????
        {
            SkillFlag = true;

            OnOutLine(1);

            FastSpeed(3);
            StateMoveFlag_ = true;
            MyBody.tag = "Shiled";
            Invoke("InitState", 3f);
            Invoke("Init_", 3f);
            Invoke("OffSkillFlag", 3f);
            Invoke("OffOutLine", 3f);
        }
        else if (FishNumber == 2)  // ????
        {
            CreateSkill(Name);
            for (int i = 0; i < 13 + (int)transform.localScale.y * 10; i++)
                CreateSkill2(Name);
        }
        else if (FishNumber == 3)  // ????
            CreateSkill2(Name);
        else if (FishNumber == 4)  // ???? ???
        {
            CreateSkill2(Name);
            SkillFlag = true;
            Invoke("OffSkillFlag", 3f);
        }
        else if (FishNumber == 9) // ???????
        {
            CreateSkill2();
        }
    }

    public void OffSkillFlag() // J
    {
        SkillFlag = false;
    }

    public void OnOutLine(int outlineSize) // J
    {
        Skin.GetComponent<Skin>().outlineSize = outlineSize;
        Skin.GetComponent<Skin>().outline = true;
    }

    public void OffOutLine() // J
    {
        Skin.GetComponent<Skin>().outline = false;
    }
    public void Stage22_ex()
    {
        if (QM.GetComponent<QuestManager>().Level_ == 2 && (QM.GetComponent<QuestManager>().IngameLevel == 2 || QM.GetComponent<QuestManager>().IngameLevel == 3))
        {
            GameObject ST = GameObject.FindGameObjectWithTag("Stage");
            if (QM.GetComponent<QuestManager>().IngameLevel == 2)
                ST.GetComponent<Stage22>().EnemyCount--;
            else if (QM.GetComponent<QuestManager>().IngameLevel == 3)
            {
                ST.GetComponent<Stage23>().EnemyCount--;
            }
        }
    }
}