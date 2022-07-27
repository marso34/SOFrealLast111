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
    public void Start()//�Ϲ����� ��ŸƮ (�ڷ�ƾ) �ݺ�����.)
    {
        
        GM = GameObject.FindGameObjectWithTag("GM");
        Flag_get = false;
        AiPlayers_ = new GameObject[9];
        S = Skin.transform.GetComponent<SpriteRenderer>();

        skin_ = Skin.GetComponent<Skin>();// ��Ų������Ʈ ����
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

        firstMoveFlag = true;
        if (transform.tag == "Attacker") waitingTime = 0.1f;
        StartCoroutine("Start_");
        MinFar = new Vector3(13f, 5f, 1f);
        ViewFlag = false;

    }
    void SetBuster()//�ν��� �÷��� ������ �ν���Ű��.
    {
        if (BusterFlag) chSpeed();
        else reSpeed();
    }
    private void Update()
    {
        transform.position = MyBody.transform.position;
        reset_();
        // *************************** Ư�̻��� ******* ���϶����� �ִ�����Ǵ¹��� �ֱ�**********        
        //SetIndicator();
        if (transform.tag == "InkOct") FishNumber = 7;
        AiPlayers_ = GameObject.FindGameObjectsWithTag("AiPlayer");// ����÷��̾� ��ƾ���
        Player = GameObject.FindGameObjectWithTag("Player");

        if (StartFlag == true && Player != null)
        {
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
                if (transform.tag == "InkOct")
                    Init_();
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
            }
            else if (!Life)
            {
                dir = Vector3.zero;
            }
            AnimState(dir);
            CheckWall();
            //CheckMaxSize();
            Check_Flag();
            //MyKnife.transform.localScale = new Vector3(0.1f, 2f, 1f);
            //MyKnife.transform.localPosition = new Vector3(0, 0.2f, 0f);
        }
        //�� ������ �������

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
            //reSpeed();
            CreateFlesh();
            Destroy(gameObject, 2f);
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
        if (transform.tag == "InkOct")
        {
            int Dice2 = Random.Range(0, 3);
            if (Dice == 0)
                return RightMove();
            // else return MinDirTracking();
            else if (Dice == 1)
                return PlayerTracking();
            else return LeftMove();
        }

        else return Vector3.zero;

    }// �̵����Ⱚ �����ϰ� �̴��Լ�

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
    public override void CheckWall()
    {

        RaycastHit2D ray2 = Physics2D.Raycast(transform.position, (Vector3.zero - transform.position).normalized, 1000f, LayerMask.GetMask("Wall"));
        if (ray2.collider != null)
        {
            transform.position = ray2.point;
        }

    }//�ʹ����� ���������ϴ��Լ�

}