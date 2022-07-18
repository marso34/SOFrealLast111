using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject StarSound;
    public ParticleSystem ItemEffect;
    float timer22 = 0;
    float watime2 = 0.6f;
    bool flag = true;

    // Start is called before the first frame update
    private void Update()
    {
        shakeObj();
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Body" && other.transform.parent.tag == "Player")
        {
            other.transform.parent.gameObject.GetComponent<Player>().EatStar();

            // ���⿡ �÷��̾� ��¦�̰� ����� ���� �Ѱ��ִ�, �Լ��������Ű�� ��� �ϱ�. ����������.
            // ������ �Ҹ���� <- ���� �����Ұ�,.
            var a = Instantiate(ItemEffect, transform.position, Quaternion.Euler(0f, 0f, 0f));
            var KE1 = Instantiate(StarSound, transform.position, Quaternion.Euler(0f, 0f, 20f));
            Destroy(gameObject, 0.2f);
        }
    }
    void shakeObj() // ���Ʒ� ������
    {
        timer22 += Time.deltaTime;
        if (timer22 > watime2)
        {
            flag = !flag;
            timer22 = 0;
        }
        if (flag)
            transform.Translate(Vector3.up * 0.6f * Time.deltaTime);
        else transform.Translate(Vector3.down * 0.6f * Time.deltaTime);
    }
}
