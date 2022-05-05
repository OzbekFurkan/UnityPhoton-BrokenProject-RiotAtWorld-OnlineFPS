using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class weaponswitch : MonoBehaviourPun
{
    public int selectedweapon = 0;
    shopcontrol sc;
    public GameObject shopcontrol;
    public GameObject pistol;
    public GameObject heavy;
    Animator anmt;
   
    void Start()
    {
        sc = shopcontrol.GetComponent<shopcontrol>();
        anmt = this.gameObject.GetComponent<Animator>();
        GetComponent<PhotonView>().RPC("selectweapon", RpcTarget.All, null);
        //selectweapon();
    }

  [PunRPC]
    public IEnumerator selectweapon()
    {

       // GetComponent<PhotonView>().RPC("ratata", RpcTarget.All, null);
        int i = selectedweapon;
        if (i == 0)
        {
            anmt.SetBool("switching", true);
            
            yield return new WaitForSeconds(0.2f);
            pistol.GetComponent<pistol>().enabled = true;
            pistol.GetComponent<sway_weapon>().enabled = true;
            pistol.transform.localScale = new Vector3(1,1,1);
            heavy.GetComponent<gun>().enabled = false;
            heavy.GetComponent<sway_weapon>().enabled = false;
            heavy.transform.localScale = Vector3.zero;
            anmt.SetBool("switching", false);
        }
        else if (i == 1)
        {
            anmt.SetBool("switching", true);
            
            yield return new WaitForSeconds(0.2f);
            pistol.GetComponent<pistol>().enabled = false;
            pistol.GetComponent<sway_weapon>().enabled = false;
            pistol.transform.localScale = Vector3.zero;
            heavy.GetComponent<gun>().enabled = true;
            heavy.GetComponent<sway_weapon>().enabled = true;
            heavy.transform.localScale = new Vector3(1, 1, 1);
            anmt.SetBool("switching", false);

        }




    }



    


        [PunRPC]
 public void tatata()
    {
        if (sc.isbuyed && GetComponent<PhotonView>().IsMine)
        {
            int previousselectedweapon = selectedweapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedweapon >= 1)
                {
                    selectedweapon = 0;
                }
                else
                {
                    selectedweapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedweapon <= 0)
                {
                    selectedweapon = 1;
                }
                else
                {
                    selectedweapon--;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedweapon = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && /*transform.childCount*/2 >= 2)
            {
                selectedweapon = 1;
            }
            if (previousselectedweapon != selectedweapon)
            {
                ads.PlayOneShot(wpnswitch);
                GetComponent<PhotonView>().RPC("selectweapon", RpcTarget.All, null);
                //selectweapon();
            }
        }
    }


    public AudioSource ads;
   
    public AudioClip reload;
    public AudioClip wpnswitch;
    bool plad = false;


    void Update()
    {
        GetComponent<PhotonView>().RPC("tatata", RpcTarget.All, null);

        if (GetComponent<PhotonView>().IsMine)
        {
            if(anmt.GetBool("reloading") && !plad)
            {
                ads.PlayOneShot(reload);
                plad = true;
            }
            if (!anmt.GetBool("reloading") && plad)
            {
               
                plad = false;
            }

            if (Input.GetMouseButton(1))
            {
                anmt.SetBool("aiming", true);

            }

            else if (Input.GetMouseButtonUp(1))
            {
                anmt.SetBool("aiming", false);
            }

            if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0)
            {
                if (!anmt.GetBool("reloading") && !anmt.GetBool("switching") && !anmt.GetBool("shooting") && !anmt.GetBool("aiming"))
                {

                    anmt.speed = 7;
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        anmt.speed = 40;
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftShift))
                    {
                        anmt.speed = 7;
                    }
                }
                else
                {
                    anmt.speed = 1;
                }
            }

            else
            {
                anmt.speed = 1 ;
            }



        }
        
    }




}
