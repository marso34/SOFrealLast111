using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject QM;

    bool Inflag;
    void Start()
    {
        Inflag = true;
        QM = GameObject.FindGameObjectWithTag("QM");
    }

    // Update is called once per frame
    void Update()
    {
        if (QM.GetComponent<QuestManager>().GM.GetComponent<GameManager_>().StartButtonFlag)
        {
            Debug.Log("�αװ� " + QM.GetComponent<QuestManager>().Flag);
            if (QM.GetComponent<QuestManager>().Flag && Inflag)
            {
                for (int i = 0; i < 2; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                if(QM.GetComponent<QuestManager>().Level_  <3)
                    transform.GetChild(QM.GetComponent<QuestManager>().Level_ - 1).gameObject.SetActive(true);
                Inflag = false;
                Debug.Log("���ѱ�");
            }

            if (QM.GetComponent<QuestManager>().Flag) Inflag = true;
        }
    }
}
