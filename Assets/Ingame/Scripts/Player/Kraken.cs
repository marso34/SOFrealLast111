using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public GameObject GM;
    public GameObject SkillTentacle; // �ٸ� ��ų
    public GameObject SkillInk;      // �Թ� �߻�
    public GameObject SkillInkSwarm; // �Ա��� ����
    public GameObject DamageText; // ������ ǥ��
    public Sprite[] Image;
    public GameObject Point;
    GameObject Player;
    CircleCollider2D Circle;
    SpriteRenderer Skin;

    float timer = 0f;
    float timer_ = 0f;
    bool test = true;

    Vector3 dir;
    Vector3 Far;
    Vector3 CMPD;
    int HP;
    public GameObject DieImg;
    public float Speed;
    public float RotationSpeed;

    public int LegCount;
    public GameObject KillEffect;
    public GameObject KillEffect2;
    public GameObject KS_;
    public float waitTime;
    public Sprite HitSkin;
    public GameObject InkOct;
    public bool FRZFlag;
    Color c;
    SpriteRenderer S;
    public GameObject Bubble;
    public bool HitFlag;
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
                CreateBubbles();
            }
            //ChangeCollider();
        }
    }

    IEnumerator Start_()
    {
        while (true) yield return StartCoroutine("ChangeImg");
    }
    
    IEnumerator ChangeImg()//�����Ӿִϸ��̼����
    {
        for (int i = 0; i < 10; ++i)
        {
            Skin.sprite = Image[i];
            yield return new WaitForSeconds(0.125f);
        }
    }

    public void CreateBubbles()//�����ϰ� ����� ����
    {

        int count = 0;

        for (int i = 0; i < 1; ++i)
        {
            float randemX = 0;
            float randemY = 0;

            randemX = Random.Range(-0.5f, 0.5f); //0.3~-0.3



            randemY = Random.Range(-0.4f, -0.4f); //-0.2 ~-0.4


            var V_ = Point.transform.position;
            V_ = new Vector3(V_.x + randemX, V_.y + randemY, 1);
            var bubble_ = Instantiate(Bubble, V_, transform.rotation);

            bubble_.transform.parent = transform;
            bubble_.transform.parent = transform.parent;
            float randemsize = Random.Range(0.2f, 1f);

            bubble_.transform.localScale = transform.localScale / 2 * randemsize;
            float bubbleSpeed;
            bubbleSpeed = 0.003f;

            bubble_.GetComponent<bubble>().Speed = bubbleSpeed;
            bubble_.GetComponent<bubble>().dir = dir * -1;
        }


    } //�������
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
        var IO = Instantiate(InkOct, transform.position, Quaternion.Euler(0, 0, 0));
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
                DestroyKraken(other.gameObject);
            }
        }
        if (other.gameObject.tag == "FRZ")
        {
            FRZOn();
            Invoke("FRZOff", 2.5f);
        }
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
    void ChangeCollider() // �����ӿ� ���� �ݶ��̴� ����, ���� ���x ���߿� ����ϰ� �Ǹ� �ڷ�ƾ���� ����
    {
        float position = 0.1f;

        if (Skin.sprite == Image[0])
            Circle.offset = new Vector2(-0.34f, 0.15f + position);
        else if (Skin.sprite == Image[1])
            Circle.offset = new Vector2(-0.34f, 0.1f + position);
        else if (Skin.sprite == Image[2])
            Circle.offset = new Vector2(-0.34f, 0.05f + position);
        else if (Skin.sprite == Image[3])
            Circle.offset = new Vector2(-0.34f, 0f + position);
        else if (Skin.sprite == Image[4])
            Circle.offset = new Vector2(-0.34f, 0.2f + position);
        else if (Skin.sprite == Image[5])
            Circle.offset = new Vector2(-0.34f, 0.15f + position);
        else if (Skin.sprite == Image[6])
            Circle.offset = new Vector2(-0.34f, 0.1f + position);
        else if (Skin.sprite == Image[7])
            Circle.offset = new Vector2(-0.34f, 0f + position);
        else if (Skin.sprite == Image[8])
            Circle.offset = new Vector2(-0.34f, 0.04f + position);
        else if (Skin.sprite == Image[9])
            Circle.offset = new Vector2(-0.34f, 0.1f + position);
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
    void HitOn()
    {
        DieImg.SetActive(true);//DieImg�� ���������� ����
    }
    void HitOff()
    {
        DieImg.SetActive(false);//DieImg�� ���������� ����

    }
    public void DestroyKraken(GameObject other2)
    {

        float QR = Random.Range(1, 7);
        float R = Random.Range(2f, 5f);
        if (HP > 0)
        {
            UpdateOutline(true);
            Invoke("OffOutline", 0.07f);



            if (other2.gameObject.tag == "EXPL")
            {
                var DT = Instantiate(DamageText, transform.position, Quaternion.Euler(0f, 0f, 0f));
                DT.GetComponent<DamageTxt>().dtxt.text = 5.ToString();
                DT.transform.localScale *= 2f;
                HP -= 5;
            }
            else
            {
                var DT = Instantiate(DamageText, transform.position, Quaternion.Euler(0f, 0f, 0f));
                DT.GetComponent<DamageTxt>().dtxt.text = 1.ToString();
                DT.transform.localScale *= 2f;
                HP--;
                var KE = Instantiate(KillEffect, Point.transform.position, Quaternion.Euler(0f, 0f, 20f * QR));
                KE.transform.localScale = transform.localScale * R / 5f;
            }


            var KE1 = Instantiate(KillEffect2, Point.transform.position, Quaternion.Euler(0f, 0f, 20f * QR));

            Debug.Log("ssss0");
            KE1.transform.localScale = transform.localScale / 4.5f;

            var KS = Instantiate(KS_, Point.transform.position, Quaternion.Euler(0f, 0f, 20f * QR));
        }

        if (HP <= 0)
        {

            HitOn();//DieImg�� ���������� ����
            gameObject.SetActive(false);
            Invoke("win", 1.5f);
        }
    }
    void win()
    {
        Player.GetComponent<PlayerScript>().BosskillScore++;
        Destroy(gameObject); // ���� ���� ������ �̹��� �־�ٶ�� 1.2�� �ذ�
    }
    void UpdateOutline(bool outline)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        DieImg.GetComponent<SpriteRenderer>().GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", Color.white);
        mpb.SetFloat("_OutlineSize", 14);
        DieImg.GetComponent<SpriteRenderer>().SetPropertyBlock(mpb);

        MaterialPropertyBlock mpb1 = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb1);
        mpb1.SetFloat("_Outline", outline ? 1f : 0);
        mpb1.SetColor("_OutlineColor", Color.white);
        mpb1.SetFloat("_OutlineSize", 14);
        spriteRenderer.SetPropertyBlock(mpb1);
    }
    void OffOutline()
    {
        UpdateOutline(false);
    }
}
