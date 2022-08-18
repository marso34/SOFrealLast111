using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabSkill : MonoBehaviour
{
    public GameObject GM;
    public ParticleSystem Nippers; // ���Թ� ��ƼŬ
    public ParticleSystem Effect;
    public GameObject Colider;

    GameObject Player;
    Rigidbody2D RB;

    public bool flag;
    public bool SkillFlag;

    float timer;
    float waitTime;

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM");
        Colider.transform.tag = "CrabNippers";
        Player = GameObject.FindGameObjectWithTag("Player");
        RB = transform.GetComponent<Rigidbody2D>();

        flag = false;
        SkillFlag = false;
        timer = 0f;
        waitTime = 5f;

        Vector3 dir = Player.transform.position - transform.position;
        RB.velocity = dir.normalized * 1f;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, dir.normalized); //�̵����⿡ �°� ������ ������ ȸ���� �޾ƿ���.
        transform.localRotation = toRotation; //ȸ���� ����

        if (Nippers != null)
        {
            ParticleSystem.MainModule main = Nippers.main;

            if (main.startRotation.mode == ParticleSystemCurveMode.Constant)
            {
                main.startRotation = (220f - transform.eulerAngles.z) * Mathf.Deg2Rad;
                Debug.Log(transform.localRotation.z + " : ȸ��");
            }
        }
    }

    void Update()
    {
        ParticleSystem.TextureSheetAnimationModule texture = Nippers.textureSheetAnimation;

        timer += Time.deltaTime;

        if (flag) // �÷��̾ ������ ture
        {
            RB.velocity = Vector2.zero;

            texture.fps = 5f;
            if (SkillFlag)
            {
                Invoke("CreateEffect", 0.3f);
                timer = 0f;
                waitTime = timer + 1f;
            }

            SkillFlag = false;
        }

        if (timer >= waitTime)
            Destroy(gameObject);

        if (GM.GetComponent<GameManager_>().EndFlag == true) Destroy(gameObject);
    }

    void CreateEffect()
    {
        var E = Instantiate(Effect, EffectPosition(), Quaternion.Euler(0, 0, 0));

        float x_ = transform.localScale.x;

        if (x_ > 0)
            x_ *= -1;

        E.gameObject.GetComponent<Effect>().SetEffect(2);

        flag = false;
    }

    public Vector3 EffectPosition()
    {
        return Colider.transform.position;
    }
}
