using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking_player : MonoBehaviour
{
    Transform target;// Tracking object preferp   
    float shake;
    public float z = -19;
    float bustValue_ = 1;
    bool dieFlag = false;
    private void Start()
    {
       

        shake = 0;
        target = transform;
        
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
    void Update()
    {
        
        //SetResolution();
        if (target != null && target.tag == "Player" )
        {
            transform.GetComponent<Camera>().fieldOfView = 22+target.transform.localScale.y*3f;
            transform.position = new Vector3(target.position.x, target.position.y, z);//Tracking object
            RaycastHit2D ray2 = Physics2D.Raycast(transform.position, (Vector3.zero - transform.position).normalized, 1000f, LayerMask.GetMask("CameraWall"));
            if (ray2.collider != null)
            {
                transform.position = new Vector3(ray2.point.x + shake, ray2.point.y + shake,z - shake);              
            }
        }
        else {
            transform.position = transform.position;// "Null instence" error depance
            target = null;
            dieFlag = false;
            transform.GetComponent<Camera>().fieldOfView = 21f;
        }
        if(!dieFlag)
            z = -19;
        else if(dieFlag && z < -11) z +=1;
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

        shake = (0.05f + (Mathf.Pow(2, target.localScale.y) / 100))*2;
        if (target.tag == "Player")
        {
            shake += 0.05f;
        }
      
        yield return new WaitForSecondsRealtime(0.06f); //+(Mathf.Pow(2, target.localScale.y) / 100)

        shake = 0;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);    
    }
    public void BustValue(bool value) {
        if (value)
             bustValue_ = 0.5f;
        else bustValue_ = 1.5f;
    }
    public void DieCamAction(){
        dieFlag = true;
    }
}
