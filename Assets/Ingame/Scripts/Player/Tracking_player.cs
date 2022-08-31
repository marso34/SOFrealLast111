using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking_player : MonoBehaviour
{
    public GameObject[] CameraWall; 
    public Transform target;// Tracking object preferp   
    float shake;
    public float z = -19;
    float bustValue_ = 1;
    bool dieFlag = false;
    bool StartFlag;
    public float Speed;
    Vector3 orgposition;
    Rigidbody2D RB;
    private void Start()
    {
        CameraWall = new GameObject[4];
        StartFlag = true;
        shake = 0;
        target = transform;
        RB = transform.GetComponent<Rigidbody2D>();
        z = -19;
        Speed = 2f;
    }

    public void SetResolution()
    {
        int setWidth = 1280; // ����� ���� �ʺ�
        int setHeight = 720; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(Screen.width, Screen.width * setWidth / setHeight, true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }

    }
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    ///
    void Update()
    {
        CameraWall = GameObject.FindGameObjectsWithTag("Finish");

        if (target != null && (target.tag == "Player" || target.tag == "Victem"))
        {
            if (StartFlag)
            {
                z = -19;
                transform.position = new Vector3(target.position.x, target.position.y, z);
                StartFlag = false;
                transform.GetComponent<Camera>().fieldOfView = 22;
            }

            var tenp = transform.GetComponent<Camera>().fieldOfView;

            var dir = target.GetComponent<Player>().MyBody.transform.position - transform.position;
            RB.velocity = dir * Speed;//.normalized * target.GetComponent<PlayerScript>().Speed * 4f;

            if (transform.GetComponent<Camera>().fieldOfView < 28)
            {
                
                transform.GetComponent<Camera>().fieldOfView = 22 + target.transform.localScale.y * 3f;
                
            }
            else transform.GetComponent<Camera>().fieldOfView = 28;



            // CameraWall[1].transform.Translate(new Vector3(0, (transform.GetComponent<Camera>().fieldOfView - tenp)/2, 0));

            // transform.position = target.GetComponent<PlayerScript>().MyBody.transform.position + new Vector3(0f, 0f, z);//Tracking object
            // RaycastHit2D ray2 = Physics2D.Raycast(transform.position, (new Vector3(0,1.1f,0) - transform.position).normalized, 1000f, LayerMask.GetMask("CameraWall"));
            // if (ray2.collider != null)
            // {
            //     transform.position = new Vector3(ray2.point.x + shake, ray2.point.y + shake,z - shake);              
            // }

        }
        else
        {
            StartFlag = true;
            transform.position = transform.position;// "Null instence" error depance
            target = null;
            dieFlag = false;
            transform.GetComponent<Camera>().fieldOfView = 21f;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        //SetResolution();

        if (dieFlag && z < -11) z += 1;
    }
    public void target_set(GameObject player)
    {
        target = player.transform;

    }
    public void CrushCam()
    {
        StartCoroutine("CamAction");
    }
    IEnumerator CamAction()//ī�޶� ����.
    {
        orgposition = transform.position;
        //shake = (0.05f + (Mathf.Pow(2, target.localScale.y) / 100)) * 2;
        shake = 0.5f;

        transform.position = new Vector3(transform.position.x + Random.Range(-shake,shake),transform.position.y + Random.Range(-shake, shake),transform.position.z + Random.Range(-shake, shake));

        yield return new WaitForSecondsRealtime(0.05f); //+(Mathf.Pow(2, target.localScale.y) / 100)
        transform.position = new Vector3(transform.position.x + Random.Range(-shake, shake), transform.position.y + Random.Range(-shake, shake), transform.position.z + Random.Range(-shake, shake));
        yield return new WaitForSecondsRealtime(0.05f); //+(Mathf.Pow(2, target.localScale.y) / 100)
        //shake = 0;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
    public void BustValue(bool value)
    {
        if (value)
            bustValue_ = 0.5f;
        else bustValue_ = 1.5f;
    }
    public void DieCamAction()
    {
        dieFlag = true;
    }
}
