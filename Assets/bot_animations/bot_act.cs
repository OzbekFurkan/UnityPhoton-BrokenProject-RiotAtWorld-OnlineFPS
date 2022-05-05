using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bot_act : MonoBehaviour
{
    GameObject mainplayer;
    GameObject alien;
    NavMeshAgent mynav;
    float dtmn;
    float dtaln;
    public float firerate = 15f;
    public float nexttimetofire;
    float currenthealth;
    float oldhealth=100;
    GameObject car;
    
    // Start is called before the first frame update
    void Start()
    {
        mynav = GetComponent<NavMeshAgent>();
       
        alien = GameObject.Find("uzaylı");
        //car = GameObject.Find("Car01");
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamemodescript.isinsideufo)
        {
            currenthealth = GetComponent<bot_tgt>().health;

            if (gamemodescript.isdead)
            {
                oldhealth = currenthealth;
            }

            if (!gamemodescript.isdead)
            {
                mainplayer = GameObject.FindGameObjectWithTag("mainplayer");
            }


            if (currenthealth > 0)
            {

                if (!gamemodescript.isdead)
                {
                    mynav.SetDestination(alien.transform.position);
                }

                dtaln = Vector3.Distance(alien.transform.position, transform.position);
                if (dtaln < 4 && !gamemodescript.isgameover)
                {
                    transform.GetChild(2).gameObject.SetActive(true);
                    gamemodescript.isgameover = true;
                    Destroy(mainplayer);
                }
                if (!gamemodescript.isdead)
                {
                    dtmn = Vector3.Distance(mainplayer.transform.position, transform.position);
                }
                if (currenthealth != oldhealth)
                {
                    
                        mynav.SetDestination(mainplayer.transform.position);
                    
                    

                    if (dtmn < 50 && Time.time >= nexttimetofire)
                    {
                        
                            transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).LookAt(mainplayer.transform);
                            transform.GetChild(0).LookAt(mainplayer.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2));
                            mynav.SetDestination(mainplayer.transform.position);
                        
                        
                        nexttimetofire = Time.time + 1f / firerate;
                        shoot();
                        mynav.stoppingDistance = 0;
                    }

                }

                else if (dtmn > 30)
                {
                    mynav.SetDestination(alien.transform.position);
                    mynav.stoppingDistance = 4;
                }

                else if (dtmn < 30 && Time.time >= nexttimetofire)
                {

                  
                        transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).LookAt(mainplayer.transform);
                        transform.GetChild(0).LookAt(mainplayer.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2));
                        mynav.SetDestination(mainplayer.transform.position);
                    
                    
                    nexttimetofire = Time.time + 1f / firerate;
                    shoot();
                    mynav.stoppingDistance = 0;
                }

            }


        }
       
       

    }


    void shoot()
    {
        



        RaycastHit hit;
        if (Physics.Raycast(transform.GetChild(0).GetChild(5).position, transform.GetChild(0).GetChild(5).forward, out hit, 50))
        {

            singletakedamage tkdmg = hit.transform.GetComponent<singletakedamage>();
            //target trgt = hit.transform.GetComponent<target>();

            if (tkdmg != null)
            {
                tkdmg.gameObject.GetComponent<singletakedamage>().TakeDamage(5);

            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 30);
                if (hit.rigidbody.gameObject.tag == "barel")
                {
                    hit.rigidbody.gameObject.GetComponent<barelexplosion>().readytoexp = true;
                }
               
            }
           
          

        }

    }

}
