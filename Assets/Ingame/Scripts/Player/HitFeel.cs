using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFeel : MonoBehaviour
{
    //https://www.youtube.com/watch?v=ChOtkGLIGyU �̿��� ���� ���� �ڵ���. �ð��� ���߰ų� �����Ը���� ��� ������ ȸ���ӵ���, �̵��ӵ��� ���ο� ������ ������ �ð����� ����. 
    bool stopping; //TimeStop Ű�� �Լ�.
    public float stopTime;
    public GameObject Player_;
    float TempBusterSp;
    float TempMoveSp;
    float TempRotateSp;
    float TempSpeed;
    //Temp�� ������ �ִ��ӵ� ��Ƶ� ����.
    public Transform cam;
    Vector3 camPosition_original;
    public float shake;
    public float FishWeight;//
    public bool SlowFlag_;

    private void Start()
    {
        shake = 2f;
        cam = GameObject.FindWithTag("MainCamera").transform;
        camPosition_original = cam.position;
        stopTime = 0.2f;
        FishWeight = 1f;
    }

    public void TimeStop(float weight)
    {
        if (transform.parent.tag == "Player")
        {
            Vibrate vibrate1 = new Vibrate();
            vibrate1.vibrate(30);
        }
        FishWeight = weight;
        if (!stopping)
        {
            TempBusterSp = Player_.transform.GetComponent<Player>().BusterSpeed;
            TempMoveSp = Player_.transform.GetComponent<Player>().MovementSpeed;
            TempRotateSp = Player_.transform.GetComponent<Player>().RotationSpeed;
            TempSpeed = Player_.transform.GetComponent<Player>().Speed;
            stopping = true;
            PlayerValue(0);
            if (transform.parent.tag == "Player")
                cam.GetComponent<Tracking_player>().StartCoroutine("CrushCam"); // ų�Ҷ� ī�޶� ��鸮�� �ؼ� Ÿ�ݰ��츮��.
            // StartCoroutine("Stop_");

        }
    } // ����ȸ��,�̵� �ӵ� ���̴� �Լ�.
    IEnumerator Stop_()
    {

        // yield return new WaitForSecondsRealtime(0.00f);
        //PlayerValue(1);
        PlayerSlowValue();

        yield return new WaitForSecondsRealtime(0.1f * FishWeight); //0.07f + (Mathf.Pow(2, FishWeight) / 100) / 2
        PlayerValue(1);
        stopping = false;


        Player_.transform.GetComponent<Player>().StopCoroutine("Start_");

        Player_.transform.GetComponent<Player>().StartCoroutine("Start_");

    }// �ٿ��ٰ�, ���󺹱���Ű�� �ڷ�ƾ.

    void PlayerValue(float value) // �� �g���ϴ� ��!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        SlowFlag_ = false;
    } // ����ȸ��,�̵� �ӵ� �پ��� �������� �ٲ��ִ��Լ�.
    void PlayerSlowValue()
    {
        SlowFlag_ = true;
    }  //���������� ����ȸ��,�̵� �ӵ� ���̴� �Լ�.
    public void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.transform.tag == "Trush" || other.transform.tag == "BigTrash" || other.transform.tag == "Kraken" || other.transform.tag == "Attacker" || other.transform.tag == "Tentacle" || other.transform.tag == "InkOct" || other.transform.tag == "BTK") && (transform.tag == "Knife" && transform.parent.tag == "Player"))
        {
            // if (other.transform.tag == "Kraken" || other.transform.tag == "Attacker" ||other.transform.tag == "Tentacle")

            TimeStop(1f);
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (transform.parent != null)
        {
            if (transform.parent.tag == "Player")
                transform.localScale = transform.localScale / transform.localScale.y;
            else if (transform.parent.tag == "AiPlayer")
            {
                transform.localScale = (transform.localScale / transform.localScale.y) / 2f;
            }
            else if (transform.parent.tag == "InkOct")
            {
                transform.localScale = new Vector3(0.1f, 1f, 1f);
                GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            }
            if (SlowFlag_)
            {
                Player_.GetComponent<Player>().RB.velocity = Vector2.zero;
            }

        }
    }
}
