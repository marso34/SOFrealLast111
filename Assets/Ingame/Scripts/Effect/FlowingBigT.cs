using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingBigT : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject BigT;
    GameObject Player;
    Vector2 min;
    void Start()
    {
        min = new Vector2(3f,3f);
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player != null) transform.position = Player.transform.position;
        BigT = GameObject.FindGameObjectWithTag("BTP"); 
        if (BigT != null)
        {
            
            // Debug.Log("aaaawkwlsksdkakd");
            Vector2 dir = BigT.transform.position - transform.position; 
           transform.Translate(dir.normalized * 0.001f * Time.deltaTime, Space.World);
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, dir.normalized);//�̵����⿡ �°� ������ ������ ȸ���� �޾ƿ���.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime);///�÷��̾������Ʈ���� �޾ƿ� ȸ���� ����
            if(dir.magnitude < min.magnitude) transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            else transform.GetChild(0).GetComponent<SpriteRenderer>().color =Color.white;
        }
    }
}
