using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyInteraction : MonoBehaviour
{
    public GameObject MyKnife;
    public GameObject KillEffect;
    public GameObject KillEffect2;
    public GameObject KillEffectO;
    public Transform cam;
    public float chsize = 0.001f;
    public float chc;
    public bool TutorialFlesh = false;
    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").transform;
        Time.timeScale = 1;
    }
    private void Update()
    {
        transform.localPosition = Vector3.zero;
    }
    public void OnCollisionEnter2D(Collision2D other2)
    {
        EatFlesh(other2.gameObject);
        StabbedKnife(other2.gameObject);
        HitSkill(other2.gameObject);
        TrashHit(other2.gameObject);
        HitEXPL(other2.gameObject);
        if (other2.transform.tag == "Wall")
        {
            transform.parent.GetComponent<Player>().CheckWall(other2.gameObject,true);
           
        }
    }
    /// <summary>
    /// Sent each frame where a collider on another object is touching
    /// this object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionStay2D(Collision2D other)
    {
        
    }
    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    
    void HitEXPL(GameObject other)
    {
        if (transform.parent.tag != "Player")
        {
            if (other.gameObject.tag == "EXPL" && transform.tag == "Body")
            {
                transform.parent.GetComponent<Player>().HP--;
                transform.parent.GetComponent<Player>().DieLife();
            }
        }
    }
    void HitSkill(GameObject other)
    {
        if (other.gameObject.tag == "SkillB" && transform.tag == "Body")
        {
            if (other.name == "Bullet" && transform.parent.tag == "Player")
            {
                transform.parent.GetComponent<PlayerScript>().DieLife();
                transform.parent.gameObject.GetComponent<Player>().SlowMoveSpeed(0.8f);
                transform.parent.gameObject.GetComponent<Player>().SlowRotateSpeed(0.2f);
                other.transform.gameObject.GetComponent<Skill2>().DelFalg = true;
            }

            if (transform.parent.tag == "AiPlayer")
            {
                transform.parent.gameObject.GetComponent<Player>().SlowMoveSpeed(0.8f);
                transform.parent.gameObject.GetComponent<Player>().SlowRotateSpeed(0.2f);
                other.transform.gameObject.GetComponent<Skill2>().DelFalg = true;
            }
        }

        if (other.gameObject.tag == "SkillO" && transform.tag == "Body")
        {
            transform.parent.gameObject.GetComponent<Player>().DieLife();
            //other.transform.gameObject.GetComponent<Skill2>().DelFalg = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().KillScoreUp();
            var KE22 = Instantiate(KillEffectO, transform.parent.position, Quaternion.Euler(0f, 0f, Random.Range(-80, 80)));
            KE22.transform.localScale = transform.localScale;
        }

        if (other.gameObject.tag == "FRZ" && transform.tag == "Body" && transform.parent.tag != "Player")
        {
            transform.parent.gameObject.GetComponent<Player>().C = new Color(60f / 255f, 150f / 255f, 255f / 255f);
            transform.parent.gameObject.GetComponent<Player>().ResetColor();
            transform.parent.GetComponent<Player>().StopMoveSpeed();
            transform.parent.GetComponent<Player>().StopRotateSpeed();


        }
        if (other.gameObject.tag == "BossSkillA" && transform.tag == "Body")
            if (transform.parent.tag == "Player") transform.parent.GetComponent<Player>().DieLife();
    }


    void StabbedKnife(GameObject other)
    {
        //Debug.Log(other.transform.tag +"iiiii");
        if (other.gameObject.tag == "Knife" && transform.tag == "Body")
            if (((other.transform.parent.tag == "InkOct" || other.transform.parent.gameObject.tag == "AiPlayer") && transform.parent.tag == "Player") || ((transform.parent.tag == "AiPlayer" || transform.parent.tag == "InkOct") && other.transform.parent.tag == "Player"))
            {
                if (other.transform.parent.gameObject != transform.parent.gameObject)
                {
                    transform.parent.gameObject.GetComponent<Player>().DieLife();
                    other.transform.parent.gameObject.GetComponent<Player>().KillScoreUp();
                    if (other.name != "body" && other.transform.parent.tag == "Player")
                        other.transform.GetComponent<HitFeel>().TimeStop(1f);

                    float QR = Random.Range(1, 7);
                    var KE = Instantiate(KillEffect, transform.parent.position, Quaternion.Euler(0f, 0f, 20f * QR));
                    float R = Random.Range(0.8f, 1.7f);
                    float x_ = transform.localScale.x;
                    if (x_ > 0)
                        x_ *= -1;

                    KE.transform.localScale = new Vector3(x_, transform.localScale.y, transform.localScale.z);
                    // KE.transform.localScale = transform.parent.localScale; * R / 1.5f;
                    Flag_Still(other.transform.parent.gameObject);
                }

                if (other.transform.parent.tag == "AiPlayer" && transform.parent.tag == "Player" && transform.parent.GetComponent<Player>().HP == 0)
                {
                    StopTime_();
                    Invoke("StartTime_", 0.015f);
                    cam.gameObject.GetComponent<Tracking_player>().DieCamAction();
                }
            }
    }// Į�� �ٝ�����
    void EatFlesh(GameObject other)
    {
        if (other.gameObject.tag == "Flesh" && ((transform.name == "body" && transform.parent.tag == "Player") || transform.tag == "Body" || transform.tag == "Shiled"))
        {
            Debug.Log("����");
            TutorialFlesh = true; //y
            GiveSize();
            GiveBusterGage();
            other.gameObject.GetComponent<flesh>().destroyme(transform.parent.gameObject);
        }
    }   // ��ü �Ծ�����

    void GiveSize()
    {

        if (transform.parent.tag == "Player") chc = 0.2f;
        else if ((transform.parent.tag == "AiPlayer")) chc = 3f;
        Vector3 Porce = new Vector3(chsize, chsize, 0f);
        if (transform.parent.localScale.x < 0) Porce = new Vector3(-1 * chsize * chc, chsize * chc, 0f);
        transform.parent.gameObject.GetComponent<Player>().Sizech(transform.parent.localScale + Porce);
        transform.parent.gameObject.GetComponent<Player>().fleshCount++;
    } //�÷��̾��� ũ�⸦ Ű���ش�.
    void GiveBusterGage()
    {
        if (transform.parent.tag == "Player")
        {
            float cutGauge = 7f;
            transform.parent.gameObject.GetComponent<PlayerScript>().Handlebar(cutGauge);
        }
    }//�÷��̾��� �ν��� �������� Ű���ش�.
    void Flag_Still(GameObject other)
    {
        if (transform.parent.GetComponent<Player>().Flag_get)
        {
            other.GetComponent<Player>().Flag_get = true;
            transform.parent.GetComponent<Player>().Flag_get = false;
        }
    }//�÷��̾ ����� �������ִٸ� ������ ä���. ���� ShapeC
    void TrashHit(GameObject other)
    {
        if (transform.tag == "Body" && other.tag == "Trush" && transform.parent.tag == "Player")
        {
            transform.parent.gameObject.GetComponent<Player>().SlowMoveSpeed(1f);
            transform.parent.gameObject.GetComponent<Player>().SlowRotateSpeed(1f);
            transform.parent.gameObject.GetComponent<Player>().C = Color.green;
        }
    }
    void StopTime_()
    {
        Time.timeScale = 0.01f;
    }
    void StartTime_()
    {
        Time.timeScale = 1;
        //transform.parent.GetComponent<Player>().HP--;
    }
    public void BoomOn()
    {
        var KE1 = Instantiate(KillEffect, transform.parent.position, Quaternion.Euler(0f, 0f, 20f));
        KE1.gameObject.GetComponent<Effect>().SetEffect(0);
    }

}
