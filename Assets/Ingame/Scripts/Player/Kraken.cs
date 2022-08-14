using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : Boss
{
   
    public GameObject SkillTentacle; // �ٸ� ��ų
    public GameObject SkillInk;      // �Թ� �߻�
    public GameObject SkillInkSwarm; // �Ա��� ����
    bool test = true;
    Vector3 Far;
    Vector3 CMPD;
    public int LegCount;
    public float waitTime;
    public GameObject InkOct;
    public GameObject Bubble;
    void Start()
    {
        HitFlag = false;
        LegCount = 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GM = GameObject.FindGameObjectWithTag("GM");
        Circle = GetComponent<CircleCollider2D>();
        Skin = GetComponent<SpriteRenderer>();
        Skin.sprite = Image[0];
        HP = 12;
        Speed = 3.8f;
        RotationSpeed = 800f;
        waitTime = 4f;
        FRZFlag = false;
        S = transform.GetComponent<SpriteRenderer>();
        // c = transform.GetComponent<SpriteRenderer>().color;
        StartCoroutine("Start_");
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); // �ϴ� �ӽ÷�
        timer += Time.deltaTime; // �̹��� �ٲٴ� �ð� ���� Ÿ�̸�
        timer_ += Time.deltaTime; // �ϴ� ��ų������ ���� ���� ��
        statusColor();
        if (HP > 0 && !FRZFlag)
        {
            if (LegCount > 0)
            {
                if (timer_ >= 4f && test)
                {
                    // CreateTentacle();
                    test = false;
                }

                if (timer_ >= Random.Range(3f, 7f))
                {
                    timer_ = 0f;
                    CMPD = AbsVector(Sub(AbsVector(Player.transform.position), AbsVector(transform.position)));
                    if (LegCount > 0 && (Mathf.Abs(CMPD.x) < 8f && Mathf.Abs(CMPD.y) < 6f))
                        CreateTentacle();
                    else
                    {
                        CreateInkBomb();
                        CreateInkSwarm();
                    }
                }
            }
            else
            {
                if (timer_ >= waitTime)
                {
                    dir = SetDir();
                    for (int i = 0; i < Random.Range(3, 6); ++i)
                        CreateInkOct();
                    timer_ = 0;
                }
                MoveKraken(dir);
                // CreateBubbles();
                BubbleP.gameObject.GetComponent<BubleParticle>().Speed = Speed;
            }
            //ChangeCollider();


        }
    }

    public Vector3 Sub(Vector3 V1, Vector3 V2)
    {
        return V1 - V2;
    }
    public Vector3 SetDir()
    {
        Vector3 P = new Vector3(Random.Range(-2.0f, 2.1f), Random.Range(-1.0f, 1.1f));
        return P - transform.position;
    }
    public void CreateInkOct() // �Թ� �н� ����
    {
        var IO = Instantiate(InkOct, transform.position, Quaternion.Euler(Random.Range(-180f,180f), 0, 0));
        IO.transform.parent = transform;
        IO.transform.localPosition = new Vector3(Random.Range(-0.9f,0.1f),0f,0f);
        IO.transform.parent = null;
        IO.transform.localScale = new Vector3(1,1,1);
        IO.GetComponent<Player>().FishNumber =7;
        IO.GetComponent<Player>().StartFlag = true;
    }
    public void MoveKraken(Vector3 dir_)
    {

        transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);// ������Ʈ �̵��Լ� https://www.youtube.com/watch?v=2pf1FE-Xcc8 ������ �ڵ带 ��¦ �����Ѱ�.   
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, dir);//�̵����⿡ �°� ������ ������ ȸ���� �޾ƿ���.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);//�÷��̾������Ʈ���� �޾ƿ� ȸ���� ����
        float x_ = transform.localScale.x;// x_�� �÷��̾������Ʈ scale.x �� ����. scale.x�� �����Ͻ� �÷��̾�� �¿�������� ȸ���Ѵ�. �̸��̿��ؼ� �������� ���� ���Ƶ� �������� ����� �ȳ����� ��.                
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
    public Vector3 AbsVector(Vector3 V)
    {
        return new Vector3(Mathf.Abs(V.x), Mathf.Abs(V.y), Mathf.Abs(V.z));
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (LegCount == 0)
        {
            if ((other.transform.tag == "Knife" && other.transform.parent.tag == "Player") || other.transform.tag == "EXPL")
            {
                Damaged(other.gameObject);
            }
        }
        if (other.gameObject.tag == "FRZ")
        {
            FRZOn();
            Invoke("FRZOff", 2.5f);
        }
    }
    void CreateTentacle() // �˼� ��ų ����
    {

        Vector3 PlayerP = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (Random.Range(0, 10) == 1) CreateInkSwarm();
        var a = Instantiate(SkillTentacle, Point.transform.position, Quaternion.Euler(0, 0, 0));
        a.transform.parent = transform;
        a.transform.localPosition = Vector3.zero;
        a.transform.localScale = new Vector3(1f, 1f, 1f);

        a.transform.Translate(new Vector3(PlayerP.x, -transform.localScale.y - 1f, 1f), Space.World); // ��ŸŬ ���� �ֺ� �������� ����
        // a.transform.parent = null;

        a.GetComponent<Tentacle>().Active = true;
    }
    void CreateInkBomb() // ��ũ ��ź ����
    {
        var a = Instantiate(SkillInk, transform.position, Quaternion.Euler(0, 0, 0));
        a.transform.parent = transform;
        a.transform.localPosition = Vector3.zero;
        a.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f); // �Թ��߻� 0.3, �˼� 0.6
    }
    void CreateInkSwarm() // ȭ�� ������ ��ũ���� ����
    {

        var a = Instantiate(SkillInkSwarm, Player.transform.position, Quaternion.Euler(0, 0, 0));
        a.transform.localScale = new Vector3(2f, 2f, 1f);
        Destroy(a, 3f);
    }
    public void CreateInkSwarm(Vector3 V, float size)
    {
        var a = Instantiate(SkillInkSwarm, V, Quaternion.Euler(0, 0, 0));
        a.transform.localScale = new Vector3(size, size, size);
        Destroy(a, 3f);
    }

    float RandomPositionX()
    {
        int x = Random.Range(-8, 8);

        if (x < 0)
            x -= 7;
        else
            x += 7;

        return (float)x;
    }
}
