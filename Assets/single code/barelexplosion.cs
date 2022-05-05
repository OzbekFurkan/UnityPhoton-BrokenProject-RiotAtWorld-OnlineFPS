using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barelexplosion : MonoBehaviour
{
    public bool readytoexp = false;
    public GameObject impacteffect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(readytoexp)
        {

            Instantiate(impacteffect, transform.position, transform.rotation);
            Collider[] coll = Physics.OverlapSphere(transform.position, 10f);
            foreach (var bulunan in coll)
            {
                if (bulunan.transform.gameObject.tag == "enemy")
                {
                    bulunan.gameObject.GetComponent<bot_sgl_tkdmg>().TakeDamage(20);
                }
                else if (bulunan.transform.gameObject.tag == "barel")
                {
                    bulunan.gameObject.GetComponent<barelexplosion>().readytoexp = true;
                }
                if (bulunan.GetComponent<Rigidbody>() != null)
                {
                    Rigidbody govde = bulunan.GetComponent<Rigidbody>();

                    govde.AddExplosionForce(400f, transform.position, 10f, 0.2f);
                }

            }
           
            StartCoroutine(yoket());
            gameObject.layer = 10;
            readytoexp = false;
        }
    }

    IEnumerator yoket()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


}
