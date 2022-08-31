using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackPanel : MonoBehaviour
{
    public bool flag;
    public Image S;
    public Color C;
    // Start is called before the first frame update
    void Start()
    {
        S = GetComponent<Image>();
        C = Color.black;
        C.a = 0f;
        S.color = C;
    }

    // Update is called once per frame
    public void OnPanel()
    {
        
        StartCoroutine("Life");
        Debug.Log("����");
    }
    public void QuickOffPanel()
    {
        
        StartCoroutine("Life");
        Debug.Log("����");
    }
    public void OffPanel()
    {
        
        StartCoroutine("Die");
    }
    IEnumerator Die() //죽음 ?��?��
    {
        Debug.Log("����---");
        for (int i = 0; i < 50; ++i)
        {
            //if (Life) break;
            ShowDieAnim(i);
            yield return new WaitForSeconds(0.01f);
        }
        transform.SetAsFirstSibling();
        StopCoroutine("Die");
    }
    public void ShowDieAnim(int index)//죽었?��?�� ?��?��매이?�� ?��?��?��?��
    {

        if (S.color.a > 0)
        {
            for (int i = 0; i < 50; i++)
            {
                if (i == index)
                {
                    C.a -= 5.1f/255f;
                    S.color = C;
                }
            }
        }
    }
    IEnumerator Life() //죽음 ?��?��
    {
        Debug.Log("����---");
        transform.SetAsLastSibling();
        for (int i = 0; i < 50; ++i)
        {
            //if (Life) break;
            ShowLifeAnim(i);
            yield return new WaitForSeconds(0.01f);
        }
        StopCoroutine("Life");
    }
    public void ShowLifeAnim(int index)//죽었?��?�� ?��?��매이?�� ?��?��?��?��
    {

        if (S.color.a  <1)
        {
            for (int i = 0; i < 50; i++)
            {
                if (i == index)
                {
                    C.a += 5.1f/255f;
                    S.color = C;

                }
            }
        }
    }
    private void update()
    {
      
    }
}
