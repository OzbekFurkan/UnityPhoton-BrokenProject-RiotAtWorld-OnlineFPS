using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot_sgl_tkdmg : MonoBehaviour
{
    bot_tgt tg;
    public GameObject tgobj;



    // Start is called before the first frame update
    void Start()
    {
        tg = tgobj.GetComponent<bot_tgt>();



    }




    // Update is called once per frame
    void Update()
    {



    }

    



    public void TakeDamage(float amount)
    {
        if (!tg.isdead)
        {
            tg.health -= amount;

            
            if (tg.health <= 0f)
            {
               
                tg.die();

            }
        }
    }
}
