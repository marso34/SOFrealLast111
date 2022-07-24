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
    //Temp�� ������ �ִ��ӵ� ��Ƶ� ����.
    public Transform cam;
    Vector3 camPosition_original;
    public float shake;
    float FishWeight;//

    private void Start()
    {
        shake = 2f;
        cam = GameObject.FindWithTag("MainCamera").transform;
        camPosition_original = cam.position;
        stopTime = 0.2f;
    }

    public void TimeStop(float weight)
    {

        FishWeight = weight;
        if (!stopping)
        {
            TempBusterSp = Player_.transform.GetComponent<Player>().RotationSpeed;
            TempMoveSp = Player_.transform.GetComponent<Player>().BusterSpeed;
            TempRotateSp = Player_.transform.GetComponent<Player>().MovementSpeed;
            stopping = true;
            PlayerValue(0);
            if (transform.parent.tag == "Player")
                cam.GetComponent<Tracking_player>().StartCoroutine("CrushCam"); // ų�Ҷ� ī�޶� ��鸮�� �ؼ� Ÿ�ݰ��츮��.
            StartCoroutine("Stop_");

        }
    } // ����ȸ��,�̵� �ӵ� ���̴� �Լ�.
    IEnumerator Stop_()
    {

        yield return new WaitForSecondsRealtime(0.00f);
        PlayerValue(1);
        PlayerSlowValue();

        yield return new WaitForSecondsRealtime(0.07f + (Mathf.Pow(2, FishWeight) / 100) / 2);
        PlayerValue(1);
        stopping = false;


        Player_.transform.GetComponent<Player>().StopCoroutine("Start_");

        Player_.transform.GetComponent<Player>().StartCoroutine("Start_");

    }// �ٿ��ٰ�, ���󺹱���Ű�� �ڷ�ƾ.

    void PlayerValue(float value)
    {
        Player_.transform.GetComponent<Player>().RotationSpeed = TempBusterSp * value;
        Player_.transform.GetComponent<Player>().BusterSpeed = TempMoveSp * value;
        Player_.transform.GetComponent<Player>().MovementSpeed = TempRotateSp * value;
        Player_.transform.GetComponent<Player>().Speed = TempRotateSp * value;

    } // ����ȸ��,�̵� �ӵ� �پ��� �������� �ٲ��ִ��Լ�.
    void PlayerSlowValue()
    {
        Player_.transform.GetComponent<Player>().RotationSpeed = TempBusterSp * 0.1f;
        Player_.transform.GetComponent<Player>().BusterSpeed = TempMoveSp * 0.1f;
        Player_.transform.GetComponent<Player>().Speed = TempRotateSp * 0.1f;
        Player_.transform.GetComponent<Player>().MovementSpeed = TempRotateSp * 0.01f;
    }  //���������� ����ȸ��,�̵� �ӵ� ���̴� �Լ�.
    public void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.transform.tag == "Trush" || (other.transform.tag == "SkillB" && other.transform.name == "Bullet") ||other.transform.tag =="BigTrash" || other.transform.tag =="Kraken" || other.transform.tag  =="Attacker" ||other.transform.tag =="Tentacle" || other.transform.tag =="InkOct" || other.transform.tag == "BTK") && (transform.tag == "Knife" && transform.parent.tag == "Player"))
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
        if(transform.parent.tag =="Player")
        transform.localScale = transform.localScale/transform.localScale.y;
        else if(transform.parent.tag =="AiPlayer")
        {
            transform.localScale = (transform.localScale/transform.localScale.y)/2f;
        }
    }
}
