using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour
{
    public int Touch = 0; //?���? ?��?�� 초기?��

    public GameObject tutorial;
    public GameObject GM;
    public GameObject TutorialPlan; //?���? ?��?��?�� ?��?��?��
    public GameObject TutorialName; //�? 처음?�� ?��?��?���? ?��르고 ?��?��리얼 ?��?�� ?��미�??
    public GameObject QM;

    public GameObject player;


    public GameObject TutoBack; //Canvas/Image �???? 배경
    public GameObject GuidePet; //?��?��버�??

    public GameObject QB;





    public GameObject Player;

    public GameObject Guide;

    public GameObject Slider;

    public GameObject QuestBoard;    

    public bool One;
    public bool TouchMo = false;
    public void Start() 
    {
        
        

        GuidePet = Instantiate(GuidePet);
        player = GameObject.Find("Player(Clone)").gameObject;
        GuidePet.transform.SetParent(player.transform);
        GuidePet.GetComponent<GuidePet>().BornGuide();
        //tutorial.SetActive(true);
        //TutorialPlan.SetActive(true);
        //GuidePet = GameObject.FindWithTag("Guide");
        Touch = 0;
    
    }
        

    

    public void OffCanvas()
    {
        tutorial.SetActive(true);
        TutoBack.SetActive(true);
        GameObject.Find("Player(Clone)").transform.Find("Canvas").gameObject.SetActive(false);
    }



    public void Update()
    {


        

        if(Touch == 0) 
        {
            //tutorial.SetActive(true);
            //GuidePet.SetActive(true);
            GuidePet.SetActive(true);
            OffCanvas();

            TouchMo = false;
            GuidePet.GetComponent<GuidePet>().ShowMove();
            Invoke("ShowExplain", 1f);

        }

        if(Touch == 1)
        {

            GuidePet.GetComponent<GuidePet>().GoOut();
            TouchMo = true;
            //TutorialPlan.SetActive(false);
           
            Invoke("OnPlayerCanvas", 1f);


        }

        player = GameObject.Find("Player(Clone)").gameObject;
        tutorial = GameObject.Find("Canvas").transform.Find("Tutorial(Clone)").gameObject;
        TutorialPlan = GameObject.Find("Canvas").transform.Find("Tutorial(Clone)").transform.Find("TuText").gameObject;
        GuidePet = GameObject.Find("Player(Clone)").transform.Find("GuidePet(Clone)").gameObject;
 
    }

    public void OnClick()
    {
        Touch++;
        //Touch1 = Touch;

    }

    public void ShowExplain()
    {
        TutorialPlan.SetActive(true);
        
    }


    public void OnPlayerCanvas() 
    {
        tutorial.SetActive(false);
        //Tuto1.SetActive(false);
        TutoBack.SetActive(false);



        
        GuidePet.SetActive(false);
        

    }
}
