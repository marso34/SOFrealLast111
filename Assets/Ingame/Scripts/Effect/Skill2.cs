using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    public GameObject Bubble;
    public GameObject SkillSkin; // ��Ų ��� ������Ʈ
    public SkillSkin SkillSkin_; //��Ų
    public Vector3 dir; //�����Ϲ���

    public bool DelFalg;
    double Timer; // ��ų ���� �ð�
    double RotateTimer; // Ÿ�ھ� ��ų ���� ���� �ð�
    float Speed;
    int FishNumber;
    public GameObject GM;

    public GameObject BboomEffect;
    public GameObject StabSound;
    bool FRZFlag;
    Color c;
    SpriteRenderer S;
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM");
        SkillSkin_ = SkillSkin.GetComponent<SkillSkin>();
        S = transform.GetComponent<SpriteRenderer>();
        FishNumber = transform.parent.gameObject.GetComponent<Player>().FishNumber;
        DelFalg = false;
        FRZFlag = false;
        Timer = 0;
        RotateTimer = 0;
        Init();
    }

    void Update()
    {
        statusColor();
        if (!FRZFlag)
        {
            Timer += Time.deltaTime;
            transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);// ������Ʈ �̵��Լ� https://www.youtube.com/watch?v=2pf1FE-Xcc8 ������ �ڵ带 ��¦ �����Ѱ�.

            if (FishNumber == 3) // Ÿ�ھ� ��ų
            {
                RotateTimer += Time.deltaTime;
                transform.Rotate(Vector3.forward * 40 * Time.deltaTime); // Ÿ�ھ� ��ų ȸ�� ��Ű��
                if (RotateTimer > 0.2f)
                {
                    CreateBubbles();
                    RotateTimer = 0;
                }
            }
        }
        if (Timer > 5f)
            DelFalg = true;

        if (DelFalg)
            DelSkill();
        if (GM.GetComponent<GameManager_>().EndFlag == true) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Knife" && other.transform.parent.tag == "Player" && transform.name == "Bullet")
        {
            DestroyBossSkill(other.gameObject);
        }
        if (other.gameObject.tag == "FRZ")
        {
            FRZOn();
            Invoke("FRZOff", 2.5f);
        }
    }
    public void Init()
    {
        DirInit();
        ImgInit();
    }
    public void ImgInit()
    {
        if (FishNumber == 2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = SkillSkin_.BlowfishSkill;
            transform.tag = "SkillB";
        }
        else if (FishNumber == 3)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = SkillSkin_.OctopusSkill;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = SkillSkin_.OctopusSkillAnims;
            transform.tag = "SkillO";
        }
        // else if (FishNumber == ???) ���߿� �߰��� ����� ��ų
    }
    public void DirInit()
    {
        if (FishNumber == 2)
        {
            float DirX = Random.Range(-0.9f, 0.9f);
            float DirY = Random.Range(-0.9f, 0.9f);

            dir = new Vector3(DirX, DirY, 0); // ���� ����
            transform.Translate(dir.normalized * (transform.parent.localScale.y) * 0.8f, Space.World); // �̵�

            // Vector3 temp = dir + transform.parent.gameObject.GetComponent<Player>().dir;
            transform.localScale *= 0.23f;
            Speed = 4.5f;
        }
        else if (FishNumber == 3)
        {
            dir = transform.parent.gameObject.GetComponent<Player>().dir; // �θ� ���� �������� -> �θ� ������ ���� �� ��ų ����ϸ� ��ų�� �� ������ ���� �ʿ� -> ���� �÷��̾��� �������°� ���⿡ �״�� ���

            // float Radian = transform.parent.eulerAngles.z * Mathf.Deg2Rad - 11; // �θ��� ���� �޾ƿ���
            // float DirX = Mathf.Cos(Radian);
            // float DirY = Mathf.Sin(Radian);
            // dir = new Vector3(DirX, DirY, 0); // �θ��� ������ ���� ���� ����

            transform.Translate(dir * (transform.parent.localScale.y), Space.World);
            Speed = 6f;
        }

        transform.parent = null;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, dir.normalized); //�̵����⿡ �°� ������ ������ ȸ���� �޾ƿ���.
        transform.localRotation = toRotation; //ȸ���� ����
    }
    public void DestroyBossSkill(GameObject other)
    {
        Instantiate(BboomEffect, transform.position, Quaternion.Euler(0f, 0f, 0f));
        Instantiate(StabSound, transform.position, Quaternion.Euler(0f, 0f, 0f));
        other.transform.parent.GetComponent<PlayerScript>().Handlebar(8f);
        Destroy(gameObject);
    }

    void FRZOn()
    {
        FRZFlag = true;
    }
    void FRZOff()
    {
        FRZFlag = false;
    }
    void statusColor()
    {
        if (FRZFlag == true)
        {
            c = new Color(60f / 255f, 150f / 255f, 255f / 255f);
            S.color = c;
        }
        else if (FRZFlag == false)
        {
            c = Color.white;
            S.color = c;
        }
    }
    public void DelSkill()
    {
        Destroy(gameObject);
    }
    public void CreateBubbles()
    {
        for (int i = 0; i < 2; i++)
        {
            float randemX = 0;
            float randemY = 0;

            randemX = Random.Range(-1f, 1f);
            randemY = Random.Range(-1f, -1f);

            var V_ = new Vector3(transform.position.x, transform.position.y, 1);
            var bubble_ = Instantiate(Bubble, V_, transform.rotation);

            bubble_.transform.parent = transform;
            bubble_.transform.localPosition = new Vector3(randemX, randemY, 1);
            bubble_.transform.parent = null;

            float randemsize = Random.Range(1f, 2f);
            bubble_.transform.localScale = transform.localScale * randemsize;

            float bubbleSpeed = 0.003f;

            bubble_.GetComponent<bubble>().Speed = bubbleSpeed;
            bubble_.GetComponent<bubble>().dir = dir * -1;
        }
    }
}