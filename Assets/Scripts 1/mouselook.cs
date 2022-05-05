using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class mouselook : MonoBehaviour
{
    [SerializeField] public GameObject gb;
    public static float mousesensitivity;
    public Transform playerbody;
    float xrotation = 0f;
  
    // Start is called before the first frame update
    void Start()
    {
    



        Cursor.lockState = CursorLockMode.Locked;
        if(!GetComponent<PhotonView>().IsMine)
        {
            gb.GetComponent<Camera>().enabled = false;
            gb.GetComponent<AudioListener>().enabled = false;
        }
        else if (GetComponent<PhotonView>().IsMine)
        {
            gb.GetComponent<Camera>().enabled = true;
            gb.GetComponent<AudioListener>().enabled = true;


           


        }
    }

   
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            float mousex = Input.GetAxis("Mouse X") * mousesensitivity * Time.deltaTime;
            float mousey = Input.GetAxis("Mouse Y") * mousesensitivity * Time.deltaTime;
            xrotation -= mousey;
            xrotation = Mathf.Clamp(xrotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);
            playerbody.Rotate(Vector3.up * mousex);


           

        } 
          
    }




}
