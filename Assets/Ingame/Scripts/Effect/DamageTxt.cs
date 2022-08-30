using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTxt : MonoBehaviour
{
    public Text dtxt;
    public GameObject Player;
    float moveSpeed; // �ؽ�Ʈ �̵� �ӵ�
    float alphaSpeed; // ���İ�(����) ��ȭ �ӵ�
    Color alpha;
    public GameObject QM;
    public Gradient ComboColor;
    [Range(0, 1)]
    public float C;

    // Start is called before the first frame update
    void Start()
    {
        QM = GameObject.FindGameObjectWithTag("QM");
        if (GameObject.FindGameObjectWithTag("Player") != null)
            Player = GameObject.FindGameObjectWithTag("Player");
        else Player = null;

        moveSpeed = 1f;
        alphaSpeed = 0.5f;
        alpha = dtxt.color;
        transform.Translate(RandomPosition(), Space.World); // ó�� ���� ��ġ�� �������� ������ ��ġ�� �̵�
        QM.GetComponent<QuestManager>().Score += ++Player.GetComponent<PlayerScript>().KomBoCount;
        C = Player.GetComponent<PlayerScript>().KomBoCount / 100f;

        Debug.Log("���� : " + C);

        if (C >= 1f)
            C = 1f;

        alpha = ComboColor.Evaluate(C);
        dtxt.text = Player.GetComponent<PlayerScript>().KomBoCount.ToString();

        Invoke("DelTxt", 2f);
    }

    // Update is called once per frame
    void Update() // �ǰݽ� ������ �ؽ�Ʈ�� ���� ���� �ø�
    {
        moveSpeed = Mathf.Lerp(moveSpeed, 0, Time.deltaTime);
        alphaSpeed = Mathf.Lerp(alphaSpeed, 2f, Time.deltaTime * 2f);

        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        dtxt.color = alpha;
    }

    void DelTxt()
    {
        Destroy(gameObject);
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(0, 1f);
        float y = Random.Range(0, 1f);

        return new Vector3(x, y, 1f).normalized;
    }
}
