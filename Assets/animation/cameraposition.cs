using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraposition : MonoBehaviour
{
    public Transform solkolu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(solkolu.position.x, solkolu.position.y, solkolu.position.z);
    }
}
