using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//flesh�κ��� ���������� ũ��Ű����Լ��� ����ɼ������� ���ǹٶ�.
public class AiPlayerScript : Player
{
    bool flag;
    double timer; // �̵� �̱⿡ ����
    float waitingTime; // �̵� �̱⿡ ����

    GameObject Player;//���
    public GameObject Target;// player 
    public GameObject Indicator;//�����
    Vector3 TargetDirection;//me position - player position


    //�ν��� �÷���
    bool backFlag;
    GameObject[] AiPlayers_;
    bool firstMoveFlag;
    Vector3 MinFar;
    bool ViewFlag;

    float SkillTimer;

    public void Start()//�Ϲ����� ��ŸƮ (�ڷ�ƾ) �ݺ�����.)
    {
        QM = GameObject.FindGameObjectWithTag("QM");
        GM = GameObject.FindGameObjectWithTag("GM");
        Flag_get = false;
        AiPlayers_ = new GameObject[9];


        skin_ = Skin.GetComponent<Skin>();// ��Ų������Ʈ ����
        S = Skin.transform.GetComponent<SpriteRenderer>();
        RotationSpeed = 720f;
        Life = true;// ������ ��
        timer = 3;
        waitingTime = 3f;
        killScore = 0;
        MovementSpeed = 2.3f + transform.localScale.y / 2;//3.8
        BusterSpeed = 4.6f + transform.localScale.y / 2;// �ν��� �ӵ� //10      
        Speed = MovementSpeed;// ���ǵ� ������ �⺻���ǵ�� �ٽ� �ʱ�ȭ    


        C.a = 1f;
        C.b = 1f;
        C.r = 1f;
        C.g = 1f;
        SetRandomBody();
        SetRandomKnife();
        GameWaitInit();
        RB = MyBody.transform.GetComponent<Rigidbody2D>();
        SkillTimer = 0f;
         isMove = true;
        firstMoveFlag = true;
        if (transform.tag == "Attacker") waitingTime = 0.1f;
        StartCoroutine("Start_");
        MinFar = new Vector3(13f, 5f, 1f);
        ViewFlag = false;
        
    }
    void SetBuster()//�ν��� �÷��� ������ �ν���Ű��.
    {
        if (BusterFlag) FastSpeed(1);
        //else DefaultMoveSpeed();
    }
    private void Update()
    {
        transform.position = MyBody.transform.position;
        reset_();
        // *************************** Ư�̻��� ******* ���϶����� �ִ�����Ǵ¹��� �ֱ�**********        
        //SetIndicator();
        
        AiPlayers_ = GameObject.FindGameObjectsWithTag("AiPlayer");// ����÷��̾� ��ƾ���
        Player = GameObject.FindGameObjectWithTag("Player");

        if (StartFlag == true && Player != null)
        {
            if (transform.tag == "InkOct") FishNumber = 7;
            GameStartInit();
            Target = Player;
            Init_();
        }

        else if (Target != null)
        {
            if (firstMoveFlag)
            {
                firstMoveFlag = false;
            }
            if (Life)
            {
                
                var T = (Player.transform.position - transform.position);
                if (Mathf.Abs(MinFar.magnitude) > Mathf.Abs(T.magnitude)) ViewFlag = true;
                else ViewFlag = false;
                if (ViewFlag)
                {
                    timer += Time.deltaTime;
                    if (timer > waitingTime && !backFlag)
                    {
                        dir = Move_().normalized;
                        timer = 0;
                        waitingTime = Random.Range(0f, 1.5f);
                        if (transform.tag == "Attacker") waitingTime = 0.1f;
                    }
                }
                else
                {
                    dir = PlayerTracking().normalized;
                    BusterFlag = false;
                }
                PlayerMove();//������ �� State�ʱ�ȭ         
                SetBuster();
                BubbleP.gameObject.GetComponent<BubleParticle>().Speed = Speed;

                SkillTimer += Time.deltaTime;

                if (SkillTimer > 4f)
                {
                    PlaySkill();
                    SkillTimer = 0f;
                }
                DieCheck();
            }
            else if (!Life)
            {
                dir = Vector3.zero;
                transform.Find("Bubble Particle").gameObject.SetActive(false);
            }
            AnimState(dir);
       
            //CheckMaxSize();
            Check_Flag();
            //MyKnife.transform.localScale = new Vector3(0.1f, 2f, 1f);
            //MyKnife.transform.localPosition = new Vector3(0, 0.2f, 0f);
        }
        //�� ������ �������

    }
    public void DieCheck(){
        if(HP <0 && Life){

            DieLife();
            Life = false;
        }
    }
    public override void DieLife()
    {
        Speed = 0f;   // ���߿� ���� �ʿ�. 
        PlayerMove(); // RigidBody2D�� velocity�� �ѹ��� �����ص� �� �ӵ���� ��� ������
        if (transform.tag == "InkOct")
        {
            if (GameObject.FindWithTag("Kraken") != null)
                GameObject.FindWithTag("Kraken").GetComponent<Kraken>().CreateInkSwarm(transform.position, 0.4f);
            Destroy(gameObject);
        }
        else
        {
            OnOutLine(14);
            Invoke("OffOutLine", 0.07f);
            state = State.Die;
            LifeOff();

            QM = GameObject.FindGameObjectWithTag("QM");
            QM.GetComponent<QuestManager>().KnifeEC--;
            if (SkillFlag)
                OffSkillFlag(); // J
            InitState(); // J
            NotInit();
            //DefaultMoveSpeed();
            CreateFlesh();
            Stage22_ex();
            
            MyKnife.transform.parent = null;
            MyKnife.transform.localScale = new Vector3(0.01f,0.01f,0.01f);
            MKnife.transform.parent = transform;
        }
    }
    void StartInit()//���۽� ����
    {
        GameStartInit();
    }  //���۽� ����
    public void SetTargeting()
    {
        if (Player != null) Target = Player;
    }
    public void SetIndicator()
    {
        Vector2 direction = Target.transform.position - transform.position;

        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, 1000f, LayerMask.GetMask("CamBoxLayer"));

        if (ray.collider != null)
        {

            Indicator.transform.position = ray.point;

        }
        else Indicator.SetActive(true);
    }//����� �÷��̾�� �ֱ� ���������ȵ�
    public void EraserPlayer()
    {
        Destroy(gameObject);
    }//�÷��̾���� �����Ⱦ���
    Vector3 Move_()
    {
        float y = transform.localPosition.y;
        float x = transform.localPosition.x;

        if (x < 0)
            x *= -1f;
        if (y < 0)
            y *= -1f;

        if (x > 25f || y > 12f)
            return PlayerTracking();

        int Dice = Random.Range(0, 5);
        if (transform.tag == "AiPlayer")
        {
            if (QM.GetComponent<QuestManager>().Level_ == 2 && QM.GetComponent<QuestManager>().IngameLevel == 2)
            {
                if (Random.Range(0, 7) == 1) BusterFlag = true;
                else BusterFlag = false;
                if (Dice == 0) return VictemTracking();
                else if (Dice == 1) return LeftMove();
                else if (Dice == 2) return RightMove();
                else return FleshTracking();
            }
            else
            {
                if (Random.Range(0, 7) == 1) BusterFlag = true;
                else BusterFlag = false;
                if (Dice == 0) return FleshTracking();
                else if (Dice == 1) return LeftMove();
                else if (Dice == 2)
                    return RightMove();
                // else return MinDirTracking();
                else if (Dice == 3)
                    return FleshTracking();
                else return LeftMove();
            }
        }

        if (transform.tag == "InkOct")
        {
            Debug.Log("�����̵�");
            if (Dice == 0)
                return RightMove();
            // else return MinDirTracking();
            else if (Dice == 1)
                 return LeftMove();
            else return PlayerTracking();
            
        }

        else return Vector3.zero;

    }// �̵����Ⱚ �����ϰ� �̴��Լ�
    Vector3 VictemTracking()
    {
        GameObject vi;
        if (GameObject.FindGameObjectWithTag("Victem") != null)
        {
            vi = GameObject.FindGameObjectWithTag("Victem");
            return vi.transform.position - transform.position;
        }
        else
            return Vector3.zero - transform.position;
    }
    Vector3 MinDirTracking()
    {

        Vector3 min = Player.transform.position - transform.position;
        Vector3 TrDir;
        for (int i = 0; i < PlayerCount - 1; i++)
        {
            if (AiPlayers_[i] != transform.gameObject)
            {
                TrDir = AiPlayers_[i].transform.position - transform.position;
                if (min.magnitude > TrDir.magnitude) min = TrDir;
            }
        }

        BusterFlag = false;
        return min;
    }//���尡����� ���� ��ȯ
    Vector3 EnemyTrackingMove() //�������� ���󰡱� �������� �����ȯ
    {
        BusterFlag = false;
        int R = Random.Range(0, PlayerCount - 1);
        return AiPlayers_[R].transform.position - transform.position;
    }
    Vector3 PlayerTracking()
    {
       
            return Player.transform.position - transform.position;
    }// ���� ���󰡱� �������� ��ȯ
    Vector3 LeftMove()
    {

        float DirY = Random.Range(-0.9f, 0.9f);
        Vector3 Left_ = new Vector3(-1, DirY, 0);


        return Left_;


    }// ���ʹ����� �������Ʒ� ����
    Vector3 RightMove()//�������� �������Ʒ� ����
    {

        float DirY = Random.Range(-0.9f, 0.9f);
        Vector3 Right_ = new Vector3(1, DirY, 0);

        return Right_;


    }
    Vector3 FleshTracking()
    {
        BusterFlag = true;
        if (GameObject.FindGameObjectWithTag("Flesh") != null)
            return GameObject.FindGameObjectWithTag("Flesh").transform.position;
        //else return EnemyTrackingMove();
        else return PlayerTracking();

    }//��ü ����
 

}