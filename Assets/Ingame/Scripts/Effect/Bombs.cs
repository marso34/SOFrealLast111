using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : Item
{
    // Start is called before the first frame update
    public GameObject BombSound;
    public GameObject BombSound2;
    public ParticleSystem Explosion;

    public bool Active = false; // true�� ��ġ�� ��ź, false�� ��ź ������
    float ColorTimer = 0f; // ������ Ÿ�̸�
    float timer_ = 0f; // ��ź ��ġ ���� �ð�
    int c = 0;

    private void Update()
    {
        if (Active)
        {
            transform.GetChild(0).gameObject.SetActive(false); // �踮�� �̹���(GetChild(0)) �����
            ColorTimer += Time.deltaTime;
            timer_ += Time.deltaTime;

            if (ColorTimer >= 0.3f) // 0.3�� �������� ������ ������
            {
                ColorTimer = 0f;
                c ^= 1;
            }

            GetComponent<SpriteRenderer>().color = (c == 0) ? Color.white : Color.red; // c�� 0�̸� �⺻��, 1�̸� ������

            if (timer_ >= 1f)
            {
                var KE1 = Instantiate(BombSound, transform.position, Quaternion.Euler(0f, 0f, 20f)); // ���� �Ҹ�
                var a = Instantiate(Explosion, transform.position, Quaternion.Euler(0f, 0f, 0f)); // ���� ����Ʈ
                a.transform.localScale = transform.localScale;
                Destroy(gameObject);
            }
        }
        else
            shakeObj();
    }

    public override void eatItem(GameObject T)
    {
        if (!Active)
        {
            T.transform.gameObject.GetComponent<PlayerScript>().EatItem(1);

            var a = Instantiate(ItemEffect, transform.position, Quaternion.Euler(0f, 0f, 0f)); // ������ �Դ� ����Ʈ
            var b = Instantiate(BombSound2, transform.position, Quaternion.Euler(0f, 0f, 0f));
            Destroy(gameObject);
        }
    }
}