using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillBtn : UiButton
{
    public GameObject Player;
    public GameObject Image;
    public Image SkillFill;

    bool SkillFlag; // ��ų ��� �����ϸ� true
    float SkillGauge; // ���� ��ų ������
    float FullGauge; // ��ų ������ ���� á����

    public void Start()
    {
        SkillFill = Image.GetComponent<Image>();
        SkillFlag = false;
        SkillGauge = 0f;
        FullGauge = 6f;
    }

    public void Update()
    {
        SkillFill.fillAmount = SkillGauge / FullGauge;

        if (!SkillFlag && Player.GetComponent<PlayerScript>().FishNumber != 0)
        {
            if (!Player.GetComponent<PlayerScript>().SkillFlag) // ��ų ���ӽð��� �ִ� ��쿡�� SkillFlag == true
                SkillGauge += Time.deltaTime;
            else
                SkillGauge -= Time.deltaTime * 2;

            if (SkillGauge >= FullGauge)
                SkillFlag = true;
            // SkillFill.color = new Color(112/255f, 219/255f, 1f);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (SkillFlag && Player.GetComponent<PlayerScript>().MyBody.tag != "NotBody") // �÷��̾ not�� ���� ��ų ��� �� �ϰ�
        {
            Effect();
            if (Player.GetComponent<PlayerScript>().FishNumber == 1 && Player.GetComponent<PlayerScript>().StateMoveFlag_) return;
            else
            {
                Player.GetComponent<PlayerScript>().PlaySkill();
                SkillFlag = false;

                if (!Player.GetComponent<PlayerScript>().SkillFlag) // ��ų ���� �ð��� ���� ���
                    SkillGauge = 0f;

                Debug.Log("��ų �ߵ�");
            }
        }
    }
    public void UseSkill()
    {
        if (SkillFlag && Player.GetComponent<PlayerScript>().MyBody.tag != "NotBody")  // �÷��̾ not�� ���� ��ų ��� �� �ϰ�
        {
            if (Player.GetComponent<PlayerScript>().FishNumber == 1 && Player.GetComponent<PlayerScript>().StateMoveFlag_) return;
            else
            {
                Player.GetComponent<PlayerScript>().PlaySkill();
                SkillFlag = false;

                if (!Player.GetComponent<PlayerScript>().SkillFlag)  // ��ų ���� �ð��� ���� ���
                    SkillGauge = 0f;

                Debug.Log("��ų �ߵ�");
            }
        }
    }
}
