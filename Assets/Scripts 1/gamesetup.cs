using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using System.Threading;
using Photon.Pun.UtilityScripts;
using System.Net;

public class PlayerInfo
    {
        public ProfileData profile;
        public int actor;
        public short kills;
        public short deaths;
    public bool awayTeam;
  

        public PlayerInfo(ProfileData p, int a, short k, short d, bool t)
        {
            this.profile = p;
            this.actor = a;
            this.kills = k;
            this.deaths = d;
        this.awayTeam = t;
       
        }
    }


public enum GameState
{
    waiting=0,
    starting=1,
    playing=2,
    ending=3


}


    public class gamesetup : MonoBehaviourPunCallbacks, IOnEventCallback
    {

        public List<PlayerInfo> playerinfo = new List<PlayerInfo>();
        public int myind;
    public Text killtxt;
    public Text deathtxt;
    public Transform ui_leaderboard;

     Text killertxt;
     Text victimtext;
    public Transform kilertab;
    int sayi = 0;
    public Transform sp1;
    public Transform sp2;
   

    public int mainmenu = 0;
    public int killcount = 40;
    public GameObject mapcam;
    private Transform ui_endgame;
    public bool perpetual = true;
    private GameState state = GameState.waiting;
    int bornforonce = 0;

    int matchlength = 180;
    public Text ui_timer;
    private int currentMatchTime;
    private Coroutine timerCoroutine;
    private Coroutine statrefreshatstart;

    private bool playerAdded;
    

    public Text humankilltxt;
    public Text alienkilltext;
    private int homekill;
     private  int awaykill;

    public Text gotext;


    public Text moneytxt;
    public int money = 0;


    


    public enum Eventcode : byte
        {
            NewPlayer,
            UpdatePlayers,
            ChangeStat,
            NewMatch,
            RefreshTimer,
            Refreshstat
        }



    



   






    // Start is called before the first frame update
    void Start()
        {

        mapcam.SetActive(false);
           
        NewPlayer_s(yonet.myprofile);
        
        validateconnection();
        if (PhotonNetwork.IsMasterClient)
        {
            playerAdded = true;
            Refreshstatui();
            spawn();

        }
       

        initializeui();
        initializeTimer();
        initializestat();

        StartCoroutine(setkillertab());
        StartCoroutine(makebfozero());
        
    }

    IEnumerator makebfozero()
    {
    e:
        yield return new WaitForSeconds(5);
        if(bornforonce==1)
        {
            bornforonce = 0;
        }
        else
        {

        }

        goto e;

    }
    


    private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }


        private void validateconnection()
        {
            if (PhotonNetwork.IsConnected) return;
            SceneManager.LoadScene(mainmenu);

        }


    private void StateCheck()
    {
        if(state == GameState.ending)
        {
            EndGame();
        }
    }



    private void ScoreCheck()
    {
        
        bool detectwin = false;

        foreach(PlayerInfo a in playerinfo)
        {
            if(a.kills>= killcount)
            {
                detectwin = true;
                break;
            }
        }

        if(detectwin)
        {
            if(PhotonNetwork.IsMasterClient && state != GameState.ending)
            {
                UpdatePlayer_S((int)GameState.ending, playerinfo);
            }
        }


    }

    private void EndGame()
    {
        state = GameState.ending;

        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        currentMatchTime = 0;
        RefreshTimerUI();
        Refreshstatui();


        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.DestroyAll();
            if (!perpetual)
            {
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
            
        }

        mapcam.SetActive(true);

        ui_endgame.gameObject.SetActive(true);
        Leaderboard(ui_leaderboard);
        StartCoroutine(End(6f));

    }


    private void initializeTimer()
    {
        currentMatchTime = matchlength;
        RefreshTimerUI();

        if(PhotonNetwork.IsMasterClient)
        {

            timerCoroutine = StartCoroutine(Timer());

        }
    }


    private void RefreshTimerUI()
    {
        string minutes = (currentMatchTime / 60).ToString("00");
        string seconds = (currentMatchTime % 60).ToString("00");
        ui_timer.text = $"{minutes}:{seconds}";
    }


    public void RefreshTimer_S()
    {

        object[] package = new object[] { currentMatchTime };
        PhotonNetwork.RaiseEvent(
            (byte)Eventcode.RefreshTimer,
            package,
            new RaiseEventOptions { Receivers=ReceiverGroup.All},
            new SendOptions { Reliability=true}
            
            
            );

    }


    public void RefreshTimer_R(object[] data)
    {

        currentMatchTime = (int)data[0];
        RefreshTimerUI();

    }


    private IEnumerator Timer()
    {

        yield return new WaitForSeconds(1f);

        currentMatchTime -= 1;
        
        if (currentMatchTime<=0)
        {
            timerCoroutine = null;
            UpdatePlayer_S((int)GameState.ending, playerinfo);
        }
        else
        {
            RefreshStat_S();
            RefreshTimer_S();
            timerCoroutine = StartCoroutine(Timer());

        }

    }

    public void RefreshStat_R(object[] data)
    {

        awaykill = (int)data[0];
        homekill = (int)data[1];
        Refreshstatui();




    }
    public void Refreshstatui()
    {
        alienkilltext.text = awaykill + "";
        humankilltxt.text = homekill + "";
        
        
    }
    private void initializestat()
    {
        awaykill=0;
        homekill = 0;
        Refreshstatui();
       


    }
    public void RefreshStat_S()
    {

        object[] package = new object[] { awaykill, homekill };
        PhotonNetwork.RaiseEvent(
            (byte)Eventcode.Refreshstat,
            package,
            new RaiseEventOptions { Receivers = ReceiverGroup.All },
            new SendOptions { Reliability = true }


            );

    }


    // Update is called once per frame
    void Update()
        {

        moneytxt.text = money + "";


        if(state==GameState.ending)
        {
            return;
        }
        
        if(Input.GetKey(KeyCode.Tab))
        {
            /*if (ui_leaderboard.gameObject.activeSelf) ui_leaderboard.gameObject.SetActive(false);
            else Leaderboard(ui_leaderboard);*/
            Leaderboard(ui_leaderboard);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
           ui_leaderboard.gameObject.SetActive(false);
            
        }

        if (sayi==8)
        {
            sayi = 0;
           
            kilertab.GetChild(1).gameObject.SetActive(false);
            kilertab.GetChild(2).gameObject.SetActive(false);
           
        }
        

        }

    IEnumerator setkillertab()
    {
    e:
        yield return new WaitForSeconds(4);
        if(kilertab.GetChild(0).gameObject.activeSelf)
        {
            kilertab.GetChild(0).gameObject.SetActive(false);
        }
        if (kilertab.GetChild(1).gameObject.activeSelf)
        {
            kilertab.GetChild(1).gameObject.SetActive(false);
        }
        if (kilertab.GetChild(2).gameObject.activeSelf)
        {
            kilertab.GetChild(2).gameObject.SetActive(false);
        }
        if (kilertab.GetChild(3).gameObject.activeSelf)
        {
            kilertab.GetChild(3).gameObject.SetActive(false);
        }
        goto e;
    }

        public void OnEvent(EventData photonevent)
        {
            if (photonevent.Code >= 200) return;

            Eventcode e = (Eventcode)photonevent.Code;
            object[] o = (object[])photonevent.CustomData;

            switch (e)
            {
                case Eventcode.NewPlayer:
                    NewPlayer_R(o);
                    break;

                case Eventcode.UpdatePlayers:
                    UpdatePlayer_R(o);
                    break;

                case Eventcode.ChangeStat:
                    ChangeStat_R(o);
                    break;

            case Eventcode.NewMatch:
                NewMatch_R();
                break;

            case Eventcode.RefreshTimer:
                RefreshTimer_R(o);
                break;
            case Eventcode.Refreshstat:
                RefreshStat_R(o);
                break;

        }

        }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
    
        
    }

   


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        List<PlayerInfo> sorted = SortPlayers(playerinfo);

        foreach (PlayerInfo a in sorted)
        {

            if(a.actor==otherPlayer.ActorNumber)
            {
              
                playerinfo.Remove(a);
               

            }

        }

        
        UpdatePlayer_S((int)state, playerinfo);
    }


    
    
   


   
    

    private bool CalculateTeam()
    {


        return PhotonNetwork.CurrentRoom.PlayerCount % 2 ==0;
        //return tmdmscript.IsAwayTeam;


    }
  
   
   

    public void NewPlayer_s(ProfileData p)
        {
            object[] package = new object[5];
            package[0] = p.username;
            package[1] = PhotonNetwork.LocalPlayer.ActorNumber;
            package[2] = (short)0;
            package[3] = (short)0;
        package[4] = CalculateTeam();

        PhotonNetwork.RaiseEvent(

                (byte)Eventcode.NewPlayer,
                package,
                new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient },
                new SendOptions { Reliability = true }

                );

        }


        public void NewPlayer_R(object[] data)
        {

            PlayerInfo p = new PlayerInfo(
                new ProfileData((string)data[0]),
                (int)data[1],
                (short)data[2],
                (short)data[3],
                (bool)data[4]
                );

            playerinfo.Add(p);

        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            gameObject.GetComponent<playermovement>().trysync();
        }


       
            UpdatePlayer_S((int)state ,playerinfo);
        }


        public void UpdatePlayer_S(int state,List<PlayerInfo> info)
        {

            object[] package = new object[info.Count+1];


        package[0] = state;
            for (int i = 0; i < info.Count; i++)
            {
                object[] piece = new object[5];
                piece[0] = info[i].profile.username;
                piece[1] = info[i].actor;
                piece[2] = info[i].kills;
                piece[3] = info[i].deaths;
            piece[4] = info[i].awayTeam;

            package[i+1] = piece;
            }

            PhotonNetwork.RaiseEvent(

                (byte)Eventcode.UpdatePlayers,
                package,
                new RaiseEventOptions { Receivers = ReceiverGroup.All },
                new SendOptions { Reliability = true }

                );

        }


        public void UpdatePlayer_R(object[] data)
        {
        state = (GameState)data[0];

        if(playerinfo.Count < data.Length-1)
        {

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                gameObject.GetComponent<playermovement>().trysync();
            }

        }



            playerinfo = new List<PlayerInfo>();

            for (int i = 1; i < data.Length; i++)
            {
                object[] extract = (object[])data[i];

                PlayerInfo p = new PlayerInfo(
                    new ProfileData((string)extract[0]),
                    (int)extract[1],
                    (short)extract[2],
                    (short)extract[3],
                    (bool)extract[4]
                    );
                playerinfo.Add(p);
            if (PhotonNetwork.LocalPlayer.ActorNumber == p.actor)
            {
                myind = i - 1;

                if(!playerAdded)
                {
                    playerAdded = true;
                    tmdmscript.IsAwayTeam = p.awayTeam;
                    spawn();
                }

            }
            }
        StateCheck();
        }


        public void ChangeStat_R(object[] data)
        {
            int actor = (int)data[0];
            byte stat = (byte)data[1];
            byte amt = (byte)data[2];

        
            for (int i = 0; i < playerinfo.Count; i++)
            {
                if (playerinfo[i].actor == actor)
                {
                    switch (stat)
                    {
                        case 0:
                            playerinfo[i].kills += amt;
                            Debug.Log($"players: {playerinfo[i].profile.username}    kills: {playerinfo[i].kills}");
                        kilertab.GetChild(sayi / 2).gameObject.SetActive(true);
                        killertxt = kilertab.GetChild(sayi/2).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
                        killertxt.text = playerinfo[i].profile.username;
                        sayi++;
                        if(playerinfo[i].awayTeam)
                        {
                            homekill++;
                        }
                        else if (!playerinfo[i].awayTeam)
                        {
                            awaykill++;
                        }
                       if(playerinfo[myind].actor==playerinfo[i].actor)
                        {
                            money += 600;
                        }
                        break;

                        case 1:
                            playerinfo[i].deaths += amt;
                            Debug.Log($"players: {playerinfo[i].profile.username}    deaths: {playerinfo[i].deaths}");
                        kilertab.GetChild(sayi / 2).gameObject.SetActive(true);
                        victimtext = kilertab.GetChild(sayi / 2).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
                        victimtext.text = playerinfo[i].profile.username;
                        sayi++;
                        if (playerinfo[myind].actor == playerinfo[i].actor)
                        {
                            money += 300;
                        }
                        break;
                  

                    }

                if (i == myind) refreshmystats();
                if (ui_leaderboard.gameObject.activeSelf) Leaderboard(ui_leaderboard);

                break;
                }
            }
        ScoreCheck();
        }



    public void NewMatch_S()
    {
        PhotonNetwork.RaiseEvent
            (
            (byte)Eventcode.NewMatch,
            null,
            new RaiseEventOptions { Receivers=ReceiverGroup.All},
            new SendOptions { Reliability=true}
            
            );
    }

    public void NewMatch_R()
    {
        state = GameState.waiting;

        mapcam.SetActive(false);

        ui_endgame.gameObject.SetActive(false);


        foreach(PlayerInfo p in playerinfo)
        {
            p.kills = 0;
            p.deaths = 0;
        }


        refreshmystats();

        initializeTimer();
       

        if (bornforonce == 0)
        {
            spawn();
            bornforonce = 1;
        }

    }



        public void ChangeStat_S(int actor, byte stat, byte amt)
        {
            object[] package = new object[] { actor, stat, amt };
            PhotonNetwork.RaiseEvent(
                (byte)Eventcode.ChangeStat,
                package,
                new RaiseEventOptions { Receivers = ReceiverGroup.All },
                new SendOptions { Reliability = true }


                );
        }


        public void spawn()
        {

        if (!tmdmscript.IsAwayTeam)
        {
           
            
            PhotonNetwork.Instantiate("alien", sp1.position, Quaternion.identity, 0, null);
        }

        else
        {

            PhotonNetwork.Instantiate("farmer (1)", sp2.position, Quaternion.identity, 0, null);

        }

    }

 
    private void OnApplicationQuit()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void initializeui()
    {
        killtxt = GameObject.Find("Canvas/multi_ui/kill").gameObject.GetComponent<Text>();
        deathtxt = GameObject.Find("Canvas/multi_ui/death").gameObject.GetComponent<Text>();
        ui_leaderboard = GameObject.Find("Canvas/multi_ui").transform.Find("tab").transform;
        ui_endgame = GameObject.Find("Canvas/multi_ui").transform.Find("endgame").transform;
        refreshmystats();
    }

    private void refreshmystats()
    {
        if(playerinfo.Count>myind)
        {
            //killtxt.text = $"kill: {playerinfo[myind].kills}";
            //deathtxt.text = $"death: {playerinfo[myind].deaths}";
        }
        else
        {
           // killtxt.text = "kill: 0";
           // deathtxt.text = "death: 0";
        }

       
    }



    private void Leaderboard(Transform p_lb)
    {

        for(int i=2; i<p_lb.childCount;i++)
        {

            Destroy(p_lb.GetChild(i).gameObject);

        }

        GameObject playercard = p_lb.GetChild(1).gameObject;
        playercard.SetActive(false);

        List<PlayerInfo> sorted = SortPlayers(playerinfo);

        foreach(PlayerInfo a in sorted)
        {
           
                GameObject newcard = Instantiate(playercard, p_lb) as GameObject;

                newcard.transform.Find("Home").gameObject.SetActive(a.awayTeam);
                newcard.transform.Find("Away").gameObject.SetActive(!a.awayTeam);

                newcard.transform.Find("usernamepanel/username").GetComponent<Text>().text = a.profile.username;
                newcard.transform.Find("killcountertext").GetComponent<Text>().text = a.kills.ToString();
                newcard.transform.Find("deadcountertext").GetComponent<Text>().text = a.deaths.ToString();
            
            newcard.SetActive(true);
            

        }

        p_lb.gameObject.SetActive(true);
        p_lb.parent.gameObject.SetActive(true);
    }

   

       


        private List<PlayerInfo> SortPlayers (List<PlayerInfo> p_info)
    {
        List<PlayerInfo> sorted = new List<PlayerInfo>();

        List<PlayerInfo> homesorted = new List<PlayerInfo>();

        List<PlayerInfo> awaysorted = new List<PlayerInfo>();

        int homeSize = 0;
        int awaySize = 0; 
        
       
        foreach (PlayerInfo p in p_info)
        {
            if (p.awayTeam) awaySize++;
            else homeSize++;
        }
        

        while (homesorted.Count< homeSize)
        {
        
            short highest = -1;
            PlayerInfo selection = p_info[0];
            
            foreach (PlayerInfo a in p_info)
            {
                if (a.awayTeam) continue;
                if (homesorted.Contains(a)) continue;
                if(a.kills>highest)
                {
                    selection = a;
                    highest = a.kills;
                }
               
                  
                
            }

            homesorted.Add(selection);
        }

        while (awaysorted.Count < awaySize)
        {
           
            short highest = -1;
            PlayerInfo selection = p_info[0];

            foreach (PlayerInfo a in p_info)
            {
                if (!a.awayTeam) continue;
                if (awaysorted.Contains(a)) continue;
                if (a.kills > highest)
                {
                    selection = a;
                    highest = a.kills;
                }
                
                    
                
            }

            awaysorted.Add(selection);
        }

        sorted.AddRange(homesorted);
        sorted.AddRange(awaysorted);


        return sorted;

    }

    
    private IEnumerator End(float p_wait)
    {
        yield return new WaitForSeconds(1.1f);
        money = 0;
        if (awaykill>homekill)
        {
            gotext.text = "Alien Wins";
        }
        else if (homekill > awaykill)
        {
            gotext.text = "Home Wins";

        }
        else if (homekill == awaykill)
        {
            gotext.text = "Draw";
        }
        yield return new WaitForSeconds(p_wait);

        if(perpetual)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                awaykill = 0;
                homekill = 0;
                NewMatch_S();
            }
        }

        else
        {
            PhotonNetwork.AutomaticallySyncScene = false;
            PhotonNetwork.LeaveRoom();
        }
    }
    


}
