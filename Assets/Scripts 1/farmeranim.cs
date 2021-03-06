using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Photon.Pun.Demo.Cockpit.Forms;

public class farmeranim : MonoBehaviour
{
    public Animator farmeranimation;
    public GameObject farmerplayer;
    public AnimationClip jump;
    public Camera mncmra;
    public ParticleSystem runprt;
    public AudioSource ads;
    public AudioClip jumpingsound;

    bool ismoving = false;

    public AudioSource adsr;

    // Start is called before the first frame update
    void Start()
    {

    }

   



        // Update is called once per frame
        void Update()
        {
     
            if (GetComponent<PhotonView>().IsMine)
            {
                farmeranimation.SetFloat("vertical", Input.GetAxis("Vertical"));
                farmeranimation.SetFloat("horizontal", Input.GetAxis("Horizontal"));
          
            if(Input.GetAxis("Vertical") != 0 && Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") != 0)
            {
                if (!ismoving && farmerplayer.GetComponent<playermovement>().isgrounded)
                {
                    ads.Play();
                    ismoving = true;
                }
                else if(!farmerplayer.GetComponent<playermovement>().isgrounded)
                {
                    ads.Stop();
                    ismoving = false;
                    
                }
                
            }
            else if (Input.GetAxis("Vertical")==0 && Input.GetAxis("Horizontal")==0)
            {
                if (ismoving)
                {
                    ads.Stop();
                    ismoving = false;
                }
                else
                {

                }
            }
          

            if (Input.GetKeyDown("space"))
                {
                adsr.PlayOneShot(jumpingsound);
                
                ads.PlayOneShot(jumpingsound);
                farmeranimation.Play("jump");
                

                }
                if (Input.GetKeyDown("left shift"))
                {
                ads.pitch = 1.4f;
                    farmeranimation.speed = 2;
                    farmerplayer.GetComponent<playermovement>().speed = 15;
                    mncmra.fieldOfView = Mathf.Lerp(mncmra.fieldOfView, 70, 20f);
                    runprt.Play();
                }
                else if (Input.GetKeyUp("left shift"))
                {
                ads.pitch = 1;
                    farmeranimation.speed = 1;
                    farmerplayer.GetComponent<playermovement>().speed = 8;
                    mncmra.fieldOfView = Mathf.Lerp(mncmra.fieldOfView, 60, 20f);
                    runprt.Stop();
                }
            }
        }
    }

