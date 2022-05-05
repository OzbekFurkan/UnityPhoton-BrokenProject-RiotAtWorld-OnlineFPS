using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    // public ProjectileGun gunScript;                                               **************************
    public Rigidbody rb;
    public BoxCollider coll;
    Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        //Setup
        if (!equipped)
        {
            // gunScript.enabled = false;                                              **************************
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            // gunScript.enabled = true;                                               **************************
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        if (!gamemodescript.isdead)
        {
            player = GameObject.FindGameObjectWithTag("mainplayer").transform;
            gunContainer = GameObject.FindGameObjectWithTag("mainplayer").transform.GetChild(1).GetChild(0).GetChild(3).GetChild(1);
            fpsCam = GameObject.FindGameObjectWithTag("mainplayer").transform.GetChild(1).GetChild(0);

            //Check if player is in range and "E" is pressed
            Vector3 distanceToPlayer = player.position - transform.position;
            if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

            //Drop if equipped and "Q" is pressed
            if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();

        }
       
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = new Vector3(-0.97f, 4.77f, 0.85f);
        transform.localRotation = Quaternion.Euler(0,180,0);
        transform.localScale = new Vector3(4, 4, 4);

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable script
       // gunScript.enabled = true;                                    **************************
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

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
