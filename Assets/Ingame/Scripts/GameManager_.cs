
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//�г� ����, ���� �������, ���� ���� ������
public class GameManager_ : MonoBehaviour
{
    public GameObject Lobby_;
    public GameObject InGame;
    public GameObject Intro;
    public GameObject Player;//�÷��̾� ���ӿ�����Ʈ
    public GameObject Player_p;
    //public GameObject AiPlayer;//ai���ӿ�����Ʈ
    //public GameObject[] AiPlayers;
    //public GameObject[] Players;//ai��  �÷��̾� ������ ���� [0]���÷��̾�
    GameObject[] Fleshs;
    GameObject[] Bubbles;
    public GameObject LobbyPlayerBody;//�κ��÷��̾�
    public GameObject LobbyPlayerKnife;
    public Camera maincam;// ����ķ
    //0�� myplayer
    public int MaxPlayerCount = 4;
    public bool enterFlag;
    public GameObject Flesh;//��ü
                            // fakePanel ���� �޾ƿ�.���� ���� �Ѱ��ֱ� ����.
    public bool StartKeyFlag;
    public bool resetFlag;
    // ��ȯ�� �ʿ��� ��ü
    public bool EndFlag = false;
    //public GameObject FakePanel;// ��¥ ��Ī �г�

    public bool StartFlag_;// ������ �����ߴ���, ����ũ �г��� ������� �����̴�. fakePanel ���� �Ѱ���. true ��. PlayerScript�� �Ѱ�����.
    public bool enterGame;//��������� Ʈ��
    public bool StartButtonFlag;

    public float GlobalTime;

    public GameObject WinPanel;
    public GameObject LosePanel;
    public bool SuccesFlag = false;
    public GameObject QM;
    private void Start()
    {

        Application.targetFrameRate = 60;
        GlobalTime = 1;
        resetFlag = false;
        // Players = new GameObject[MaxPlayerCount];
        // AiPlayers = new GameObject[MaxPlayerCount - 1];
        // Ai �� �Ҵ�
        StartKeyFlag = false;
        enterGame = false;
        // FakePanel.SetActive(false);
        StartFlag_ = false;
        StartButtonFlag = false;
        //SetResolution();
        Lobby_ = GameObject.FindGameObjectWithTag("Lobby").gameObject;
    }

    private void Update()
    {
       
        //OnPreCull();
        if (StartKeyFlag)
        {
            StartKey();
             EnterInit();
               enterGame = true;
            //  SetTarget();
            resetFlag = false;
            Debug.Log("aaaa");
            InGame = GameObject.FindGameObjectWithTag("InGame").gameObject;
         
            Player_p.GetComponent<PlayerScript>().StartFlag = true;
        }

       
        Player_p = GameObject.FindGameObjectWithTag("Player");
        if (Player_p != null)
        {

            if (GlobalTime > 0 && EndFlag == false)
            {
                GlobalTime += Time.deltaTime;
                Player_p.GetComponent<PlayerScript>().globalTime = GlobalTime;
            }

        }
        else if(Player_p ==null) ObjectCleaner();
    }

    public void GoLobby()
    {
        ResetGame();
        Lobby_.SetActive(true);
        Debug.Log("lobby");
        InGame.SetActive(false);
        LosePanel.SetActive(false);
        WinPanel.SetActive(false);
    }
    public void GoNext()
    {
        ResetGame();
        InGame.SetActive(true);
        Intro.SetActive(true);
        LosePanel.SetActive(false);
        WinPanel.SetActive(false);

    }

    public void ReStart_()
    {
        ResetGame();
        InGame.SetActive(true);
        Intro.SetActive(true);
        LosePanel.SetActive(false);
        WinPanel.SetActive(false);
    }


    public void ResetGame()
    {
        Debug.Log("reset");
        enterGame = false;
        resetFlag = true;
        EndFlag = true;
        StartKeyFlag = false;
        GlobalTime = 0;
        
        StartButtonFlag = false;
        ObjectCleaner();
        QM.GetComponent<QuestManager>().Flag = true;
        QM.GetComponent<QuestManager>().ResetCounter();
        QM.GetComponent<QuestManager>().IngameLevel = 1;
        QM.GetComponent<QuestManager>().ResetMaxCounter();
        QM.GetComponent<QuestManager>().ResetPlayerStat();
        Destroy(QM.GetComponent<QuestManager>().Player);
    }
    public void ObjectCleaner(){
        GameObject[] trush_ = GameObject.FindGameObjectsWithTag("Trush");
        GameObject[] Items = GameObject.FindGameObjectsWithTag("Item");
        GameObject[] Attackers = GameObject.FindGameObjectsWithTag("Attacker");
        GameObject[] AiPlayers = GameObject .FindGameObjectsWithTag("AiPlayer");
        GameObject Kraken = GameObject.FindGameObjectWithTag("Kraken");
        GameObject []BigTrash = GameObject.FindGameObjectsWithTag("BigTrash");
        GameObject V = GameObject.FindWithTag("V");
        Destroy(Kraken);
        Destroy(V);
        for (int i = 0; i < BigTrash.Length; ++i)
        {
            Destroy(BigTrash[i],0f);
        }
        for (int i = 0; i < trush_.Length; ++i)
        {
            Destroy(trush_[i],0f);
        }
        for (int i = 0; i < Items.Length; ++i)
        {
            Destroy(Items[i],0f);
        }
        for (int i = 0; i < Attackers.Length; ++i)
        {
            Destroy(Attackers[i],0f);
        }
        for(int i=0;i<AiPlayers.Length;++i){
            Destroy(AiPlayers[i],0f);
        }
    }
    public void Start___()
    {
        StartKeyFlag = true;
        StartButtonFlag = true;
    }
    void StartKey()
    {

      
        StartKeyFlag = false;


    }//T������ ���ӽ���
    void EnterInit()
    {
         resetFlag = false;
          EndFlag = false;
        //FakePanel.SetActive(true);

        MyPlayerCreate();

        //Ai�÷��̾� 9���� ����
        /*for (int i = 1; i < MaxPlayerCount; i++)
        {
            CreateAiPlayer(i);
        }*/
     

    }//���ӵ����� �ϴ���
    /*void CreateAiPlayer(int index)
    {     
        //Ai�÷��̾����
        Players[index] = Instantiate(AiPlayer, RandomPosition(), Quaternion.Euler(0, 0, 0));
        giveProfil(index);
        SetExtra(Players[index]);
    }   //Ai ������ �ۼ��� ĳ���ͻ���
    */
    public void CreateVictem(Vector3 V, int AttackerCount)
    {// Ư����ġ�� ����, ����Ŀ�� �������� ����Ǯ���� �÷��̾�� ���Ⱑ���� ���°� ��.

    }
    public void CreateAttacker(Vector3 VictemPosition, Vector3 UserPosition)
    {// ��������ġ�� ���� -> �÷��̾�� �����߻�

    }

    void MyPlayerCreate()
    {
        Debug.Log("�ü�ȯ");
        //�κ񿡼� ������ ��
        int BN = LobbyPlayerBody.GetComponent<LobbyFish>().FishNum;
        int KN = LobbyPlayerKnife.GetComponent<LobbySword>().SwordNum;
        //�����÷��̾����
        Player_p = Instantiate(Player, RandomPosition(), Quaternion.Euler(0, 0, 0));
        //�÷��̾�� ī�޶� ���̱�
        maincam.GetComponent<Tracking_player>().target_set(Player_p);
        //MyPlayerInit
        Player_p.GetComponent<Player>().FishNumber = BN;//BN�κ񿡼� �������°�       
        Player_p.GetComponent<Player>().KnifeNumber = KN;
        Player_p.GetComponent<PlayerScript>().maincam_ = maincam;


        //giveProfil(0);
        SetExtra(Player_p);
    }// ��ĳ�� ���� Players[0] ��°�� MyPlayer
    void giveProfil(int index)
    {
        //FakePanel.GetComponent<FakePanel>().SetProfil(index, Players[index]);//�������ֱ�.
    }//������ ����ũ��η� �Ѱ��ֱ�
    void SetExtra(GameObject _Player1__)
    {
        _Player1__.GetComponent<Player>().GM = gameObject;
        //_Player1__.GetComponent<Player>().StartFlag = true;

        // _Player1__.SetActive(false);
    }//�ݺ��Ǵ� �ڵ� ���ΰ�
    /*
    public void SetTarget()//�������ڸ��� 
    {       
        for (int i = 1; i < MaxPlayerCount; ++i)
        {
            Players[i].GetComponent<AiPlayerScript>().Target = Players[0];
            
        }
    }//��� AI�� �÷��̾�����ġ�˰��ִ� �����ġ�������� �÷��̾� ī�޶󿡶�����ϱ⶧���� �ٸ� �̱������������ x
*/
    void ShowPlayers()
    {

        Player_p.SetActive(true);
    }//����ũ ��� ������ �÷��̾�� �����ֱ�

    Vector3 RandomPosition() //������ ���� ��ȯ
    {
        return new Vector3(Random.Range(-13, 13), Random.Range(-8, 8), 0);
    }

    public void SetResolution()
    {
        int setWidth = 1920; // ����� ���� �ʺ�
        int setHeight = 1080; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(Screen.width, Screen.width * setWidth / setHeight, true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }

    }
    private void OnCollisionEnter2D(Collision2D other2)
    {
        if (resetFlag == true)
            if (other2.transform.tag == "Flesh" || other2.transform.tag == "Bubble" || other2.transform.tag == "AiPlayer" || other2.transform.tag == "Player")
            {
                Destroy(other2.gameObject);

            }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (resetFlag == true)
            if (other.transform.tag == "Flesh" || other.transform.tag == "Bubble" || other.transform.tag == "AiPlayer" || other.transform.tag == "Player")
            {
                Destroy(other.gameObject);
            }
    }
    void OnPreCull() => GL.Clear(true, true, Color.black);


}










