using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class shopcontrol : MonoBehaviourPun
{
    
    public GameObject pompik;
    public GameObject m4;
    public GameObject negev;
    int kackere = 0;
    public bool isbuyed = false;
    [SerializeField]public GameObject shop;
    void Start()
    {
        m4.transform.localScale = Vector3.zero;
        negev.transform.localScale = Vector3.zero;
        pompik.transform.localScale = Vector3.zero;

        shop.transform.localScale = new Vector3(0, 0, 0);
        Cursor.visible = false;

    }
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                if (kackere == 0)
                {
                    kackere = 1;
                }
                else if (kackere == 1)
                {
                    kackere = 0;
                }
                Camera.main.GetComponent<mouselook>().enabled = !Camera.main.GetComponent<mouselook>().enabled;

                shop.transform.localScale = new Vector3(kackere, kackere, kackere);

                if (kackere == 1)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else if (kackere == 0)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                Cursor.visible = !Cursor.visible;
            }

        }
        

    }

  


    [PunRPC]
    public void pompa()
    {
        if (GameObject.FindGameObjectWithTag("gamesetup").GetComponent<gamesetup>().money>2000 && !isbuyed)
        {
            GetComponent<PhotonView>().RPC("s1", RpcTarget.All, null);
            GameObject.FindGameObjectWithTag("gamesetup").GetComponent<gamesetup>().money -= 2000;
        }
        
    }

    [PunRPC]
    public void em4()
    {
        if (GameObject.FindGameObjectWithTag("gamesetup").GetComponent<gamesetup>().money > 1000 && !isbuyed)
        {
            GetComponent<PhotonView>().RPC("s2", RpcTarget.All, null);
            GameObject.FindGameObjectWithTag("gamesetup").GetComponent<gamesetup>().money -= 1000;
        }
    }

    [PunRPC]
    public void ngv()
    {
        if (GameObject.FindGameObjectWithTag("gamesetup").GetComponent<gamesetup>().money > 1500 && !isbuyed)
        {
            GetComponent<PhotonView>().RPC("s3", RpcTarget.All, null);
            GameObject.FindGameObjectWithTag("gamesetup").GetComponent<gamesetup>().money -= 1500;
        }
    }




    [PunRPC]
    public void s1()
    {

        kackere = 0;

        m4.transform.localScale = Vector3.zero;
        negev.transform.localScale = Vector3.zero;
        pompik.transform.localScale = new Vector3(100, 95.86308f, 97.39389f);
        shop.transform.localScale = new Vector3(kackere, kackere, kackere);
        if (GetComponent<PhotonView>().IsMine)
        {
            Camera.main.GetComponent<mouselook>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;

            
        }
        isbuyed = true;
    }
    [PunRPC]
    public void s2()
    {
        kackere = 0;

        pompik.transform.localScale = Vector3.zero;
        negev.transform.localScale = Vector3.zero;
        m4.transform.localScale = new Vector3(9.00992f, 9.00992f, 9.00992f);
        shop.transform.localScale = new Vector3(kackere, kackere, kackere);
        if (GetComponent<PhotonView>().IsMine)
        {
            Camera.main.GetComponent<mouselook>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
           
               
        } 
        isbuyed = true;
    }
    [PunRPC]
    public void s3()
    {
        kackere = 0;

        pompik.transform.localScale = Vector3.zero;
        negev.transform.localScale = new Vector3(32.80648f, 14.72629f, 32.80648f);
        m4.transform.localScale = Vector3.zero;
        shop.transform.localScale = new Vector3(kackere, kackere, kackere);
        if (GetComponent<PhotonView>().IsMine)
        {
            Camera.main.GetComponent<mouselook>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
            
               
        } 
        isbuyed = true;

    }

}
