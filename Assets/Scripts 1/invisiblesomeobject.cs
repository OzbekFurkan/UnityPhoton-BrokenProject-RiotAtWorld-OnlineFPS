using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisiblesomeobject : MonoBehaviour
{
    public GameObject maincharacter;
    // Start is called before the first frame update
    void Start()
    {
        if(maincharacter.GetComponent<PhotonView>().IsMine)
        {
            gameObject.layer=10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
     

    }
}
