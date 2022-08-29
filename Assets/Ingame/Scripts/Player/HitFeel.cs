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
        // if (transform.parent.tag == "Player")
        // {
        //     Vibrate vibrate1 = new Vibrate();
        //     vibrate1.vibrate(20);
        // }
        FishWeight = weight;
        if (!stopping)
        {
            stopping = true;
            //if (transform.parent.tag == "Player")
              ///  cam.GetComponent<Tracking_player>().StartCoroutine("CrushCam"); // ų�Ҷ� ī�޶� ��鸮�� �ؼ� Ÿ�ݰ��츮��.
            //StartCoroutine("Stop_");

        }
    } // ����ȸ��,�̵� �ӵ� ���̴� �Լ�.
    
    
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
    
        }
    }
}
