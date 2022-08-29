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

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            Player = GameObject.FindGameObjectWithTag("Player");
        else Player = null;
        moveSpeed = 2f;
        alphaSpeed = 1f;
        alpha = dtxt.color;
        transform.Translate(RandomPosition(), Space.World); // ó�� ���� ��ġ�� �������� ������ ��ġ�� �̵�
        Player.GetComponent<PlayerScript>().KomBoCount++;
        if(Player.GetComponent<PlayerScript>().KomBoCount > 200) alpha = Color.black;
        else if(Player.GetComponent<PlayerScript>().KomBoCount > 100) alpha = Color.red;

        else if (Player.GetComponent<PlayerScript>().KomBoCount > 50) alpha = Color.yellow;

        else if (Player.GetComponent<PlayerScript>().KomBoCount > 20) alpha = Color.gray;
        dtxt.text = Player.GetComponent<PlayerScript>().KomBoCount.ToString();
        dtxt.color = Color.clear;
        Invoke("DelTxt", 2f);
    }

    // Update is called once per frame
    void Update() // �ǰݽ� ������ �ؽ�Ʈ�� ���� ���� �ø�
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        // dtxt.color = alpha;
        dtxt.color = Color.clear;
    }

    void DelTxt()
    {
        Destroy(gameObject);
    }

    Vector3 RandomPosition()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        return new Vector3(x, y, 1f).normalized;
    }
}
