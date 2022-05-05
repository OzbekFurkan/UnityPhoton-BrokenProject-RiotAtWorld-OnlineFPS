using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;

using UnityEngine.UI;
using UnityEngine.Assertions.Must;

public class chatmanagerphoton : MonoBehaviour, IChatClientListener
{
    string currentchanelname;

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
       
    }

    public void OnConnected()
    {
        chatClient.Subscribe(new string[] {currentchanelname, PhotonNetwork.CurrentRoom.Name },10);

        chatClient.SetOnlineStatus(ChatUserStatus.Offline, null);
    }

    public void OnDisconnected()
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for(int i=0; i<messages.Length; i++)
        {
            AddLine(string.Format(senders[i]+": " + messages[i].ToString()));
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        AddLine(string.Format(sender+": " + message.ToString()));
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
       
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        
    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
       
    }




    ChatClient chatClient;
     public string username;
   
   
    public InputField msginput;
    public Text msgarea;
    public GameObject chatpanel;

   
    void Start()
    {
        
    username = yonet.myprofile.username;
       
       
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        currentchanelname = PhotonNetwork.CurrentRoom.Name;

        StartCoroutine(silchat());
    }

    IEnumerator silchat()
    {
        again:
        yield return new WaitForSeconds(25);
        msgarea.text = "";
        goto again;
    }



    bool setchat=false;

    
    void Update()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Service();
        }

        

        if(Input.GetKeyDown(KeyCode.Return))
        {
            setchat = !setchat;

            if(setchat)
            {
                msginput.interactable = true;
                msginput.readOnly=false;
                msginput.Select();
            }
            else if(!setchat)
            { 
                msginput.Select();
                msginput.readOnly = true;
                msginput.interactable = false;

            }
            
            
            
        }
        

    }

    public void AddLine(string linestring)
    {
        msgarea.text += linestring + "\r\n";
    }

    public void inputonedit(string text)
    {
        if(chatClient.State == ChatState.ConnectedToFrontEnd)
        {
            chatClient.PublishMessage(currentchanelname, msginput.text);
            msginput.text = "";
        }
    }


}
