using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFillBody : MonoBehaviour
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
        Player_ = GameObject.FindGameObjectWithTag("Player");
        shake = 2f;
        cam = GameObject.FindWithTag("MainCamera").transform;
        camPosition_original = cam.position;
        stopTime = 0.2f;
        FishWeight = 1f;
        stopping = false;
        SlowFlag_ = false;
    }

    public void TimeStop_(float weight)
    {
        FishWeight = weight;
        if (!stopping)
        {
            stopping = true;
            PlayerValue(0);
            if (transform.parent.tag == "Player")
                cam.GetComponent<Tracking_player>().StartCoroutine("CrushCam"); // ų�Ҷ� ī�޶� ��鸮�� �ؼ� Ÿ�ݰ��츮��.
            StartCoroutine("Stop_");

        }
    } // ����ȸ��,�̵� �ӵ� ���̴� �Լ�.
    IEnumerator Stop_()
    {

        // yield return new WaitForSecondsRealtime(0.00f);
        //PlayerValue(1);
        PlayerSlowValue();

        yield return new WaitForSecondsRealtime(FishWeight /2f); //0.07f + (Mathf.Pow(2, FishWeight) / 100) / 2
        PlayerValue(1);
        stopping = false;

//�ڷ�ƾ ���� �ذ��
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
    void update()
    {
        if (SlowFlag_)
        {
            Player_.GetComponent<Player>().RB.velocity = Vector2.zero;
        }
    }
}