
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections;

public class target : MonoBehaviour
{
    public float health = 100f;
    public GameObject gb;
    ragdolldeath rd;
    public Text healthtext;
    public Transform h_bar;
    public GameObject healthobj;
    public GameObject ammoobj;
    public ProfileData pf;
    gamesetup gs;
    public bool isdead;
    public void die()
    {
        isdead = true;
        GetComponent<PhotonView>().RPC("deadpls", RpcTarget.All);
        
        gs = GameObject.Find("gamesetup").GetComponent<gamesetup>();
       
        if (GetComponent<PhotonView>().IsMine)
        {
            gs.ChangeStat_S(PhotonNetwork.LocalPlayer.ActorNumber, 1, 1);
            StartCoroutine(destroyafterdie());
            
        }

       


    }

    [PunRPC]
    public void deadpls()
    {
        rd = gb.GetComponent<ragdolldeath>();
        rd.die();
    }


    IEnumerator destroyafterdie()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.Destroy(gameObject);
        gs.spawn();
    }
    

        public void Start()
        {
            healthobj.transform.localScale = new Vector3(0, 0, 0);
            ammoobj.transform.localScale = new Vector3(0, 0, 0);
        }

        void Update()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                //healthtext.text = health + "";
                healthobj.transform.localScale = new Vector3(1, 1, 1);
                ammoobj.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                h_bar.localScale = Vector3.Lerp(h_bar.localScale, new Vector3(health / 100, 1, 1), Time.deltaTime * 8f);




                if (health <= 0f)
                {
                    h_bar.localScale = new Vector3(0, 1, 1);


                }
            }
        }


    }

