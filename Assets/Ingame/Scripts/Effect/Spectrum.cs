using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{
    public GameObject PlayerSkin;
    public ParticleSystem ps;

    Sprite image;
    Material m;
    Vector3 dir;

    float Angles;

    // Start is called before the first frame update
    void Start()
    {
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        m = transform.GetChild(0).GetComponent<Renderer>().material; // �ڽ��� ��Ƽ���� -> �������� ��ƼŬ
        PlayerSkin = transform.parent.GetComponent<PlayerScript>().Skin;
        image = PlayerSkin.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        Angles = transform.parent.eulerAngles.z;

        if (ps != null)
        {
            ParticleSystem.MainModule main = ps.main;

            if (main.startRotation.mode == ParticleSystemCurveMode.Constant)
            {
                if (Angles >= 0f && Angles < 180f)
                {
                    main.startRotation = (Angles + 90f) * Mathf.Deg2Rad; //ȸ���� ����
                }
                else
                {
                    main.startRotation = (-Angles - 90f) * Mathf.Deg2Rad; //ȸ���� ����
                }
                
                main.startSize = transform.parent.localScale.y;
                // Debug.Log(Angles + " : ȸ��");
            }

            // if (transform.parent.localScale.x < 0)
            //     transform.localScale = new Vector3(-1, transform.localScale.y, 1);
            // else
            //     transform.localScale = new Vector3(1, transform.localScale.y, 1);
        }
    }
}
