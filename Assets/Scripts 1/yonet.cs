using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;
using System.Configuration;

public class ProfileData
   {
    
         public string username;


    public ProfileData()
    {
        this.username = "default username";
    }
    public ProfileData(string u)
    {
        this.username = u;
    }


    
   }


    public class yonet : MonoBehaviourPunCallbacks
    {

        public static ProfileData myprofile = new ProfileData();
        public InputField nn;

        void Start()
        {
        
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
       
       
        }





        public override void OnConnectedToMaster()
        {
            Debug.Log("servere girildi");
            PhotonNetwork.JoinLobby();
        }


        public override void OnJoinedLobby()
        {
            Debug.Log("lobiye girildi");
       

    }




        public override void OnJoinedRoom()
        {
            Debug.Log("odaya girildi");

        setname();
        PhotonNetwork.LoadLevel("ExampleSceneMulti");

        }
    public void setname()
    {
 myprofile.username = nn.text;
    }

        public override void OnLeftLobby()
        {
            Debug.Log("lobiden çıkıldı");
        }

   

    public override void OnLeftRoom()
        {
        base.OnLeftRoom();
        
            Debug.Log("odadan çıkıldı");
        
       
       
        
        

    }

  









    public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("Hata: Odaya girilemedi !");
        }


        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Hata: Herhangi bir odaya girilemedi !");
            PhotonNetwork.CreateRoom("oda", new RoomOptions { MaxPlayers = 6, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        }


        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Hata: Odaya kurulamadı !");

        }

    }

