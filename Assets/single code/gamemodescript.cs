using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;


public class gamemodescript : MonoBehaviour
{
    public GameObject mapcam;
    public GameObject mainplayer;
    public GameObject mainplayerspawnpoint;
    public static bool isdead=true;
    public static bool isgameover=false;
    public Text timetext;
    public int timeinseconds=300;
    public GameObject enemy;
    public GameObject enemyspawmpoint;
    Animator alienanim;
    NavMeshAgent alienact;
    float alienufodist;
    public static bool isinsideufo=false;

     TextMeshProUGUI gameinfo;
    // Start is called before the first frame update
    void Start()
    {
        spawn();
        StartCoroutine(timer());
        StartCoroutine(enemyspawn());
        alienanim = GameObject.Find("uzaylı").GetComponent<Animator>();
        alienact = GameObject.Find("uzaylı").GetComponent<NavMeshAgent>();
        gameinfo = GameObject.Find("info").GetComponent<TextMeshProUGUI>();
    }


    IEnumerator enemyspawn()
    {
        yield return new WaitForSeconds(20);
    loopspawnenemy:
        yield return new WaitForSeconds(10);
        for(int i = 1;i<=5;i++)
        {
            Instantiate(enemy, enemyspawmpoint.transform.GetChild(i).position, transform.rotation);
            yield return new WaitForSeconds(1);
        }
        
      
        goto loopspawnenemy;
    }


    IEnumerator timer()
    {
    looptime:

        yield return new WaitForSeconds(1);

        timetext.text = timeinseconds / 60 +":"+ timeinseconds % 60 + "0";
        timeinseconds--;
        goto looptime;
    }
    public void spawn()
    {
        mapcam.SetActive(false);
        Instantiate(mainplayer, mainplayerspawnpoint.transform.position, transform.rotation);
        isdead = false;
        PickUpController.slotFull = false;
        PickUpController_bomb.slotFull = false;
        PickUpController_pistol.slotFull = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isinsideufo)
        {
            alienufodist = Vector3.Distance(GameObject.Find("uzaylı").transform.position, GameObject.Find("ufo").transform.position);
        }

        if (isgameover) { isdead = true;

            gameinfo.text = "Exposed !";

        }

        if(timeinseconds<15 && !isinsideufo)
        {
            alienanim.SetBool("istime",true);
            StartCoroutine(aliendelay());
           
        }

        if(alienufodist<1)
        {
            isinsideufo = true;
        }

        if(isinsideufo)
        {
            GameObject.Find("ufo").GetComponent<Rigidbody>().AddForce(Vector3.up*20);
           if(GameObject.Find("uzaylı")!=null)
            {
                Destroy(GameObject.Find("uzaylı"));
            }
        }

        if (!isgameover)
        {


            gameinfo.text = "";

            if (timeinseconds > 290)
            {
                gameinfo.text = "Protect our guest dont let him being exposed !";
            }
            else if (timeinseconds > 275 && timeinseconds < 285)
            {
                gameinfo.text = "Humans are coming... take precautions. You can set trap for them";
            }
            else if (timeinseconds > 270 && timeinseconds < 275)
            {
                gameinfo.text = "Some objects are carriable";
            }
            else if (timeinseconds > 265 && timeinseconds < 270)
            {
                gameinfo.text = "you can drive car and shoot them easily";
            }
            else if (timeinseconds > 260 && timeinseconds < 255)
            {
                gameinfo.text = "exploding them is looking goo solution";
            }
            else if (timeinseconds > 0 && timeinseconds < 15)
            {
                gameinfo.text = "Our guest is leaving be careful !";
            }
            else if (timeinseconds < 0)
            {
                gameinfo.text = "Good Job !";
            }
        }
    }

    IEnumerator aliendelay()
    {
        yield return new WaitForSeconds(5);
        if (!isinsideufo)
        {
            alienact.SetDestination(GameObject.Find("ufo").transform.position);
        }
    }
}
