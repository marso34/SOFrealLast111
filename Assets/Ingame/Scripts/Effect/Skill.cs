using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public GameObject Spectrum;
    GameObject GM;

    public float skillTime = 3f;

    int FishNumber;
    float Speed;
    float timer = 0f;

    void Start()
    {
        FishNumber = transform.parent.gameObject.GetComponent<Player>().FishNumber;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        init();
        SetSpeed();
        Destroy(gameObject, skillTime + 0.3f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (FishNumber == 1)
        {
            if (timer > 1f / (Speed))
            {
                timer = 0f;
                Instantiate(Spectrum, transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
        else if (FishNumber == 4)  // ���Ż�
        {
            transform.rotation = Quaternion.Euler(0, 0, 30 * timer * (-1)); // �θ��� ȸ������ ������� ȸ��
        }

        FixDir();
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

    void SetSpeed()
    {
        Speed = transform.parent.gameObject.GetComponent<Player>().Speed;

        if (Speed < 10f)
            Speed += 5f;
    }

    void FixDir()
    {
        if (transform.parent.localScale.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}
