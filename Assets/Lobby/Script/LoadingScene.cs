using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    public GameObject Loading;      //�ε� �г�
    void Start()        
    {
        Invoke("StopShowLoading", 1f);  //�ε� 1��
    }

    void StopShowLoading()
    {
        Loading.SetActive(false);   //�ε� ����
        SceneManager.LoadScene("LobbyScene");   //�κ� ������ �̵�
    }
}
