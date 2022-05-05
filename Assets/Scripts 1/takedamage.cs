using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngineInternal;

public class takedamage : MonoBehaviour
    {
        target tg;
        public GameObject tgobj;
    gamesetup gmsetup;
    public GameObject bloodscreen;

        // Start is called before the first frame update
        void Start()
        {
            tg = tgobj.GetComponent<target>();
        gmsetup = GameObject.Find("gamesetup").GetComponent<gamesetup>();

        bloodscreen.SetActive(false);
        }


   

        // Update is called once per frame
        void Update()
        {



        }

    IEnumerator noblood()
    {
        yield return new WaitForSeconds(1.5f);
        bloodscreen.SetActive(false);
    }


    [PunRPC]
    public void TakeDamage(float amount, int p_actor)
    {
        if (tgobj.gameObject.GetComponent<PhotonView>().IsMine && !tg.isdead)
        {
            tg.health -= amount;
            bloodscreen.SetActive(true);
            StartCoroutine(noblood());
            if (tg.health <= 0f)
            {
                if (p_actor >= 0)
                {
                    gmsetup.ChangeStat_S(p_actor, 0, 1);
                }
                tg.die();
                
            }
        }
    }
    }

