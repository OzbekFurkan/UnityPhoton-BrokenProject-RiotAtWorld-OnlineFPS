using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombscript : MonoBehaviour
{

    Rigidbody rb;
    CapsuleCollider coll;
    public Transform player, fpsCam;

   
    public float dropForwardForce, dropUpwardForce;

    public static bool isthrowed = false;
   
    // Start is called before the first frame update
    private void Start()
    {


       
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount>1)
        {
            rb = transform.GetChild(1).gameObject.GetComponent<Rigidbody>();
            coll = transform.GetChild(1).gameObject.GetComponent<CapsuleCollider>();
        }
        
        if(Input.GetMouseButtonDown(0) && transform.childCount > 1)
        {
            Drop();
            isthrowed = true;
        }


    }
    private void Drop()
    {
        

        //Set parent to null
        if(transform.childCount>1)
        {transform.GetChild(1).SetParent(null); }
        

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
        //gunScript.enabled = false;                                              **************************
    }
}
