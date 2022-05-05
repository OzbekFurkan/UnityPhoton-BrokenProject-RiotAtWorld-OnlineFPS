using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.Cockpit;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;

public class menumng : MonoBehaviourPunCallbacks
{
    public GameObject menupanel;




    public GameObject crtroompanel;
    public InputField roomname;

    byte mxpl = 0;

    public GameObject roomlistpanel;

    public GameObject tabroom;
    public GameObject buttonroom;
    private List<RoomInfo> roomlist;


    

    public GameObject setpanel;

    public GameObject sngl;
    private void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void getintocrtroompnl()
    {
        crtroompanel.SetActive(true);
        roomlistpanel.SetActive(false);
        setpanel.SetActive(false);
        sngl.SetActive(false);
    }
    public void getintosnglroompnl()
    {
        crtroompanel.SetActive(false);
        roomlistpanel.SetActive(false);
        setpanel.SetActive(false);
        sngl.SetActive(true);
    }

    public void createroom()
    {
        if (string.IsNullOrEmpty(roomname.text))
        {
            return;
        }
        if (mxpl == 0)
        {
            mxpl = 2;
        }
        RoomOptions ro = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = mxpl };

         PhotonNetwork.CreateRoom(roomname.text, ro);

    }
    public void createsinglegame()
    {
        SceneManager.LoadScene("ExampleScene");


    }


    public void getintoroomlist()
    {
        roomlistpanel.SetActive(true);
        crtroompanel.SetActive(false);
        setpanel.SetActive(false);
        sngl.SetActive(false);
    }

    public void getintosetpanel()
    {
        roomlistpanel.SetActive(false);
        crtroompanel.SetActive(false);
        setpanel.SetActive(true);
        sngl.SetActive(false);
    }


    private void clearroomlist()
    {
        Transform content = tabroom.transform.Find("Scroll View/Viewport/Content");
        foreach (Transform a in content) { Destroy(a.gameObject); }
    }

     public override void OnRoomListUpdate(List<RoomInfo> p_list)
     {
         roomlist = p_list;
         clearroomlist();
         Transform content = tabroom.transform.Find("Scroll View/Viewport/Content");
         foreach(RoomInfo a in roomlist)
         {
             GameObject newroombutton = Instantiate(buttonroom, content) as GameObject;
             newroombutton.transform.Find("Name").GetComponent<Text>().text = a.Name;
             newroombutton.transform.Find("Players").GetComponent<Text>().text = a.PlayerCount + "/" + a.MaxPlayers;
             newroombutton.GetComponent<Button>().onClick.AddListener(delegate { joinroom(newroombutton.transform); });
         }
         base.OnRoomListUpdate(roomlist);
     }

     public void joinroom(Transform p_button)
     {
         string t_roomname = p_button.transform.Find("Name").GetComponent<Text>().text;
         PhotonNetwork.JoinRoom(t_roomname);
     }



    public void extgm()
    {
        Application.Quit();
    }



    public void setteam(int val)
    {
        if (val == 0)
        {
            mxpl = 2;
        }
        else if (val == 1)
        {
            mxpl = 4;

        }
        else if (val == 2)
        {
            mxpl = 6;

        }
        else if (val == 3)
        {
            mxpl = 8;

        }

    }
}
