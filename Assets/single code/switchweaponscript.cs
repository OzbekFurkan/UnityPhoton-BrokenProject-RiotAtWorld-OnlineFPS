using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchweaponscript : MonoBehaviour
{
    public int selectedweapon = 0;
   
    
    public GameObject pistol;
    public GameObject heavy;
    public GameObject bomb;
    Animator anmt;
    public GameObject allequipmentholder;
    void Start()
    {
        
        anmt = this.gameObject.GetComponent<Animator>();

        StartCoroutine(selectweapon());
    }

    
    public IEnumerator selectweapon()
    {

        // GetComponent<PhotonView>().RPC("ratata", RpcTarget.All, null);
        int i = selectedweapon;
        if (i == 0)
        {
            anmt.SetBool("switching", true);

            yield return new WaitForSeconds(0.2f);
            pistol.GetComponent<singlepistol>().enabled = true;
            pistol.GetComponent<sway_weapon>().enabled = true;
            pistol.transform.localScale = new Vector3(1, 1, 1);
            heavy.GetComponent<singleweapon>().enabled = false;
            heavy.GetComponent<sway_weapon>().enabled = false;
            heavy.transform.localScale = Vector3.zero;
            bomb.transform.localScale = Vector3.zero;
            anmt.SetBool("switching", false);
        }
        else if (i == 1)
        {
            anmt.SetBool("switching", true);

            yield return new WaitForSeconds(0.2f);
            pistol.GetComponent<singlepistol>().enabled = false;
            pistol.GetComponent<sway_weapon>().enabled = false;
            pistol.transform.localScale = Vector3.zero;
            heavy.GetComponent<singleweapon>().enabled = true;
            heavy.GetComponent<sway_weapon>().enabled = true;
            heavy.transform.localScale = new Vector3(1, 1, 1);
            bomb.transform.localScale = Vector3.zero;
            anmt.SetBool("switching", false);

        }
        else if (i == 2)
        {
            anmt.SetBool("switching", true);

            yield return new WaitForSeconds(0.2f);
            pistol.GetComponent<singlepistol>().enabled = false;
            pistol.GetComponent<sway_weapon>().enabled = false;
            pistol.transform.localScale = Vector3.zero;
            heavy.GetComponent<singleweapon>().enabled = false;
            heavy.GetComponent<sway_weapon>().enabled = false;
            heavy.transform.localScale = Vector3.zero;
            bomb.transform.localScale = new Vector3(1,1,1); ;
            anmt.SetBool("switching", false);

        }



    }






    
    public void tatata()
    {
        
            int previousselectedweapon = selectedweapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedweapon >= 2)
                {
                    selectedweapon = 0;
                }
                else if(allequipmentholder.transform.GetChild(selectedweapon+1).childCount>1)
                {
                    selectedweapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedweapon <= 0)
                {
                    selectedweapon = 2;
                }
                else if (allequipmentholder.transform.GetChild(selectedweapon-1).childCount > 1)
                {
                    selectedweapon--;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedweapon = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) /*&& transform.childCount2 >= 2*/)
            {
                selectedweapon = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) /*&& transform.childCount2 >= 2*/)
            {
                selectedweapon = 2;
            }
            

            if (previousselectedweapon != selectedweapon)
            {
                ads.PlayOneShot(wpnswitch);
                StartCoroutine(selectweapon());

            }

    }


    public AudioSource ads;

    public AudioClip reload;
    public AudioClip wpnswitch;
    bool plad = false;


    void Update()
    {
        
        tatata();
      
            if (anmt.GetBool("reloading") && !plad)
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
                anmt.speed = 1;
            }



        

    }
}
