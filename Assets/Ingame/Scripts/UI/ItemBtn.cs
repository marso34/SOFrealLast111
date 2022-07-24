using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemBtn : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject Image;
    public GameObject Bombs;
    public ParticleSystem Preeze;


    public Image img;
    public Sprite Defualt;
    public Sprite ItemBomb;
    public Sprite ItemIce;
    public Sprite ItemShield;

    public bool TutorialItem = false; //y

    int ItemNumber;
    int c;
    float timer;


    void Start()
    {
        img = Image.GetComponent<Image>();
        img.sprite = Defualt;
        ItemNumber = 0;
        
        c = 0;
        timer = 0f;
        color = transform.GetComponent<Image>().color;
    }

    private void Update()
    {
        if (img.sprite != Defualt) // ������ �Ծ��� �� Ǫ�������� �����Ÿ���
        {
            timer += Time.deltaTime;
 
            if (timer >= 0.5f)
            {
                c ^= 1;
                timer = 0f;
            }
        }
        else c = 0;

        color.r = (c == 0) ? Mathf.Lerp(color.r, 1f, Time.deltaTime * 10f) : Mathf.Lerp(color.r, 0, Time.deltaTime * 10f);
        transform.GetComponent<Image>().color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (img.sprite != Defualt)  // ������ ��ư �̹����� �⺻ ���°� �ƴϸ�, �� �������� �Ծ�����
        {
            img.sprite = Defualt;
            TutorialItem = true;
            if (ItemNumber == 1)  // ��ź
            {
                var a = Instantiate(Bombs, Player.transform.position, Quaternion.Euler(0f, 0f, 0f));
                a.GetComponent<Bombs>().Active = true;
            }
            else if (ItemNumber == 2)  // ����
            {
                var a = Instantiate(Preeze, Player.transform.position, Quaternion.Euler(0f, 0f, 0f));
            }
            else if (ItemNumber == 3)  // ����
                Player.GetComponent<Player>().CreatBarriar();

            ItemNumber = 0;
        }
    }

    public void UseItem()
    {
        if (img.sprite != Defualt) // ������ ��ư �̹����� �⺻ ���°� �ƴϸ�, �� �������� �Ծ�����
        {
            img.sprite = Defualt;
            
            if (ItemNumber == 1)  // ��ź
            {
                var a = Instantiate(Bombs, Player.transform.position, Quaternion.Euler(0f, 0f, 0f));
                a.GetComponent<Bombs>().Active = true;
            }
            else if (ItemNumber == 2)  // ����
            {
                var a = Instantiate(Preeze, Player.transform.position, Quaternion.Euler(0f, 0f, 0f));
            }
            else if (ItemNumber == 3)  // ����
            {
                Player.GetComponent<Player>().CreatBarriar();
            }

            ItemNumber = 0;
        }
    }

    public void ChangeImage(int i) // �÷��̾ ������ �Ծ��� �� ȣ��
    {
        // if (img.sprite == Defualt) {
        ItemNumber = i;

        if (i == 1) img.sprite = ItemBomb;        // ��ź
        else if (i == 2) img.sprite = ItemIce;    // ����
        else if (i == 3) img.sprite = ItemShield; // ����
        // }
    }
}