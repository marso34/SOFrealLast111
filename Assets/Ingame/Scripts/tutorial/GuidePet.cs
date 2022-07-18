using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidePet : MonoBehaviour
{


    public GameObject Player;
    public GameObject GM;
    public GameObject Guide;
    public GameObject QM;
    public GameObject Slider1;
    public GameObject Stop;
    public GameObject TimeBoard;
    public GameObject KillBoard;
    public GameObject QB;
    public GameObject JoyStick;
    

    public bool lev4up = false; 
    
    public void OnCanvas()
    {

      /*
            transform.parent.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);//�����̴�
            transform.parent.transform.GetChild(3).transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true); //��ž
            transform.parent.transform.GetChild(3).transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(true); //�����۹�ư
            transform.parent.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true); //Ÿ�Ӻ���
            transform.parent.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true); //���̽�ƽ
            transform.parent.transform.GetChild(3).transform.GetChild(3).gameObject.SetActive(true); //����Ʈ����
            transform.parent.transform.GetChild(3).transform.GetChild(4).gameObject.SetActive(true); //ų����
*/
        GameObject.FindWithTag("Slider").SetActive(true);
        GameObject.FindWithTag("Stop").SetActive(true);
        GameObject.FindWithTag("ItemBtn").SetActive(true);
        GameObject.FindWithTag("TimeBoard").SetActive(true);
        GameObject.FindWithTag("JoyStick").SetActive(true);
        GameObject.FindWithTag("QB").SetActive(true);
        GameObject.FindWithTag("KillBoard").SetActive(true);


        
    }
    public void OffCanvas()
    {
            /*
            transform.parent.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false); //�����̴�
            transform.parent.transform.GetChild(3).transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false); //��ž
            transform.parent.transform.GetChild(3).transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(false); //�����۹�ư
            transform.parent.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false); //Ÿ�Ӻ���
            transform.parent.transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false); //���̽�ƽ
            transform.parent.transform.GetChild(3).transform.GetChild(3).gameObject.SetActive(false); //����Ʈ����
            transform.parent.transform.GetChild(3).transform.GetChild(4).gameObject.SetActive(false); //ų����*/
        GameObject.FindWithTag("Slider").SetActive(false);
        GameObject.FindWithTag("Stop").SetActive(false);
        GameObject.FindWithTag("ItemBtn").SetActive(false);
        GameObject.FindWithTag("TimeBoard").SetActive(false);
        GameObject.FindWithTag("JoyStick").SetActive(false);
        GameObject.FindWithTag("QB").SetActive(false);
        GameObject.FindWithTag("KillBoard").SetActive(false);

    }
    public void BornGuide() 
    {
        QM.GetComponent<QuestManager>().bornguide(); //�÷��̾� ���߰� ���̵� ����⸦ �÷��̾� �ڽ����� ��
        Guide.transform.localPosition = new Vector3(-2, 4, 0); //�������� ���� ȭ�� ���� �ִ� ��ġ

        OffCanvas(); 
        /*
        if (QM.GetComponent<QuestManager>().TutorialLev == 1)
        {
            transform.parent.transform.GetChild(3).transform.GetChild(4).gameObject.SetActive(true); 
        }
        else if (QM.GetComponent<QuestManager>().TutorialLev == 2)
        {
            transform.parent.transform.GetChild(3).transform.GetChild(4).gameObject.SetActive(false);
        }
*/
    }



    public void ShowMove() //���̵� ����� �ڸ� ������ �̵�
    {
        Vector3 destination = new Vector3(-2, 2, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, destination, 0.01f);
        //QM.GetComponent<QuestManager>().HideAiSkin();

    }
    public void GoOut() //���̵� ����� ���� �ٽ� �̵�
    {


        Vector3 EndDes = new Vector3(-2, 5, 0);
        Vector3 speed = Vector3.zero; 
        transform.localPosition = Vector3.Lerp(transform.localPosition, EndDes, 0.01f);
        Invoke("OnCanvas", 1f);
        
       
            
    }

    private void Update() //Ʃ�丮�� �ܲ����� �� ������ ����� ���� ����
    {
        Vector3 direction = Guide.transform.localRotation * new Vector3(0,0,90);

    }
}
