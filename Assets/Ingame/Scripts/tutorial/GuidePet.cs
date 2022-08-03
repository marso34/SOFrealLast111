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

    public GameObject tutorial;
    public GameObject TutoBack;
    public GameObject TutorialPlan;
    public bool lev4up = false; 
    public bool A = false;
    
    public void OnCanvas()
    {
        tutorial.SetActive(false);
        TutoBack.SetActive(false);
        GameObject.Find("Player(Clone)").transform.Find("Canvas").gameObject.SetActive(true);

    }

    public void OffCanvas() {
        tutorial.SetActive(true);

        GameObject.Find("Player(Clone)").transform.Find("Canvas").gameObject.SetActive(false);

        
    }
    public void BornGuide() 
    {
        Guide = GameObject.Find("Player(Clone)").transform.Find("GuidePet(Clone)").gameObject;
        //QM.GetComponent<QuestManager>().A = true;
        //QM.GetComponent<QuestManager>().bornguide(); 
        //Guide.transform.SetParent(Player.transform);
        Guide.transform.localPosition = new Vector3(-4.5f, 6, 0); //�������� ���� ȭ�� ���� �ִ� ��ġ
        

    }



    public void ShowMove() //���̵� ����� �ڸ� ������ �̵�
    {
        Vector3 destination = new Vector3(-4.5f, 3.5f, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, destination, 0.05f);
        //QM.GetComponent<QuestManager>().HideAiSkin();

    }
    public void GoOut() //���̵� ����� ���� �ٽ� �̵�
    {


        Vector3 EndDes = new Vector3(-4.5f, 8, 0);
        Vector3 speed = Vector3.zero; 
        transform.localPosition = Vector3.Lerp(transform.localPosition, EndDes, 0.05f);
        Invoke("OnCanvas", 1f);

            
    }

    public void Start()
    {
        TutoBack = GameObject.Find("GameManager").transform.Find("TutoBack(Clone)").gameObject;

        tutorial = GameObject.Find("Tutorial(Clone)").transform.Find("TutorialCanvas").gameObject;
    }
    private void Update() //Ʃ�丮�� �ܲ����� �� ������ ����� ���� ����
    {
        //transform.SetParent(Player.transform);

        
        Player = GameObject.Find("Player(Clone)").transform.gameObject;

        Player.GetComponent<PlayerScript>().StopMove();


    }

}
