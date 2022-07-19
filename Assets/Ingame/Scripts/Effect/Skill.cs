using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float skillTime = 3f;
    int FishNumber;

    GameObject GM;

    float timer = 0f;

    void Start()
    {
        FishNumber = transform.parent.gameObject.GetComponent<Player>().FishNumber;
        init();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Destroy(gameObject, skillTime);
        GM = GameObject.FindGameObjectWithTag("GM");
    }

    void init()
    {
        if (FishNumber == 1) // �Ʊ���
        {
            transform.Find("shark1").gameObject.SetActive(true);
        }
        else if (FishNumber == 2) // ����
        {
            transform.Find("bock1").gameObject.SetActive(true);
            transform.Find("bock2").gameObject.SetActive(true);
        }
        else if (FishNumber == 4) // ���Ż�
        {
            transform.Find("whale1").gameObject.SetActive(true);
            transform.Find("whale2").gameObject.SetActive(true);
            transform.Find("whale3").gameObject.SetActive(true);
        }
        // else if (FishNumber == 5 or 6 or 7 ...) �ٸ� ����� �߰���
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (FishNumber == 4)  // ���Ż�
            transform.rotation = Quaternion.Euler(0, 0, 30 * timer * (-1)); // �θ��� ȸ������ ������� ȸ��

        if (transform.parent.localScale.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        if (GM.GetComponent<GameManager_>().EndFlag == true) Destroy(gameObject);
    }
}
