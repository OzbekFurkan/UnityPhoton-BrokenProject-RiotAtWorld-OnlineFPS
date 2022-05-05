using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singletakedamage : MonoBehaviour
{
    tgt tg;
    public GameObject tgobj;
    public GameObject bloodscreen;
   

    // Start is called before the first frame update
    void Start()
    {
        tg = tgobj.GetComponent<tgt>();

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


   
    public void TakeDamage(float amount)
    {
        if (!tg.isdead)
        {
            tg.health -= amount;
            bloodscreen.SetActive(true);
            StartCoroutine(noblood());
            if (tg.health <= 0f)
            {
               
                tg.die();

            }
        }
    }
}
