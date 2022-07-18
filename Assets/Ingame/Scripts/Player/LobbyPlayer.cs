using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyPlayer : MonoBehaviour
{
   

    public GameObject GM;
    public GameObject MyKnife;   
    public GameObject Skin;
    public Skin skin_;
    public Sprite Body;
    public Sprite Knife;

    public int KnifeNumber_;
    public int BodyNumber_;
    public Text NicName;
    
    void Start()
    {
        //�ε��� �ʱ�ȭ
        skin_ = Skin.GetComponent<Skin>();
        Body = Skin.GetComponent<SpriteRenderer>().sprite;
        Knife = MyKnife.GetComponent<SpriteRenderer>().sprite;
        KnifeNumber_ = 1;
        BodyNumber_ = 1;
        InitBodyKnife();
        KnifeSkinInit();
    }   
    void BodySkinInit()
    {
        if (BodyNumber_ == 1) Body = skin_.FirstTailAnims[0];
        else if (BodyNumber_ == 2) Body = skin_.SharkTailAnims[0];
        else if (BodyNumber_ == 3) Body = skin_.WaileTailAnims_R[0];
        else if (BodyNumber_ == 4) Body = skin_.BlowfishTailAnims[0];
        else if (BodyNumber_ == 5) Body = skin_.OctopusTailAnims[0];
    }
    void KnifeSkinInit()
    {
        if (KnifeNumber_ == 1) Knife = skin_.BasicKnife[0];
        else if (KnifeNumber_ == 2) Knife = skin_.CandyKnife[0];
        else if (KnifeNumber_ == 3) Knife = skin_.PanKnife_R[0];
        else if (KnifeNumber_ == 4) Knife = skin_.SpearKnife[0];
        else if (KnifeNumber_ == 5) Knife = skin_.XKnife[0];
        else if (KnifeNumber_ == 6) Knife = skin_.Rager_R[0];
    }
    void InitBodyKnife()//knife,body�⺻��� �ʱ�ȭ
    {      
        BodySkinInit();
        KnifeSkinInit();  
    }
   
    public void KnifeChange(int index)//knife ��� �κ��丮���� ���õȰ����� �ٲٱ�(�⺻ �ʱ�ȭ) inventorybtn���� ����
    {
        KnifeNumber_ = index;
        KnifeSkinInit();
    }

    public void BodyChange(int index)//body ��� �κ��丮���� ���õȰ����� �ٲٱ�(�⺻ �ʱ�ȭ) inventorybtn���� ����
    {
        BodyNumber_ = index;
        BodySkinInit();
    }
}
