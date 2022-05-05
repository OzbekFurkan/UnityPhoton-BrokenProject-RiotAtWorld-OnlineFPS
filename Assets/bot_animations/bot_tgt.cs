using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot_tgt : MonoBehaviour
{
    public float health = 100f;
    public GameObject gb;
    ragdolldeath rd;
    public bool isdead=false;

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
        Destroy(transform.GetChild(0).gameObject);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(health<=0)
        {
            
        }
    }
}
