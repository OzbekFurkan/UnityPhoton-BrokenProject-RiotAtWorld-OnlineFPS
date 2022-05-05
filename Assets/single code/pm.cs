using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pm : MonoBehaviour
{
    public CharacterController ccontroller;
    public float speed = 12f;
    Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundcheck;
    public float grounddistance = 0.4f;
    public LayerMask groundmask;
    public bool isgrounded;
    public float jumpheight = 3f;


    


   
    public ProfileData playerprofile;

   
    public Animator anmt;
    bool isshooting = false;


    public Slider sdvfx;
    public Slider sdmsc;
    public Slider sdsens;
    public Slider dd;
    AudioSource adsmsc;



    public AudioSource ads1;

    public AudioSource ads2;

    public GameObject escpanel;
    public GameObject setpanel;


    public GameObject tgobj;
    tgt tg;

    // Start is called before the first frame update
    void Start()
    {
       
        tg = tgobj.GetComponent<tgt>();
        adsmsc = GameObject.FindGameObjectWithTag("gamesmusic").GetComponent<AudioSource>();

        adsmsc.volume = PlayerPrefs.GetFloat("music");
        ads1.volume = PlayerPrefs.GetFloat("vfx");
        ads2.volume = PlayerPrefs.GetFloat("vfx");
        adsmsc.volume = PlayerPrefs.GetFloat("music");
        mouselookscript.mousesensitivity = PlayerPrefs.GetFloat("sens");
        sdvfx.value = PlayerPrefs.GetFloat("vfx");
        sdmsc.value = PlayerPrefs.GetFloat("music");
        sdsens.value = PlayerPrefs.GetFloat("sens");
        dd.value = PlayerPrefs.GetInt("graphind");
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("graphind"));

      

    }






    // Update is called once per frame
    void Update()
    {



        if (tg.health > 0)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }





            isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);
            if (isgrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            ccontroller.Move(move * speed * Time.deltaTime);
            if (Input.GetButtonDown("Jump") && isgrounded)
            {
                velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);

            }
            velocity.y += gravity * Time.deltaTime;
            ccontroller.Move(velocity * Time.deltaTime);

            if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0)
            {

                if (!isshooting)
                {
                    anmt.SetBool("ismoving", true);
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        anmt.speed = 2;
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftShift))
                    {
                        anmt.speed = 1;
                    }
                }
            }

            else
            {
                anmt.SetBool("ismoving", false);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                isshooting = true;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                isshooting = false;
            }

           


        }
    }







    public void setsens(float newsens)
    {
        
            mouselookscript.mousesensitivity = newsens;
            PlayerPrefs.SetFloat("sens", newsens);
        
    }

    public void setvfx(float newvfxvalue)
    {
        ads1.volume = newvfxvalue;
        ads2.volume = newvfxvalue;
        PlayerPrefs.SetFloat("vfx", newvfxvalue);

    }
    public void setmusic(float newmusicvalue)
    {

        adsmsc.volume = newmusicvalue;
        PlayerPrefs.SetFloat("music", newmusicvalue);

    }




    public void setquality(float graphindex)
    {

        if (graphindex == 0)
        {
            QualitySettings.SetQualityLevel(0);
            PlayerPrefs.SetInt("graphind", 0);
        }
        else if (graphindex == 1)
        {
            QualitySettings.SetQualityLevel(1);
            PlayerPrefs.SetInt("graphind", 1);
        }
        else if (graphindex == 2)
        {
            QualitySettings.SetQualityLevel(2);
            PlayerPrefs.SetInt("graphind", 2);
        }

    }


    bool paused = false;
    private bool disconnecting = false;
    public void TogglePause()
    {
       
            if (disconnecting) return;

            paused = !paused;
            escpanel.SetActive(paused);
            if (!paused)
            {
                setpanel.SetActive(false);
            }
            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Confined;
            Cursor.visible = paused;
        
    }

    public void openset()
    {
        
            setpanel.SetActive(true);
        

    }

    public void Quit()
    {
       
            disconnecting = true;

        SceneManager.LoadScene("menu");
        

    }


}
