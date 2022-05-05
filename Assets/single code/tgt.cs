using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tgt : MonoBehaviour
{
    public float health = 100f;
    public GameObject gb;
    ragdolldeath rd;
    public Text healthtext;
    public Transform h_bar;
    public GameObject healthobj;
    public GameObject ammoobj;
    
    
    public bool isdead;
    public void die()
    {
        isdead = true;

        deadpls();
        StartCoroutine(destroyafterdie());

    }

    
    public void deadpls()
    {
        rd = gb.GetComponent<ragdolldeath>();
        rd.die();
    }


    IEnumerator destroyafterdie()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        gamemodescript.isdead = true;
        
        GameObject.Find("gamescript").GetComponent<gamemodescript>().spawn();
        

    }


    public void Start()
    {
        healthobj.transform.localScale = new Vector3(0, 0, 0);
        ammoobj.transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
       
            //healthtext.text = health + "";
            healthobj.transform.localScale = new Vector3(1, 1, 1);
            ammoobj.transform.localScale = new Vector3(0.4f, 0.4f, 1);
            h_bar.localScale = Vector3.Lerp(h_bar.localScale, new Vector3(health / 100, 1, 1), Time.deltaTime * 8f);




            if (health <= 0f)
            {
                h_bar.localScale = new Vector3(0, 1, 1);
            if (!transform.GetChild(0).gameObject.activeSelf)
            {
                die();
               
            }
            }
        
    }
}
